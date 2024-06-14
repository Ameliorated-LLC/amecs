using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;

namespace amecs.Actions
{
    public class Users
    {
        public static Task<bool> ShowMenu()
        {
            while (true)
            {
                Program.Frame.Clear();
                
                bool autoLogonEnabled = new Reg.Value()
                                        {
                                            KeyName = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon",
                                            ValueName = "DefaultUsername",
                                            Data = Globals.Username,
                                        }.IsEqual() &&
                                        new Reg.Value()
                                        {
                                            KeyName = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon",
                                            ValueName = "AutoAdminLogon",
                                            Data = "1",
                                        }.IsEqual();

                var mainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    Choices =
                    {
                        new Menu.MenuItem("Change Username", new Func<bool>(UserPass.ChangeUsername)),
                        new Menu.MenuItem("Change Password", new Func<bool>(UserPass.ChangePassword)),
                        new Menu.MenuItem("Change Display Name", new Func<bool>(UserPass.ChangeDisplayName)),
                        new Menu.MenuItem("Change Profile Image", new Func<bool>(Profile.ChangeImage)),
                        autoLogonEnabled
                            ? new Menu.MenuItem("Disable AutoLogon", new Func<bool>(AutoLogon.Disable))
                            : new Menu.MenuItem("Enable AutoLogon", new Func<bool>(AutoLogon.Enable)),
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Change Administrator Password", new Func<bool>(UserPass.ChangeAdminPassword)),
                        Globals.WinVer <= 19043 ?
                            new Menu.MenuItem("Create New User (Legacy)", new Func<bool>(CreateNewUserLegacy)) : 
                            new Menu.MenuItem("Create New User", new Func<bool>(CreateNewUser)),
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
                    var res = mainMenu.Load(true);
                    if (res == null)
                        return Task.FromResult(true);
                    result = (Func<bool>)res;
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                    return Task.FromResult(false);
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

        private static bool CreateNewUserLegacy()
        {
            var choice = new ChoicePrompt() { Text = "WARNING: This is a deprecated action, and the new user\r\nwill not be fully functional. Continue? (Y/N): ", TextForeground = ConsoleColor.Yellow}.Start();
            if (choice == null || choice == 1)
                return false;
            
            ConsoleTUI.OpenFrame.WriteLine();
            try
            {
                if (CreateUser() == false)
                    return true;

            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }
            
            ConsoleTUI.OpenFrame.WriteCentered("\r\nConfiguring user");

            try
            {
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    try { ServiceController.GetServices().First(x => x.ServiceName.Equals("AppReadiness")).Stop(); } catch (Exception e) { }

                    NSudo.GetOwnershipPrivilege();
                    Reg.AcquirePrivileges();
                    var subKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppReadiness\Parameters", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.TakeOwnership);
                    if (subKey != null)
                    {
                        var sec = subKey.GetAccessControl();
            
                        sec.SetOwner(new NTAccount("SYSTEM"));

                        subKey.SetAccessControl(sec);
                        subKey.Close();
                        
                        subKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\AppReadiness\Parameters", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions);
                        sec = subKey.GetAccessControl();
                        
                        sec.AddAccessRule(new RegistryAccessRule("SYSTEM", RegistryRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            
                        subKey.SetAccessControl(sec);
                    }
                    new Reg.Key() { KeyName = @"HKLM\SYSTEM\CurrentControlSet\Services\AppReadiness" }.Apply();
                    
                    new Reg.Value() { KeyName = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\OOBE", ValueName = "DisablePrivacyExperience", Data = 1, Type = Reg.RegistryValueType.REG_DWORD}.Apply();
                    new Reg.Value() { KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", ValueName = "EnableFirstLogonAnimation", Data = 0, Type = Reg.RegistryValueType.REG_DWORD}.Apply();
                    
                    Reg.LoadDefaultUserHive();
                    try
                    {
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell", ValueName = "", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\OpenShell", ValueName = "", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\OpenShell\Settings", ValueName = "", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu", ValueName = "", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\ClassicExplorer", ValueName = "", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\ClassicExplorer\Settings", ValueName = "", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\ClassicExplorer", ValueName = "ShowedToolbar", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\ClassicExplorer", ValueName = "NewLine", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\ClassicExplorer\Settings", ValueName = "ShowStatusBar", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu", ValueName = "ShowedStyle2", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu", ValueName = "CSettingsDlg", Data = "c80100001a0100000000000000000000360d00000100000000000000", Type = Reg.RegistryValueType.REG_BINARY }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu", ValueName = "OldItems", Data = "00", Type = Reg.RegistryValueType.REG_BINARY }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu", ValueName = "ItemRanks", Data = "00", Type = Reg.RegistryValueType.REG_BINARY }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\MRU", ValueName = "0", Data = @"%SYSTEMDRIVE%\Windows\regedit.exe", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "Version", Data = 04040098, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "AllProgramsMetro", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "RecentMetroApps", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "StartScreenShortcut", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "SearchInternet", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "GlassOverride", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "GlassColor", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "SkinW7", Data = "Fluent-Metro", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "SkinVariationW7", Data = "", Type = Reg.RegistryValueType.REG_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "SkipMetro", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "MenuItems7", Data = @"Item1.Command=user_files\0Item1.Settings=NOEXPAND\0Item2.Command=user_documents\0Item2.Settings=NOEXPAND\0Item3.Command=user_pictures\0Item3.Settings=NOEXPAND\0Item4.Command=user_music\0Item4.Settings=NOEXPAND\0Item5.Command=user_videos\0Item5.Settings=NOEXPAND\0Item6.Command=downloads\0Item6.Settings=NOEXPAND\0Item7.Command=homegroup\0Item7.Settings=ITEM_DISABLED\0Item8.Command=separator\0Item9.Command=games\0Item9.Settings=TRACK_RECENT|NOEXPAND|ITEM_DISABLED\0Item10.Command=favorites\0Item10.Settings=ITEM_DISABLED\0Item11.Command=recent_documents\0Item12.Command=computer\0Item12.Settings=NOEXPAND\0Item13.Command=network\0Item13.Settings=ITEM_DISABLED\0Item14.Command=network_connections\0Item14.Settings=ITEM_DISABLED\0Item15.Command=separator\0Item16.Command=control_panel\0Item16.Settings=TRACK_RECENT\0Item17.Command=pc_settings\0Item17.Settings=TRACK_RECENT\0Item18.Command=admin\0Item18.Settings=TRACK_RECENT|ITEM_DISABLED\0Item19.Command=devices\0Item19.Settings=ITEM_DISABLED\0Item20.Command=defaults\0Item20.Settings=ITEM_DISABLED\0Item21.Command=help\0Item21.Settings=ITEM_DISABLED\0Item22.Command=run\0Item23.Command=apps\0Item23.Settings=ITEM_DISABLED\0Item24.Command=windows_security\0Item24.Settings=ITEM_DISABLED\0", Type = Reg.RegistryValueType.REG_MULTI_SZ }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\OpenShell\StartMenu\Settings", ValueName = "SkinOptionsW7", Data = @"DARK_MAIN=0\0METRO_MAIN=0\0LIGHT_MAIN=0\0AUTOMODE_MAIN=1\0DARK_SUBMENU=0\0METRO_SUBMENU=0\0LIGHT_SUBMENU=0\0AUTOMODE_SUBMENU=1\0SUBMENU_SEPARATORS=1\0DARK_SEARCH=0\0METRO_SEARCH=0\0LIGHT_SEARCH=0\0AUTOMODE_SEARCH=1\0SEARCH_FRAME=1\0SEARCH_COLOR=0\0MODERN_SEARCH=1\0SEARCH_ITALICS=0\0NONE=0\0SEPARATOR=0\0TWO_TONE=1\0CLASSIC_SELECTOR=1\0HALF_SELECTOR=0\0CURVED_MENUSEL=1\0CURVED_SUBMENU=0\0SELECTOR_REVEAL=1\0TRANSPARENT=0\0OPAQUE_SUBMENU=1\0OPAQUE_MENU=0\0OPAQUE=0\0STANDARD=0\0SMALL_MAIN2=1\0SMALL_ICONS=0\0COMPACT_SUBMENU=0\0PRESERVE_MAIN2=0\0LESS_PADDING=0\0EXTRA_PADDING=1\024_PADDING=0\0LARGE_PROGRAMS=0\0TRANSPARENT_SHUTDOWN=0\0OUTLINE_SHUTDOWN=0\0BUTTON_SHUTDOWN=1\0EXPERIMENTAL_SHUTDOWN=0\0LARGE_FONT=0\0CONNECTED_BORDER=1\0FLOATING_BORDER=0\0LARGE_SUBMENU=0\0LARGE_LISTS=0\0THIN_MAIN2=0\0EXPERIMENTAL_MAIN2=1\0USER_IMAGE=1\0USER_OUTSIDE=0\0SCALING_USER=1\056=0\064=0\0TRANSPARENT_USER=0\0UWP_SCROLLBAR=0\0MODERN_SCROLLBAR=1\0SMALL_ARROWS=0\0ARROW_BACKGROUND=1\0ICON_FRAME=0\0SEARCH_SEPARATOR=0\0NO_PROGRAMS_BUTTON=0\0", Type = Reg.RegistryValueType.REG_MULTI_SZ }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", ValueName = "SearchboxTaskbarMode", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", ValueName = "ShowTaskViewButton", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", ValueName = "EnableAutoTray", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo", ValueName = "Enabled", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", ValueName = "EnableWebContentEvaluation", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\Control Panel\International\User Profile", ValueName = "HttpAcceptLanguageOptOut", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Policies\Microsoft\Windows\Explorer", ValueName = "DisableNotificationCenter", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\GameDVR", ValueName = "AppCaptureEnabled", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\System\GameConfigStore", ValueName = "GameDVR_Enabled", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments", ValueName = "SaveZoneInformation", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost", ValueName = "ContentEvaluation", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\Control Panel\Desktop", ValueName = "WaitToKillAppTimeOut", Data = "2000", Type = Reg.RegistryValueType.REG_SZ }.Apply();

                        new Reg.Key() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\StorageSense" }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", ValueName = "BingSearchEnabled", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", ValueName = "CortanaConsent", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", ValueName = "CortanaInAmbientMode", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", ValueName = "HistoryViewEnabled", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", ValueName = "HasAboveLockTips", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Search", ValueName = "AllowSearchToUseLocation", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\SearchSettings", ValueName = "SafeSearchMode", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Policies\Microsoft\Windows\Explorer", ValueName = "DisableSearchBoxSuggestions", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\InputPersonalization", ValueName = "RestrictImplicitTextCollection", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\InputPersonalization", ValueName = "RestrictImplicitInkCollection", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\InputPersonalization\TrainedDataStore", ValueName = "AcceptedPrivacyPolicy", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\InputPersonalization\TrainedDataStore", ValueName = "HarvestContacts", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Personalization\Settings", ValueName = "AcceptedPrivacyPolicy", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Policies\Microsoft\Windows\Explorer", ValueName = "DisableSearchBoxSuggestions", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();

                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", ValueName = "NavPaneShowAllFolders", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", ValueName = "LaunchTo", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", ValueName = "HideFileExt", Data = 0, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                        new Reg.Value() { KeyName = @"HKU\DefaultUserHive\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", ValueName = "Hidden", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
                    } catch (Exception e)
                    {
                        Reg.UnloadDefaultUserHive();
                        throw e;
                    }
                    
                    Reg.UnloadDefaultUserHive();
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }

            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("User created successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }
        
        private static bool CreateNewUser()
        {
            try
            {
                if (CreateUser() == false)
                    return true;

            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }
            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("User created successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }

        private static bool CreateUser()
        {
            var choice = new ChoicePrompt() { Text = "Make user an Administrator? (Y/N): " }.Start();
            if (choice == null)
                return false;

            bool makeAdmin = choice == 0;
            
            Console.WriteLine();
            while (true)
            {
                var username = new InputPrompt() { Text = "Enter name of new user, or press\r\nescape to quit: " }.Start();
                if (username == null)
                    return false;

                if (String.IsNullOrEmpty(username) || !Regex.Match(username, @"^\w[\w\.\- ]{0,19}$").Success)
                {
                    ConsoleTUI.OpenFrame.WriteLine("Username is invalid.");
                    Console.WriteLine();
                    continue;
                }

                if (Globals.Username.Equals(username))
                {
                    ConsoleTUI.OpenFrame.WriteLine("Specified user already exists.");
                    Console.WriteLine();
                    continue;
                }
                
                var password = new InputPrompt() { MaskInput = true, Text = "\r\nEnter password for new user, or press\r\nescape to quit: " }.Start();
                if (password == null)
                    return false;
                
                var passwordConfirmation = new InputPrompt() { MaskInput = true, Text = "\r\nRe-enter your password, or press\r\nescape to quit: " }.Start();
                if (passwordConfirmation == null)
                    return false;
                
                if (password != passwordConfirmation)
                {
                    ConsoleTUI.OpenFrame.WriteLine("The password re-entered does not match your original password.");
                    Console.WriteLine();
                    continue;
                }

                try
                {
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nCreating user");
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        DirectoryEntry AD = new DirectoryEntry("WinNT://" +  
                                                               Environment.MachineName + ",computer");  
                        DirectoryEntry NewUser = AD.Children.Add(username, "user");  
                        NewUser.Invoke("SetPassword", new object[] { password });  
                        NewUser.Invoke("Put", new object[] { "Description", "Normal User" });  
                        NewUser.CommitChanges();

                        var ugrp = AD.Children.Find("Users", "group");  
                        ugrp.Invoke("Add", new object[] { NewUser.Path.ToString() });
                        ugrp.CommitChanges();

                        if (makeAdmin)
                        {
                            var agrp = AD.Children.Find("Administrators", "group");
                            agrp.Invoke("Add", new object[] { NewUser.Path.ToString() });
                            agrp.CommitChanges();
                        }
                    }
                } catch (System.Runtime.InteropServices.COMException e)
                {
                    if (e.ErrorCode != -2147022694)
                        throw e;
                    ConsoleTUI.OpenFrame.WriteLine("Username is invalid.");
                    Console.WriteLine();
                }
                return true;
            }
        }
    }
}