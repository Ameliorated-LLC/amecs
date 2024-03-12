using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using amecs.Misc;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;

namespace amecs.Actions
{
    public class Deameliorate
    {
        private static string _mountedPath;
        private static string _winVer;
        
        public static bool DeAme()
        {
            if (new ChoicePrompt()
                {
                    Text = @"
This will de-ameliorate by reinstalling Windows.

Although user data should be kept, we strongly recommend
making backups of any important user data.

Continue? (Y/N): "
                }.Start().Value == 2) return true;
            
            Program.Frame.Clear();
            (_mountedPath, _, _winVer, _, _) = SelectWindowsImage.GetMediaPath();
            if (_mountedPath == null) return false;
            
            if (new ChoicePrompt {Text = $"\r\nYour Windows image is {_winVer}. Continue? (Y/N): "}.Start().Value == 2)
                return true;
            
            var fc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            string userSid = null;
            try
            {
                NSudo.GetSystemPrivilege();
                NSudo.RunAsUser(() =>
                {
                    userSid = WindowsIdentity.GetCurrent().User.ToString();
                });
            }
            catch
            {
                // do nothing
            }
            
            try
            {
                ConsoleTUI.OpenFrame.WriteCentered("\r\nUninstalling AME-installed UI software...");
                
                string openShellId = null;
                
                var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (var item in key.GetSubKeyNames())
                {
                    try
                    {
                        if (((string)key.OpenSubKey(item).GetValue("DisplayName")).Equals("Open-Shell"))
                        {
                            openShellId = item;
                        }
                    }
                    catch
                    {
                        // do nothing
                    }
                }

                if (openShellId != null)
                {
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nUninstalling Open-Shell...");

                    var proc = Process.Start("MsiExec.exe", $"/X{openShellId} /quiet");
                    proc.WaitForExit();

                    if (userSid != null)
                    {
                        var appData = (string)Registry.Users.OpenSubKey(userSid + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders").GetValue("AppData");
                        
                        if (Directory.Exists(Path.Combine(appData, "OpenShell")))
                            Directory.Delete(Path.Combine(appData, "OpenShell"), true);
                    }
                }
                
                var epSetupPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\ExplorerPatcher\ep_setup.exe";
                if (File.Exists(epSetupPath))
                {
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nUninstalling ExplorerPatcher, you'll need to click the prompts...");
                
                    var proc = Process.Start(epSetupPath, $"/uninstall");
                    proc.WaitForExit();
                }
                
                Program.Frame.Clear();
            } catch (Exception e)
            {
                ConsoleTUI.OpenFrame.Close(
                    "Error when uninstalling software: " + e.Message,
                    ConsoleColor.Red, Console.BackgroundColor,
                    new ChoicePrompt { AnyKey = true, Text = "Press any key to continue anyways: " });
                    
                Program.Frame.Clear();
                ConsoleTUI.OpenFrame.WriteCentered("\r\nContinuing without uninstalling software...\r\n");
            }
            
            // ExplorerPatcher re-opens Explorer as SYSTEM after uninstalling, so it has to be restarted
            // no silent uninstall is available, so user has to manually click 'Yes' to uninstall
            // https://github.com/valinet/ExplorerPatcher/discussions/2007
            Thread.Sleep(500);
            var winlogon = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            winlogon.SetValue("AutoRestartShell", 0);
            foreach (var exp in Process.GetProcessesByName("explorer"))
            {
                exp.Kill();
            }
            winlogon.SetValue("AutoRestartShell", 1);
            NSudo.RunProcessAsUser(NSudo.GetUserToken(), "explorer.exe", "", 0);
            
            // all policies are cleared as a user that's de-ameliorating is unlikely
            // to have their own policies in the first place
            ConsoleTUI.OpenFrame.WriteCentered("\r\nClearing policies...");
            foreach (var keyPath in new[] {
                $@"HKU\{userSid}\Software\Microsoft\Windows\CurrentVersion\Policies",
                $@"HKU\{userSid}\Software\Policies",
                @"HKLM\Software\Microsoft\Windows\CurrentVersion\Policies",
                @"HKLM\Software\Policies",
                @"HKLM\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Policies"
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

            Console.ReadKey();
            Program.Frame.Clear();
            ConsoleTUI.OpenFrame.WriteCentered("\r\nCompleted initial setup!", ConsoleColor.Green);
            ConsoleTUI.OpenFrame.WriteCentered("\r\nWindows Setup will display as 'Windows Server,' but it's not actually installing Windows Server and is only set as such to bypass hardware requirements.");
            
            Console.WriteLine();
            ConsoleTUI.OpenFrame.WriteCentered("\r\nWaiting 10 seconds and starting...");
            
            Console.ForegroundColor = fc;
            Thread.Sleep(10000);
            
            Console.WriteLine();
            try
            {
                Process.Start(Path.Combine(_mountedPath, "setup.exe"), "/Auto Upgrade /DynamicUpdate Disable /Product Server");
            } catch (Exception e)
            {
                ConsoleTUI.OpenFrame.Close(
                    $"There was an error when trying to run the Windows Setup: {e}\r\nTry running the Windows Setup manually from File Explorer.",
                    ConsoleColor.Red, Console.BackgroundColor,
                    new ChoicePrompt { AnyKey = true, Text = $"Press any key to exit: " });

                return false;
            }
            
            ConsoleTUI.OpenFrame.Close(
                "Completed, Windows Setup should have started.",
                ConsoleColor.Cyan, Console.BackgroundColor,
                new ChoicePrompt { AnyKey = true, Text = $"Press any key to go back: " });
            
            return true;
        }
    }
}