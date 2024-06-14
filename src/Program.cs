using System;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;
using amecs.Actions;
using Menu = Ameliorated.ConsoleUtils.Menu;

namespace amecs
{
    internal class Program
    {
        public const string Ver = "2.2";
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

        private static string PendingUpdate = null;
        private static readonly SemaphoreSlim MainMenuLock = new SemaphoreSlim(0);
        private static Menu CurrentMainMenu = null;
        
        [STAThread]
        public static async Task Main(string[] args)
        {
            ConsoleTUI.Initialize("Configure AME");

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
            
            if (!File.Exists(Environment.ExpandEnvironmentVariables(@"%WINDIR%\System32\sfc1.exe")))
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
            catch (Exception e)
            {
            }

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
                    
                    Console.SetCursorPosition(16, CurrentMainMenu.Choices.Count + 5);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Update Central AME Script ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[v{Ver} --> v{PendingUpdate}]");
                    Console.ForegroundColor = foreground;
                    Console.SetCursorPosition(cursorLeft, cursorTop);

                        CurrentMainMenu.Choices[CurrentMainMenu.Choices.Count - 1] =
                            new Menu.MenuItem("Update Central AME Script", new Func<Task<bool>>(InstallUpdate))
                            {
                                SecondaryText = $"[v{Ver} --> v{PendingUpdate}]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                            };

                    MainMenuLock.Release();
                }
            });

            Frame = new ConsoleTUI.Frame($"| Central AME Script v{Ver} |", false);
            Frame.Open();

            while (true)
            {
                Globals.UserElevated = Globals.User.IsMemberOf(Globals.Administrators);

                Frame.Clear();
                bool usernameRequirement = new Reg.Value()
                {
                    KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                    ValueName = "dontdisplaylastusername",
                    Data = 1,
                }.IsEqual();

                                bool hibernation = new Reg.Value()
                                   {
                                       KeyName = @"HKLM\SYSTEM\CurrentControlSet\Control\Power",
                                       ValueName = "HibernateEnabled",
                                       Data = 1,
                                   }.IsEqual()
                                   &&
                                   new Reg.Value()
                                   {
                                       KeyName = @"HKLM\SYSTEM\CurrentControlSet\Control\Power",
                                       ValueName = "HiberFileType",
                                       Data = 2,
                                   }.IsEqual();

                bool uiModified = (File.Exists(Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"),
                    "Fluent-AME.skin7")) || File.Exists(Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"),
                    "Fluent-Metro.skin7"))) && File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\StartMenuDLL.dll"));
                
                bool netInstalled = new Reg.Key()
                {
                    KeyName = @"HKLM\SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5",
                    Operation = RegistryOperation.Add
                }.IsEqual();

                CurrentMainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    Choices =
                    {
                        Globals.UserElevated
                            ? new Menu.MenuItem("Enable Enhanced Security", new Func<Task<bool>>(Elevation.DeElevate))
                            : new Menu.MenuItem("Disable Enhanced Security", new Func<Task<bool>>(Elevation.Elevate)),
                        !uiModified
                            ? new Menu.MenuItem("Enable UI Modifications", new Func<Task<bool>>(UIModifications.Enable))
                            : new Menu.MenuItem("Disable UI Modifications", new Func<Task<bool>>(UIModifications.Disable)),
                        usernameRequirement
                            ? new Menu.MenuItem("Disable Corporate Login",
                                new Func<Task<bool>>(UsernameRequirement.Disable))
                            : new Menu.MenuItem("Enable Corporate Login",
                                new Func<Task<bool>>(UsernameRequirement.Enable)),
                        hibernation ? 
                            new Menu.MenuItem("Disable Hibernation", new Func<Task<bool>>(Hibernation.DisableHibernation)) : 
                            new Menu.MenuItem("Enable Hibernation", new Func<Task<bool>>(Hibernation.EnableHibernation)),
                        new Menu.MenuItem("Manage Browsers", new Func<Task<bool>>(Browsers.ShowMenu)),
                        new Menu.MenuItem("Manage User Settings", new Func<Task<bool>>(Users.ShowMenu)),
                        new Menu.MenuItem("Manage Language Settings", new Func<Task<bool>>(Languages.ShowMenu)),
                        !netInstalled
                            ? new Menu.MenuItem("Install .NET 3.5", new Func<Task<bool>>(_NET.ShowMenu))
                            : new Menu.MenuItem("Install .NET 3.5", new Func<Task<bool>>(_NET.ShowMenu))
                            {
                                IsEnabled = false, SecondaryText = "[Installed]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            },
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Check AME Integrity", new Func<Task<bool>>(Integrity.CheckIntegrity)),
                        new Menu.MenuItem("Uninstall AME", new Func<Task<bool>>(Deameliorate.ShowMenu)),
                        new Menu.MenuItem("Extra", new Func<Task<bool>>(Extra.Extra.ShowMenu)),
                        
                        Menu.MenuItem.Blank,
                        PendingUpdate == null ?
                            new Menu.MenuItem("Check For Updates", new Func<Task<bool>>(Update.CheckForUpdateAction))
                            : new Menu.MenuItem("Update Central AME Script", new Func<Task<bool>>(InstallUpdate))
                            {
                                SecondaryText = $"[v{Ver} --> v{PendingUpdate}]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                            },
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
            
            Console.SetCursorPosition(14, CurrentMainMenu.Choices.Count + 5);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("  Updating Central AME Script ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[Downloading (0%)]");
            
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.ProgressChanged += (sender, args) =>
            {
                Console.SetCursorPosition(14, CurrentMainMenu.Choices.Count + 5);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("  Updating Central AME Script ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(args.ProgressPercentage == 100 ? $"[Installing]          " : $"[Downloading ({args.ProgressPercentage}%)]");
                Console.ForegroundColor = foreground;
            };
            await Update.InstallUpdate(backgroundWorker);
            
            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            return true;
        }
    }
}