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
                    EscapeValue = null,
                    Choices =
                    {
                        new Menu.MenuItem("Change Username", new Func<bool>(UserPass.ChangeUsername)),
                        new Menu.MenuItem("Change Password", new Func<bool>(UserPass.ChangePassword)),
                        new Menu.MenuItem("Change Profile Picture", new Func<bool>(Profile.ChangeImage)),
                        autoLogonEnabled
                            ? new Menu.MenuItem("Disable AutoLogon", new Func<bool>(AutoLogon.Disable))
                            : new Menu.MenuItem("Enable AutoLogon", new Func<bool>(AutoLogon.Enable)),
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank, 
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
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