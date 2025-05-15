using System;
using System.IO;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;

namespace amecs.Actions.SystemActions
{
    public class SystemMenu
    {
        public static Task<bool> ShowMenu()
        {
            while (true)
            {
                Program.Frame.Clear();
                
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
                

                bool settingsHidden = !new Reg.Value()
                {
                    KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    ValueName = "SettingsPageVisibility",
                    Operation = Reg.RegistryValueOperation.Delete,
                    Type = Reg.RegistryValueType.REG_SZ
                }.IsEqual();
                

                bool usernameRequirement = new Reg.Value()
                {
                    KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                    ValueName = "dontdisplaylastusername",
                    Data = 1,
                }.IsEqual();

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

                bool uiModified = (File.Exists(Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"),
                    "Fluent-AME.skin7")) || File.Exists(Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"),
                    "Fluent-Metro.skin7"))) && File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\StartMenuDLL.dll"));


                var mainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    EscapeValue = null,
                    Choices =
                    {
                        Globals.PendingUserElevationChange ?? Globals.UserElevated
                            ? new Menu.MenuItem("Enable Enhanced Security", new Func<bool>(Elevation.DeElevate))
                            : new Menu.MenuItem("Disable Enhanced Security", new Func<bool>(Elevation.Elevate)),
                        !uiModified
                            ? new Menu.MenuItem("Enable UI Modifications", new Func<bool>(UIModifications.Enable))
                            : new Menu.MenuItem("Disable UI Modifications", new Func<bool>(UIModifications.Disable)),
                        usernameRequirement
                            ? new Menu.MenuItem("Disable Corporate Login",
                                new Func<bool>(UsernameRequirement.Disable))
                            : new Menu.MenuItem("Enable Corporate Login",
                                new Func<bool>(UsernameRequirement.Enable)),
                        hibernation ? 
                            new Menu.MenuItem("Disable Hibernation", new Func<bool>(Hibernation.DisableHibernation)) : 
                            new Menu.MenuItem("Enable Hibernation", new Func<bool>(Hibernation.EnableHibernation)),
                        settingsHidden ?
                            new Menu.MenuItem("Restore Hidden Settings Pages", new Func<bool>(Extra.RestoreSettingsPages)) : 
                            new Menu.MenuItem("Hide Restored Settings Pages", new Func<bool>(Extra.HideSettingsPages)),
                        notifications ? 
                            new Menu.MenuItem("Disable Desktop Notifications", new Func<bool>(Extra.DisableNotifications)) : 
                            new Menu.MenuItem("Enable Desktop Notifications", new Func<bool>(Extra.EnableNotifications)),
                        notificationCenter ? 
                            new Menu.MenuItem("Disable Notification Center", new Func<bool>(Extra.DisableNotifCen)) : 
                            new Menu.MenuItem("Enable Notification Center", new Func<bool>(Extra.EnableNotifCen)),
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Change Administrator Password", new Func<bool>(UserPass.ChangeAdminPassword)),
                        Globals.WinVer <= 19043 ?
                            new Menu.MenuItem("Create New User (Legacy)", new Func<bool>(CreateUser.CreateNewUserLegacy)) : 
                            new Menu.MenuItem("Create New User", new Func<bool>(CreateUser.CreateNewUser)), 
                        Menu.MenuItem.Blank,
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
    }
}