using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using Ameliorated.ConsoleUtils;
using System.Drawing;
using System.Security.AccessControl;
using System.Security.Principal;

namespace amecs.Actions
{
    public class Profile
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
                
                ConsoleTUI.OpenFrame.WriteCentered("\r\nSetting profile image");

                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    var pfpDir = Path.Combine(Environment.ExpandEnvironmentVariables("%PUBLIC%\\AccountPictures"), Globals.UserSID);

                    if (Directory.Exists(pfpDir))
                    {
                        try
                        {
                            Directory.Delete(pfpDir, true);
                        } catch (Exception e)
                        {
                            var logdi = new DirectoryInfo(pfpDir) { Attributes = FileAttributes.Normal };
                            try
                            {
                                NSudo.GetOwnershipPrivilege();

                                var logdirsec = logdi.GetAccessControl();
                                logdirsec.SetOwner(WindowsIdentity.GetCurrent().User);

                                logdi.SetAccessControl(logdirsec);
                            
                                logdirsec = new DirectorySecurity();
                                logdirsec.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().User, FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                                logdi.SetAccessControl(logdirsec);

                            } catch (Exception exception)
                            {

                            }
                            foreach (var info in logdi.GetFileSystemInfos("*", SearchOption.AllDirectories))
                            {
                                info.Attributes = FileAttributes.Normal;
                            }
                            
                            Directory.Delete(pfpDir, true);
                        }
                    }
                    Directory.CreateDirectory(pfpDir);

                    var image = Image.FromFile(file);
                    var pfpKey = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\AccountPicture\Users\" + Globals.UserSID;
                    new Reg.Key() { KeyName = pfpKey, Operation = RegistryOperation.Delete }.Apply();

                    foreach (var res in new [] { 32, 40, 48, 64, 96, 192, 208, 240, 424, 448, 1080 })
                    {
                        var bitmap = new Bitmap(res, res);
                        var graph = Graphics.FromImage(bitmap);
                        graph.DrawImage(image, 0, 0, res, res);

                        var saveLoc = Path.Combine(pfpDir, $"{res}x{res}.png");
                        bitmap.Save(saveLoc);

                        new Reg.Value() { KeyName = pfpKey, ValueName = "Image" + res, Type = Reg.RegistryValueType.REG_SZ, Data = saveLoc }.Apply();
                    }
                    new Reg.Value() { KeyName = pfpKey, ValueName = "UserPicturePath", Type = Reg.RegistryValueType.REG_SZ, Data = Path.Combine(pfpDir, $"448x448.png") }.Apply();

                    try
                    {
                        Process proc = new Process();
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proc.StartInfo.FileName = "gpupdate.exe";
                        proc.StartInfo.Arguments = "/force";
                        proc.Start();
                        proc.WaitForExit(20000);
                    } catch { }

                }
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Profile image changed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
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