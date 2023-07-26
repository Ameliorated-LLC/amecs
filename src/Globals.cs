using System;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;

namespace amecs
{
    public static class Globals
    {
        public static string Username = null;
        public static string UserDomain = null;
        public static string UserSID = null;
        public static string UserFolder = null;
        public static bool UserElevated = false;

        public static string UserHive
        {
            get
            {
                return "HKU\\" + UserSID;
            }
        }

        public static Task UserLoadTask = Task.CompletedTask;
        
        public static ConsoleUtils.LoadingIndicator CurrentIndicator = new ConsoleUtils.LoadingIndicator();

        public static GroupPrincipal Administrators;
        public static UserPrincipal User;

        public static bool Exit()
        {
            ConsoleTUI.Close();
            Environment.Exit(0);
            return true;
        }
        
        public static readonly int WinVer = Int32.Parse(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("CurrentBuildNumber").ToString());
    }
}