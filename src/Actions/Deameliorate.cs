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
using JetBrains.Annotations;
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
                EscapeValue = new Func<bool>(() => true),
                Choices =
                {
                    new Menu.MenuItem("Uninstall AME using a Windows USB", new Func<bool>(DeameliorateUSB)),
                    new Menu.MenuItem("Uninstall AME using a Windows ISO", new Func<bool>(DeameliorateISO)),
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
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
        public static Task<bool> ShowMenuNoWarn()
        {
            rebooted = true;
            ConsoleTUI.OpenFrame.Clear();
            
            var mainMenu = new Ameliorated.ConsoleUtils.Menu()
            {
                EscapeValue = new Func<bool>(() => true),
                Choices =
                {
                    new Menu.MenuItem("Uninstall AME using a Windows USB", new Func<bool>(DeameliorateUSB)),
                    new Menu.MenuItem("Uninstall AME using a Windows ISO", new Func<bool>(DeameliorateISO)),
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
                    Menu.MenuItem.Blank,
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
        private static bool rebooted = false;
        
        
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
        
        public static bool DeameliorateCore(bool usb, bool iso, [CanBeNull] string path = null)
        {
            string _isoPath = null;
            if (path != null)
            {
                if (File.Exists(path))
                {
                    var startInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = false,
                        UseShellExecute = false,
                        FileName = "PowerShell.exe",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = $"-NoP -C \"(Mount-DiskImage '{path}' -PassThru | Get-Volume).DriveLetter + ':\'\"",
                        RedirectStandardOutput = true
                    };

                    var proc = Process.Start(startInfo);
                    proc.WaitForExit();

                    _mountedPath = proc.StandardOutput.ReadLine();
                } else
                    _mountedPath = path;
            }
            else
            {
                (_mountedPath, _isoPath, _winVer, _, _) = SelectWindowsImage.GetMediaPath(usb: usb, iso: iso);
                if (_mountedPath == null) return false;
            }


            if (path == null && !rebooted)
            {
                try
                {
                    ConsoleTUI.OpenFrame.WriteCentered("Restoring Defender package");
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        RestoreDefender();

                        try
                        {
                            new Reg.Value()
                            {
                                KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce",
                                ValueName = "UninstallAME",
                                Type = Reg.RegistryValueType.REG_SZ,
                                Data = $"\"{Assembly.GetExecutingAssembly().Location}\" -Uninstall \"{_isoPath ?? _mountedPath.TrimEnd('\\')}\"",
                            }.Apply();
                        }
                        catch (Exception exception) { }
                    }
                    
                    Console.WriteLine();
                    if ((int?)ConsoleTUI.OpenFrame.Close("A restart is required to continue de-amelioration", ConsoleColor.Green, Console.BackgroundColor,
                            new ChoicePrompt()
                            {
                                TextForeground = ConsoleColor.Yellow,
                                Text = "Restart now? (Y/N): "
                            }) == 0) amecs.RestartWindows(false);

                    Environment.Exit(0);
                    return true;
                }
                catch (Exception e)
                {
                    if ((int?)ConsoleTUI.OpenFrame.Close("Failed to restore Microsoft Defender: " + e.Message, ConsoleColor.Yellow, Console.BackgroundColor,
                            new ChoicePrompt()
                            {
                                TextForeground = ConsoleColor.Yellow,
                                Text = "Continue de-amelioration anyways? (Y/N): "
                            }) != 0) return false;
                }
            }

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
                
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\AppFetch", false);
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Privacy+ Settings", false);
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\AME10 Settings", false);
                foreach (var userDir in Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users")))
                {
                    if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Privacy+ Settings.lnk")))
                        File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Privacy+ Settings.lnk"));
                    if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\AME10 Settings.lnk")))
                        File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\AME10 Settings.lnk"));
                }
                    
                if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\Privacy+ Settings.lnk")))
                    File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\Privacy+ Settings.lnk"));
                if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\AME10 Settings.lnk")))
                    File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\AME10 Settings.lnk"));
                
                foreach (var userDir in Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users")))
                {
                    if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\App Fetch Experimental.lnk")))
                        File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\App Fetch Experimental.lnk"));
                }

                if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\App Fetch Experimental.lnk")))
                    File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\App Fetch Experimental.lnk"));
                try
                {
                    if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\AME\appfetch.exe")))
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\AME\appfetch.exe"));
                }
                catch (Exception e)
                {
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
                TaskDefinition td = TaskService.Instance.NewTask();
                td.Principal.UserId = "SYSTEM";
                td.Principal.RunLevel = TaskRunLevel.Highest;

                td.Triggers.Add(new BootTrigger() { });

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
        private static string ExtractCab()
        {
            var cabArch = Win32.SystemInfoEx.SystemArchitecture == Architecture.Arm || Win32.SystemInfoEx.SystemArchitecture == Architecture.Arm64 ? "arm64" : "amd64";
            
            var fileDir = Environment.ExpandEnvironmentVariables("%ProgramData%\\AME");
            if (!Directory.Exists(fileDir)) Directory.CreateDirectory(fileDir);

            var destination = Path.Combine(fileDir, $"Z-AME-NoDefender-Package31bf3856ad364e35{cabArch}1.0.0.0.cab");
            
            if (File.Exists(destination))
            {
                return destination;
            }
            
            Assembly assembly = Assembly.GetEntryAssembly();
            using (UnmanagedMemoryStream stream = (UnmanagedMemoryStream)assembly!.GetManifestResourceStream($"amecs.Properties.Z-AME-NoDefender-Package31bf3856ad364e35{cabArch}1.0.0.0.cab"))
            {
                byte[] buffer = new byte[stream!.Length];
                stream.Read(buffer, 0, buffer.Length);
                File.WriteAllBytes(destination, buffer);
            }
            return destination;
        }

        private static bool RestoreDefender()
        {
            //string cabPath = null;

           // cabPath = ExtractCab();

            //var certPath = Path.GetTempFileName();

            int exitCode;

            /*
            exitCode = RunPSCommand(
                $"try {{" +
                $"$cert = (Get-AuthenticodeSignature '{cabPath}').SignerCertificate; " +
                $"[System.IO.File]::WriteAllBytes('{certPath}', $cert.Export([System.Security.Cryptography.X509Certificates.X509ContentType]::Cert)); " +
                $"Import-Certificate '{certPath}' -CertStoreLocation 'Cert:\\LocalMachine\\Root' | Out-Null; " +
                $"Copy-Item -Path \"HKLM:\\Software\\Microsoft\\SystemCertificates\\ROOT\\Certificates\\$($cert.Thumbprint)\" \"HKLM:\\Software\\Microsoft\\SystemCertificates\\ROOT\\Certificates\\8A334AA8052DD244A647306A76B8178FA215F344\" -Force | Out-Null; " +
                $"EXIT 0; " +
                $"}} catch {{EXIT 1}}", null, null);

            if (exitCode == 1)
                throw new Exception("Could not add certificate.");
*/
            var cabArch = Win32.SystemInfoEx.SystemArchitecture == Architecture.Arm || Win32.SystemInfoEx.SystemArchitecture == Architecture.Arm64 ? "arm64" : "amd64";
            string err = null;

            double lastDismProgress = 0;
            exitCode = RunCommand("DISM.exe", $"/Online /Remove-Package /PackageName:\"Z-AME-NoDefender-Package~31bf3856ad364e35~{cabArch}~~1.0.0.0\" /NoRestart",
                (sender, args) =>
                {
                    if (args.Data != null && args.Data.Contains("%"))
                    {
                        int i = args.Data.IndexOf('%') - 1;
                        while (args.Data[i] == '.' || Char.IsDigit(args.Data[i])) i--;
                        if (double.TryParse(args.Data.Substring(i + 1, args.Data.IndexOf('%') - i - 1), out double dismProgress))
                        {
                            lastDismProgress = dismProgress;
                        }
                    }
                },
                ((sender, args) =>
                {
                    if (err == null && args.Data != null)
                        err = args.Data;
                    else if (err != null && args.Data != null)
                        err = err + Environment.NewLine + args.Data;
                }));

            // 3010 = Restart required, -2146498555 = Package not found (In our case it may already be removed from a previous run)
            if (exitCode != 0 && exitCode != 3010 && exitCode != -2146498555)
            {
                /*
                exitCode = RunPSCommand(
                    $"$cert = (Get-AuthenticodeSignature '{cabPath}').SignerCertificate; " +
                    $"Get-ChildItem 'Cert:\\LocalMachine\\Root\\$($cert.Thumbprint)' | Remove-Item -Force | Out-Null; " +
                    $"Remove-Item \"HKLM:\\Software\\Microsoft\\SystemCertificates\\ROOT\\Certificates\\8A334AA8052DD244A647306A76B8178FA215F344\" -Force -Recurse | Out-Null"
                    , null, null);
*/
                throw new Exception("Could not apply package: " + exitCode);
            }

            if (exitCode == -2146498555)
                return false;

            return true;
/*
            exitCode = RunPSCommand(
                $"$cert = (Get-AuthenticodeSignature '{cabPath}').SignerCertificate; " +
                $"Get-ChildItem 'Cert:\\LocalMachine\\Root\\$($cert.Thumbprint)' | Remove-Item -Force | Out-Null; " +
                $"Remove-Item \"HKLM:\\Software\\Microsoft\\SystemCertificates\\ROOT\\Certificates\\8A334AA8052DD244A647306A76B8178FA215F344\" -Force -Recurse | Out-Null"
                , null, null);
*/
/*
            try
            {
                File.Delete(cabPath);
            }
            catch { }
*/
        }
        private static int RunPSCommand(string command, DataReceivedEventHandler outputHandler, DataReceivedEventHandler errorHandler) =>
            RunCommand("powershell.exe", $"-NoP -C \"{command}\"", outputHandler, errorHandler);
        private static int RunCommand(string exe, string arguments, [CanBeNull] DataReceivedEventHandler outputHandler, [CanBeNull] DataReceivedEventHandler errorHandler)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = exe,
                    Arguments = arguments,

                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = outputHandler != null,
                    RedirectStandardError = errorHandler != null
                }
            };

            if (outputHandler != null)
                process.OutputDataReceived += outputHandler;
            if (errorHandler != null)
                process.ErrorDataReceived += errorHandler;

            process.Start();
            
            if (outputHandler != null)
                process.BeginOutputReadLine();
            if (errorHandler != null)
                process.BeginErrorReadLine();

            process.WaitForExit();
            return process.ExitCode;
        }
        
        
        private static string GetUsername()
        {
            var currentSessionId = (uint)Process.GetCurrentProcess().SessionId;

            try
            {
                var sessionId = Win32.WTS.WTSGetActiveConsoleSessionId();
                if (sessionId != 0xFFFFFFFF)
                {
                    bool successState1 = Win32.WTS.WTSQuerySessionInformation(IntPtr.Zero, sessionId, Win32.WTS.WTS_INFO_CLASS.WTSConnectState,
                        out IntPtr bufferState1, out int returnedState1);

                    if (successState1 && Marshal.ReadInt32(bufferState1) == 0)
                    {
                        bool success = Win32.WTS.WTSQuerySessionInformation(IntPtr.Zero, sessionId, Win32.WTS.WTS_INFO_CLASS.WTSUserName,
                            out IntPtr buffer, out int returned);
                        if (success)
                            return Marshal.PtrToStringAnsi(buffer);
                    }
                }
            }
            catch (Exception e) { }
            

            IntPtr pSessionInfo = IntPtr.Zero;
            Int32 count = 0;
            if (Win32.WTS.WTSEnumerateSessions(IntPtr.Zero, 0, 1, ref pSessionInfo, ref count) == 0)
            {
                bool successState = Win32.WTS.WTSQuerySessionInformation(IntPtr.Zero, currentSessionId, Win32.WTS.WTS_INFO_CLASS.WTSConnectState,
                    out IntPtr bufferState, out int returnedState);

                if (successState && Marshal.ReadInt32(bufferState) == 0)
                {
                    bool success = Win32.WTS.WTSQuerySessionInformation(IntPtr.Zero, currentSessionId, Win32.WTS.WTS_INFO_CLASS.WTSUserName,
                        out IntPtr buffer, out int returned);
                    if (success)
                        return Marshal.PtrToStringAnsi(buffer);
                    else
                        throw new Exception("Couldn't query username.");
                }
                else
                    throw new Exception("Couldn't connect state.");
            }
            Int32 dataSize = Marshal.SizeOf(typeof(Win32.WTS.WTS_SESSION_INFO));
            Int64 current = (Int64)pSessionInfo;
            uint sessionIdResult = 0xFFFFFFFF;
            for (int i = 0; i < count; i++)
            {
                Win32.WTS.WTS_SESSION_INFO si =
                    (Win32.WTS.WTS_SESSION_INFO)Marshal.PtrToStructure((System.IntPtr)current,
                        typeof(Win32.WTS.WTS_SESSION_INFO));
                current += dataSize;
                if (si.State == Win32.WTS.WTS_CONNECTSTATE_CLASS.WTSActive)
                {
                    sessionIdResult = (uint)si.SessionID;
                    if (sessionIdResult == currentSessionId)
                        break;
                }
            }
            Win32.WTS.WTSFreeMemory(pSessionInfo);

            if (sessionIdResult == 0xFFFFFFFF)
                sessionIdResult = currentSessionId;
            
            bool successState2 = Win32.WTS.WTSQuerySessionInformation(IntPtr.Zero, sessionIdResult, Win32.WTS.WTS_INFO_CLASS.WTSConnectState,
                out IntPtr bufferState2, out int returnedState2);

            if (successState2 && Marshal.ReadInt32(bufferState2) == 0)
            {
                bool success = Win32.WTS.WTSQuerySessionInformation(IntPtr.Zero, sessionIdResult, Win32.WTS.WTS_INFO_CLASS.WTSUserName,
                    out IntPtr buffer, out int returned);
                if (success)
                    return Marshal.PtrToStringAnsi(buffer);
                else
                    throw new Exception("Couldn't query username.");
            }
            else
                throw new Exception("Couldn't connect state.");
        }
    }
}