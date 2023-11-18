using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Misc.FolderPicker;
using Ameliorated.ConsoleUtils;
using Microsoft.Dism;

namespace amecs.Actions
{
    public class _NET
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
                ConsoleTUI.OpenFrame.Close("Security error: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return true;
            }

            return false;
        }
        
        private static bool DismountImage()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "PowerShell.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = $"-NoP -C \"Dismount-DiskImage '{file}'\"";
            startInfo.RedirectStandardOutput = true;

            var proc = Process.Start(startInfo);
            proc.WaitForExit();
            return true;
        }
        
        public static bool Install()
        {
            string letter;
            
            var choice = new ChoicePrompt() { Text = "A Windows Setup image must be provided to install .NET 3.5.\r\nWould you like to use a Windows Setup USB/folder instead of an ISO? (Y/N): " }.Start();
            if (choice == null) return false;
            var usingFolder = choice == 0;
            if (usingFolder)
            {
                var dlg = new FolderPicker();
                if (dlg.ShowDialog(default, false) == false) return false;
                if (CheckFileViolation(dlg.ResultPath)) return false;
                letter = dlg.ResultPath;
            }
            else
            {
                var dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.Filter = "ISO Files (*.ISO)| *.ISO"; // Filter files by extension
                dialog.Multiselect = false;
                dialog.InitialDirectory = Globals.UserFolder;

                NativeWindow window = new NativeWindow();
                window.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
                if (dialog.ShowDialog(window) == DialogResult.OK)
                {
                    if (CheckFileViolation(dialog.FileName)) return false;
                    
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nMounting ISO");
                    
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.CreateNoWindow = false;
                        startInfo.UseShellExecute = false;
                        startInfo.FileName = "PowerShell.exe";
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        startInfo.Arguments = $"-NoP -C \"(Mount-DiskImage '{file}' -PassThru | Get-Volume).DriveLetter + ':'\"";
                        startInfo.RedirectStandardOutput = true;

                        var proc = Process.Start(startInfo);
                        proc.WaitForExit();

                        letter = proc.StandardOutput.ReadLine();
                    }
                    
                    if (!Directory.Exists(letter + @"\sources\sxs") || !Directory.GetFiles(letter + @"\sources\sxs", "*netfx3*").Any())
                    {
                        try
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.CreateNoWindow = false;
                            startInfo.UseShellExecute = false;
                            startInfo.FileName = "PowerShell.exe";
                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.Arguments = $"-NoP -C \"Dismount-DiskImage '{file}'\"";
                            startInfo.RedirectStandardOutput = true;

                            var proc = Process.Start(startInfo);
                            proc.WaitForExit();
                        } catch (Exception e)
                        {
                        }

                        Console.WriteLine();
                        ConsoleTUI.OpenFrame.Close("ISO does not contain the required files.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("\r\nYou must select an ISO.", new ChoicePrompt() {AnyKey = true, Text = "Press any key to return to the Menu: "});
                    return true;
                }
            }

            ConsoleTUI.OpenFrame.WriteCentered("\r\nInstalling .NET 3.5");
            var topCache = Console.CursorTop;
            var leftCache = Console.CursorLeft;
            Console.WriteLine();
            bool inProgress = false;
            try
            {
                using (var indicator = new ConsoleUtils.LoadingIndicator())
                {
                    DismApi.Initialize(DismLogLevel.LogErrors);
                    using (var session = DismApi.OpenOnlineSession())
                    {
                        var stdout = GetStdHandle(-11);
                        bool indicatorStopped = false;
                        var maxHashTags = (ConsoleTUI.OpenFrame.DisplayWidth - 5);
                        DismApi.EnableFeatureByPackagePath(session, "NetFX3", null, true, true, new List<string>() { letter + @"\sources\sxs" }, delegate(DismProgress progress)
                        {
                            inProgress = true;
                            if (!indicatorStopped)
                            {
                                indicator.Stop();
                                Console.SetCursorPosition(leftCache, topCache);
                                Console.WriteLine("   ");
                            }

                            indicatorStopped = true;
                            var progressPerc = progress.Current / 10;
                            var currentHashTags = (int)Math.Ceiling(Math.Min(((double)progressPerc / 100) * maxHashTags, maxHashTags));
                            var spaces = maxHashTags - currentHashTags + (4 - progressPerc.ToString().Length);
                            var sb = new StringBuilder(new string('#', currentHashTags) + new string(' ', spaces) + progressPerc + "%");
                            uint throwaway;
                            WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new Languages.COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                            inProgress = false;
                        });
                        session.Close();
                        Thread.Sleep(100);
                        var sb = new StringBuilder(new string('#', maxHashTags) + " 100%");
                        uint throwaway;
                        WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new Languages.COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                    }

                    DismApi.Shutdown();
                    if (usingFolder) DismountImage();
                }
            } catch (Exception e)
            {
                while (inProgress)
                {
                    Thread.Sleep(50);
                }

                if (usingFolder)
                {
                    try
                    {
                        DismountImage();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("DISM error: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }
            
            Console.WriteLine();
            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close(".NET 3.5 installed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
            {
                AnyKey = true,
                Text = "Press any key to return to the Menu: "
            });
            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, StringBuilder lpCharacter, uint nLength, Languages.COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
    }
}