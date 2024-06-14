using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using amecs.Misc;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using Task = System.Threading.Tasks.Task;

namespace amecs.Actions
{
    public class Deameliorate
    {
        
        public static Task<bool> ShowMenu()
        {
            if (new ChoicePrompt()
                {
                    Text = @"
We recommend backing up important data before removing AME
Continue? (Y/N): "
                }.Start()!.Value == 1) return Task.FromResult(true);
            
            ConsoleTUI.OpenFrame.Clear();
            
            var mainMenu = new Ameliorated.ConsoleUtils.Menu()
            {
                Choices =
                {
                    new Menu.MenuItem("Uninstall AME using a Windows USB", new Func<bool>(DeameliorateUSB)),
                    new Menu.MenuItem("Uninstall AME using a Windows ISO", new Func<bool>(DeameliorateISO)),
                    Menu.MenuItem.Blank,
                    new Menu.MenuItem("Return to Menu", new Func<bool>(() => true)),
                    new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit)),
                },
                SelectionForeground = ConsoleColor.Green
            };
            mainMenu.Write("Windows install media is required to restore files.");
            var result = (Func<bool>)mainMenu.Load(true);
            return Task.FromResult(result.Invoke());
        }
        
        
        [DllImport("kernel32.dll", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
        
        private static string _mountedPath;
        private static string _winVer;
        private static string _win11Setup = "";
        private static bool _win11 = Environment.OSVersion.Version.Build >= 22000;
        private const string ExplorerPatcherId = "D17F1E1A-5919-4427-8F89-A1A8503CA3EB";

        public static bool DeameliorateUSB() => DeameliorateCore(true, false);
        public static bool DeameliorateISO() => DeameliorateCore(false, true);
        
        public static bool DeameliorateCore(bool usb, bool iso)
        {
            if (usb && !iso)
                ConsoleTUI.OpenFrame.WriteCenteredLine("Select Windows USB drive");
            if (iso && !usb)
                ConsoleTUI.OpenFrame.WriteCenteredLine("Select Windows ISO file");
            
            (_mountedPath, _, _winVer, _, _) = SelectWindowsImage.GetMediaPath(usb: usb, iso: iso);
            if (_mountedPath == null) return false;
            
            try
            {
                string openShellId = null;
                
                var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (var item in key.GetSubKeyNames())
                {
                    try
                    {
                        if (((string)key.OpenSubKey(item).GetValue("DisplayName")).Equals("Open-Shell"))
                            openShellId = item;
                    }
                    catch
                    {
                        // do nothing
                    }
                }

                Console.WriteLine();
                
                if (openShellId != null)
                {
                    ConsoleTUI.OpenFrame.WriteCentered("Uninstalling Open-Shell");
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        foreach (var process in Process.GetProcessesByName("explorer"))
                        {
                            try
                            {
                                TerminateProcess(process.Handle, 1);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                        
                        Process.Start("MsiExec.exe", $"/X{openShellId} /quiet")?.WaitForExit();

                        if (Globals.UserSID != null)
                        {
                            var appData = (string)Registry.Users
                                .OpenSubKey(Globals.UserSID +
                                            @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders")?
                                .GetValue("AppData");

                            if (Directory.Exists(Path.Combine(appData ?? "NULL:", "OpenShell")))
                                Directory.Delete(Path.Combine(appData!, "OpenShell"), true);
                        }
                    }
                    Console.WriteLine();
                }
                
                var epSetupPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\ExplorerPatcher\ep_setup.exe";
                if (File.Exists(epSetupPath))
                {
                    ConsoleTUI.OpenFrame.WriteCentered("Uninstalling ExplorerPatcher");
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        foreach (var process in Process.GetProcessesByName("explorer"))
                        {
                            try
                            {
                                TerminateProcess(process.Handle, 1);
                            }
                            catch (Exception e)
                            {
                            }
                        }
                        
                        var winlogon =
                            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon",
                                true);
                        winlogon?.SetValue("AutoRestartShell", 0);

                        // kill processes that the files use
                        foreach (var processName in new[]
                                 {
                                     "explorer.exe", "rundll32.exe", "dllhost.exe", "ShellExperienceHost.exe",
                                     "StartMenuExperienceHost.exe"
                                 })
                        {
                            foreach (var process in Process.GetProcessesByName(
                                         Path.GetFileNameWithoutExtension(processName)))
                            {
                                process.Kill();
                                process.WaitForExit();
                            }
                        }

                        // delete DWM service that removes rounded corners
                        Process.Start("sc", $"stop \"ep_dwm_{ExplorerPatcherId}\"")?.WaitForExit();
                        Process.Start("sc", $"delete \"ep_dwm_{ExplorerPatcherId}\"")?.WaitForExit();

                        // remove registered DLL
                        var explorerPatcherDllPath =
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                "ExplorerPatcher", "ExplorerPatcher.amd64.dll");
                        Process.Start("regsvr32.exe", $"/s /u \"{explorerPatcherDllPath}\"")?.WaitForExit();

                        // delete files
                        foreach (var file in new[]
                                 {
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                         @"SystemApps\ShellExperienceHost_cw5n1h2txyewy\dxgi.dll"),
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                         @"SystemApps\ShellExperienceHost_cw5n1h2txyewy\wincorlib.dll"),
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                         @"SystemApps\ShellExperienceHost_cw5n1h2txyewy\wincorlib_orig.dll"),
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                         @"SystemApps\Microsoft.Windows.StartMenuExperienceHost_cw5n1h2txyewy\dxgi.dll"),
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                         @"SystemApps\Microsoft.Windows.StartMenuExperienceHost_cw5n1h2txyewy\wincorlib.dll"),
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                         @"SystemApps\Microsoft.Windows.StartMenuExperienceHost_cw5n1h2txyewy\wincorlib_orig.dll"),
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                         "dxgi.dll")
                                 })
                        {
                            if (File.Exists(file)) File.Delete(file);
                        }

                        foreach (var folder in new[]
                                 {
                                     Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                         "ExplorerPatcher"),
                                     Path.Combine(
                                         Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                                         @"Microsoft\Windows\Start Menu\Programs\ExplorerPatcher")
                                 })
                        {
                            if (Directory.Exists(folder)) Directory.Delete(folder, true);
                        }

                        winlogon?.SetValue("AutoRestartShell", 1);
                    }
                    Console.WriteLine();
                }
            } catch (Exception e)
            {
                ConsoleTUI.OpenFrame.Close(
                    "Error while uninstalling software: " + e.Message,
                    ConsoleColor.Yellow, Console.BackgroundColor,
                    new ChoicePrompt { AnyKey = true, Text = "Press any key to continue: " });
                    
                Program.Frame.Clear();
                ConsoleTUI.OpenFrame.WriteCenteredLine("\r\nContinuing de-amelioration process...");
            }
            
            // restart Explorer
            if (Process.GetProcessesByName("explorer").Length == 0)
                NSudo.RunProcessAsUser(NSudo.GetUserToken(), "explorer.exe", "", 0);

            // all policies are cleared as a user that's de-ameliorating is unlikely to have their own policies in the first place
            // also clear ExplorerPatcher Registry entries
            ConsoleTUI.OpenFrame.WriteCentered("Clearing policies");
            using (new ConsoleUtils.LoadingIndicator(true))
            {
                foreach (var keyPath in new[]
                         {
                             $@"HKU\{Globals.UserSID}\Software\Microsoft\Windows\CurrentVersion\Policies",
                             $@"HKU\{Globals.UserSID}\Software\Policies",
                             $@"HKU\{Globals.UserSID}\Software\ExplorerPatcher",
                             $@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{{{ExplorerPatcherId}}}_ExplorerPatcher",
                             @"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies",
                             @"HKLM\Software\Policies",
                             @"HKLM\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Policies",
                             @"HKLM\Software\AME\Playbooks\Applied\{9010E718-4B54-443F-8354-D893CD50FDDE}",
                             @"HKLM\Software\AME\Playbooks\Applied\{513722D2-CE95-4D2A-A88A-53570642BC4E}"
                         })
                {
                    var hive = RegistryHive.LocalMachine;
                    if (keyPath.StartsWith("HKU"))
                        hive = RegistryHive.Users;

                    var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default);
                    var subKeyPath = keyPath.Substring(keyPath.IndexOf('\\') + 1);
                    var key = baseKey.OpenSubKey(subKeyPath, true);
                    if (key == null) continue;

                    try
                    {
                        baseKey.DeleteSubKeyTree(subKeyPath);
                    }
                    catch
                    {
                        // do nothing - some values might fail, but almost all are deleted
                    }

                    key.Close();
                }
                Thread.Sleep(2000);
            }
            ConsoleTUI.OpenFrame.WriteCentered(
                "\r\nInitiating Windows setup for file restoration");


            try
            {
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    if (_win11) _win11Setup = " /Product Server";
                    Process.Start(Path.Combine(_mountedPath, "setup.exe"),
                        $"/Auto Upgrade /DynamicUpdate Disable{_win11Setup}");
                    
                    Thread.Sleep(2000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close(
                    $"There was an error when trying to run the Windows Setup: {e}\r\nTry running the Windows Setup manually from File Explorer.",
                    ConsoleColor.Red, Console.BackgroundColor,
                    new ChoicePrompt { AnyKey = true, Text = $"Press any key to exit: " });

                return false;
            }

            Console.WriteLine();
            
            try
            {
                // Create a new task definition and assign properties
                TaskDefinition td = TaskService.Instance.NewTask();
                td.Principal.UserId = "SYSTEM";
                td.Principal.RunLevel = TaskRunLevel.Highest;

                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(new BootTrigger() { });

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction("sfc", "/scannow"));
                td.Actions.Add(new ExecAction("SCHTASKS", @"/delete /tn ""sfc"" /f"));

                td.Settings.DisallowStartIfOnBatteries = false;
                td.Settings.StopIfGoingOnBatteries = false;
                td.Settings.AllowHardTerminate = false;
                td.Settings.ExecutionTimeLimit = TimeSpan.Zero;

                // Register the task in the root folder.
                TaskService.Instance.RootFolder.RegisterTaskDefinition(@"sfc", td);
            }
            catch (Exception e)
            {
            }
            
            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close(
                "Windows setup has begun, accept the license to begin restoring system files. Your system will restart.",
                ConsoleColor.Yellow, Console.BackgroundColor,
                new ChoicePrompt { AnyKey = true, Text = $"Press any key to Exit: " });
            
            Environment.Exit(0);
            Thread.Sleep(-1);
            return true;
        }
    }
}