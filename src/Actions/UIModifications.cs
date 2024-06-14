using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;

namespace amecs.Actions
{
    public class UIModifications
    {
        public static Task<bool> Enable() => amecs.RunBasicActionTask("Enabling AME UI Modifications", "AME UI modifications are now enabled",
            () =>
            {
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    ValueName = "TaskbarGlomLevel",
                    Data = 2,
                }.Apply();
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    ValueName = "TaskbarAl",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    ValueName = "ShowTaskViewButton",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                    ValueName = "SearchboxTaskbarMode",
                    Data = 0,
                }.Apply();

                try
                {
                    new Reg.Value()
                    {
                        KeyName = "HKU\\" + Globals.UserSID + @"_Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32",
                        ValueName = "",
                        Data = @"",
                    }.Apply();
                    new Reg.Value()
                    {
                        KeyName = "HKU\\" + Globals.UserSID + @"_Classes\CLSID\{1d64637d-31e9-4b06-9124-e83fb178ac6e}\TreatAs",
                        ValueName = "",
                        Data = @"{64bc32b5-4eec-4de7-972d-bd8bd0324537}",
                    }.Apply();
                }
                catch (Exception e)
                {
                }

                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu",
                    ValueName = "ShowedStyle2",
                    Data = 1,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_BINARY,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu",
                    ValueName = "CSettingsDlg",
                    Data = "c80100001a0100000000000000000000360d00000100000000000000",
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_BINARY,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu",
                    ValueName = "ItemRanks",
                    Data = "00",
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_SZ,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\MRU",
                    ValueName = "0",
                    Data = @"C:\Windows\regedit.exe",
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "Version",
                    Data = 04040098,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "AllProgramsMetro",
                    Data = 1,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "RecentMetroApps",
                    Data = 1,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "StartScreenShortcut",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "SearchInternet",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "GlassOverride",
                    Data = 1,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "GlassColor",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_SZ,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "SkinW7",
                    Data = "Fluent-AME",
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_SZ,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "ShiftWin",
                    Data = "Nothing",
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_MULTI_SZ,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "SkinOptionsW7",
                    Data =
                        "DARK_MAIN=0\0METRO_MAIN=0\0LIGHT_MAIN=0\0AUTOMODE_MAIN=1\0DARK_SUBMENU=0\0METRO_SUBMENU=\0LIGHT_SUBMENU=0\0AUTOMODE_SUBMENU=1\0SUBMENU_SEPARATORS=1\0DARK_SEARCH=0\0METRO_SEARCH=\0LIGHT_SEARCH=0\0AUTOMODE_SEARCH=1\0SEARCH_FRAME=1\0SEARCH_COLOR=0\0SMALL_SEARCH=0\0MOERN_SEARCH=1\0SEARCH_ITALICS=0\0NONE=0\0SEPARATOR=0\0TWO_TONE=1\0CLASSIC_SELECTOR=1\0HAF_SELECTOR=0\0CURVED_MENUSEL=1\0CURVED_SUBMENU=0\0SELECTOR_REVEAL=1\0TRANSPARENT=0\0OPAQU_SUBMENU=1\0OPAQUE_MENU=0\0OPAQUE=0\0STANDARD=0\0SMALL_MAIN2=1\0SMALL_ICONS=0\0COMPACT_UBMENU=0\0PRESERVE_MAIN2=0\0LESS_PADDING=0\0EXTRA_PADDING=1\024_PADDING=0\0LARGE_PROGRAMS0\0TRANSPARENT_SHUTDOWN=0\0OUTLINE_SHUTDOWN=0\0BUTTON_SHUTDOWN=1\0EXPERIMENTAL_SHUTDOWN=0\0LARGE_FONT=0\0CONNECTED_BORDER=0\0FLOATING_BORDER=1\0LARGE_SUBMENU=0\0LARGE_LISTS=0\0THI_MAIN2=0\0EXPERIMENTAL_MAIN2=1\0USER_IMAGE=1\0USER_OUTSIDE=0\0SCALING_USER=1\056=0\064=\0TRANSPARENT_USER=0\0UWP_SCROLLBAR=0\0MODERN_SCROLLBAR=1\0SMALL_ARROWS=0\0ARROW_BACKGROUD=1\0ICON_FRAME=0\0SEARCH_SEPARATOR=0\0NO_PROGRAMS_BUTTON=0",
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "SkipMetro",
                    Data = 1,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_MULTI_SZ,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "MenuItems7",
                    Data =
                        "Item1.Command=user_files\0Item1.Settings=NOEXPAND\0Item2.Command=user_documents\0Item2.Settings=NOEXPAND\0Item3.Command=user_pictures\0Item3.Settings=NOEXPAND\0Item4.Command=user_music\0Item4.Settings=NOEXPAND\0Item5.Command=user_videos\0Item5.Settings=NOEXPAND\0Item6.Command=downloads\0Item6.Settings=NOEXPAND\0Item7.Command=homegroup\0Item7.Settings=ITEM_DISABLED\0Item8.Command=separator\0Item9.Command=games\0Item9.Settings=TRACK_RECENT|NOEXPAND|ITEM_DISABLED\0Item10.Command=favorites\0Item10.Settings=ITEM_DISABLED\0Item11.Command=recent_documents\0Item12.Command=computer\0Item12.Settings=NOEXPAND\0Item13.Command=network\0Item13.Settings=ITEM_DISABLED\0Item14.Command=network_connections\0Item14.Settings=ITEM_DISABLED\0Item15.Command=separator\0Item16.Command=control_panel\0Item16.Settings=TRACK_RECENT\0Item17.Command=pc_settings\0Item17.Settings=TRACK_RECENT\0Item18.Command=admin\0Item18.Settings=TRACK_RECENT|ITEM_DISABLED\0Item19.Command=devices\0Item19.Settings=ITEM_DISABLED\0Item20.Command=defaults\0Item20.Settings=ITEM_DISABLED\0Item21.Command=help\0Item21.Settings=ITEM_DISABLED\0Item22.Command=run\0Item23.Command=apps\0Item23.Settings=ITEM_DISABLED\0Item24.Command=windows_security\0Item24.Settings=ITEM_DISABLED\0",
                }.Apply();

                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\OpenShell\Settings",
                    ValueName = "Update",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "CheckWinUpdates",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "HighlightNew",
                    Data = 0,
                }.Apply();

                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "MaxRecentPrograms",
                    Data = 5,
                }.Apply();

                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "EnableGlass",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    Type = Reg.RegistryValueType.REG_DWORD,
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\OpenShell\StartMenu\Settings",
                    ValueName = "MenuShadow",
                    Data = 0,
                }.Apply();

                foreach (var process in Process.GetProcessesByName("explorer"))
                {
                    try
                    {
                        TerminateProcess(process.Handle, 1);
                    }
                    catch (Exception e)
                    {
                    }
                }
                
                Assembly assembly = Assembly.GetExecutingAssembly();
                if (!File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\StartMenuDLL.dll")))
                {
                    var tempFilePath = Environment.ExpandEnvironmentVariables(@"%TEMP%\OpenShell-" + new Random().Next(0, 9999) + ".exe");
                    using (Stream stream = assembly.GetManifestResourceStream("amecs.Properties.OpenShellSetup_4_4_191.exe"))
                    {
                        if (stream == null)
                            throw new InvalidOperationException("Could not find embedded resource");

                        using (FileStream outputFileStream = new FileStream(tempFilePath, FileMode.Create))
                            stream.CopyTo(outputFileStream);
                    }

                    Process.Start(new ProcessStartInfo(tempFilePath, "/qn /quiet ADDLOCAL=StartMenu")
                        { UseShellExecute = false, CreateNoWindow = true })!.WaitForExit();

                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch (Exception e)
                    {
                    }
                }

                if (!Directory.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins")))
                    Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"));

                if (!File.Exists(Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"), "Fluent-AME.skin7")))
                {
                    using (Stream stream = assembly.GetManifestResourceStream("amecs.Properties.Fluent-AME.skin7"))
                    {
                        if (stream == null)
                            throw new InvalidOperationException("Could not find embedded resource");

                        using (FileStream outputFileStream =
                               new FileStream(
                                   Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"), "Fluent-AME.skin7"),
                                   FileMode.Create))
                            stream.CopyTo(outputFileStream);
                    }
                }

                if (!File.Exists(Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"), "Fluent-Metro.skin")))
                {
                    using (Stream stream = assembly.GetManifestResourceStream("amecs.Properties.Fluent-Metro.skin"))
                    {
                        if (stream == null)
                            throw new InvalidOperationException("Could not find embedded resource");

                        using (FileStream outputFileStream =
                               new FileStream(
                                   Path.Combine(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\Open-Shell\Skins"), "Fluent-Metro.skin"),
                                   FileMode.Create))
                            stream.CopyTo(outputFileStream);
                    }
                }
                
                if (Process.GetProcessesByName("explorer").Length == 0)
                    NSudo.RunProcessAsUser(NSudo.GetUserToken(), "explorer.exe", "", 0);
                
                if (File.Exists(Environment.ExpandEnvironmentVariables(@"%WINDIR%\System32\amecs.exe")))
                {
                    if (!Directory.Exists($@"{Globals.UserFolder}\AppData\Roaming\OpenShell\Pinned"))
                        Directory.CreateDirectory($@"{Globals.UserFolder}\AppData\Roaming\OpenShell\Pinned");
                    Process.Start(new ProcessStartInfo("PowerShell",
                            $@" -NoP -C ""$ws = New-Object -ComObject WScript.Shell; $s = $ws.CreateShortcut('{Globals.UserFolder}\AppData\Roaming\OpenShell\Pinned\Configure AME.lnk'); $S.TargetPath = '{Environment.ExpandEnvironmentVariables(@"%WINDIR%\System32\amecs.exe")}'; $S.Save()""")
                        { UseShellExecute = false, CreateNoWindow = true })!.WaitForExit();
                }
            });

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        public static Task<bool> Disable() => amecs.RunBasicActionTask("Disabling AME UI Modifications", "AME UI modifications are now disabled",
            () =>
            {
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    ValueName = "TaskbarGlomLevel",
                    Data = 0,
                }.Apply();
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    ValueName = "TaskbarAl",
                    Data = 1,
                }.Apply();
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    ValueName = "ShowTaskViewButton",
                    Data = 1,
                }.Apply();
                new Reg.Value()
                {
                    KeyName = "HKU\\" + Globals.UserSID + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                    ValueName = "SearchboxTaskbarMode",
                    Data = 1,
                }.Apply();

                try
                {
                    Registry.Users.DeleteSubKeyTree(Globals.UserSID + @"_Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}");
                    Registry.Users.DeleteSubKeyTree(Globals.UserSID + @"_Classes\CLSID\{1d64637d-31e9-4b06-9124-e83fb178ac6e}");
                }
                catch (Exception e)
                {
                }

                string openShellId = null;

                var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (var item in key.GetSubKeyNames())
                {
                    try
                    {
                        if (((string)key.OpenSubKey(item).GetValue("DisplayName")).Equals("Open-Shell"))
                            openShellId = item;
                    }
                    catch
                    {
                        // do nothing
                    }
                }

                if (openShellId != null)
                {
                    foreach (var process in Process.GetProcessesByName("explorer"))
                    {
                        try
                        {
                            TerminateProcess(process.Handle, 1);
                        }
                        catch (Exception e)
                        {
                        }
                    }

                    Process.Start("MsiExec.exe", $"/X{openShellId} /quiet")?.WaitForExit();

                    if (Globals.UserSID != null)
                    {
                        var appData = (string)Registry.Users
                            .OpenSubKey(Globals.UserSID +
                                        @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders")?
                            .GetValue("AppData");

                        if (Directory.Exists(Path.Combine(appData ?? "NULL:", "OpenShell")))
                            Directory.Delete(Path.Combine(appData!, "OpenShell"), true);
                    }
                }

                if (Process.GetProcessesByName("explorer").Length == 0)
                    NSudo.RunProcessAsUser(NSudo.GetUserToken(), "explorer.exe", "", 0);
            });
    }
}