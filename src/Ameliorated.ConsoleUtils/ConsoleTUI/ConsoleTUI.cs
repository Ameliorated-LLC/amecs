using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using amecs;

namespace Ameliorated.ConsoleUtils
{
    public static partial class ConsoleTUI
    {
        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000; //resize

        private const uint CHECK_QUICK_EDIT = 0x0040;
        private const int ENABLE_QUICK_EDIT = 0x40 | 0x80;

        // STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
        private const int STD_INPUT_HANDLE = -10;
        private static string PreviousTitle;
        private static int PreviousBufferHeight = 26;
        private static int PreviousBufferWidth = 80;
        private static int PreviousSizeHeight = 26;
        private static int PreviousSizeWidth = 80;

        private static bool IsInitialized;
        private static int InitializedWidth = 80;

        public static void ShowErrorBox(string message, string caption)
        {
            NativeWindow window = new NativeWindow();
            window.AssignHandle(Process.GetCurrentProcess().MainWindowHandle);
            MessageBox.Show(window, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public enum BackdropType
        {
            None = 1,
            Mica = 2,
            Acrylic = 3,
            Tabbed = 4
        }
        
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int pvAttribute, int cbAttribute);

        public static void Initialize(string title, int width = 80, int height = 26, bool resize = false, bool quickedit = false)
        {
            if (width < 2) throw new ArgumentException("Width must be greater than one.");

            IsInitialized = true;
            PreviousSizeHeight = Console.WindowHeight;
            PreviousSizeWidth = Console.WindowWidth;
            PreviousBufferHeight = Console.BufferHeight;
            PreviousBufferWidth = Console.BufferWidth;

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.SetWindowSize(width, height);

            InitializedWidth = width;

            Console.Clear();
            
            Console.CursorVisible = false;

            PreviousTitle = Console.Title;
            Console.Title = title;

            try
            {
                if ((Console.CursorLeft == 0 && Console.CursorTop == 0) || ParentProcess.ProcessName.Equals("Explorer", StringComparison.OrdinalIgnoreCase))
                {
                    var bd = (int)BackdropType.Mica;
                    var trueA = 0x01;

                    if (Globals.WinVer >= 22523)
                    {
                        var handle = Process.GetCurrentProcess().MainWindowHandle;
                        DwmSetWindowAttribute(handle, 38, ref bd, Marshal.SizeOf<int>());
                        DwmSetWindowAttribute(handle, 20, ref trueA, Marshal.SizeOf<int>());
                    }
                }
            } catch (Exception e) { }
            
            if (!resize)
                try
                {
                    DisableResize();
                } catch (Exception e)
                {
                    ConsoleUtils.WriteError("Error disabling window resize - " + e.Message);
                }

            if (!quickedit)
                try
                {
                    DisableQuickEdit();
                } catch (Exception e)
                {
                    ConsoleUtils.WriteError("Error disabling quickedit - " + e.Message);
                }
        }

        public static void Close()
        {
            if (!IsInitialized) throw new MethodAccessException("Console TUI must be initialized before calling other TUI functions.");
            IsInitialized = false;

            var parent = ParentProcess.ProcessName;
            if (parent.Equals("Explorer", StringComparison.CurrentCultureIgnoreCase)) return;

            try
            {
                EnableResize();
            } catch (Exception e)
            {
                ConsoleUtils.WriteError("Error enabling window resize - " + e.Message);
            }

            try
            {
                EnableQuickEdit();
            } catch (Exception e)
            {
                ConsoleUtils.WriteError("Error enabling quickedit - " + e.Message);
            }

            Console.CursorVisible = true;
            Console.Clear();
            Console.Title = PreviousTitle;

            Console.SetWindowSize(PreviousSizeWidth, PreviousSizeHeight);
            Console.SetBufferSize(PreviousBufferWidth, PreviousBufferHeight);
        }

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        private static void DisableResize()
        {
            var handle = GetConsoleWindow();
            var sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND); //resize
            }
        }

        private static void EnableResize()
        {
            var handle = GetConsoleWindow();
            GetSystemMenu(handle, true);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        private static void DisableQuickEdit()
        {
            var consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // get current console mode
            uint consoleMode;
            GetConsoleMode(consoleHandle, out consoleMode);

            // set the new mode
            SetConsoleMode(consoleHandle, consoleMode &= ~CHECK_QUICK_EDIT);
        }

        private static void EnableQuickEdit()
        {
            var consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            // get current console mode
            uint consoleMode;
            GetConsoleMode(consoleHandle, out consoleMode);

            // set the new mode
            SetConsoleMode(consoleHandle, consoleMode | ENABLE_QUICK_EDIT);
        }
    }
}