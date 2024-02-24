using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;
using amecs.Actions;
using Menu = Ameliorated.ConsoleUtils.Menu;

namespace amecs
{
    internal class Program
    {
        private static void RunAsUser(Action action)
        {
            var token = NSudo.GetUserToken();
            Task.Run((Action)Delegate.Combine((Action)(() => { NSudo.GetUserPrivilege(token); }),
                action)).Wait();
            Marshal.FreeHGlobal(token);
        }

        private static async Task RunAsUserAsync(Action action)
        {
            var token = NSudo.GetUserToken();
            await Task.Run((Action)Delegate.Combine((Action)(() => { NSudo.GetUserPrivilege(token); }),
                action));
            Marshal.FreeHGlobal(token);
        }


        private const string Ver = "2.1";
        public static ConsoleTUI.Frame Frame;
        [STAThread]
        
        public static void Main(string[] args)
        {
            ConsoleTUI.Initialize("Central AME Script");

            try
            {
                NSudo.GetSystemPrivilege();
                if (!WindowsIdentity.GetCurrent().IsSystem)
                    throw new Exception("Identity did not change.");
                RunAsUser(() =>
                {
                    Globals.Username = WindowsIdentity.GetCurrent().Name.Split('\\').Last();
                    Globals.UserDomain = WindowsIdentity.GetCurrent().Name.Split('\\').FirstOrDefault();
                    Globals.UserSID = WindowsIdentity.GetCurrent().User.ToString();
                });
                try
                {
                    Globals.UserFolder = Registry.Users.OpenSubKey(Globals.UserSID + "\\Volatile Environment").GetValue("USERPROFILE").ToString();
                } catch (Exception e)
                {
                    Console.WriteLine(Globals.Username);
                    
                    ConsoleTUI.ShowErrorBox($"Could not fetch user folder information from user with SID '{Globals.UserSID}': " + e, "Central AME Script");
                    Environment.Exit(1);
                }
                
                PrincipalContext context = new PrincipalContext(ContextType.Machine);

                PrincipalSearcher userPrincipalSearcher = new PrincipalSearcher(new UserPrincipal(context));
                Globals.User = userPrincipalSearcher.FindAll().FirstOrDefault(x => (x is UserPrincipal) && x.Sid.Value == Globals.UserSID) as UserPrincipal;

                PrincipalSearcher groupPrincipalSearcher = new PrincipalSearcher(new GroupPrincipal(context));
                Globals.Administrators = groupPrincipalSearcher.FindAll().FirstOrDefault(x => (x is GroupPrincipal) && x.Sid.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid)) as GroupPrincipal;
            } catch (Exception e)
            {
                ConsoleTUI.ShowErrorBox("Could not acquire System privileges: " + e, "Central AME Script");
                Environment.Exit(1);
            }

            Frame = new ConsoleTUI.Frame($"| Central AME Script v{Ver} |", false);

            Frame.Open();

            while (true)
            {
                Globals.UserElevated = Globals.User.IsMemberOf(Globals.Administrators);
                
                Frame.Clear();
                bool usernameRequirement = new Reg.Value()
                {
                    KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                    ValueName = "dontdisplaylastusername",
                    Data = 1,
                }.IsEqual();
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
                bool netInstalled = new Reg.Key()
                {
                    KeyName = @"HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5",
                    Operation = RegistryOperation.Add
                }.IsEqual();

                var mainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    Choices =
                    {
                        new Menu.MenuItem("Change Username or Password", new Func<bool>(UserPass.ShowMenu)),
                        new Menu.MenuItem("Change Lockscreen Image", new Func<bool>(Lockscreen.ChangeImage)),
                        new Menu.MenuItem("Change Profile Image", new Func<bool>(Profile.ChangeImage)),

                        Globals.UserElevated ? 
                            new Menu.MenuItem( "De-elevate User", new Func<bool>(Elevation.DeElevate)) : 
                            new Menu.MenuItem("Elevate User to Administrator", new Func<bool>(Elevation.Elevate)),
                        usernameRequirement ? 
                            new Menu.MenuItem( "Disable Username Login Requirement", new Func<bool>(UsernameRequirement.Disable)) : 
                            new Menu.MenuItem("Enable Username Login Requirement", new Func<bool>(UsernameRequirement.Enable)),
                        autoLogonEnabled ?
                            new Menu.MenuItem( "Disable AutoLogon", new Func<bool>(AutoLogon.Disable)) : 
                            new Menu.MenuItem("Enable AutoLogon", new Func<bool>(AutoLogon.Enable)),
                        new Menu.MenuItem("Manage Language Settings", new Func<bool>(Languages.ShowMenu)),
                        new Menu.MenuItem("Manage Users", new Func<bool>(Users.ShowMenu)),
                        !netInstalled ?
                            new Menu.MenuItem("Install .NET 3.5", new Func<bool>(_NET.Install)) :
                            new Menu.MenuItem("Install .NET 3.5", new Func<bool>(_NET.Install)) {SecondaryText = "[Installed]", SecondaryTextForeground = ConsoleColor.Yellow, PrimaryTextForeground = ConsoleColor.DarkGray},
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Extra", new Func<bool>(Extra.Extra.ShowMenu)),
                        new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit))
                    },
                    SelectionForeground = ConsoleColor.Green
                };

                Func<bool> result;
                try
                {
                    mainMenu.Write();
                    result = (Func<bool>)mainMenu.Load();
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                    return;
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