using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Ameliorated.ConsoleUtils;
using Microsoft.Dism;
using Microsoft.Win32;
using static Ameliorated.ConsoleUtils.Menu;

namespace amecs.Actions
{
    public class Languages
    {
        public static bool ShowMenu()
        {
            while (true)
            {
                Program.Frame.Clear();
                
                var mainMenu = new Ameliorated.ConsoleUtils.Menu()
                {
                    Choices =
                    {
                        true
                            ? new Menu.MenuItem("Change Display Language", new Func<bool>(ShowDisplayMenu))
                            {
                                IsEnabled = false,
                                SecondaryText = "[Not Supported]",
                                SecondaryTextForeground = ConsoleColor.Red,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            }
                            : new Menu.MenuItem("Change Display Language", new Func<bool>(ShowDisplayMenu)),
                        new Menu.MenuItem("Add Keyboard Language", new Func<bool>(ShowAddKeyboardLanguageMenu)),
                        new Menu.MenuItem("Remove Keyboard Language", new Func<bool>(ShowRemoveKeyboardLanguageMenu)),
                        Globals.WinVer >= 22000
                            ? new Menu.MenuItem("Install Language Pack", new Func<bool>(ShowLanguagePackMenu))
                            {
                                IsEnabled = false,
                                SecondaryText = "[Not Supported]",
                                SecondaryTextForeground = ConsoleColor.Red,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            }
                            : new Menu.MenuItem("Install Language Pack", new Func<bool>(ShowLanguagePackMenu)),
                        Globals.WinVer >= 22000
                            ? new Menu.MenuItem("Uninstall Language Pack", new Func<bool>(ShowRemoveLanguagePackMenu))
                            {
                                IsEnabled = false,
                                SecondaryText = "[Not Supported]",
                                SecondaryTextForeground = ConsoleColor.Red,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            }
                            : new Menu.MenuItem("Uninstall Language Pack", new Func<bool>(ShowRemoveLanguagePackMenu)),
                        Menu.MenuItem.Blank,
                        new Menu.MenuItem("Return to Menu", null),
                        new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit))
                    },
                    SelectionForeground = ConsoleColor.Green
                };

                Func<bool> result;
                try
                {
                    mainMenu.Write();
                    var res = mainMenu.Load();
                    if (res == null)
                        return true;
                    result = (Func<bool>)res;
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                    return false;
                }

                try
                {
                    result.Invoke();
                } catch (Exception e)
                {
                    ConsoleTUI.ShowErrorBox("Error while loading a menu: " + e.ToString(), null);
                }
            }
        }

        private static bool ShowDisplayMenu() => ShowLanguageMenu(true);
        private static bool ShowLanguagePackMenu() => ShowLanguageMenu(false);
        private static List<string> currentLanguagePacks;

        public static bool ShowLanguageMenu(bool display)
        {
            /*
            if (display)
            {
                try
                {
                    var deskKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\Desktop");
                    var pendingLang = ((string[])deskKey.GetValue("PreferredUILanguagesPending")).First();
                    
                    ConsoleTUI.OpenFrame.WriteCentered("Checking languages");
                    var indicator = new ConsoleUtils.LoadingIndicator(true);
                    Thread.Sleep(50);
                    indicator.Dispose();
                    
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("A language change is currently pending.\r\nYou must restart before continuing.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                    {
                        AnyKey = true,
                        Text = "Press any key to return to the Menu: "
                    });
                    return false;
                } catch {}
            }
            */
            
            List<MenuItem> displayItems = new List<MenuItem>(LanguagePackItems);
            if (!display)
            {
                DismApi.Initialize(DismLogLevel.LogErrors);
                DismPackageCollection packages;
                using (var session = DismApi.OpenOnlineSession())
                {
                    packages = DismApi.GetPackages(session);
                    session.Close();
                }

                DismApi.Shutdown();
                currentLanguagePacks = new List<string>(packages.Where(x => x.PackageName.StartsWith("Microsoft-Windows-Client-LanguagePack-Package")).Select(x => x.PackageName));
            }

            foreach (var menuItem in new List<MenuItem>(displayItems))
            {
                var tag =  ((string)menuItem.ReturnValue).Split('|').First();

                string activeLang = null;
                string pendingLang = null;
                try
                {
                    var deskKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\Desktop");
                    activeLang = ((string[])deskKey.GetValue("PreferredUILanguages")).First();
                    pendingLang = ((string[])deskKey.GetValue("PreferredUILanguagesPending")).First();
                } catch
                {
                }
                
                if (display && tag == pendingLang && activeLang != pendingLang)
                {
                    var clone = menuItem.Clone();
                    clone.IsEnabled = false;
                    clone.PrimaryTextForeground = ConsoleColor.DarkGray;
                    clone.SecondaryText = "[Pending Restart]";
                    clone.SecondaryTextForeground = ConsoleColor.Yellow;
                    displayItems.ReplaceItem(menuItem, clone);
                }
                
                if (display && tag == activeLang && (activeLang == pendingLang || pendingLang == null))
                {
                    var clone = menuItem.Clone();
                    clone.IsEnabled = false;
                    clone.PrimaryTextForeground = ConsoleColor.DarkGray;
                    clone.SecondaryText = "[Current]";
                    clone.SecondaryTextForeground = ConsoleColor.Yellow;
                    displayItems.ReplaceItem(menuItem, clone);
                }

                if (!display)
                {
                    if (currentLanguagePacks.Any(x => x.Contains('~' + tag + '~')))
                    {
                        var clone = menuItem.Clone();
                        clone.IsEnabled = false;
                        clone.PrimaryTextForeground = ConsoleColor.DarkGray;
                        clone.SecondaryText = "[Installed]";
                        clone.SecondaryTextForeground = ConsoleColor.Yellow;
                        displayItems.ReplaceItem(menuItem, clone);
                    }
                }
            }
            

            var result = amecs.ShowDefaultMenu(displayItems.ToList());
            if (result.GetType().Name.StartsWith("Func")) return ((Func<bool>)result).Invoke();
            var languageTag = ((string)result).Split('|').First();
            var downloadAmount = int.Parse(((string)result).Split('|').Last(), CultureInfo.InvariantCulture);
            if (!display)
            {
                return InstallLanguagePack(downloadAmount, languageTag);
            }
            else
            {
                return ChangeDisplayLanguage(downloadAmount, languageTag);
            }
        }

        private static List<string> CurrentLanguages;

        public static bool ShowAddKeyboardLanguageMenu()
        {
            while (true)
            {
                CurrentLanguages = new List<string>();
                try
                {
                    var langsKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\International\\User Profile");
                    foreach (var langKey in langsKey.GetSubKeyNames())
                    {
                        CurrentLanguages.AddRange(langsKey.OpenSubKey(langKey).GetValueNames().Where(x => x.Contains(':')));
                    }
                } catch
                {
                }

                List<MenuItem> displayItems = new List<MenuItem>(KeyboardLanguageItems);
                foreach (var menuItem in displayItems.Where(x => x.ReturnValue.GetType().Name == "String").ToList())
                {
                    if (CurrentLanguages.Contains((string)menuItem.ReturnValue, StringComparer.InvariantCultureIgnoreCase))
                    {
                        var clone = menuItem.Clone();
                        clone.IsEnabled = false;
                        clone.PrimaryTextForeground = ConsoleColor.DarkGray;
                        clone.SecondaryText = "[Installed]";
                        clone.SecondaryTextForeground = ConsoleColor.Yellow;
                        displayItems.ReplaceItem(menuItem, clone);
                    }
                }

                var result = amecs.ShowDefaultMenu(displayItems);
                if (result.GetType().Name.StartsWith("Func")) return ((Func<bool>)result).Invoke();
                if (result.GetType().Name == "String")
                {
                    return AddKeyboardLanguage((string)result);
                }
                else
                {
                    var subDisplayItems = ((MenuItem[])result).ToList();
                    foreach (var menuItem in subDisplayItems)
                    {
                        if (CurrentLanguages.Contains((string)menuItem.ReturnValue, StringComparer.InvariantCultureIgnoreCase))
                        {
                            menuItem.IsEnabled = false;
                            menuItem.PrimaryTextForeground = ConsoleColor.DarkGray;
                            menuItem.SecondaryText = "[Installed]";
                            menuItem.SecondaryTextForeground = ConsoleColor.Yellow;
                        }
                        else
                        {
                            menuItem.IsEnabled = true;
                            menuItem.PrimaryTextForeground = null;
                            menuItem.SecondaryText = null;
                            menuItem.SecondaryTextForeground = null;
                        }
                    }

                    var subResult = amecs.ShowDefaultMenu(subDisplayItems);
                    if (subResult.GetType().Name.StartsWith("Func"))
                    {
                        ((Func<bool>)subResult).Invoke();
                        continue;
                    }

                    return AddKeyboardLanguage((string)subResult);
                }
            }
        }

        public static bool AddKeyboardLanguage(string tip)
        {
            var choice = new ChoicePrompt()
            {
                Text = "Make default keyboard language? (Y/N): "
            }.Start();
            if (!choice.HasValue) return true;
            bool makeDefault = choice == 0;
            try
            {
                ConsoleTUI.OpenFrame.WriteCentered("\r\nAdding keyboard language");
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    var result = NSudo.RunProcessAsUser(NSudo.GetUserToken(), "PowerShell.exe", makeDefault ? $"-NoP -C \"$List=Get-WinUserLanguageList;" + $"$List[0].InputMethodTips.Add('{tip}');" + $"Set-WinUserLanguageList $List -Force;" + $"Set-WinDefaultInputMethodOverride -InputTip '{tip}'\"" : $"-NoP -C \"$List=Get-WinUserLanguageList;" + $"$List[0].InputMethodTips.Add('{tip}');" + $"Set-WinUserLanguageList $List -Force\"");
                    if (result == null)
                    {
                        throw new Exception("Could not start user PowerShell process.");
                    }

                    if (result > 0)
                    {
                        throw new Exception("PowerShell exited with a non-zero exitcode.");
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }

            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close($"Keyboard language added successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
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
        internal static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, StringBuilder lpCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        private static bool InstallLanguagePack(int downloadAmount, string language) => InstallLanguage(downloadAmount, language, true);

        private static bool InstallLanguage(int downloadAmount, string language, bool finalize)
        {
            var downloadFolder = Directory.Exists(Path.Combine(Globals.UserFolder, "Desktop")) ? Path.Combine(Globals.UserFolder, "Desktop") : Environment.GetEnvironmentVariable("TEMP");
            var file = "LangPacks-" + new Random().Next(0, 9999);
            try
            {
                try
                {
                    DriveInfo drive = DriveInfo.GetDrives().First(x => x.Name == Environment.GetEnvironmentVariable("SYSTEMDRIVE") + "\\");
                    if (drive.AvailableFreeSpace < downloadAmount * 2.3)
                    {
                        Console.WriteLine();
                        ConsoleTUI.OpenFrame.Close("There is not enough available space on the system drive.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                        {
                            AnyKey = true,
                            Text = "Press any key to return to the Menu: "
                        });
                        return false;
                    }
                } catch { }

                string amountString = "0";
                if (downloadAmount == 2480000) amountString = "2.5";
                if (downloadAmount == 2900000) amountString = "2.9";
                if (downloadAmount == 3230000) amountString = "3.2";
                var choice = new ChoicePrompt()
                {
                    Text = $"A ~{amountString}GB Language Packs ISO must be downloaded\r\nContinue? (Y/N): "
                }.Start();
                if (choice == null || choice == 1) return false;
                if (!amecs.IsInternetAvailable())
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("An internet connection is required for this action.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                    {
                        AnyKey = true,
                        Text = "Press any key to return to the Menu: "
                    });
                    return false;
                }

                var sevenZip = ExistsInPath("7z.exe");
                sevenZip = sevenZip == null ? File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\7-Zip\7z.exe")) ? Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\7-Zip\7z.exe") : null : sevenZip;
                var choco = ExistsInPath("choco.exe");
                choco = choco == null ? File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\chocolatey\choco.exe")) ? Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\chocolatey\choco.exe") : null : choco;

                if (String.IsNullOrEmpty(sevenZip) && String.IsNullOrEmpty(choco))
                {
                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("7zip or Chocolatey must be installed for this action.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                    {
                        AnyKey = true,
                        Text = "Press any key to return to the Menu: "
                    });
                    return false;
                }

                bool wasMissing = false;
                if (String.IsNullOrEmpty(sevenZip))
                {
                    wasMissing = true;
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nInstalling 7zip");
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        var proc = new Process();
                        var startInfo = new ProcessStartInfo
                        {
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Normal,
                            Arguments = "install -y --force --allow-empty-checksums 7zip",
                            FileName = choco,
                        };
                        proc.StartInfo = startInfo;
                        proc.Start();
                        proc.WaitForExit();
                    }

                    sevenZip = ExistsInPath("7z.exe");
                    sevenZip = sevenZip == null ? File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\7-Zip\7z.exe")) ? Environment.ExpandEnvironmentVariables(@"%PROGRAMFILES%\7-Zip\7z.exe") : null : sevenZip;

                    if (sevenZip == null)
                    {
                        ConsoleTUI.OpenFrame.WriteCentered("\r\nUninstalling 7zip");
                        using (new ConsoleUtils.LoadingIndicator(true))
                        {
                            var proc = new Process();
                            var startInfo = new ProcessStartInfo
                            {
                                CreateNoWindow = true,
                                UseShellExecute = false,
                                WindowStyle = ProcessWindowStyle.Normal,
                                Arguments = "uninstall 7zip -y --force-dependencies --allow-empty-checksums",
                                FileName = choco,
                            };
                            proc.StartInfo = startInfo;
                            proc.Start();
                            proc.WaitForExit();
                        }

                        Console.WriteLine();
                        ConsoleTUI.OpenFrame.Close("Could not detect 7zip.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                        {
                            AnyKey = true,
                            Text = "Press any key to return to the Menu: "
                        });
                        return false;
                    }
                }

                try
                {
                    ConsoleTUI.OpenFrame.WriteCenteredLine("\r\nDownload progress");
                    using (WebClientEx wc = new WebClientEx(0, (long)downloadAmount * 1000))
                    {
                        var stdout = GetStdHandle(-11);
                        var maxHashTags = (ConsoleTUI.OpenFrame.DisplayWidth - 5);
                        wc.DownloadProgressChanged += delegate(object sender, DownloadProgressChangedEventArgs e)
                        {
                            var currentHashTags = (int)Math.Ceiling(Math.Min(((double)e.ProgressPercentage / 100) * maxHashTags, maxHashTags));
                            var spaces = maxHashTags - currentHashTags + (4 - e.ProgressPercentage.ToString().Length);
                            var sb = new StringBuilder(new string('#', currentHashTags) + new string(' ', spaces) + e.ProgressPercentage + "%");
                            uint throwaway;
                            WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                        };
                        var task = wc.DownloadFileTaskAsync(new Uri("https://software-download.microsoft.com/download/pr/19041.1.191206-1406.vb_release_CLIENTLANGPACKDVD_OEM_MULTI.iso"), Path.Combine(downloadFolder, file + ".ISO"));
                        task.Wait();
                        Thread.Sleep(100);
                        var sb = new StringBuilder(new string('#', maxHashTags) + " 100%");
                        uint throwaway;
                        WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                    }

                    ConsoleTUI.OpenFrame.WriteCentered("\r\n\r\nExtracting ISO");
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        var proc = new Process();
                        var startInfo = new ProcessStartInfo
                        {
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Normal,
                            Arguments = $@"e -y -o""{Path.Combine(downloadFolder, file)}"" ""{Path.Combine(downloadFolder, file + ".ISO")}"" x64\langpacks\*.cab",
                            FileName = sevenZip,
                        };
                        proc.StartInfo = startInfo;
                        proc.Start();
                        proc.WaitForExit();
                    }

                    try
                    {
                        File.Delete(Path.Combine(downloadFolder, file + ".ISO"));
                    } catch
                    {
                    }
                } catch (Exception e)
                {
                    try
                    {
                        if (wasMissing)
                        {
                            ConsoleTUI.OpenFrame.WriteCentered("\r\nUninstalling 7zip");
                            using (new ConsoleUtils.LoadingIndicator(true))
                            {
                                var proc = new Process();
                                var startInfo = new ProcessStartInfo
                                {
                                    CreateNoWindow = true,
                                    UseShellExecute = false,
                                    WindowStyle = ProcessWindowStyle.Normal,
                                    Arguments = "uninstall 7zip -y --force-dependencies --allow-empty-checksums",
                                    FileName = choco,
                                };
                                proc.StartInfo = startInfo;
                                proc.Start();
                                proc.WaitForExit();
                            }
                        }
                    } catch
                    {
                    }

                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                    {
                        AnyKey = true,
                        Text = "Press any key to return to the Menu: "
                    });
                    return false;
                }

                if (wasMissing)
                {
                    ConsoleTUI.OpenFrame.WriteCentered("\r\nUninstalling 7zip");
                    using (new ConsoleUtils.LoadingIndicator(true))
                    {
                        var proc = new Process();
                        var startInfo = new ProcessStartInfo
                        {
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            WindowStyle = ProcessWindowStyle.Normal,
                            Arguments = "uninstall 7zip -y --force-dependencies --allow-empty-checksums",
                            FileName = choco,
                        };
                        proc.StartInfo = startInfo;
                        proc.Start();
                        proc.WaitForExit();
                    }
                }

                var cab = Path.Combine(downloadFolder, file, $"Microsoft-Windows-Client-Language-Pack_x64_{language}.cab");
                if (!File.Exists(cab))
                {
                    try
                    {
                        Directory.Delete(Path.Combine(downloadFolder, file));
                    } catch
                    {
                    }

                    throw new Exception("Extraction failed.");
                }

                ConsoleTUI.OpenFrame.WriteCentered("\r\nInstalling language pack");
                var topCache = Console.CursorTop;
                var leftCache = Console.CursorLeft;
                Console.WriteLine();
                bool inProgress = false;
                try
                {
                    using (var indicator = new ConsoleUtils.LoadingIndicator(true))
                    {
                        DismApi.Initialize(DismLogLevel.LogErrors);
                        using (var session = DismApi.OpenOnlineSession())
                        {
                            var stdout = GetStdHandle(-11);
                            bool indicatorStopped = false;
                            var maxHashTags = (ConsoleTUI.OpenFrame.DisplayWidth - 5);
                            DismApi.AddPackage(session, cab, false, true, delegate(DismProgress progress)
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
                                WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)topCache), out throwaway);
                                inProgress = false;
                            });
                            session.Close();
                            Thread.Sleep(100);
                            var sb = new StringBuilder(new string('#', maxHashTags) + " 100%");
                            uint throwaway;
                            WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)topCache), out throwaway);
                        }

                        DismApi.Shutdown();
                    }
                } catch (Exception e)
                {
                    while (inProgress)
                    {
                        Thread.Sleep(50);
                    }

                    try
                    {
                        Directory.Delete(Path.Combine(downloadFolder, file), true);
                    } catch
                    {
                    }

                    Console.WriteLine();
                    ConsoleTUI.OpenFrame.Close("DISM error: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                    {
                        AnyKey = true,
                        Text = "Press any key to return to the Menu: "
                    });
                    return false;
                }

                Directory.Delete(Path.Combine(downloadFolder, file), true);
            } catch (Exception e)
            {
                try
                {
                    Directory.Delete(Path.Combine(downloadFolder, file), true);
                } catch
                {
                }

                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }

            if (finalize)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Language pack installed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
            }

            return true;
        }

        public static string ExistsInPath(string fileName)
        {
            if (File.Exists(fileName)) return fileName;
            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(Path.PathSeparator).Where(x => !x.Contains("chocolatey\\bin")))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath)) return fullPath;
                if (!fileName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) && File.Exists(fullPath + ".exe")) return fullPath + ".exe";
            }

            return null;
        }

        public static bool ShowRemoveKeyboardLanguageMenu()
        {
            CurrentLanguages = new List<string>();
            try
            {
                var langsKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\International\\User Profile");
                foreach (var langKey in langsKey.GetSubKeyNames())
                {
                    CurrentLanguages.AddRange(langsKey.OpenSubKey(langKey).GetValueNames().Where(x => x.Contains(':')));
                }
            } catch
            {
                throw new Exception("Could not fetch installed keyboard languages.\r\n");
            }

            List<MenuItem> displayItems = new List<MenuItem>();
            foreach (var menuItem in KeyboardLanguageItems.Where(x => x.ReturnValue.GetType().Name == "String"))
            {
                if (CurrentLanguages.Contains((string)menuItem.ReturnValue, StringComparer.InvariantCultureIgnoreCase))
                {
                    displayItems.Add(menuItem);
                }
            }

            foreach (var menuItemList in KeyboardLanguageItems.Where(x => x.ReturnValue.GetType().Name != "String").Select(x => x.ReturnValue))
            {
                var subDisplayItems = ((MenuItem[])menuItemList).ToList();

                foreach (var subMenuItem in subDisplayItems)
                {
                    if (CurrentLanguages.Contains((string)subMenuItem.ReturnValue, StringComparer.InvariantCultureIgnoreCase))
                    {
                        displayItems.Add(subMenuItem);
                    }
                }
            }

            var result = amecs.ShowDefaultMenu(displayItems);
            if (result.GetType().Name.StartsWith("Func")) return ((Func<bool>)result).Invoke();
            return RemoveKeyboardLanguage((string)result);
        }

        private static bool RemoveKeyboardLanguage(string language)
        {
            ConsoleTUI.OpenFrame.WriteCentered("Removing keyboard language");
            try
            {
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    StringBuilder reAddCommand = new StringBuilder("");
                    foreach (var tag in CurrentLanguages.Where(x => !x.Equals(language, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        reAddCommand.Append($@";$List[0].InputMethodTips.Add('{tag}')");
                    }

                    var result = NSudo.RunProcessAsUser(NSudo.GetUserToken(), 
                        "PowerShell.exe", $"-NoP -C \"$Tag=(Get-WinUserLanguageList)[0].LanguageTag;" + 
                                          $"$List = New-WinUserLanguageList $Tag" + 
                                          $"{reAddCommand.ToString()};" + 
                                          $"Set-WinUserLanguageList $List -Force\"");
                    if (result == null)
                    {
                        throw new Exception("Could not start user PowerShell process.");
                    }

                    if (result > 0)
                    {
                        throw new Exception("PowerShell exited with a non-zero exitcode.");
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }

            CurrentLanguages = new List<string>();
            var langsKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\International\\User Profile");
            foreach (var langKey in langsKey.GetSubKeyNames())
            {
                CurrentLanguages.AddRange(langsKey.OpenSubKey(langKey).GetValueNames().Where(x => x.Contains(':')));
            }

            if (CurrentLanguages.Contains(language, StringComparer.InvariantCultureIgnoreCase))
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Keyboard language could not be removed. If it is the\r\nactive UI language, this is normal.", ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }

            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("Keyboard language removed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
            {
                AnyKey = true,
                Text = "Press any key to return to the Menu: "
            });
            return true;
        }

        public static bool ShowRemoveLanguagePackMenu()
        {
            DismApi.Initialize(DismLogLevel.LogErrors);
            DismPackageCollection packages;
            using (var session = DismApi.OpenOnlineSession())
            {
                packages = DismApi.GetPackages(session);
                session.Close();
            }

            DismApi.Shutdown();
            currentLanguagePacks = new List<string>(packages.Where(x => x.PackageName.StartsWith("Microsoft-Windows-Client-LanguagePack-Package")).Select(x => x.PackageName));
            List<MenuItem> displayItems = new List<MenuItem>();
            foreach (var menuItem in LanguagePackItems)
            {
                var tag = ((string)menuItem.ReturnValue).Split('|').First();
                var pkg = currentLanguagePacks.FirstOrDefault(x => x.Contains('~' + tag + '~'));
                if (pkg != null)
                {
                    var clone = menuItem.Clone();
                    
                    string activeLang = null;
                    try
                    {
                        var deskKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\Desktop");
                        activeLang = ((string[])deskKey.GetValue("PreferredUILanguages")).First();
                    } catch
                    {
                    }
                    
                    if (activeLang == tag)
                    {
                        clone.IsEnabled = false;
                        clone.PrimaryTextForeground = ConsoleColor.DarkGray;
                        clone.SecondaryText = "[UI Language]";
                        clone.SecondaryTextForeground = ConsoleColor.Yellow;
                    }

                    clone.ReturnValue = pkg;
                    displayItems.Add(clone);
                }
            }

            var result = amecs.ShowDefaultMenu(displayItems.ToList());
            if (result.GetType().Name.StartsWith("Func")) return ((Func<bool>)result).Invoke();
            return RemoveLanguagePack((string)result);
        }

        private static bool RemoveLanguagePack(string package)
        {
            ConsoleTUI.OpenFrame.WriteCentered("Removing language pack");
            var topCache = Console.CursorTop;
            var leftCache = Console.CursorLeft;
            bool inProgress = false;
            try
            {
                using (var indicator = new ConsoleUtils.LoadingIndicator(true))
                {
                    DismApi.Initialize(DismLogLevel.LogErrors);
                    using (var session = DismApi.OpenOnlineSession())
                    {
                        var stdout = GetStdHandle(-11);
                        bool indicatorStopped = false;
                        var maxHashTags = (ConsoleTUI.OpenFrame.DisplayWidth - 5);
                        DismApi.RemovePackageByName(session, package, delegate(DismProgress progress)
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
                            WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                            inProgress = false;
                        });
                        session.Close();
                        Thread.Sleep(100);
                        var sb = new StringBuilder(new string('#', maxHashTags) + " 100%");
                        uint throwaway;
                        WriteConsoleOutputCharacter(stdout, sb, (uint)sb.Length, new COORD((short)ConsoleTUI.OpenFrame.DisplayOffset, (short)Console.CursorTop), out throwaway);
                    }

                    DismApi.Shutdown();
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("DISM error: " + e.Message, ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }

            Console.WriteLine();
            ConsoleTUI.OpenFrame.Close("Language pack removed successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
            {
                AnyKey = true,
                Text = "Press any key to return to the Menu: "
            });
            return true;
        }

        public static bool ChangeDisplayLanguage(int downloadAmount, string language)
        {
            DismApi.Initialize(DismLogLevel.LogErrors);
            DismPackageCollection packages;
            using (var session = DismApi.OpenOnlineSession())
            {
                packages = DismApi.GetPackages(session);
                session.Close();
            }

            DismApi.Shutdown();
            currentLanguagePacks = new List<string>(packages.Where(x => x.PackageName.StartsWith("Microsoft-Windows-Client-LanguagePack-Package")).Select(x => x.PackageName));

            if (!currentLanguagePacks.Any(x => x.Contains('~' + language + '~')))
            {
                if (!InstallLanguage(downloadAmount, language, false))
                    return false;
                
                ConsoleTUI.OpenFrame.WriteLine();
                ConsoleTUI.OpenFrame.WriteLine();
            }
            
            var choice = new ChoicePrompt()
            {
                Text = "Make default keyboard language? (Y/N): "
            }.Start();
            if (!choice.HasValue) return true;
            bool makeDefault = choice == 0;

            string previousDefault = null;
            if (!makeDefault)
            {
                try
                {
                    var langsKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\International\\User Profile");

                    try { previousDefault = (string)langsKey.GetValue("InputMethodOverride");} catch {}

                    if (previousDefault == null)
                    {
                        string activeLang = null;
                        try
                        {
                            var deskKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\Desktop");
                            activeLang = ((string[])deskKey.GetValue("PreferredUILanguages")).First();
                        } catch
                        {
                        }

                        var keyName = langsKey.GetSubKeyNames().FirstOrDefault(x => x == activeLang);
                        if (keyName == null) keyName = langsKey.GetSubKeyNames().FirstOrDefault(x => x.StartsWith(activeLang.Split('-').First()));
                        var langKey = langsKey.OpenSubKey(keyName);
                        
                        foreach (var langValue in langKey.GetValueNames().Where(x => x.Contains(':')))
                        {
                            if ((int)langKey.GetValue(langValue) == 1)
                                previousDefault = langValue;
                        }
                    }
                } catch (Exception e) {
                }
            }
            
            
            CurrentLanguages = new List<string>();
            try
            {
                var langsKey = Registry.Users.OpenSubKey(Globals.UserSID + "\\Control Panel\\International\\User Profile");
                foreach (var langKey in langsKey.GetSubKeyNames())
                {
                    CurrentLanguages.AddRange(langsKey.OpenSubKey(langKey).GetValueNames().Where(x => x.Contains(':')));
                }
            } catch
            {
            }
            
            ConsoleTUI.OpenFrame.WriteCentered("\r\nSetting language");
            try
            {
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    try
                    {
                        Process proc = new Process();
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proc.StartInfo.FileName = "PowerShell.exe";
                        proc.StartInfo.Arguments = $"-NoP -C \"Set-WinSystemLocale {language}\"";
                        proc.Start();
                        proc.WaitForExit();
                    } catch { }
                    
                    StringBuilder reAddCommand = new StringBuilder("");
                    foreach (var tag in CurrentLanguages)
                    {
                        reAddCommand.Append($@";$List[0].InputMethodTips.Add('{tag}')");
                    }

                    var result = NSudo.RunProcessAsUser(NSudo.GetUserToken(),
                        "PowerShell.exe", previousDefault == null
                            ? $"-NoP -C \"$List = New-WinUserLanguageList {language}" +
                              $"{reAddCommand.ToString()};" +
                              $"Set-WinUserLanguageList $List -Force;" +
                              $"Set-WinDefaultInputMethodOverride\""
                            : $"-NoP -C \"$List = New-WinUserLanguageList {language}" +
                              $"{reAddCommand.ToString()};" +
                              $"Set-WinUserLanguageList $List -Force;" +
                              $"Set-WinDefaultInputMethodOverride '{previousDefault}'\""
                    );
                    if (result == null)
                    {
                        throw new Exception("Could not start user PowerShell process.");
                    }

                    if (result > 0)
                    {
                        throw new Exception("PowerShell exited with a non-zero exitcode.");
                    }

                    new Reg.Value() { KeyName = "HKU\\" + Globals.UserSID + "\\Control Panel\\Desktop", ValueName = "PreferredUILanguagesPending", Data = new[] { language }, Type = Reg.RegistryValueType.REG_MULTI_SZ }.Apply();
                }
            } catch (Exception e)
            {
                Console.WriteLine();
                ConsoleTUI.OpenFrame.Close("Error: " + e.Message.TrimEnd('\n').TrimEnd('\r'), ConsoleColor.Red, Console.BackgroundColor, new ChoicePrompt()
                {
                    AnyKey = true,
                    Text = "Press any key to return to the Menu: "
                });
                return false;
            }
            
            Console.WriteLine();
            if ((int?)ConsoleTUI.OpenFrame.Close("Display language changed to " + language, ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
            {
                TextForeground = ConsoleColor.Yellow,
                Text = "Restart to apply changes? (Y/N): "
            }) == 0) amecs.RestartWindows(false, true);
            return true;
        }

        private static readonly MenuItem[] LanguagePackItems = new[] { new MenuItem("Arabic (ar-SA)", "ar-SA|2480000"), new MenuItem("Bulgarian (bg-BG)", "bg-BG|2480000"), new MenuItem("Chineese [Simplified] (zh-CN)", "zh-CN|3230000"), new MenuItem("Chineese [Traditional] (zh-TW)", "zh-TW|3230000"), new MenuItem("Croatian (hr-HR)", "hr-HR|2900000"), new MenuItem("Czech (cs-CZ)", "cs-CZ|2480000"), new MenuItem("Danish (da-DK)", "da-DK|2480000"), new MenuItem("Dutch (nl-NL)", "nl-NL|2900000"), new MenuItem("English [US] (en-US)", "en-US|2480000"), new MenuItem("English [UK] (en-GB)", "en-GB|2480000"), new MenuItem("Estonian (et-EE)", "et-EE|2480000"), new MenuItem("Finnish (fi-FI)", "fi-FI|2480000"), new MenuItem("French [Canada] (fr-CA)", "fr-CA|2480000"), new MenuItem("French [France] (fr-FR)", "fr-FR|2900000"), new MenuItem("German (de-DE)", "de-DE|2480000"), new MenuItem("Greek (el-GR)", "el-GR|2480000"), new MenuItem("Hebrew (he-IL)", "he-IL|2900000"), new MenuItem("Hungarian (hu-HU)", "hu-HU|2900000"), new MenuItem("Italian (it-IT)", "it-IT|2900000"), new MenuItem("Japanese (ja-JP)", "ja-JP|2900000"), new MenuItem("Korean (ko-KR)", "ko-KR|2900000"), new MenuItem("Latvian (lv-LV)", "lv-LV|2900000"), new MenuItem("Lithuanian (lt-LT)", "lt-LT|2900000"), new MenuItem("Norwegian (nb-NO)", "nb-NO|2900000"), new MenuItem("Polish (pl-PL)", "pl-PL|3230000"), new MenuItem("Portugeese [Brazil] (pt-BR)", "pt-BR|3230000"), new MenuItem("Portugeese [Portugal] (pt-PT)", "pt-PT|3230000"), new MenuItem("Romanian (ro-RO)", "ro-RO|3230000"), new MenuItem("Russian (ru-RU)", "ru-RU|3230000"), new MenuItem("Serbian (sr-Latn-RS)", "tn-RS|3230000"), new MenuItem("Slovak (sk-SK)", "sk-SK|3230000"), new MenuItem("Slovenian (sl-SI)", "sl-SI|3230000"), new MenuItem("Spanish [Mexico] (es-MX)", "es-MX|2480000"), new MenuItem("Spanish [Spain] (es-ES)", "es-ES|2480000"), new MenuItem("Swedish (sv-SE)", "sv-SE|3230000"), new MenuItem("Thai (th-TH)", "th-TH|3230000"), new MenuItem("Turkish (tr-TR)", "tr-TR|3230000"), new MenuItem("Ukrainian (uk-UA)", "uk-UA|3230000"), };
        private static readonly MenuItem[] KeyboardLanguageItems = new[] { new MenuItem("United States", new[] { new MenuItem("United States - English", "0409:00000409"), new MenuItem("United States - International", "0409:00020409"), new MenuItem("United States - Dvorak", "0409:00010409"), new MenuItem("United States - Dvorak (Left Hand)", "0409:00030409"), new MenuItem("United States - Dvorak (Right Hand)", "0409:00040409") }), new MenuItem("Chinese", new[] { new MenuItem("Chineese (Simplified)", "0804:{81D4E9C9-1D3B-41BC-9E6C-4B40BF79E35E}{FA550B04-5AD7-411f-A5AC-CA038EC515D7}"), new MenuItem("Chineese (Traditional) NON-FUNCTIONAL", "0404:{B115690A-EA02-48D5-A231-E3578D2FDF80}{B2F9C502-1742-11D4-9790-0080C882687E}"), new MenuItem("Chineese (Traditional, Hong Kong S.A.R.)", "0404:00000c04"), new MenuItem("Chineese (Traditonal Macao S.A.R.)", "0404:00001404"), new MenuItem("Chineese (Simplified, Singapore)", "0404:00001004") }), new MenuItem("Hindi  (Devanagari) Traditional", "0439:00010439"), new MenuItem("Spanish", new[] { new MenuItem("Spanish (Spain)", "0c0a:0000040a"), new MenuItem("Spanish (Mexico)", "080a:0000080a"), new MenuItem("Spanish Variation", "0c0a:0001040a") }), new MenuItem("French", "040c:0000040c"), new MenuItem("Arabic", new[] { new MenuItem("Arabic (101)", "0401:00000401"), new MenuItem("Arabic (102)", "0401:00010401"), new MenuItem("Arabic (102 AZERTY)", "0401:00020401") }), new MenuItem("Russian", new[] { new MenuItem("Russian", "0419:00000419"), new MenuItem("Russian - Mnemonic", "0419:00020419"), new MenuItem("Russian (Typewriter)", "0419:00010419") }), new MenuItem("Bangla", new[] { new MenuItem("Bangla (Bangladesh)", "0445:00000445"), new MenuItem("Bangla (India)", "0445:00020445"), new MenuItem("Bangla (India) - Legacy", "0445:00010445") }), new MenuItem("Portuguese", new[] { new MenuItem("Portuguese", "0816:00000816"), new MenuItem("Portuguese (Brazilian ABNT)", "0816:00000416"), new MenuItem("Portuguese (Brazilian ABNT2)", "0816:00010416") }), new MenuItem("Albanian", "041c:0000041c"), new MenuItem("Amharic", "045e:{E429B25A-E5D3-4D1F-9BE3-0C608477E3A1}{8F96574E-C86C-4bd6-9666-3F7327D4CBE8}"), new MenuItem("Armenian", new[] { new MenuItem("Armenian Eastern", "042b:0000042b"), new MenuItem("Armenian Phonetic", "042b:0002042b"), new MenuItem("Armenian Typewriter", "042b:0003042b"), new MenuItem("Armenian Western", "042b:0001042b") }), new MenuItem("Assamese - Inscript", "044d:0000044d"), new MenuItem("Azerbaijani", new[] { new MenuItem("Azerbaijani (Standard)", "042c:0001042c"), new MenuItem("Azerbaijani Cyrillic", "042c:0000082c"), new MenuItem("Azerbaijani Latin", "042c:0000042c") }), new MenuItem("Bashkir", "046d:0000046d"), new MenuItem("Belarusian", "0423:00000423"), new MenuItem("Belgian", new[] { new MenuItem("Belgian (Comma)", "080c:0001080c"), new MenuItem("Belgian (Period)", "080c:00000813"), new MenuItem("Belgian French", "080c:0000080c") }), new MenuItem("Bosnian  (Cyrillic)", "141a:00000201a"), new MenuItem("Buginese", "0421:000b0c00"), new MenuItem("Bulgarian", new[] { new MenuItem("Bulgarian", "0402:00030402"), new MenuItem("Bulgarian Latin", "0402:00010402"), new MenuItem("Bulgarian (Phonetic Layout)", "0402:00020402"), new MenuItem("Bulgarian (Phonetic Traditonal)", "0402:00040402"), new MenuItem("Bulgarian (Typewriter)", "0402:00000402") }), new MenuItem("Canadian", new[] { new MenuItem("Canadian French", "0c0c:00001009"), new MenuItem("Canadian French (Legacy)", "0c0c:00000c0c"), new MenuItem("Canadian Multilingual Standard", "0c0c:00011009") }), new MenuItem("Central Atlas Tamazight", "085f:0000085f"), new MenuItem("Central Kurdish", "0429:00000429"), new MenuItem("Cherokee", new[] { new MenuItem("Cherokee Nation", "045c:0000045c"), new MenuItem("Cherokee Nation Phonetic", "045c:0001045c") }), new MenuItem("Croatian", "041a:0000041a"), new MenuItem("Czech", new[] { new MenuItem("Czech", "2000:00000405"), new MenuItem("Czech (QWERTY)", "2000:00010405"), new MenuItem("Czech Programmers", "2000:00020405") }), new MenuItem("Danish", "0406:00000406"), new MenuItem("Divehi", new[] { new MenuItem("Divehi Phonetic", "0465:00000465"), new MenuItem("Divehi Typewriter", "0465:00010465") }), new MenuItem("Dutch", "0413:00000413"), new MenuItem("Dzongkha", "0C51:00000C51"), new MenuItem("Estonian", "0425:00000425"), new MenuItem("Faeroese", "0438:00000438"), new MenuItem("Finnish", new[] { new MenuItem("Finnish", "040b:0000040b"), new MenuItem("Finnish with Sami", "040b:0001083b") }), new MenuItem("Futhark", "0407:00120c00"), new MenuItem("Georgian", new[] { new MenuItem("Georgian", "0437:00020437"), new MenuItem("Georgian (Ergonomic)", "0437:00020437"), new MenuItem("Georgian (QWERTY)", "0437:00010437"), new MenuItem("Georgian Ministry of Education and Science Schools", "0437:00030437"), new MenuItem("Georgian (Old Alphabets)", "0437:00030437") }), new MenuItem("German", new[] { new MenuItem("German", "0407:00000407"), new MenuItem("German (IBM)", "0407:00010407") }), new MenuItem("Gothic", "0407:000c0c00"), new MenuItem("Greek", new[] { new MenuItem("Greek", "0408:00000408"), new MenuItem("Greek (220)", "0408:00010408"), new MenuItem("Greek (220) Latin", "0408:00030408"), new MenuItem("Greek (319)", "0408:00020408"), new MenuItem("Greek (319) Latin", "0408:00040408"), new MenuItem("Greek Latin", "0408:00050408"), new MenuItem("Greek Polytonic", "0408:00600408") }), new MenuItem("Greenlandic", "046f:0000046f"), new MenuItem("Guarani", "0474:00000474"), new MenuItem("Gujarati", "0447:00000447"), new MenuItem("Hausa", "0468:00000468"), new MenuItem("Hebrew", "040d:0000040d"), new MenuItem("Hungarian", new[] { new MenuItem("Hungarian", "040e:0000040e"), new MenuItem("Hungarian 101-key", "040e:0001040e") }), new MenuItem("Icelandic", "040f:0000040f"), new MenuItem("Igbo", "0470:00000470"), new MenuItem("Indian", "4009:00004009"), new MenuItem("Inuktitut", new[] { new MenuItem("Inuktitut - Latin", "085d:0000085d"), new MenuItem("Inuktitut - Naqittaut", "085d:0001045d") }), new MenuItem("Irish", "083C:000001809"), new MenuItem("Italian", new[] { new MenuItem("Italian", "0410:00000410"), new MenuItem("Italian (142)", "0410:00010410") }), new MenuItem("Japanese NON-FUNCTIONAL", "0411:{03B5835F-F03C-411B-9CE2-AA23E1171E36}{A76C93D9-5523-4E90-AAFA-4DB112F9AC76}"), new MenuItem("Javanese", "0421:00110c00"), new MenuItem("Kannada", "044b:0000044b"), new MenuItem("Kazakh", "043f:0000043f"), new MenuItem("Khmer", new[] { new MenuItem("Khmer", "0453:00000453"), new MenuItem("Khmer (NIDA)", "0453:00010453") }), new MenuItem("Konkoni  (Devanagari) - INSCRIPT", "0457:00000439"), new MenuItem("Korean", new[] { new MenuItem("Korean (Hangul)", "0412:{A028AE76-01B1-46C2-99C4-ACD9858AE02F}{B5FE1F02-D5F2-4445-9C03-C568F23C99A1}"), new MenuItem("Korean (Old Hangul)", "0412:{a1e2b86b-924a-4d43-80f6-8a820df7190f}{b60af051-257a-46bc-b9d3-84dad819bafb}") }), new MenuItem("Kyrgyz Cyrillic", "0440:00000440"), new MenuItem("Lao", "0454:00000454"), new MenuItem("Latin American", "080a:0000080a"), new MenuItem("Latvian", new[] { new MenuItem("Latvian (Standard)", "0426:00020426"), new MenuItem("Latvian (Legacy)", "0426:00010426") }), new MenuItem("Lisu", new[] { new MenuItem("Lisu (Basic)", "0409:00070c00"), new MenuItem("Lisu (Standard)", "0409:00080c00") }), new MenuItem("Lithuanian", new[] { new MenuItem("Lithuanian", "0427:00010427"), new MenuItem("Lithuanian IBM", "0427:00000427"), new MenuItem("Lithuanian Standard", "0427:00020427") }), new MenuItem("Luxembourgish", "046e:0000046e"), new MenuItem("Macedonia", new[] { new MenuItem("Macedonian (FYROM)", "042f:0000042f"), new MenuItem("Macedonian (FYROM) - Standard", "042f:0001042f") }), new MenuItem("Malayalam", "044c:0000044c"), new MenuItem("Maltese", new[] { new MenuItem("Maltese 47-key", "043a:0000043a"), new MenuItem("Maltese 48-key", "043a:0001043a") }), new MenuItem("Maori", "0481:00000481"), new MenuItem("Marathi", "044e:0000044e"), new MenuItem("Mongolian", new[] { new MenuItem("Mongoloian (Mongolian Script - Legacy)", "0850:00000850"), new MenuItem("Mongolian (Mongolian Script - Standard)", "0850:00020850"), new MenuItem("Mongolian Cyrillic", "0850:00000450") }), new MenuItem("Myanmar", "0455:00010c00"), new MenuItem("N'ko", "0409:000090c00"), new MenuItem("Nepali", "0461:00000461"), new MenuItem("New Tai Lue", "0409:00020c00"), new MenuItem("Norwegian", new[] { new MenuItem("Norwegian", "0814:00000414"), new MenuItem("Norwegian with Sami", "0814:0000043b") }), new MenuItem("Odia", "0448:00000448"), new MenuItem("Ol Chiki", "0409:d0c00"), new MenuItem("Old Italic", "0409:000f0c00"), new MenuItem("Osmanya", "0409:000e0c00"), new MenuItem("Pashto  (Afghanistan)", "0463:00000463"), new MenuItem("Persian", new[] { new MenuItem("Persian", "0429:00000429"), new MenuItem("Persian (Standard)", "0429:00050429") }), new MenuItem("Phags-pa", "0409:000a0c00"), new MenuItem("Polish", new[] { new MenuItem("Polish (214)", "0415:00010415"), new MenuItem("Polish (Programmers)", "0415:00000415") }), new MenuItem("Punjabi", "0446:00000446"), new MenuItem("Romanian", new[] { new MenuItem("Romanian (Legacy)", "0418:00000418"), new MenuItem("Romanian (Programmers)", "0418:00020418"), new MenuItem("Romanian (Standard)", "0418:00010418") }), new MenuItem("Sakha", "0485:00000485"), new MenuItem("Sami", new[] { new MenuItem("Sami Extended Finland-Sweden", "083b:0002083b"), new MenuItem("Sami Extended Norway", "043b:0001043b") }), new MenuItem("Scottish Gaelic", "0809:00011809"), new MenuItem("Serbian", new[] { new MenuItem("Serbian (Cyrillic)", "1C1A:00000c1a"), new MenuItem("Serbian (Latin)", "241A:0000081a") }), new MenuItem("Sesotho sa Leboa", "046c:0000046c"), new MenuItem("Setswana", "0432:00000432"), new MenuItem("Sinhala", new[] { new MenuItem("Sinhala", "045b:0000045b"), new MenuItem("Sinhala - wij 9", "045b:0001045b") }), new MenuItem("Slovak", new[] { new MenuItem("Slovak", "041b:0000041b"), new MenuItem("Slovak (QWERTY)", "041b:0001041b") }), new MenuItem("Slovenian", "0424:00000424"), new MenuItem("Sora", "0409:00100c00"), new MenuItem("Sorbian", new[] { new MenuItem("Sorbian Extended", "042e:0001042e"), new MenuItem("Sorbian Standard", "042e:0002042e"), new MenuItem("Sorbian Standard (Legacy)", "042e:0000042e") }), new MenuItem("Swedish", new[] { new MenuItem("Swedish", "041d:0000041d"), new MenuItem("Swedish with Sami", "083b:0000083b") }), new MenuItem("Swiss", new[] { new MenuItem("Swiss French", "100c:0000100c"), new MenuItem("Swiss German", "0807:00000807") }), new MenuItem("Syriac", new[] { new MenuItem("Syriac", "045a:0000045a"), new MenuItem("Syriac Phonetic", "045a:0001045a") }), new MenuItem("Tai Le", "0409:00030c00"), new MenuItem("Tajik", "0428:00000428"), new MenuItem("Tamil", new[] { new MenuItem("Tamil", "0449:00000449"), new MenuItem("Tamil (99 Keyboard)", "0449:00020449") }), new MenuItem("Tatar", new[] { new MenuItem("Tatar", "0444:00010444"), new MenuItem("Tatar (Legacy)", "0444:00000444") }), new MenuItem("Telugu", "044a:0000044a"), new MenuItem("Thai", new[] { new MenuItem("Thai Kedmanee", "041e:0000041e"), new MenuItem("Thai Kedmanee (non-ShiftLock)", "041e:0002041e"), new MenuItem("Thai Pattachote", "041e:0001041e"), new MenuItem("Thai Pattachote (non-ShiftLock)", "041e:0003041e") }), new MenuItem("Tibetan", new[] { new MenuItem("Tibetan (PRC - Standard)", "0451:00010451"), new MenuItem("Tibetan (PRC - Legacy)", "0451:00000451") }), new MenuItem("Tifinagh", new[] { new MenuItem("Tifinagh (Basic)", "0409:00050c00"), new MenuItem("Tifinagh (Full)", "0409:00050c00") }), new MenuItem("Tigrinya", "0473:{E429B25A-E5D3-4D1F-9BE3-0C608477E3A1}{3CAB88B7-CC3E-46A6-9765-B772AD7761FF}"), new MenuItem("Turkish", new[] { new MenuItem("Turkish F", "041f:0001041f"), new MenuItem("Turkish Q", "041f:0000041f") }), new MenuItem("Turkmen", "0442:00000442"), new MenuItem("Uyghur", new[] { new MenuItem("Uyghur", "0480:00010480"), new MenuItem("Uygher (Legacy)", "0480:00000480"), new MenuItem("Uyghur (Greek 220)", "0480:00010408") }), new MenuItem("Ukrainian", new[] { new MenuItem("Ukrainian", "0422:00000422"), new MenuItem("Ukrainian (Enhanced)", "0422:00020422") }), new MenuItem("United Kingdom", new[] { new MenuItem("United Kingdom", "0809:00000809"), new MenuItem("United Kingdom Extended", "0809:00000452") }), new MenuItem("Urdu", "0420:00000420"), new MenuItem("Uzbek", "0843:00000843"), new MenuItem("Vietnamese", new[] { new MenuItem("Vietnamese", "042a:0000042a"), new MenuItem("Vietnamese Telex", "042A:{C2CB2CF0-AF47-413E-9780-8BC3A3C16068}{5FB02EC5-0A77-4684-B4FA-DEF8A2195628}") }), new MenuItem("Wolof", "0488:00000488"), new MenuItem("Yakut", "0485:00000485"), new MenuItem("Yoruba", "046a:0000056a"), };
    }

    public static class NativeMethods
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        private const uint FILE_READ_EA = 0x0008;
        private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x2000000;

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetFinalPathNameByHandle(IntPtr hFile, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszFilePath, uint cchFilePath, uint dwFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPTStr)] string filename, [MarshalAs(UnmanagedType.U4)] uint access, [MarshalAs(UnmanagedType.U4)] FileShare share, IntPtr securityAttributes, // optional SECURITY_ATTRIBUTES struct or IntPtr.Zero
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition, [MarshalAs(UnmanagedType.U4)] uint flagsAndAttributes, IntPtr templateFile);

        public static string GetFinalPathName(string path)
        {
            var h = CreateFile(path, FILE_READ_EA, FileShare.ReadWrite | FileShare.Delete, IntPtr.Zero, FileMode.Open, FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero);
            if (h == INVALID_HANDLE_VALUE) return null;
            try
            {
                var sb = new StringBuilder(1024);
                var res = GetFinalPathNameByHandle(h, sb, 1024, 0);
                if (res == 0) return null;
                return sb.ToString();
            } finally
            {
                CloseHandle(h);
            }
        }
    }

    public class WebClientEx : WebClient
    {
        private readonly long from;
        private readonly long to;

        public WebClientEx(long from, long to)
        {
            this.from = from;
            this.to = to;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.AddRange(this.from, this.to);
            return request;
        }
    }
}