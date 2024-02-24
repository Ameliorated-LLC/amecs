using System;
using System.Diagnostics;
using System.Security;
using System.Windows.Forms;
using Ameliorated.ConsoleUtils;

// Asks user to select Windows installation media, and mounts it if applicable
// Returns path to where it's mounted

namespace amecs.Misc
{
    /// <summary>
    /// Asks user to select Windows installation media, and mounts it if applicable
    /// Returns path to where it's mounted
    /// </summary>
    public class SelectWindowsImage
    {
        private static string file;
        private static bool CheckFileViolation(string inputFile)
        {
            try
            {
                file = inputFile;
            }
            catch (SecurityException e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Security exception: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return true;
            }

            return false;
        }
        
        public static bool DismountIso(string path)
        {
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "PowerShell.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = $"-NoP -C \"Dismount-DiskImage '{path}'\"",
                RedirectStandardOutput = true
            };

            var proc = Process.Start(startInfo);
            if (proc == null) return false;
            proc.WaitForExit();
            return true;
        }
        
        public static (string MountedPath, string IsoPath) GetMediaPath()
        {
            var choice =
                new ChoicePrompt() { Text = "To continue, Windows installation media is needed.\r\nDo you have a Windows USB instead of an ISO file? (Y/N): " }.Start();
            if (choice == null) return (null, "none");
            
            // Folder/drive chosen
            var usingFolder = choice == 0;
            if (usingFolder)
            {
                var dlg = new FolderPicker
                {
                    InputPath = Globals.UserFolder
                };
                
                if (dlg.ShowDialog(IntPtr.Zero, false).GetValueOrDefault())
                    return CheckFileViolation(dlg.ResultPath) ? (null, "none") : (dlg.ResultPath, "none");
                
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("\r\nYou must select a folder or drive containing Windows installation media.",
                    new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return (null, "none");
            }

            // Mounting the ISO
            var dialog = new OpenFileDialog();
            dialog.Filter = "ISO Files (*.ISO)| *.ISO";
            dialog.Multiselect = false;
            dialog.InitialDirectory = Globals.UserFolder;

            NativeWindow window = new NativeWindow();
            window.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
            if (dialog.ShowDialog(window) == DialogResult.OK)
            {
                if (CheckFileViolation(dialog.FileName)) return (null, "none");
                Console.WriteLine();
                ConsoleTUI.OpenFrame.WriteCentered("\r\nMounting ISO");
            }
            else
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("\r\nYou must select an ISO.",
                    new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return (null, "none");
            }

            using (new ConsoleUtils.LoadingIndicator(true))
            {
                var startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    FileName = "PowerShell.exe",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = $"-NoP -C \"(Mount-DiskImage '{file}' -PassThru | Get-Volume).DriveLetter + ':\'\"",
                    RedirectStandardOutput = true
                };

                var proc = Process.Start(startInfo);
                if (proc == null) return (null, "none");
                proc.WaitForExit();
                
                return (proc.StandardOutput.ReadLine(), file);
            }
        }
    }
}