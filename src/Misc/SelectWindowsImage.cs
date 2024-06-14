using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Windows.Forms;
using Ameliorated.ConsoleUtils;
using Microsoft.Dism;

// Asks user to select Windows installation media, and mounts it if applicable
// Returns path to where it's mounted

namespace amecs.Misc
{
    public static class SelectWindowsImage
    {
        private static string _fileViolationTest;
        private static bool CheckFileViolation(string inputFile)
        {
            try
            {
                _fileViolationTest = inputFile;
            }
            catch (SecurityException e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Security exception: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return true;
            }

            return false;
        }
        
        public static string GetWindowsVersion(float majorMinor, int isoBuild)
        {
            return (majorMinor, isoBuild) switch
            {
                (6, _) => "Windows Vista",
                (6.1f, _) => "Windows 7",
                (6.2f, _) => "Windows 8",
                (6.3f, _) => "Windows 8.1",
                (10, var a) when a < 19041 => "Windows 10 (Old)",
                (10, var a) when a >= 22000 => "Windows 11",
                (10, _) => "Windows 10",
                _ => "Unknown"
            };
        }
        
        public static bool DismountIso(string imagePath)
        {
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "PowerShell.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-NoP -C \"Dismount-DiskImage '{imagePath}'\"",
                RedirectStandardOutput = true
            };

            var proc = Process.Start(startInfo);
            if (proc == null) return false;
            proc.WaitForExit();
            return true;
        }
        
        private static string _mountedPath;
        private static string _isoPath;
        private static string _isoWinVer;
        private static int _isoBuild;
        
        /// <summary>
        /// Asks user to select Windows installation media, mounts it if applicable, and checks its version
        /// </summary>
        /// <param name="winVersionsMustMatch">If true when ISO and host versions mismatch, prompts user that things can break if they continue</param>
        /// <param name="isoBuildMustBeReturned">If true and the ISO build can't be retrieved, prompts a user with an error</param>
        public static (
            string MountedPath, string IsoPath, string Winver, int? Build, bool? VersionsMatch
            ) GetMediaPath(bool winVersionsMustMatch = false, bool isoBuildMustBeReturned = false, bool iso = false, bool usb = false)
        {
            var error = ((string)null, "none", (string)null, (int?)null, (bool?)null);

            bool usingFolder = usb;
            if (iso == usb)
            {
                var choice =
                    new ChoicePrompt
                    {
                        Text =
                            "Windows installation media is required to restore files.\r\nTo select a Windows USB drive, press U\r\nTo select a Windows ISO file,  press I: ",
                        Choices = "UI"
                    }.Start();
                if (!choice.HasValue) return error;
                
                usingFolder = choice == 0;
            }

            // Folder/drive chosen

            if (usingFolder)
            {
                string value = null;
                var thread = new Thread(() =>
                {
                    var dlg = new FolderPicker
                    {
                        InputPath = Globals.UserFolder
                    };
                    if (dlg.ShowDialog(IntPtr.Zero).GetValueOrDefault())
                        value = dlg.ResultPath;
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();

                if (value != null)
                {
                    _mountedPath = value;
                }                     
                else
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("\r\nYou must select a folder or drive containing Windows installation media.",
                        new ChoicePrompt { AnyKey = true, Text = "Press any key to return to the Menu: " });
                    return error;
                }
            }
            else
            {
                // Mounting the ISO
                var dialog = new OpenFileDialog();
                dialog.Filter = "ISO Files|*.ISO";
                dialog.Multiselect = false;
                dialog.InitialDirectory = Globals.UserFolder;

                string value = null;
                var thread = new Thread(() =>
                {
                    var window = new NativeWindow();
                    window.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
                    if (dialog.ShowDialog(window) == DialogResult.OK)
                    {
                        value = dialog.FileName;
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                

                if (value != null)
                {
                    _isoPath = dialog.FileName;
                    if (CheckFileViolation(_isoPath)) return error;
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.WriteCentered("Mounting ISO");
                }
                else
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("\r\nYou must select an ISO.",
                        new ChoicePrompt { AnyKey = true, Text = "Press any key to return to the Menu: " });
                    return error;
                }

                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    var startInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = false,
                        UseShellExecute = false,
                        FileName = "PowerShell.exe",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = $"-NoP -C \"(Mount-DiskImage '{_isoPath}' -PassThru | Get-Volume).DriveLetter + ':\'\"",
                        RedirectStandardOutput = true
                    };

                    var proc = Process.Start(startInfo);
                    if (proc == null) return error;
                    proc.WaitForExit();

                    _mountedPath = proc.StandardOutput.ReadLine();
                }   
            }                        
            
            // Check WIM version
            var wimOrEsdPath = new[] { $@"{_mountedPath}\sources\install.esd", $@"{_mountedPath}\sources\install.wim" }.FirstOrDefault(File.Exists);
            if (!string.IsNullOrEmpty(wimOrEsdPath))
            {
                try
                {
                    DismApi.Initialize(DismLogLevel.LogErrors);
                    
                    string previousIndexVersion = null;
                    string isoFullVersion = null;
                    var multiVersion = false;
                    
                    var imageInfos = DismApi.GetImageInfo(wimOrEsdPath);
                    foreach (var imageInfo in imageInfos)
                    {
                        isoFullVersion = imageInfo.ProductVersion.ToString();
                        if (isoFullVersion != previousIndexVersion && previousIndexVersion != null)
                        {
                            // If it's multi-version, WinVer will be "Unknown" as well
                            multiVersion = true;
                            isoFullVersion = "0.0.0.0";
                            break;
                        }
                        previousIndexVersion = isoFullVersion;
                    }

                    switch (multiVersion)
                    {
                        case true when isoBuildMustBeReturned:
                            ConsoleTUI.OpenFrame.Close(
                                "Multiple Windows versions were found in the Windows image, can't determine which Windows build it is. Please use an unmodified Windows ISO.",
                                ConsoleColor.Red, Console.BackgroundColor,
                                new ChoicePrompt { AnyKey = true, Text = "Press any key to return to the Menu: " });
                            return error;
                        case true when winVersionsMustMatch:
                            ConsoleTUI.OpenFrame.Close(
                                "Multiple Windows versions were found in the Windows image, can't determine which Windows build it is. If your Windows version doesn't match the ISO, there will be problems.",
                                ConsoleColor.Red, Console.BackgroundColor,
                                new ChoicePrompt { AnyKey = true, Text = "Press any key to continue anyways: " });
                        
                            Program.Frame.Clear();
                            ConsoleTUI.OpenFrame.WriteCentered("\r\nContinuing without version check...\r\n");
                            break;
                    }

                    var buildSplit = isoFullVersion.Split('.');
                    _isoBuild = int.Parse(buildSplit[2]);
                    _isoWinVer = GetWindowsVersion(float.Parse($"{buildSplit[0]}.{buildSplit[1]}"), _isoBuild);
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close(
                        "Error checking ISO version: " + e.Message.TrimEnd('\n').TrimEnd('\r'),
                        ConsoleColor.Red, Console.BackgroundColor,
                        new ChoicePrompt { AnyKey = true, Text = "Press any key to return to the Menu: " });
                    return error;
                }
                finally
                {
                    try
                    {
                        DismApi.Shutdown();
                    }
                    catch
                    {
                        // do nothing
                    }
                }
                
                // Check the current OS version
                var hostVersion = Environment.OSVersion.Version;
                var hostWinver = GetWindowsVersion(float.Parse($"{hostVersion.Major}.{hostVersion.Minor}"), hostVersion.Build);
                
                // If it all matches & winVersionsMustMatch
                if (hostWinver == _isoWinVer) return (_mountedPath, _isoPath, _isoWinVer, _isoBuild, true);
                // If ISO version doesn't match host version & winVersionsMustMatch
                if (hostWinver != _isoWinVer && winVersionsMustMatch)
                {
                    if (!string.IsNullOrEmpty(_isoPath)) DismountIso(_isoPath);
                    ConsoleTUI.OpenFrame.Close(
                        $"You're on {hostWinver}, but the selected image is {_isoWinVer}. You can only use an ISO that matches your Windows version.",
                        ConsoleColor.Red, Console.BackgroundColor,
                        new ChoicePrompt { AnyKey = true, Text = "Press any key to return to the Menu: " });
                    return error;
                }
                
                // If ISO version doesn't match host version, and winVersionsMustMatch is true 
                if (hostWinver != _isoWinVer) return (_mountedPath, _isoPath, _isoWinVer, _isoBuild, false);
            }

            var noWimText = isoBuildMustBeReturned
                ? "Press any key to return to the Menu"
                : "Press any key to continue anyways";
            
            ConsoleTUI.OpenFrame.Close(
                "No Windows installation image was found inside the selected Windows media. No version check can be done, things might break.",
                ConsoleColor.Red, Console.BackgroundColor,
                new ChoicePrompt { AnyKey = true, Text = $"{noWimText}: " });
            
            Program.Frame.Clear();
            ConsoleTUI.OpenFrame.WriteCentered("\r\nContinuing without version check\r\n");
            
            return isoBuildMustBeReturned ? error : (_mountedPath, _isoPath, null, null, null);
        }
    }
}