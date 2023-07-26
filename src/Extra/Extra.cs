using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;
using amecs.Actions;
using Ameliorated.ConsoleUtils;

namespace amecs.Extra
{
    public static partial class Extra
    {
        public static bool ShowMenu()
        {
            while (true)
            {
                Program.Frame.Clear();
                
                bool hibernation = new Reg.Value()
                                   {
                                       KeyName = @"HKLM\SYSTEM\CurrentControlSet\Control\Power",
                                       ValueName = "HibernateEnabled",
                                       Data = 1,
                                   }.IsEqual()
                                   &&
                                   new Reg.Value()
                                   {
                                       KeyName = @"HKLM\SYSTEM\CurrentControlSet\Control\Power",
                                       ValueName = "HiberFileType",
                                       Data = 2,
                                   }.IsEqual();
                
                bool notifications = new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications",
                    ValueName = "ToastEnabled",
                    Data = 1,
                }.IsEqual();
                
                bool notificationCenter = !new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Policies\Microsoft\Windows\Explorer",
                    ValueName = "DisableNotificationCenter",
                    Data = 1,
                }.IsEqual();
                
                bool vbsEnabled = new Reg.Value()
                {
                    KeyName = @"HKCR\.vbs",
                    ValueName = "",
                    Data = "VBSFile",
                    Type = Reg.RegistryValueType.REG_SZ
                }.IsEqual();
                
                bool ncsiEnabled = new Reg.Value()
                {
                    KeyName = @"HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet",
                    ValueName = "EnableActiveProbing",
                    Data = 1,
                }.IsEqual();

                var mainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    Choices =
                    {
                        new Menu.MenuItem("Manage WSL", null) {IsEnabled = false, SecondaryText = "[Not Supported]", PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red},
                        hibernation ? 
                            new Menu.MenuItem("Disable Hibernation", new Func<bool>(DisableHibernation)) : 
                            new Menu.MenuItem("Enable Hibernation", new Func<bool>(EnableHibernation)),
                        notificationCenter ? 
                            new Menu.MenuItem("Disable Notification Center", new Func<bool>(DisableNotifCen)) : 
                            new Menu.MenuItem("Enable Notification Center", new Func<bool>(EnableNotifCen)),
                        notifications ? 
                            new Menu.MenuItem("Disable Desktop Notifications", new Func<bool>(DisableNotifications)) : 
                            new Menu.MenuItem("Enable Desktop Notifications", new Func<bool>(EnableNotifications)),
                        
                        GetWSHItem(),
                        vbsEnabled ? 
                            new Menu.MenuItem("Disable Visual Basic Script [VBS] (Legacy)", new Func<bool>(DisableVBS)) : 
                            new Menu.MenuItem("Enable Visual Basic Script [VBS] (Legacy)", new Func<bool>(EnableVBS)),
                        ncsiEnabled ? 
                            new Menu.MenuItem("Disable NCSI Active Probing (Legacy)", new Func<bool>(DisableNCSI)) : 
                            new Menu.MenuItem("Enable NCSI Active Probing (Legacy)", new Func<bool>(EnableNCSI)),
                        
                        GetNVCPItem(),
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Return to Menu", null),
                        new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit))
                    },
                    SelectionForeground = ConsoleColor.Green
                };
                Func<bool> result;
                try
                {
                    mainMenu.Write();
                    var res = mainMenu.Load();
                    if (res == null)
                        return true;
                    result = (Func<bool>)res;
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                    return false;
                }

                try
                {
                    result.Invoke();
                } catch (Exception e)
                {
                    ConsoleTUI.ShowErrorBox("Error while running an action: " + e.ToString(), null);
                }
            }
        }

        private static Menu.MenuItem GetNVCPItem()
        {
            if (File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\NVIDIA Control Panel\nvcplui.exe")))
                return new Menu.MenuItem("Uninstall NVIDIA Control Panel", new Func<bool>(NVCP.Uninstall));
            
            if (Globals.WinVer > 19043)
                return new Menu.MenuItem("Install NVIDIA Control Panel", null) {IsEnabled = false, PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red, SecondaryText = "[Not Supported]"};

            try
            {

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT NAME FROM Win32_VideoController");
                    
                bool foundGPU = false;
                foreach (ManagementObject mo in searcher.Get())
                {
                    PropertyData Name = mo.Properties["Name"];
                    if (Name.Value != null)
                    {
                        var name = ((string)Name.Value);
                        if (name.Contains("NVIDIA") || name.Contains("GeForce") || name.Contains("GTX") || name.Contains("RTX"))
                        {
                            foundGPU = true;
                            break;
                        }
                    }
                }

                if (!foundGPU)
                {
                    return new Menu.MenuItem("Install NVIDIA Control Panel", null) {IsEnabled = false, PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red, SecondaryText = "[No NVIDIA GPU]"};
                }
            } catch {}

            try
            {
                if (!ServiceController.GetServices().Any(x => x.ServiceName.Equals("NVDisplay.ContainerLocalSystem")))
                    return new Menu.MenuItem("Install NVIDIA Control Panel", null) {IsEnabled = false, PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red, SecondaryText = "[No NVIDIA Driver]"};
            } catch { }
            
            try
            {
                var dir = Directory.EnumerateDirectories(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\WindowsApps")).First(x => x.Contains("NVIDIACorp.NVIDIAControlPanel"));
                if (File.Exists(Path.Combine(dir, "nvcplui.exe")))
                    return new Menu.MenuItem("Install NVIDIA Control Panel", new Func<bool>(() => NVCP.Install(dir)));
            } catch { }
            
            if (!amecs.IsInternetAvailable())
                return new Menu.MenuItem("Install NVIDIA Control Panel", null) {IsEnabled = false, PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red, SecondaryText = "[Internet Required]"};
            
            if (!amecs.InternetCheckConnection("https://store.rg-adguard.net", 1, 0))
                return new Menu.MenuItem("Install NVIDIA Control Panel", null) {IsEnabled = false, PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red, SecondaryText = "[Server Unavailable]"};
                
            if (!amecs.InternetCheckConnection("https://git.ameliorated.info/", 1, 0))
                return new Menu.MenuItem("Install NVIDIA Control Panel", null) {IsEnabled = false, PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red, SecondaryText = "[Git Unavailable]"};
            
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync("https://store.rg-adguard.net/").Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            string result = content.ReadAsStringAsync().Result;
                            if (result.Contains("Cloudflare Ray ID"))
                                return new Menu.MenuItem("Install NVIDIA Control Panel", null) {IsEnabled = false, PrimaryTextForeground = ConsoleColor.DarkGray, SecondaryTextForeground = ConsoleColor.Red, SecondaryText = "[Server Unavailable]"};
                        }
                    }
                }
            } catch { }
            
            return new Menu.MenuItem("Install NVIDIA Control Panel", new Func<bool>(NVCP.InstallFromNetwork));
        }

        private static Menu.MenuItem GetWSHItem()
        {
            if (new Reg.Value()
                {
                    KeyName = Globals.UserHive + @"\SOFTWARE\Microsoft\Windows Script Host\Settings",
                    ValueName = "Enabled",
                    Data = 1,
                }.IsEqual())
                return new Menu.MenuItem("Disable Windows Script Host [WSH] (Legacy)", new Func<bool>(WSH.Disable));
            if (new Reg.Value()
                {
                    KeyName = Globals.UserHive + @"\SOFTWARE\Microsoft\Windows Script Host\Settings",
                    ValueName = "Enabled",
                    Data = 0,
                }.IsEqual())
                return new Menu.MenuItem("Enable Windows Script Host [WSH] (Legacy)", new Func<bool>(WSH.Enable));
            
            return new Reg.Value()
            {
                KeyName = @"HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings",
                ValueName = "Enabled",
                Data = 0,
            }.IsEqual() ? new Menu.MenuItem("Disable Windows Script Host [WSH] (Legacy)", new Func<bool>(WSH.Enable)) : new Menu.MenuItem("Disable Windows Script Host [WSH] (Legacy)", new Func<bool>(WSH.Disable));
        }

        private static bool EnableHibernation() =>amecs.RunBasicAction("Enabling hibernation","Enabled hibernation successfully",() => 
        { 
            Thread.Sleep(1600); 
            
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = "powercfg.exe";
            proc.StartInfo.Arguments = "/HIBERNATE /TYPE FULL";
            proc.Start();
            proc.WaitForExit(20000);

            if (proc.ExitCode != 0)
                throw new Exception("powercfg exited with a non-zero exitcode.\r\nHibernation may not be supported by your hardware.");
        });

        private static bool DisableHibernation() =>amecs.RunBasicAction("Disabling hibernation","Disabled hibernation successfully",() => 
        { 
            Thread.Sleep(1600); 
            
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = "powercfg.exe";
            proc.StartInfo.Arguments = "/HIBERNATE OFF";
            proc.Start();
            proc.WaitForExit(20000);

            if (proc.ExitCode != 0)
                throw new Exception("powercfg exited with a non-zero exitcode.");
            Thread.Sleep(1600); 
         });

        private static bool EnableNotifCen() =>amecs.RunBasicAction("Enabling Notification Center","Notification Center enabled successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Policies\Microsoft\Windows\Explorer",
                    ValueName = "DisableNotificationCenter",
                    Operation = Reg.RegistryValueOperation.Delete
                }.Apply();
            Thread.Sleep(1600); 
         }, true);

        private static bool DisableNotifCen() =>amecs.RunBasicAction("Disabling Notification Center","Notification Center disabled successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Policies\Microsoft\Windows\Explorer",
                    ValueName = "DisableNotificationCenter",
                    Data = 1,
                }.Apply();
            Thread.Sleep(1600); 
         }, true);

        private static bool EnableNotifications() =>amecs.RunBasicAction("Enabling desktop notifications","Enabled desktop notifications successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications",
                    ValueName = "ToastEnabled",
                    Data = 1,
                }.Apply();
            Thread.Sleep(1600); 
         }, true);

        private static bool DisableNotifications() =>amecs.RunBasicAction("Disabling desktop notifications","Disabled desktop notifications successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications",
                    ValueName = "ToastEnabled",
                    Data = 0,
                }.Apply();
            Thread.Sleep(1600); 
         }, true);

        private static bool EnableVBS() =>amecs.RunBasicAction("Enabling Visual Basic Script","Enabled VBS successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = @"HKCR\.vbs",
                    ValueName = "",
                    Data = "VBSFile",
                    Type = Reg.RegistryValueType.REG_SZ
                }.Apply();
            Thread.Sleep(1600); 
         });

        private static bool DisableVBS() =>amecs.RunBasicAction("Disabling Visual Basic Script","Disabled VBS successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = @"HKCR\.vbs",
                    ValueName = "",
                    Data = "",
                    Type = Reg.RegistryValueType.REG_SZ
                }.Apply();
            Thread.Sleep(1600); 
         });

        private static bool EnableNCSI() =>amecs.RunBasicAction("Enabling NCSI Active Probing","Enabled NCSI Active Probing successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = @"HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet",
                    ValueName = "EnableActiveProbing",
                    Data = 1,
                }.Apply();
            Thread.Sleep(1600); 
         }, false, true);

        private static bool DisableNCSI() =>amecs.RunBasicAction("Disabling NCSI Active Probing","Disabled NCSI Active Probing successfully",() => 
        { 
            new Reg.Value()
                {
                    KeyName = @"HKLM\SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet",
                    ValueName = "EnableActiveProbing",
                    Data = 0,
                }.Apply();
            Thread.Sleep(1600); 
         }, false, true);
    }
}