using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;

namespace amecs
{
    public class amecs
    {
        public static Task<bool> RunBasicActionTask(string status, string result, Action action, bool logoff = false, bool restart = false) => Task.FromResult(RunBasicAction(status, result, action, logoff, restart));
        public static bool RunBasicAction(string status, string result, Action action, bool logoff = false, bool restart = false)
        {
            ConsoleTUI.OpenFrame.WriteCentered(status);

            try
            {
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    action.Invoke();
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return false;
            }
            Console.WriteLine();
            if (logoff)
            {
                if ((int?)ConsoleTUI.OpenFrame.Close(result, ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
                    {
                        TextForeground = ConsoleColor.Yellow,
                        Text = "Logoff to apply changes? (Y/N): "
                    }) == 0) amecs.RestartWindows(true);
                return true;
            }
            if (restart)
            {
                if ((int?)ConsoleTUI.OpenFrame.Close(result, ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
                    {
                        TextForeground = ConsoleColor.Yellow,
                        Text = "Restart to apply changes? (Y/N): "
                    }) == 0) amecs.RestartWindows(false);
                return true;
            }

            ConsoleTUI.OpenFrame.Close(result, ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }

        public static bool RunBasicAction(string status, string result, Action<int> action, ChoicePrompt prompt = null, bool logoff = false, bool restart = false)
        {
            int choice = -1;
            if (prompt != null)
            {
                var choiceRes = prompt.Start();
                if (!choiceRes.HasValue)
                    return true;
                choice = choiceRes.Value;
            }

            ConsoleTUI.OpenFrame.WriteCentered(status);

            try
            {
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    action.Invoke(choice);
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return false;
            }
            Console.WriteLine();
            if (logoff)
            {
                if ((int?)ConsoleTUI.OpenFrame.Close(result, ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
                    {
                        TextForeground = ConsoleColor.Yellow,
                        Text = "Logoff to apply changes? (Y/N): "
                    }) == 0) amecs.RestartWindows(true);
                return true;
            }
            if (restart)
            {
                if ((int?)ConsoleTUI.OpenFrame.Close(result, ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
                    {
                        TextForeground = ConsoleColor.Yellow,
                        Text = "Restart to apply changes? (Y/N): "
                    }) == 0) amecs.RestartWindows(false);
                return true;
            }

            ConsoleTUI.OpenFrame.Close(result, ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }
        
        [Flags]
        enum ExitWindows : uint
        {
            // ONE of the following five:
            LogOff = 0x00,
            ShutDown = 0x01,
            Reboot = 0x02,
            PowerOff = 0x08,
            RestartApps = 0x40,
            // plus AT MOST ONE of the following two:
            Force = 0x04,
            ForceIfHung = 0x10,
        }
        [Flags]
        enum ShutdownReason : uint
        {
            MajorApplication = 0x00040000,
            MajorHardware = 0x00010000,
            MajorLegacyApi = 0x00070000,
            MajorOperatingSystem = 0x00020000,
            MajorOther = 0x00000000,
            MajorPower = 0x00060000,
            MajorSoftware = 0x00030000,
            MajorSystem = 0x00050000,

            MinorBlueScreen = 0x0000000F,
            MinorCordUnplugged = 0x0000000b,
            MinorDisk = 0x00000007,
            MinorEnvironment = 0x0000000c,
            MinorHardwareDriver = 0x0000000d,
            MinorHotfix = 0x00000011,
            MinorHung = 0x00000005,
            MinorInstallation = 0x00000002,
            MinorMaintenance = 0x00000001,
            MinorMMC = 0x00000019,
            MinorNetworkConnectivity = 0x00000014,
            MinorNetworkCard = 0x00000009,
            MinorOther = 0x00000000,
            MinorOtherDriver = 0x0000000e,
            MinorPowerSupply = 0x0000000a,
            MinorProcessor = 0x00000008,
            MinorReconfig = 0x00000004,
            MinorSecurity = 0x00000013,
            MinorSecurityFix = 0x00000012,
            MinorSecurityFixUninstall = 0x00000018,
            MinorServicePack = 0x00000010,
            MinorServicePackUninstall = 0x00000016,
            MinorTermSrv = 0x00000020,
            MinorUnstable = 0x00000006,
            MinorUpgrade = 0x00000003,
            MinorWMI = 0x00000015,

            FlagUserDefined = 0x40000000,
            FlagPlanned = 0x80000000
        }
        
        [DllImport("user32.dll")]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool InitiateSystemShutdownEx(
            string lpMachineName,
            string lpMessage,
            uint dwTimeout,
            bool bForceAppsClosed,
            bool bRebootAfterShutdown,
            ShutdownReason dwReason);

        public static void RestartWindows(bool logoff, bool cmd = false)
        {
            if (cmd)
            {
                var psi = new ProcessStartInfo("shutdown","/r /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
                
                Environment.Exit(0);
            }
            
            if (!logoff)
            {
                NSudo.GetShutdownPrivilege();
                InitiateSystemShutdownEx(null, null, 0, false, true, ShutdownReason.MinorInstallation);
            } else
                ExitWindowsEx((uint)ExitWindows.LogOff | (uint)ExitWindows.ForceIfHung, (uint)ShutdownReason.MinorInstallation);

            Environment.Exit(0);
        }

        public static object ShowDefaultMenu(List<Menu.MenuItem> list)
        {
            list.AddRange(new [] {             
                Menu.MenuItem.BlankStatic,
                new Menu.MenuItem("Next Page", null) { IsNextButton = true },
                new Menu.MenuItem("Previous Page", null) { IsPreviousButton = true },
                new Menu.MenuItem("Return to Menu", new Func<bool>(() => true)) { IsStatic = true },
                new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit)) { IsStatic = true },
            });
            
            var mainMenu = new Menu()
            {
                Choices = list,
                SelectionForeground = ConsoleColor.Green
            };
            mainMenu.Write();
            return mainMenu.Load(true);
        }


        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetCheckConnection(string lpszUrl, int dwFlags, int dwReserved);

        [DllImport("wininet.dll", SetLastError=true)]
        private extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        public static bool IsInternetAvailable()
        {
            try
            {
                try
                {
                    if (!InternetCheckConnection("http://archlinux.org", 1, 0))
                    {
                        if (!InternetCheckConnection("http://google.com", 1, 0))
                            return false;
                    }
                    return true;
                }
                catch
                {
                    var request = (HttpWebRequest)WebRequest.Create("http://google.com");
                    request.KeepAlive = false;
                    request.Timeout = 5000;
                    using (var response = (HttpWebResponse)request.GetResponse())
                        return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}