using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using amecs.Actions;
using Ameliorated.ConsoleUtils;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace amecs.Extra
{
    public class NVCP
    {
        private static readonly string DestinationDir = Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\NVIDIA Control Panel");
        
        public static bool Install(string NVCP) => amecs.RunBasicAction("Installing NVIDIA Control Panel", "NVIDIA Control Panel installed successfully", () =>
        {
            Thread.Sleep(4000);
            
            try
            {
                foreach (var proc in Process.GetProcessesByName("nvcplui"))
                    proc.Kill();
            } catch
            {
            }

            if (Directory.Exists(DestinationDir))
                Directory.Delete(DestinationDir, true);

            Directory.Move(NVCP, DestinationDir);
            var di = new DirectoryInfo(DestinationDir);
            var sec = di.GetAccessControl();
            
            sec.AddAccessRule(new FileSystemAccessRule("Administrators", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            sec.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.ReadAndExecute, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            
            di.SetAccessControl(sec);
            
            Config();
        });
        public static bool InstallFromNetwork()
        {
            try
            {
                foreach (var proc in Process.GetProcessesByName("nvcplui"))
                    proc.Kill();
            } catch
            {
            }

            if (Directory.Exists(DestinationDir))
                Directory.Delete(DestinationDir, true);

            var choice = new ChoicePrompt() {Text = "NVIDIA Control Panel must be downloaded\r\nContinue? (Y/N): "}.Start();
            if (!choice.HasValue || choice == 1)
                return false;

            string link;
            string filter;
            string size;
            
            ConsoleTUI.OpenFrame.WriteCentered("\r\nFetching download link");
            
            try
            {
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = client.GetAsync("https://git.ameliorated.info/Styris/amecs/src/branch/master/links.txt").Result)
                        {
                            using (HttpContent content = response.Content)
                            {
                                string result = content.ReadAsStringAsync().Result;
                                var line = result.SplitByLine().First(x => x.Contains("NVIDIA-Control-Panel = "));
                                var split = line.Split('|');

                                link = split[1];
                                filter = split[3];

                                if (link == "REMOVED")
                                    throw new Exception("Link is no longer available.");
                            }
                        }
                        
                        var values = new Dictionary<string, string>
                        {
                            { "type", "url" },
                            { "url", link },
                            { "ring", "Retail" }
                        };

                        var request = new FormUrlEncodedContent(values);
                        
                        using (HttpResponseMessage response = client.PostAsync("https://store.rg-adguard.net/api/GetFiles", request).Result)
                        {
                            using (HttpContent content = response.Content)
                            {
                                string result = content.ReadAsStringAsync().Result;
                                var regex = new Regex($".*{filter}.*");
                                var match = regex.Match(result).Value;

                                var split = match.Split('"');
                                
                                link = split[3];
                                size = split[12].Remove(0, 1).Remove(split[12].Length - 11);
                                if (!size.Contains("MB") && !size.Contains("KB") && !size.Contains("GB"))
                                    size = "0 MB";
                            }
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Could not fetch link: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }

            var temp = Environment.ExpandEnvironmentVariables(@"%TEMP%\[amecs]-NVCP-" + new Random().Next(0, 9999) + ".zip");
            
            try
            {
                ConsoleTUI.OpenFrame.WriteCenteredLine($"\r\nDownloading NVIDIA Control Panel ({size})");
                using (WebClient wc = new WebClient())
                {
                    var stdout = GetStdHandle(-11);
                    var maxHashTags = (ConsoleTUI.OpenFrame.DisplayWidth - 5);
                    wc.DownloadProgressChanged += delegate(object sender, DownloadProgressChangedEventArgs e)
                    {
                        var currentHashTags = (int)Math.Ceiling(Math.Min(((double)e.ProgressPercentage / 100) * maxHashTags, maxHashTags));
                        var spaces = maxHashTags - currentHashTags + (4 - e.ProgressPercentage.ToString().Length);
                        var sb = new StringBuilder(new string('#', currentHashTags) + new string(' ', spaces) + e.ProgressPercentage + "%");
                        uint throwaway;
                        WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                    };
                    var task = wc.DownloadFileTaskAsync(new Uri(link), temp);
                    task.Wait();
                    Thread.Sleep(100);
                    var sb = new StringBuilder(new string('#', maxHashTags) + " 100%");
                    uint throwaway;
                    WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                }
                Console.WriteLine();
                ZipFile.ExtractToDirectory(temp, DestinationDir);

                if (!File.Exists(Path.Combine(DestinationDir, "nvcplui.exe")))
                {
                    try { Directory.Delete(DestinationDir, true);} catch {}
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("Download is missing critical executable.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                    return false;
                }
                Config();
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }
            
            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("NVIDIA Control Panel installed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }

        public static bool Uninstall() => amecs.RunBasicAction("Uninstalling NVIDIA Control Panel", "NVIDIA Control Panel uninstalled successfully", () =>
        {
            var linkPath = Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\Microsoft\Windows\Start Menu\Programs\NVIDIA Control Panel.lnk");
            if (File.Exists(linkPath))
                File.Delete(linkPath);

            Directory.Delete(DestinationDir, true);
            
            Thread.Sleep(2000);
        });

        public static void Config()
        {
            try
            {
                new Reg.Value() { KeyName = @"HKLM\System\CurrentControlSet\Services\nvlddmkm\Global\NVTweak", ValueName = "DisableStoreNvCplNotifications", Type = Reg.RegistryValueType.REG_DWORD, Data = 1 }.Apply();
            } catch (Exception e)
            {
                ConsoleTUI.ShowErrorBox("Could not disable NVIDIA Microsoft Store notification: " + e.ToString(), "Error");
            }
            
            var linkPath = Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\Microsoft\Windows\Start Menu\Programs\NVIDIA Control Panel.lnk");
            if (File.Exists(linkPath))
                File.Delete(linkPath);

            try
            {
                IShellLink link = (IShellLink)new ShellLink();
            
                link.SetDescription("NVIDIA Control Panel");
                link.SetPath(Path.Combine(DestinationDir, "nvcplui.exe"));
                
                IPersistFile file = (IPersistFile)link;
                file.Save(linkPath, false);
            } catch
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(linkPath);
                shortcut.Description = "NVIDIA Control Panel";
                shortcut.TargetPath = Path.Combine(DestinationDir, "nvcplui.exe");
                shortcut.Save();
            }

            new Reg.Value() { KeyName = @"HKLM\SYSTEM\CurrentControlSet\Services\NVDisplay.ContainerLocalSystem", ValueName = "Start", Type = Reg.RegistryValueType.REG_DWORD, Data = 2 }.Apply();
            try { ServiceController.GetServices().First(x => x.ServiceName.Equals("NVDisplay.ContainerLocalSystem")).Start(); } catch (Exception e) { }
        }
        
        
        
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, StringBuilder lpCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        
        
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
    }
    
    
    
    [ComImport]
    [Guid("00021401-0000-0000-C000-000000000046")]
    internal class ShellLink
    {
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("000214F9-0000-0000-C000-000000000046")]
    internal interface IShellLink
    {
        void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
        void GetIDList(out IntPtr ppidl);
        void SetIDList(IntPtr pidl);
        void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
        void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
        void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
        void GetHotkey(out short pwHotkey);
        void SetHotkey(short wHotkey);
        void GetShowCmd(out int piShowCmd);
        void SetShowCmd(int iShowCmd);
        void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
        void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
        void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
        void Resolve(IntPtr hwnd, int fFlags);
        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }
}