using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using amecs.Misc;
using Ameliorated.ConsoleUtils;
using Microsoft.Dism;

namespace amecs.Actions
{
    public class _NET
    {
        private static string _mountedPath;
        private static string _isoPath;

        public static Task<bool> ShowMenu()
        {
            var mainMenu = new Ameliorated.ConsoleUtils.Menu()
            {
                Choices =
                {
                    new Menu.MenuItem("Install .NET 3.5 using a Windows USB", new Func<bool>(InstallUSB)),
                    new Menu.MenuItem("Install .NET 3.5 using a Windows ISO", new Func<bool>(InstallISO)),
                    Menu.MenuItem.Blank,
                    new Menu.MenuItem("Return to Menu", new Func<bool>(() => true)),
                    new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit)),
                },
                SelectionForeground = ConsoleColor.Green
            };
            mainMenu.Write("Windows install media is required to install .NET 3.5");
            var result = (Func<bool>)mainMenu.Load(true);
            return Task.FromResult(result.Invoke());
        }
        
        private static void Unmount()
        {
            if (_isoPath == "none")
                return;
            
            SelectWindowsImage.DismountIso(_isoPath);
        }
        public static bool InstallUSB() => InstallCore(true, false);
        public static bool InstallISO() => InstallCore(false, true);
        
        public static bool InstallCore(bool usb, bool iso)
        {
            if (usb && !iso)
                ConsoleTUI.OpenFrame.WriteCenteredLine("Select Windows USB drive");
            if (iso && !usb)
                ConsoleTUI.OpenFrame.WriteCenteredLine("Select Windows ISO file");

            (_mountedPath, _isoPath, _, _, _) = SelectWindowsImage.GetMediaPath(true, usb: usb, iso: iso);
            if (_mountedPath == null) return false;

            if (!Directory.Exists(_mountedPath + @"\sources\sxs") || !Directory.GetFiles(_mountedPath + @"\sources\sxs", "*netfx3*").Any())
            {
                Unmount();
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("ISO/USB/folder does not contain the required files.",
                    ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt() { AnyKey = true, Text = "Press any key to return to the Menu: " });
                return false;
            }
            
            ConsoleTUI.OpenFrame.WriteCentered("\r\nInstalling .NET 3.5");
            var topCache = Console.CursorTop;
            var leftCache = Console.CursorLeft;
            Console.WriteLine();
            var inProgress = false;
            try
            {
                using var indicator = new ConsoleUtils.LoadingIndicator();
                DismApi.Initialize(DismLogLevel.LogErrors);
                using (var session = DismApi.OpenOnlineSession())
                {
                    var stdout = GetStdHandle(-11);
                    var indicatorStopped = false;
                    var maxHashTags = (ConsoleTUI.OpenFrame.DisplayWidth - 5);
                    DismApi.EnableFeatureByPackagePath(session, "NetFX3", null, true, true, new List<string>() { _mountedPath + @"\sources\sxs" }, delegate(DismProgress progress)
                    {
                        inProgress = true;
                        if (!indicatorStopped)
                        {
                            indicator.Stop();
                            Console.SetCursorPosition(leftCache, topCache);
                            Console.WriteLine("   ");
                        }

                        indicatorStopped = true;
                        var progressPercentage = progress.Current / 10;
                        var currentHashTags = (int)Math.Ceiling(Math.Min(((double)progressPercentage / 100) * maxHashTags, maxHashTags));
                        var spaces = maxHashTags - currentHashTags + (4 - progressPercentage.ToString().Length);
                        var sb = new StringBuilder(new string('#', currentHashTags) + new string(' ', spaces) + progressPercentage + "%");
                        WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new Languages.COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out _);
                        inProgress = false;
                    });
                    session.Close();
                    Thread.Sleep(100);
                    var sb = new StringBuilder(new string('#', maxHashTags) + " 100%");
                    uint throwaway;
                    WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new Languages.COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                }

                DismApi.Shutdown();
                Unmount();
            } catch (Exception e)
            {
                while (inProgress)
                {
                    Thread.Sleep(50);
                }
                
                Unmount();

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