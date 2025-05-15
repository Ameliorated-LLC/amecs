using System;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;
using amecs.Actions;
using Microsoft.Win32.TaskScheduler;
using TrustedUninstaller.Shared;
using Menu = Ameliorated.ConsoleUtils.Menu;
using Task = System.Threading.Tasks.Task;

namespace amecs
{
    internal class Program
    {
        public const string Ver = "2.5";
        public static ConsoleTUI.Frame Frame;

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Boolean ChangeServiceConfig(
            IntPtr hService,
            UInt32 nServiceType,
            UInt32 nStartType,
            UInt32 nErrorControl,
            String lpBinaryPathName,
            String lpLoadOrderGroup,
            IntPtr lpdwTagId,
            [In] char[] lpDependencies,
            String lpServiceStartName,
            String lpPassword,
            String lpDisplayName);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr OpenService(
            IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode,
            SetLastError = true)]
        public static extern IntPtr OpenSCManager(
            string machineName, string databaseName, uint dwAccess);

        [DllImport("advapi32.dll", EntryPoint = "CloseServiceHandle")]
        public static extern int CloseServiceHandle(IntPtr hSCObject);

        private const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;
        private const uint SERVICE_QUERY_CONFIG = 0x00000001;
        private const uint SERVICE_CHANGE_CONFIG = 0x00000002;
        private const uint SC_MANAGER_ALL_ACCESS = 0x000F003F;

        public static void ChangeStartMode(ServiceController svc, ServiceStartMode mode)
        {
            var scManagerHandle = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
            if (scManagerHandle == IntPtr.Zero)
            {
                throw new ExternalException("Open Service Manager Error");
            }

            var serviceHandle = OpenService(
                scManagerHandle,
                svc.ServiceName,
                SERVICE_QUERY_CONFIG | SERVICE_CHANGE_CONFIG);

            if (serviceHandle == IntPtr.Zero)
            {
                throw new ExternalException("Open Service Error");
            }

            var result = ChangeServiceConfig(
                serviceHandle,
                SERVICE_NO_CHANGE,
                (uint)mode,
                SERVICE_NO_CHANGE,
                null,
                null,
                IntPtr.Zero,
                null,
                null,
                null,
                null);

            if (result == false)
            {
                var nError = Marshal.GetLastWin32Error();
                var win32Exception = new Win32Exception(nError);
                throw new ExternalException("Could not change service start type: "
                                            + win32Exception.Message);
            }

            CloseServiceHandle(serviceHandle);
            CloseServiceHandle(scManagerHandle);
        }

        private static void ConfigureCulture()
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = "."; // Force use . instead of ,
            culture.DateTimeFormat.Calendar = new GregorianCalendar();

            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
        }

        private static string PendingUpdate = null;
        private static readonly SemaphoreSlim MainMenuLock = new SemaphoreSlim(0);
        private static Menu CurrentMainMenu = null;

        [STAThread]
        public static async Task Main(string[] args)
        {
            if (args.Length > 1 && args[0] == "--update")
            {
                try
                {

                    int i = 0;
                    Process process;
                    while ((process = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(args[1])).FirstOrDefault(x => x.Id != Process.GetCurrentProcess().Id)) != null)
                    {
                        Thread.Sleep(100);

                        if (i > 10)
                        {
                            try
                            {
                                process.Kill();
                            }
                            catch (Exception e) { }
                        }

                        if (i > 20)
                        {
                            ConsoleTUI.ShowErrorBox("Update timed out.", null);
                            Environment.Exit(0);
                        }

                        i++;
                    }
                }
                catch (Exception e) { }

                File.Copy(Win32.ProcessEx.GetCurrentProcessFileLocation(), args[1], true);
                Process.Start(new ProcessStartInfo(args[1], "--updated")
                {
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                });

                Environment.Exit(0);
            }

            if (args.Length > 1 && args[0] == "--updated")
            {
                try
                {

                    int i = 0;
                    Process process;
                    while ((process = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(args[1])).FirstOrDefault(x => x.Id != Process.GetCurrentProcess().Id)) != null)
                    {
                        Thread.Sleep(100);

                        if (i > 10)
                        {
                            try
                            {
                                process.Kill();
                            }
                            catch (Exception e) { }
                        }

                        if (i > 20)
                        {
                            break;
                        }

                        i++;
                    }

                    File.Delete(args[1]);
                }
                catch (Exception e) { }
            }

            ConfigureCulture();

            bool win11 = Win32.SystemInfoEx.WindowsVersion.MajorVersion >= 11;
            ConsoleTUI.Initialize($"{(win11 ? "Privacy+" : "AME10")} Settings");

            if (args.Length > 0 && args[0] == "-Updated")
            {
                int i = 0;
                while (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location)).Length > 1)
                {
                    Thread.Sleep(100);

                    if (i > 20)
                    {
                        ConsoleTUI.ShowErrorBox("Update timed out.", null);
                        Environment.Exit(0);
                    }

                    i++;
                }

                if (File.Exists(Assembly.GetExecutingAssembly().Location.Replace(".exe", ".bak")))
                {
                    try
                    {
                        File.Delete(Assembly.GetExecutingAssembly().Location.Replace(".exe", ".bak"));
                    }
                    catch { }
                }
            }

            string ame11Ver = null;
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\AME\Playbooks\Applied\{9010E718-4B54-443F-8354-D893CD50FDDE}"))
                ame11Ver = key?.GetValue("Version")?.ToString();
            string ame10Ver = null;
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\AME\Playbooks\Applied\{513722d2-ce95-4d2a-a88a-53570642bc4e}"))
                ame10Ver = key?.GetValue("Version")?.ToString();

            if (ame10Ver == null && ame11Ver == null)
            {
                try
                {
                    var playbooks = Playbook.GetAppliedPlaybooks();
                    ame11Ver = playbooks.FirstOrDefault(x => (x.Name == "AME 11" && x.Username == "Ameliorated") || x.UniqueId == Guid.Parse("{9010E718-4B54-443F-8354-D893CD50FDDE}"))?.Version.ToString();
                    ame10Ver = playbooks.FirstOrDefault(x => ((x.Name == "AME 10" || x.Name == "AME10") && x.Username == "Ameliorated") || x.UniqueId == Guid.Parse("{513722d2-ce95-4d2a-a88a-53570642bc4e}"))?.Version.ToString();
                }
                catch (Exception e)
                {
                }
            }

            if (args.Length > 0 && args[0] == "--uninstall")
            {
                Frame = new ConsoleTUI.Frame($"| {(win11 ? "Privacy+" : "AME10")}  | Playbook v{ame11Ver ?? ame10Ver ?? "X.X.X"} | Settings v{Ver} |", false);
                Frame.Open();

                Console.WriteLine();
                Console.WriteLine();
                Frame.WriteCenteredLine("Uninstalling...");
                try
                {
                    try
                    {
                        foreach (var process in Process.GetProcessesByName(Assembly.GetExecutingAssembly().Location).Where(x => x.Id != Process.GetCurrentProcess().Id))
                        {
                            process.Kill();
                        }
                    }
                    catch (Exception e) { }

                    Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Privacy+ Settings", false);
                    Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\AME10 Settings", false);

                    foreach (var userDir in Directory.GetDirectories(Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users")))
                    {
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Privacy+ Settings.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\Privacy+ Settings.lnk"));
                        if (File.Exists(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\AME10 Settings.lnk")))
                            File.Delete(Path.Combine(userDir, @"AppData\Roaming\OpenShell\Pinned\AME10 Settings.lnk"));
                    }

                    if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\Privacy+ Settings.lnk")))
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\Privacy+ Settings.lnk"));
                    if (File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\AME10 Settings.lnk")))
                        File.Delete(Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Ameliorated\AME10 Settings.lnk"));

                    Process.Start(new ProcessStartInfo("cmd.exe", $"/c \"timeout /t 3 /nobreak & del /q /f \"\"{Win32.ProcessEx.GetCurrentProcessFileLocation()}\"\"\"")
                        { UseShellExecute = true, WindowStyle = ProcessWindowStyle.Hidden });

                    Environment.Exit(0);
                }
                catch (Exception e)
                {
                    ConsoleTUI.ShowErrorBox("Error while attempting to uninstall: " + e.ToString(), null);
                }

                return;
            }

            if (!File.Exists(Environment.ExpandEnvironmentVariables(@"%WINDIR%\System32\sfc1.exe")) && File.Exists(@"%WINDIR%\System32\wuaueng.dll") && ame11Ver == null && ame10Ver == null)
            {
                ConsoleTUI.ShowErrorBox("amecs can only be used with the AME 11/10 Playbooks for AME Wizard.", "amecs");
                Environment.Exit(1);
            }

            try
            {
                var server = new ServiceController("LanmanServer");
                ChangeStartMode(server, ServiceStartMode.Automatic);
                var workstation = new ServiceController("LanmanWorkstation");
                ChangeStartMode(workstation, ServiceStartMode.Automatic);

                server.Start();
                workstation.Start();

                server.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(10000));
                workstation.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(10000));
            }
            catch (Exception e) { }

            if (win11)
                ame10Ver = null;
            else
                ame11Ver = null;

            try
            {
                NSudo.GetSystemPrivilege();
                if (!WindowsIdentity.GetCurrent().IsSystem)
                    throw new Exception("Identity did not change.");
                NSudo.RunAsUser(() =>
                {
                    Globals.Username = WindowsIdentity.GetCurrent().Name.Split('\\').Last();
                    Globals.UserDomain = WindowsIdentity.GetCurrent().Name.Split('\\').FirstOrDefault();
                    Globals.UserSID = WindowsIdentity.GetCurrent().User.ToString();
                });
                try
                {
                    Globals.UserFolder = Registry.Users.OpenSubKey(Globals.UserSID + "\\Volatile Environment")
                        .GetValue("USERPROFILE").ToString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(Globals.Username);

                    ConsoleTUI.ShowErrorBox(
                        $"Could not fetch user folder information from user with SID '{Globals.UserSID}': " + e,
                        "Central AME Script");
                    Environment.Exit(1);
                }

                PrincipalContext context = new PrincipalContext(ContextType.Machine);

                PrincipalSearcher userPrincipalSearcher = new PrincipalSearcher(new UserPrincipal(context));
                Globals.User =
                    userPrincipalSearcher.FindAll()
                        .FirstOrDefault(x => (x is UserPrincipal) && x.Sid.Value == Globals.UserSID) as UserPrincipal;

                PrincipalSearcher groupPrincipalSearcher = new PrincipalSearcher(new GroupPrincipal(context));
                Globals.Administrators = groupPrincipalSearcher.FindAll().FirstOrDefault(x =>
                        (x is GroupPrincipal) && x.Sid.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid)) as
                    GroupPrincipal;
            }
            catch (Exception e)
            {
                ConsoleTUI.ShowErrorBox("Could not acquire System privileges: " + e, "Central AME Script");
                Environment.Exit(1);
            }

            if (args.Length > 1 && args[0] == "-Uninstall")
            {
                Frame = new ConsoleTUI.Frame($"| {(win11 ? "Privacy+" : "AME10")} | Playbook v{ame11Ver ?? ame10Ver ?? "X.X.X"} | Settings v{Ver} |", false);
                Frame.Open();

                if (Directory.Exists(args[1]) || File.Exists(args[1]))
                    Deameliorate.DeameliorateCore(false, false, args[1]);
                else
                    await Deameliorate.ShowMenuNoWarn();
                return;
            }

            _ = Task.Run(async () =>
            {
                PendingUpdate = await Update.CheckForUpdate();
                if (PendingUpdate != null)
                {
                    if (!MainMenuLock.Wait(0) || CurrentMainMenu == null)
                        return;

                    var cursorTop = Console.CursorTop;
                    var cursorLeft = Console.CursorLeft;
                    var foreground = Console.ForegroundColor;

                    Console.SetCursorPosition(14, CurrentMainMenu.Choices.Count + 3);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"  Update {(win11 ? "Privacy+" : "AME10")} Settings ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[v{Ver} --> v{PendingUpdate}]");
                    Console.ForegroundColor = foreground;
                    Console.SetCursorPosition(cursorLeft, cursorTop);

                    CurrentMainMenu.Choices[CurrentMainMenu.Choices.Count - 3] =
                        new Menu.MenuItem($"Update {(win11 ? "Privacy+" : "AME10")} Settings", new Func<Task<bool>>(InstallUpdate))
                        {
                            SecondaryText = $"[v{Ver} --> v{PendingUpdate}]",
                            SecondaryTextForeground = ConsoleColor.Yellow,
                        };

                    MainMenuLock.Release();
                }
            });

            Frame = new ConsoleTUI.Frame($"| Privacy+ | Playbook v{ame11Ver ?? ame10Ver ?? "X.X.X"} | Settings v{Ver} |", false);
            Frame.Open();

            while (true)
            {
                Globals.UserElevated = Globals.User.IsMemberOf(Globals.Administrators);

                Frame.Clear();

                CurrentMainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    Choices =
                    {
                        new Menu.MenuItem("Manage User Settings", new Func<Task<bool>>(Users.ShowMenu))
                        {
                            SecondaryText = $"[Current User: {Truncate(Globals.Username, 12)}]",
                            SecondaryTextForeground = ConsoleColor.DarkGray,
                        },
                        new Menu.MenuItem("Manage System Settings", new Func<Task<bool>>(Actions.SystemActions.SystemMenu.ShowMenu)),
                        new Menu.MenuItem("Manage Software", new Func<Task<bool>>(Actions.SoftwareMenu.ShowMenu)),
                        new Menu.MenuItem("Manage Keyboard Languages", new Func<Task<bool>>(Languages.ShowMenu)),
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem($"Verify {(win11 ? "Privacy+" : "AME")} Integrity", new Func<Task<bool>>(Integrity.CheckIntegrity)),
                        new Menu.MenuItem($"Uninstall {(win11 ? "Privacy+" : "AME10")} Playbook", new Func<Task<bool>>(Deameliorate.ShowMenu)),
                        PendingUpdate == null
                            ? new Menu.MenuItem("Check for Updates", new Func<Task<bool>>(Update.CheckForUpdateAction))
                            : new Menu.MenuItem($"Update {(win11 ? "Privacy+" : "AME10")} Settings", new Func<Task<bool>>(InstallUpdate))
                            {
                                SecondaryText = $"[v{Ver} --> v{PendingUpdate}]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                            },
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Exit", new Func<Task<bool>>(Globals.ExitAsync))
                    },
                    SelectionForeground = ConsoleColor.Green,
                };

                Func<Task<bool>> result;
                try
                {

                    CurrentMainMenu.Write();
                    result = (Func<Task<bool>>)CurrentMainMenu.Load(false, MainMenuLock);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                    return;
                }

                try
                {
                    if (!result.Method.Name.Contains("InstallUpdate"))
                        CurrentMainMenu.Frame.Clear();
                    await result.Invoke();
                }
                catch (Exception e)
                {
                    ConsoleTUI.ShowErrorBox("Error while running an action: " + e.ToString(), null);
                }
            }
        }

        private static async Task<bool> InstallUpdate()
        {
            var cursorTop = Console.CursorTop;
            var cursorLeft = Console.CursorLeft;
            var foreground = Console.ForegroundColor;

            bool win11 = Win32.SystemInfoEx.WindowsVersion.MajorVersion >= 11;

            Console.SetCursorPosition(14, CurrentMainMenu.Choices.Count + 3);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"  Updating {(win11 ? "Privacy+" : "AME10")} Settings ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[Downloading (0%)]");

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.ProgressChanged += (sender, args) =>
            {
                Console.SetCursorPosition(14, CurrentMainMenu.Choices.Count + 3);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"  Updating {(win11 ? "Privacy+" : "AME10")} Settings ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(args.ProgressPercentage == 100 ? $"[Installing]          " : $"[Downloading ({args.ProgressPercentage}%)]");
                Console.ForegroundColor = foreground;
            };
            await Update.InstallUpdate(backgroundWorker);

            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            return true;
        }

        public static string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }


    }

}