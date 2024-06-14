using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;

namespace amecs.Actions
{
    public class Integrity
    {
        public static Task<bool> CheckIntegrity()
        {
            bool legacy = false;
            
            var registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            int winVer = 1;
            if (registryKey != null) winVer = Int32.Parse(registryKey.GetValue("CurrentBuildNumber").ToString());
            if (winVer < 19044) legacy = true;

            using (var scanner = new Scanner())
            {
                scanner.displayTask = scanner.DisplayQuery("Checking for Windows Defender activity...", 250);
                scanner.Query(Type.Process, "MsMpEng", true);
                
                scanner.displayTask = scanner.DisplayQuery("Checking Windows Defender files...", 200);
                if (!legacy) {
                    scanner.Query(Type.Directory, "%ProgramFiles%\\Windows Defender");
                    scanner.Query(Type.Directory, "%ProgramData%\\Microsoft\\Windows Defender", true);
                }
                else scanner.Query(Type.Directory, "%ProgramFiles%\\Windows Defender", true);

                if (!legacy) {
                    scanner.displayTask = scanner.DisplayQuery("Checking Windows Update service...", 350);
                    scanner.Query(Type.Service, "wuauserv", true);
                }

                scanner.displayTask = scanner.DisplayQuery("Checking Windows Update files...", 220);
                scanner.Query(Type.File, "%WINDIR%\\System32\\wuaueng.dll");
                scanner.Query(Type.File, "%WINDIR%\\System32\\wuapi.dll", true);
                
                scanner.displayTask = scanner.DisplayQuery("Checking Microsoft Edge...", 200);
                scanner.Query(Type.Directory, "%ProgramFiles(x86)%\\Microsoft\\Edge");
                scanner.Query(Type.Directory, "%WINDIR%\\SystemApps\\*MicrosoftEdge*", true);
                
                scanner.displayTask = scanner.DisplayQuery("Checking for Microsoft Store activity...", 200);
                scanner.Query(Type.Process, "WinStore.App", true, false);
                
                scanner.displayTask = scanner.DisplayQuery("Checking Windows SmartScreen...");
                scanner.Query(Type.Process, "smartscreen");
                scanner.Query(Type.File, "%WINDIR%\\System32\\smartscreen.exe", true);
                
                scanner.displayTask = scanner.DisplayQuery("Checking SIH Client...");
                scanner.Query(Type.File, "%WINDIR%\\System32\\SIHClient.exe", true);
                
                scanner.displayTask = scanner.DisplayQuery("Checking Storage Sense...", 300);
                scanner.Query(Type.File, "%WINDIR%\\System32\\StorSvc.dll", true);
                
                scanner.DisplayResult();
            }

            return Task.FromResult(true);
        }
    }
    
    
    
    
    
    public enum Type
    {
        File = 1,
        Directory = 2,
        Process = 3,
        Service = 4
    }
    internal class Scanner : IDisposable
    {
        private int result = 1;
        private bool allFound = true;
        private bool found = false;
        private bool errorOverride = false;
        public Task<bool> displayTask;
        
        public void Dispose() => GC.SuppressFinalize(this);

        public async Task<bool> DisplayQuery(string text, int time = 150)
        {
            ConsoleTUI.OpenFrame.Write(text);
            
            string maxSpaces = "      ";
            for (int i = 1; i < 6; i++) {
                Console.SetCursorPosition(59, Console.CursorTop);
                var spaces = maxSpaces.Remove(0, i);
                
                Console.Write($"[ {spaces.PadLeft(spaces.Length + i, '*')} ]");
                Thread.Sleep(time);
            }
            
            return true;
        }

        private static void SetQueryStatus(string status, ConsoleColor color)
        {
            Console.SetCursorPosition(64 - status.Length, Console.CursorTop);
            Console.Write(" [ ");

            var foreground = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(status);
            Console.ForegroundColor = foreground;
            Console.WriteLine(" ]");
        }
        
        public async void Query(Type type, string item, bool finalize = false, bool modifyResult = true)
        {
            item = Environment.ExpandEnvironmentVariables(item);
            bool foundItem = false;
            try {
                switch (type) {
                    case Type.File:
                        if (item.Contains("*"))
                        {
                            var lastToken = item.LastIndexOf("\\");
                            var parentPath = item.Remove(lastToken).TrimEnd('\\');

                            if (parentPath.Contains("*")) throw new ArgumentException("Parent directories to a given file filter cannot contain wildcards.");
                            var filter = item.Substring(lastToken + 1);

                            foundItem = Directory.GetFiles(parentPath, filter).Any();
                            break;
                        }
                        
                        foundItem = File.Exists(item);
                        break;
                    case Type.Directory:
                        if (item.Contains("*"))
                        {
                            var lastToken = item.LastIndexOf("\\");
                            var parentPath = item.Remove(lastToken).TrimEnd('\\');
                            
                            if (parentPath.Contains("*")) throw new ArgumentException("Parent directories to a given file filter cannot contain wildcards.");
                            var filter = item.Substring(lastToken + 1);

                            var foundDirs = Directory.GetDirectories(parentPath, filter);
                            
                            foreach (var foundDir in foundDirs)
                            {
                                foreach (var file in Directory.GetFiles(foundDir, "*", SearchOption.AllDirectories))
                                {
                                    if (!file.ToLower().EndsWith(".mui") && !file.ToLower().EndsWith(".pri") && !file.ToLower().EndsWith(".res"))
                                    {
                                        foundItem = true;
                                    }
                                }
                            }
                        }
                        else
                        {

                            if (Directory.Exists(item))
                            {
                                foreach (var file in Directory.GetFiles(item, "*", SearchOption.AllDirectories))
                                {
                                    if (!file.ToLower().EndsWith(".mui") && !file.ToLower().EndsWith(".pri") && !file.ToLower().EndsWith(".res"))
                                    {
                                        foundItem = true;
                                    }
                                }
                            }
                        }

                        break;
                    case Type.Process:
                        foundItem = Process.GetProcessesByName(item).Any();
                        break;
                    case Type.Service:
                        foundItem = ServiceController.GetServices().Any(x => x.ServiceName.Equals("wuauserv", StringComparison.CurrentCultureIgnoreCase));
                        break;
                    default:
                        foundItem = false;
                        break;
                }
            } catch (Exception e) {
                if (e.GetType().ToString() == "System.UnauthorizedAccessException" || e.GetType().ToString() == "System.Security.SecurityException")
                {
                    foundItem = true;
                }
                else
                {
                    errorOverride = true;
                }
            }

            if (foundItem) found = true;

            if (!finalize) return;

            await displayTask;
            
            if (errorOverride) {
                errorOverride = false;
                SetQueryStatus("ERROR", ConsoleColor.DarkRed);
                found = false;
                return;
            }
            
            if (!found) {
                if (modifyResult) allFound = false;
                SetQueryStatus("Absent", ConsoleColor.Green);
            } else {
                result = 2;
                if (allFound) result = 3;

                SetQueryStatus("Present", ConsoleColor.DarkRed);
            }
            found = false;
        }

        public void DisplayResult()
        {
            Console.WriteLine();

            switch (result) {
                case 1:
                    ConsoleTUI.OpenFrame.Close("AME integrity validated", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                    break;
                case 2:
                    ConsoleTUI.OpenFrame.Close("AME integrity compromised, contact the team for help.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                    break;
                case 3:
                    ConsoleTUI.OpenFrame.Close("Your system is not ameliorated.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                    break;
            }
        }
    }
}