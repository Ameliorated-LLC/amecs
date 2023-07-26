using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;
using System.Windows.Forms;

namespace amecs.Actions
{
    public class Lockscreen
    {
        public static bool ChangeImage()
        {
            ConsoleTUI.OpenFrame.WriteCenteredLine("Select an image");
            
            Thread.Sleep(1000);
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp; *.jfif)| *.jpg; *.jpeg; *.png; *.bmp; *.jfif"; // Filter files by extension
            dialog.Multiselect = false;
            dialog.InitialDirectory = Globals.UserFolder;

            NativeWindow window = new NativeWindow();
            window.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
            if (dialog.ShowDialog(window) == DialogResult.OK)
            {
                string file;
                try
                {
                    file = dialog.FileName;
                }
                catch (SecurityException e)
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("Security error: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                    return false;
                }

                Console.WriteLine();
                var choice = new ChoicePrompt() { Text = "Remove lockscreen blur? (Y/N): " }.Start();
                if (!choice.HasValue) return true;
                bool blur = choice == 0;

                ConsoleTUI.OpenFrame.WriteCentered("\r\nSetting lockscreen image");

                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    Thread.Sleep(500);
                    
                    try
                    {
                        if (blur)
                            new Reg.Value()
                            {
                                KeyName = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\System",
                                ValueName = "DisableAcrylicBackgroundOnLogon",
                                Type = Reg.RegistryValueType.REG_DWORD,
                                Data = 1,
                            }.Apply();
                        else
                            new Reg.Value()
                            {
                                KeyName = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\System",
                                ValueName = "DisableAcrylicBackgroundOnLogon",
                                Type = Reg.RegistryValueType.REG_DWORD,
                                Data = 0,
                            }.Apply();
                    } catch { }
                    
                    new Reg.Value()
                    {
                        KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Creative\" + Globals.UserSID,
                        ValueName = "RotatingLockScreenEnabled",
                        Type = Reg.RegistryValueType.REG_DWORD,
                        Data = 0,
                    }.Apply();
                    new Reg.Value()
                    {
                        KeyName = @$"HKU\{Globals.UserSID}\SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                        ValueName = "RotatingLockScreenEnabled",
                        Type = Reg.RegistryValueType.REG_DWORD,
                        Data = 0,
                    }.Apply();

                    File.Delete(Environment.ExpandEnvironmentVariables(@"%WINDIR%\Web\Screen\img100.jpg"));
                    File.Copy(file, Environment.ExpandEnvironmentVariables(@"%WINDIR%\Web\Screen\img100.jpg"));

                    foreach (var dataDir in Directory.EnumerateDirectories(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\Microsoft\Windows\SystemData")))
                    {
                        
                        
                        var subDir = Path.Combine(dataDir, "ReadOnly");
                        if (!Directory.Exists(subDir))
                            continue;
                        Directory.GetDirectories(subDir, "Lockscreen_*").ToList().ForEach(x => Directory.Delete(x, true));
                    }

                }
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Lockscreen image changed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return true;
            }
            else
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("You must select an image.", new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                return true;
            }
        }
    }
}