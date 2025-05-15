using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using File = System.IO.File;

namespace amecs.Actions
{
    public class SoftwareMenu
    {
        public static async Task<bool> ShowMenu()
        {
            while (true)
            {
                Program.Frame.Clear();

                bool netInstalled = new Reg.Key()
                {
                    KeyName = @"HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5",
                    Operation = RegistryOperation.Add
                }.IsEqual();
                
                bool win11 = Win32.SystemInfoEx.WindowsVersion.MajorVersion >= 11;
                bool appfetchInstalled = System.IO.File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\AME\appfetch.exe"));
                bool settingsInstalled = System.IO.File.Exists(Environment.ExpandEnvironmentVariables($@"%ProgramData%\AME\{(win11 ? "privacy+_settings" : "ame10_settings")}.exe")) || 
                                         System.IO.File.Exists(Environment.ExpandEnvironmentVariables(@"%WINDIR%\System32\amecs.exe"));

                var mainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    EscapeValue = null,
                    Choices =
                    {
                        new Menu.MenuItem("Manage Browsers", new Func<Task<bool>>(Browsers.ShowMenu)),
                        Menu.MenuItem.Blank,

                        !netInstalled
                            ? new Menu.MenuItem("Install .NET 3.5", new Func<Task<bool>>(_NET.ShowMenu))
                            : new Menu.MenuItem("Install .NET 3.5", new Func<Task<bool>>(_NET.ShowMenu))
                            {
                                IsEnabled = false, SecondaryText = "[Installed]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            },
                        !appfetchInstalled
                            ? new Menu.MenuItem("Install AppFetch", new Func<Task<bool>>(InstallAppFetch))
                            : new Menu.MenuItem("Install AppFetch", new Func<Task<bool>>(InstallAppFetch))
                            {
                                IsEnabled = false, SecondaryText = "[Installed]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            },
                        !settingsInstalled
                            ? new Menu.MenuItem($"Install {(win11 ? "Privacy+" : "AME10")} Settings", new Func<Task<bool>>(InstallSettings))
                            : new Menu.MenuItem($"Install {(win11 ? "Privacy+" : "AME10")} Settings", new Func<Task<bool>>(InstallSettings))
                            {
                                IsEnabled = false, SecondaryText = "[Installed]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            },
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Remove AME Tools", new Func<Task<bool>>(RemoveAMETools)),
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Return to Menu", null),
                        new Menu.MenuItem("Exit", new Func<Task<bool>>(Globals.ExitAsync))
                    },
                    SelectionForeground = ConsoleColor.Green
                };
                Func<Task<bool>> result;
                try
                {
                    mainMenu.Write();
                    var res = mainMenu.Load(true);
                    if (res == null)
                        return true;
                    result = (Func<Task<bool>>)res;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                    return false;
                }

                try
                {
                    await result.Invoke();
                }
                catch (Exception e)
                {
                    ConsoleTUI.ShowErrorBox("Error while running an action: " + e.ToString(), null);
                }
            }
        }

        private static async Task<bool> InstallAppFetch()
        {
            try
            {
                if (!Browsers.CheckInternet())
                    throw new Exception("Internet must be connected for this action.");

                ConsoleTUI.OpenFrame.WriteCentered("\r\nInstalling AppFetch");
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    var dest = Environment.ExpandEnvironmentVariables(@"%ProgramData%\AME\appfetch.exe");
                    // Download/install update
                    using (var httpClient = new Update.HttpProgressClient())
                    {
                        string downloadUrl;
                        long size = 41000000;

                        try
                        {
                            httpClient.Client.DefaultRequestHeaders.UserAgent.ParseAdd("curl/7.55.1");

                            string releasesUrl = "https://api.github.com/repos/Ameliorated-LLC/appfetch/releases";
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
                            throw new Exception("Update check failed: " + e.Message);
                        }


                        if (downloadUrl == null)
                            throw new Exception("Download link unavailable.");

                        httpClient.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => { };

                        try
                        {
                            if (File.Exists(dest))
                                File.Delete(dest);
                        }
                        catch { }

                        try
                        {
                            await httpClient.StartDownload(downloadUrl, dest, size);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Download failed: " + e.Message);
                            return false;
                        }

                        try
                        {
                            var fileInfo = new FileInfo(dest);
                            FileSecurity fileSecurity = fileInfo.GetAccessControl();
                            fileSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                                FileSystemRights.Read | FileSystemRights.ExecuteFile, AccessControlType.Allow));

                            fileInfo.SetAccessControl(fileSecurity);
                           
                            using (var appFetchKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\AppFetch"))
                            {
                                appFetchKey!.SetValue("DisplayName", "App Fetch Experimental", RegistryValueKind.String);
                                appFetchKey!.SetValue("PublisherName", "Ameliorated LLC", RegistryValueKind.String);
                                appFetchKey.SetValue("DisplayIcon", dest, RegistryValueKind.String);
                                appFetchKey.SetValue("UninstallString", $"\"{dest}\" --uninstall", RegistryValueKind.String);
                                appFetchKey.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                                appFetchKey.SetValue("NoModify", 1, RegistryValueKind.DWord);
                                appFetchKey.SetValue("EstimatedSize", 41062, RegistryValueKind.DWord);
                            }
                        }
                        catch (Exception e)
                        {
                            try
                            {
                                if (File.Exists(dest))
                                    File.Delete(dest);
                            }
                            catch { }

                            throw new Exception(e.Message);
                        }

                        try
                        {

                            foreach (var userDir in Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users")))
                            {
                                if (Directory.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned")))
                                {
                                    if (!File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\App Fetch Experimental.lnk")))
                                    {
                                        var shell1 = new WshShell();
                                        var shortcut1 = (IWshShortcut)shell1.CreateShortcut(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\App Fetch Experimental.lnk"));
                                        shortcut1.TargetPath = Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\AME\appfetch.exe");
                                        shortcut1.Save();
                                    }
                                }
                            }

                            Directory.CreateDirectory(
                                Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated"));
                            var shell2 = new WshShell();
                            var shortcut2 = (IWshShortcut)shell2.CreateShortcut(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\App Fetch Experimental.lnk"));
                            shortcut2.TargetPath = Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\AME\appfetch.exe");
                            shortcut2.Save();
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }
        }
        
        
        private static async Task<bool> InstallSettings()
        {
            try
            {
                
                bool win11 = Win32.SystemInfoEx.WindowsVersion.MajorVersion >= 11; 
                
                if (!Browsers.CheckInternet())
                    throw new Exception("Internet must be connected for this action.");

                ConsoleTUI.OpenFrame.WriteCentered($"\r\nInstalling {(win11 ? "Privacy+" : "AME10")} Settings");
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    var dest = Environment.ExpandEnvironmentVariables($@"%ProgramData%\AME\{(win11 ? "privacy+_settings" : "ame10_settings")}.exe");
                    // Download/install update
                    using (var httpClient = new Update.HttpProgressClient())
                    {
                        string downloadUrl;
                        long size = 41000000;

                        try
                        {
                            httpClient.Client.DefaultRequestHeaders.UserAgent.ParseAdd("curl/7.55.1");

                            string releasesUrl = win11 ? "https://api.github.com/repos/Ameliorated-LLC/ame-settings-cli/releases" : "https://api.github.com/repos/Ameliorated-LLC/ame-settings-legacy/releases";
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
                            throw new Exception("Update check failed: " + e.Message);
                        }


                        if (downloadUrl == null)
                            throw new Exception("Download link unavailable.");

                        httpClient.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => { };

                        try
                        {
                            if (File.Exists(dest))
                                File.Delete(dest);
                        }
                        catch { }

                        try
                        {
                            await httpClient.StartDownload(downloadUrl, dest, size);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Download failed: " + e.Message);
                            return false;
                        }

                        try
                        {
                            var fileInfo = new FileInfo(dest);
                            FileSecurity fileSecurity = fileInfo.GetAccessControl();
                            fileSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                                FileSystemRights.Read | FileSystemRights.ExecuteFile, AccessControlType.Allow));

                            fileInfo.SetAccessControl(fileSecurity);
                            
                            using (var settingsKey = Registry.LocalMachine.CreateSubKey($@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{(win11 ? "Privacy+" : "AME10")} Settings"))
                            {
                                settingsKey!.SetValue("DisplayName", $"{(win11 ? "Privacy+" : "AME10")} Settings", RegistryValueKind.String);
                                settingsKey!.SetValue("PublisherName", "Ameliorated LLC", RegistryValueKind.String);
                                settingsKey.SetValue("DisplayIcon", dest, RegistryValueKind.String);
                                settingsKey.SetValue("UninstallString", $"\"{dest}\" --uninstall", RegistryValueKind.String);
                                settingsKey.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                                settingsKey.SetValue("NoModify", 1, RegistryValueKind.DWord);
                                settingsKey.SetValue("EstimatedSize", 19558, RegistryValueKind.DWord);
                            }
                        }
                        catch (Exception e)
                        {
                            try
                            {
                                if (File.Exists(dest))
                                    File.Delete(dest);
                            }
                            catch { }

                            throw new Exception(e.Message);
                        }
                        
                        try
                        {

                            foreach (var userDir in Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users")))
                            {
                                if (Directory.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned")))
                                {
                                    if (!File.Exists(Path.Combine(userDir, $@"AppData\Roaming\OpenShell\Pinned\{(win11 ? "Privacy+" : "AME10")} Settings.lnk")))
                                    {
                                        var shell1 = new WshShell();
                                        var shortcut1 = (IWshShortcut)shell1.CreateShortcut(Path.Combine(userDir, $@"AppData\Roaming\OpenShell\Pinned\{(win11 ? "Privacy+" : "AME10")} Settings.lnk"));
                                        shortcut1.TargetPath = Environment.ExpandEnvironmentVariables($@"%PROGRAMDATA%\AME\{(win11 ? "privacy+_settings" : "ame10_settings")}.exe");
                                        shortcut1.Save();
                                    }
                                }
                            }

                            Directory.CreateDirectory(
                                Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated"));
                            var shell2 = new WshShell();
                            var shortcut2 = (IWshShortcut)shell2.CreateShortcut(Environment.ExpandEnvironmentVariables($@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\{(win11 ? "Privacy+" : "AME10")} Settings.lnk"));
                            shortcut2.TargetPath = Environment.ExpandEnvironmentVariables($@"%PROGRAMDATA%\AME\{(win11 ? "privacy+_settings" : "ame10_settings")}.exe");
                            shortcut2.Save();
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }
        }

        private static async Task<bool> RemoveAMETools()
        {
            bool win11 = Win32.SystemInfoEx.WindowsVersion.MajorVersion >= 11;
            ConsoleTUI.OpenFrame.WriteCenteredLine($"This will entirely remove all AME tools, including {(win11 ? "Privacy+" : "AME10")} Settings.");
            Console.WriteLine();
            Console.WriteLine();
            var choice = new ChoicePrompt()
            {
                Text = $"Are you sure you wish to continue? (Y/N): "
            }.Start();
            if (!choice.HasValue || choice != 0) return true;

            try
            {
                ConsoleTUI.OpenFrame.WriteCentered("\r\nRemoving AME tools");
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    if (File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\AME\appfetch.exe")))
                    {
                        foreach (var process in Process.GetProcessesByName("appfetch"))
                        {
                            process.Kill();
                            await Task.Delay(500);
                        }

                        File.Delete(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\AME\appfetch.exe"));
                    }

                    Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\AppFetch", false);

                    foreach (var userDir in Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users")))
                    {
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\App Fetch Experimental.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\App Fetch Experimental.lnk"));
                    }
                    
                    if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\App Fetch Experimental.lnk")))
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\App Fetch Experimental.lnk"));

                    Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Privacy+ Settings", false);
                    Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\AME10 Settings", false);

                    foreach (var userDir in Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users")))
                    {
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Central AME Script.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Central AME Script.lnk"));
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Configure AME.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Configure AME.lnk"));
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Configure Privacy+.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Configure Privacy+.lnk"));
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Privacy+ Settings.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Privacy+ Settings.lnk"));
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\AME10 Settings.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\AME10 Settings.lnk"));
                    }
                    
                    if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\Privacy+ Settings.lnk")))
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\Privacy+ Settings.lnk"));
                    if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\AME10 Settings.lnk")))
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\AME10 Settings.lnk"));

                    try
                    {
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%WINDIR%\System32\amecs.exe"));
                    }
                    catch (Exception e) { }

                    try
                    {
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\AME\privacy+_settings.exe"));
                    }
                    catch (Exception e) { }

                    try
                    {
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\AME\ame10_settings.exe"));
                    }
                    catch (Exception e) { }

                    try
                    {
                        foreach (var process in Process.GetProcessesByName(Assembly.GetExecutingAssembly().Location).Where(x => x.Id != Process.GetCurrentProcess().Id))
                        {
                            process.Kill();
                        }
                    }
                    catch (Exception e) { }

                    await Task.Delay(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }

            Console.WriteLine();
            Console.WriteLine();
            ConsoleTUI.OpenFrame.WriteCentered("Uninstallation complete, exiting...", ConsoleColor.Green);

            await Task.Delay(3000);
            try
            {
                Process.Start(new ProcessStartInfo("cmd.exe", $"/c \"timeout /t 3 /nobreak & del /q /f \"\"{Win32.ProcessEx.GetCurrentProcessFileLocation()}\"\"\"")
                    { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden });
            }
            catch (Exception e) { }

            Environment.Exit(0);
            return true;
        }
    }
}