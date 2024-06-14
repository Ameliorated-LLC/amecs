using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using Newtonsoft.Json.Linq;

namespace amecs.Actions
{
    public class Update
    {
        public static async Task<string> CheckForUpdate()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("curl/7.55.1");

                    string releasesUrl =
                        "https://api.github.com/repos/Ameliorated-LLC/amecs/releases";
                    var response = await httpClient.GetAsync(releasesUrl);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var array = JArray.Parse(json);

                    var tag = ((string)array.First["tag_name"]).TrimStart('v');

                    var versionNumber = GetVersionNumber(tag);

                    if (versionNumber > GetVersionNumber(Program.Ver))
                        return tag;
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<bool> CheckForUpdateAction()
        {
            try
            {
                string tag;
                ConsoleTUI.OpenFrame.WriteCentered("Checking for updates");

                try
                {
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        using (var httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("curl/7.55.1");

                            string releasesUrl =
                                "https://api.github.com/repos/Ameliorated-LLC/amecs/releases";
                            var response = await httpClient.GetAsync(releasesUrl);
                            response.EnsureSuccessStatusCode();

                            var json = await response.Content.ReadAsStringAsync();
                            var array = JArray.Parse(json);

                            tag = ((string)array.First["tag_name"]).TrimStart('v');

                            var versionNumber = GetVersionNumber(tag);

                            if (versionNumber <= GetVersionNumber(Program.Ver))
                                tag = null;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("Error fetching update link: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red,
                        Console.BackgroundColor, new ChoicePrompt()
                        {
                            AnyKey = true,
                            Text = "Press any key to return to the Menu: "
                        });
                    return false;
                }
                Console.WriteLine();
                
                if (tag == null)
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close($"No new updates are available.", ConsoleColor.Gray,
                        Console.BackgroundColor, new ChoicePrompt()
                        {
                            AnyKey = true,
                            Text = "Press any key to return to the Menu: "
                        });
                    return true;
                }

                Console.WriteLine();
                var choice = new ChoicePrompt() { Text = $"Update {tag} is available, update to v{tag}? (Y/N): " }
                    .Start();
                if (!choice.HasValue) return true;

                if (choice == 1)
                    return true;

                ConsoleTUI.OpenFrame.WriteCenteredLine("\r\nDownload progress");
                var stdout = GetStdHandle(-11);
                var maxHashTags = (ConsoleTUI.OpenFrame.DisplayWidth - 5);
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.ProgressChanged += (sender, args) =>
                {
                    var currentHashTags =
                        (int)Math.Ceiling(Math.Min(((double)args.ProgressPercentage / 100) * maxHashTags,
                            maxHashTags));
                    var spaces = maxHashTags - currentHashTags + (4 - args.ProgressPercentage.ToString().Length);
                    var sb = new StringBuilder(new string('#', currentHashTags) + new string(' ', spaces) +
                                               args.ProgressPercentage + "%");
                    uint throwaway;
                    WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length,
                        new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop),
                        out throwaway);
                };
                
                var sb = new StringBuilder(new string(' ', maxHashTags) + " 0%");
                uint throwaway;
                WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length,
                    new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop),
                    out throwaway);
                
                Thread.Sleep(1000);

                await InstallUpdate(backgroundWorker);

                sb = new StringBuilder(new string('#', maxHashTags) + " 100%");
                WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length,
                    new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop),
                    out throwaway);

                Thread.Sleep(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red,
                    Console.BackgroundColor, new ChoicePrompt()
                    {
                        AnyKey = true,
                        Text = "Press any key to return to the Menu: "
                    });
                return false;
            }

            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close($"AutoLogon enabled successfully", ConsoleColor.Green, Console.BackgroundColor,
                new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, StringBuilder lpCharacter,
            uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        public static async Task InstallUpdate(BackgroundWorker BackgroundWorker)
        {
            CultureInfo.GetCultures(CultureTypes.AllCultures);
            if (BackgroundWorker == null)
                throw new Exception("InstallWizardUpdate was called with no BackgroundWorker specified.");
            BackgroundWorker.WorkerReportsProgress = true;

            BackgroundWorker.ReportProgress(5);

            var amecsPath = Assembly.GetExecutingAssembly().Location;
            // Download/install update
            using (var httpClient = new HttpProgressClient())
            {
                string downloadUrl;
                long size = 55000000;

                try
                {
                    httpClient.Client.DefaultRequestHeaders.UserAgent.ParseAdd("curl/7.55.1");

                    string releasesUrl = "https://api.github.com/repos/Ameliorated-LLC/amecs/releases";
                    var response = await httpClient.GetAsync(releasesUrl);
                    response.EnsureSuccessStatusCode();

                    var releasesContent = await response.Content.ReadAsStringAsync();
                    var releases = JArray.Parse(releasesContent);
                    var release = releases.FirstOrDefault();

                    downloadUrl = null;

                    if (release?.SelectToken("assets") is JArray assets)
                    {
                        var asset = assets.FirstOrDefault(a => a["name"].ToString().Contains("amecs")
                                                               && a["name"].ToString().EndsWith(".exe")) ??
                                    assets.FirstOrDefault(a => a["name"].ToString().EndsWith(".exe"));
                        if (asset != null)
                        {
                            downloadUrl = asset["browser_download_url"]?.ToString();

                            if (asset["size"] != null)
                                long.TryParse(asset["size"].ToString(), out size);
                        }
                    }

                    if (downloadUrl == null)
                        throw new Exception("GitHub link unavailable.");
                }
                catch (Exception e)
                {
                    ConsoleTUI.ShowErrorBox("Failed to fetch update: " + e.Message, null);
                    return;
                }

                BackgroundWorker.ReportProgress(10);

                if (downloadUrl == null)
                    throw new Exception("Download link unavailable.");

                httpClient.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                {
                    if (progressPercentage.HasValue)
                        BackgroundWorker.ReportProgress((int)Math.Ceiling(10 + (progressPercentage.Value * 0.9)));
                };

                try
                {
                    foreach (var tempFile in Directory.GetFiles(@"%TEMP%", "AME-amecs-Update-*"))
                    {
                        File.Delete(tempFile);
                    }
                }
                catch
                {
                }

                var dest = Environment.ExpandEnvironmentVariables(@"%TEMP%\AME-amecs-Update-" +
                                                                  new Random().Next(1000, 99999).ToString() + ".exe");
                try
                {
                    await httpClient.StartDownload(downloadUrl, dest, size);
                }
                catch (Exception e)
                {
                    ConsoleTUI.ShowErrorBox("Failed to download update: " + e.Message, null);
                }

                BackgroundWorker.ReportProgress(100);

                var replacedPath = amecsPath.Replace(".exe", ".bak");
                try
                {
                    FileSecurity fileSecurity = File.GetAccessControl(dest);
                    fileSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), 
                        FileSystemRights.Read | FileSystemRights.ExecuteFile, AccessControlType.Allow));

                    File.SetAccessControl(dest, fileSecurity);
                    
                    if (File.Exists(replacedPath))
                        File.Delete(replacedPath);

                    File.Move(amecsPath, replacedPath);
                    File.Move(dest, amecsPath);
                }
                catch (Exception e)
                {
                    try
                    {
                        if (File.Exists(dest))
                            File.Delete(dest);
                    }
                    catch
                    {
                    }

                    throw new Exception(e.Message);
                }
            }

            try
            {
                Process.Start(amecsPath, "-Updated");
            }
            catch (Exception e)
            {
                try
                {
                    if (File.Exists(amecsPath))
                        File.Delete(amecsPath);
                    if (File.Exists(amecsPath.Replace(".exe", ".bak")))
                        File.Move(amecsPath.Replace(".exe", ".bak"), amecsPath);
                }
                catch
                {
                }

                ConsoleTUI.ShowErrorBox("Failed to install update: " + e.Message, null);
                return;
            }

            Process.GetCurrentProcess().Kill();
        }

        public static decimal GetVersionNumber(string toBeParsed)
        {
            // Examples:
            // 0.4
            // 0.4 Alpha
            // 1.0.5
            // 1.0.5 Beta


            // Remove characters after first space (and the space itself)
            if (toBeParsed.IndexOf(' ') >= 0)
                toBeParsed = toBeParsed.Substring(0, toBeParsed.IndexOf(' '));

            if (toBeParsed.LastIndexOf('.') != toBeParsed.IndexOf('.'))
            {
                // Example: 1.0.5
                toBeParsed = toBeParsed.Remove(toBeParsed.LastIndexOf('.'), 1);
                // Result: 1.05
            }

            return decimal.Parse(toBeParsed, CultureInfo.InvariantCulture);
        }

        public class HttpProgressClient : IDisposable
        {
            private string _downloadUrl;
            private string _destinationFilePath;

            public HttpClient Client;

            public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded,
                double? progressPercentage);

            public event ProgressChangedHandler ProgressChanged;

            public HttpProgressClient()
            {
                Client = new HttpClient { Timeout = TimeSpan.FromDays(1) };
            }

            public async Task StartDownload(string downloadUrl, string destinationFilePath, long? size = null)
            {
                _downloadUrl = downloadUrl;
                _destinationFilePath = destinationFilePath;

                using (var response = await Client.GetAsync(_downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                    await DownloadFileFromHttpResponseMessage(response, size);
            }

            public Task<HttpResponseMessage> GetAsync(string link)
            {
                return Client.GetAsync(link);
            }

            private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response, long? size)
            {
                response.EnsureSuccessStatusCode();

                if (!size.HasValue)
                    size = response.Content.Headers.ContentLength;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                    await ProcessContentStream(size, contentStream);
            }

            private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
            {
                var totalBytesRead = 0L;
                var readCount = 0L;
                var buffer = new byte[8192];
                var isMoreToRead = true;

                using (var fileStream = new FileStream(_destinationFilePath, FileMode.Create, FileAccess.Write,
                           FileShare.None, 8192, true))
                {
                    do
                    {
                        var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            isMoreToRead = false;
                            TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                            continue;
                        }

                        await fileStream.WriteAsync(buffer, 0, bytesRead);

                        totalBytesRead += bytesRead;
                        readCount += 1;

                        if (readCount % 50 == 0)
                            TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                    } while (isMoreToRead);
                }
            }

            private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
            {
                if (ProgressChanged == null)
                    return;

                double? progressPercentage = null;
                if (totalDownloadSize.HasValue)
                {
                    progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);
                }


                ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage);
            }

            public void Dispose()
            {
                Client?.Dispose();
            }
        }
    }
}