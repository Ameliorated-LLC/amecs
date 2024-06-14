using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Ameliorated.ConsoleUtils;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace amecs.Actions
{
    public static class UserPass
    {
        public static Task<bool> ShowMenu()
        {
            var mainMenu = new Ameliorated.ConsoleUtils.Menu()
            {
                Choices =
                {
                    new Menu.MenuItem("Change Username", new Func<bool>(ChangeUsername)),
                    new Menu.MenuItem("Change Password", new Func<bool>(ChangePassword)),
                    new Menu.MenuItem("Change Display Name", new Func<bool>(ChangeDisplayName)),
                    new Menu.MenuItem("Change Administrator Password", new Func<bool>(ChangeAdminPassword)),
                    Menu.MenuItem.Blank,
                    new Menu.MenuItem("Return to Menu", new Func<bool>(() => true)),
                    new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit)),
                },
                SelectionForeground = ConsoleColor.Green
            };
            mainMenu.Write();
            var result = (Func<bool>)mainMenu.Load(true);
            return Task.FromResult(result.Invoke());
        }
        
        public static bool ChangeUsername()
        {
            try
            {
                while (true)
                {
                    var username = new InputPrompt() { Text = "Enter new username, or press escape to quit: " }.Start();
                    if (username == null)
                        return true;

                    if (String.IsNullOrEmpty(username) || !Regex.Match(username, @"^\w[\w\.\- ]{0,19}$").Success)
                    {
                        ConsoleTUI.OpenFrame.WriteLine("Username is invalid.");
                        Console.WriteLine();
                        continue;
                    }

                    if (Globals.Username.Equals(username))
                    {
                        ConsoleTUI.OpenFrame.WriteLine("Username matches the current username.");
                        Console.WriteLine();
                        continue;
                    }
                    
                    try
                    {
                        ConsoleTUI.OpenFrame.WriteCentered("\r\nSetting new username");
                        using (new ConsoleUtils.LoadingIndicator(true))
                        {
                            DirectoryEntry entry = (DirectoryEntry)Globals.User.GetUnderlyingObject();

                            entry.Rename(username);
                            entry.CommitChanges();

                            PrincipalContext context = new PrincipalContext(ContextType.Machine);

                            PrincipalSearcher userPrincipalSearcher = new PrincipalSearcher(new UserPrincipal(context));
                            Globals.User = userPrincipalSearcher.FindAll().FirstOrDefault(x => (x is UserPrincipal) && x.Sid.Value == Globals.UserSID) as UserPrincipal;
                            break;
                        }
                    } catch (System.Runtime.InteropServices.COMException e)
                    {
                        if (e.ErrorCode != -2147022694)
                            throw e;
                        ConsoleTUI.OpenFrame.WriteLine("Username is invalid.");
                        Console.WriteLine();
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }
            Console.WriteLine();
            if ((int?)ConsoleTUI.OpenFrame.Close("Username changed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
                {
                    TextForeground = ConsoleColor.Yellow,
                    Text = "Logoff to apply changes? (Y/N): "
                }) == 0) amecs.RestartWindows(true);
            return true;
        }

        public static bool ChangePassword()
        {
            try
            {
                while (true)
                {
                    var password = new InputPrompt() { MaskInput = true, Text = "Enter new password, or press escape to quit: " }.Start();
                    if (password == null)
                        return true;
                    
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nSetting new password");
                    try
                    {
                        using (new ConsoleUtils.LoadingIndicator(true))
                        {
                            if (String.IsNullOrEmpty(password))
                            {
                                Globals.User.SetPassword("");
                            }
                            else
                            {
                                Globals.User.SetPassword(password);
                            }
                            
                            Thread.Sleep(800);
                            break;
                        }
                    } catch (PasswordException e)
                    {
                        ConsoleTUI.OpenFrame.WriteLine("Could not set password: " + e.Message);
                        Console.WriteLine();
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }
            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("Password changed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }
        public static bool ChangeAdminPassword()
        {
            try
            {
                PrincipalContext context = new PrincipalContext(ContextType.Machine);
            
                PrincipalSearcher userPrincipalSearcher = new PrincipalSearcher(new UserPrincipal(context));
                var administrator = userPrincipalSearcher.FindAll().FirstOrDefault(x => (x is UserPrincipal) && x.Name == "Administrator") as UserPrincipal;
                
                while (true)
                {
                    var password = new InputPrompt() { MaskInput = true, Text = "Enter new Administrator password,\r\nor press escape to quit: " }.Start();
                    if (password == null)
                        return true;
                    
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nSetting new password");
                    try
                    {
                        using (new ConsoleUtils.LoadingIndicator(true))
                        {
                            if (String.IsNullOrEmpty(password))
                            {
                                administrator.SetPassword("");
                            }
                            else
                            {
                                administrator.SetPassword(password);
                            }
                            
                            Thread.Sleep(1000);
                            break;
                        }
                    } catch (PasswordException e)
                    {
                        ConsoleTUI.OpenFrame.WriteLine("Could not set password: " + e.Message);
                        Console.WriteLine();
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }
            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("Administrator password changed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }

        public static bool ChangeDisplayName()
        {
            try
            {
                var name = new InputPrompt() { Text = "Enter new display name, or press escape to quit: " }.Start();
                if (name == null)
                    return true;

                ConsoleTUI.OpenFrame.WriteCentered("\r\nSetting new display name");

                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    Globals.User.DisplayName = name;
                    Globals.User.Save();
                    
                    Thread.Sleep(800);
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return false;
            }
            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("Display name changed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
            return true;
        }
    }
}