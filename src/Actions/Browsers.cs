using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;
using Microsoft.Win32;

namespace amecs.Actions
{
    public class Browsers
    {
        public static Task<bool> ShowMenu()
        {
            bool firefoxInstalled = Directory.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramData%\chocolatey\lib\Firefox"));
            bool firefoxConflict = !firefoxInstalled && (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet")?.GetSubKeyNames().Any(x => x.Contains("Firefox")) ?? false);
            bool ugcInstalled = File.Exists(Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\UngoogledChromium\Ungoogled Uninstaller.exe"));
            
            var mainMenu = new Ameliorated.ConsoleUtils.Menu()
            {
                Choices =
                {
                    !firefoxInstalled
                        ? !firefoxConflict
                            ? new Menu.MenuItem("Install Configured Firefox", new Func<bool>(InstallFirefox))
                            : new Menu.MenuItem("Install Configured Firefox", new Func<bool>(InstallFirefox))
                            {
                                IsEnabled = false, SecondaryText = "[Conflict]",
                                SecondaryTextForeground = ConsoleColor.Yellow,
                                PrimaryTextForeground = ConsoleColor.DarkGray
                            }
                        : new Menu.MenuItem("Uninstall Firefox", new Func<bool>(UninstallFirefox)),

                    !ugcInstalled ?
                        new Menu.MenuItem("Install Configured Ungoogled Chromium", new Func<bool>(InstallUGC)) 
                        : new Menu.MenuItem("Uninstall Ungoogled Chromium", new Func<bool>(UninstallUGC)),
                    Menu.MenuItem.Blank,
                    new Menu.MenuItem("Return to Menu", new Func<bool>(() => true)),
                    new Menu.MenuItem("Exit", new Func<bool>(Globals.Exit)),
                },
                SelectionForeground = ConsoleColor.Green
            };
            mainMenu.Write();
            var result = (Func<bool>)mainMenu.Load(true);
            return Task.FromResult(result.Invoke());
        }

        private static bool InstallFirefox() => amecs.RunBasicAction("Installing Firefox", "Firefox was successfully installed", new Action(
            () =>
            {
                if (!File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\chocolatey\choco.exe")))
                    throw new Exception("Chocolatey must be installed for this action.");
                if (!CheckInternet())
                    throw new Exception("Internet must be connected for this action.");


                Assembly assembly = Assembly.GetExecutingAssembly();

                var tempFilePath = Environment.ExpandEnvironmentVariables(@"%TEMP%\Firefox-" + new Random().Next(0, 9999) + ".zip");
                using (Stream stream = assembly.GetManifestResourceStream("amecs.Properties.Firefox.zip"))
                {
                    if (stream == null)
                        throw new InvalidOperationException("Could not find embedded resource");


                    using (FileStream outputFileStream = new FileStream(tempFilePath, FileMode.Create))
                        stream.CopyTo(outputFileStream);
                }

                string extractPath = Environment.ExpandEnvironmentVariables(@"%TEMP%\Firefox-" + new Random().Next(0, 9999));
                ZipFile.ExtractToDirectory(tempFilePath, extractPath);

                try
                {
                    File.Delete(tempFilePath);
                }
                catch (Exception e)
                {
                }

                Process.Start(new ProcessStartInfo("cmd.exe", "/c FIREFOX.bat")
                    { UseShellExecute = false, CreateNoWindow = true, WorkingDirectory = extractPath })!.WaitForExit();

                try
                {
                    Directory.Delete(extractPath, true);
                }
                catch
                {
                }
            }));

        private static bool UninstallFirefox() => amecs.RunBasicAction("Uninstalling Firefox", "Firefox was successfully uninstalled", new Action(
            () =>
            {
                if (!File.Exists(Environment.ExpandEnvironmentVariables(@"%PROGRAMDATA%\chocolatey\choco.exe")))
                    throw new Exception("Chocolatey must be installed for this action.");

                var process = Process.Start(new ProcessStartInfo("choco.exe", "uninstall -y firefox")
                    { UseShellExecute = false, CreateNoWindow = true })!;

                process.WaitForExit();
                if (process.ExitCode != 0)
                    throw new Exception("Chocolatey uninstall exited with non-zero exit code: " + process.ExitCode);

            }));
        

        private static bool InstallUGC() => amecs.RunBasicAction("Installing UGC", "Ungoogled Chromium was successfully installed", new Action(
            () =>
            {
                if (!CheckInternet())
                    throw new Exception("Internet must be connected for this action.");


                Assembly assembly = Assembly.GetExecutingAssembly();

                var tempFilePath = Environment.ExpandEnvironmentVariables(@"%TEMP%\UGC-" + new Random().Next(0, 9999) + ".zip");
                using (Stream stream = assembly.GetManifestResourceStream("amecs.Properties.UGC.zip"))
                {
                    if (stream == null)
                        throw new InvalidOperationException("Could not find embedded resource");


                    using (FileStream outputFileStream = new FileStream(tempFilePath, FileMode.Create))
                        stream.CopyTo(outputFileStream);
                }

                string extractPath = Environment.ExpandEnvironmentVariables(@"%TEMP%\UGC-" + new Random().Next(0, 9999));
                ZipFile.ExtractToDirectory(tempFilePath, extractPath);

                try
                {
                    File.Delete(tempFilePath);
                }
                catch (Exception e)
                {
                }

                Process.Start(new ProcessStartInfo("cmd.exe", "/c UGC.bat")
                    { UseShellExecute = false, CreateNoWindow = true, WorkingDirectory = extractPath })!.WaitForExit();

                try
                {
                    Directory.Delete(extractPath, true);
                }
                catch
                {
                }
            }));

        private static bool UninstallUGC() => amecs.RunBasicAction("Uninstalling UGC", "Ungoogled Chromium was successfully installed", new Action(
            () =>
            {
                Process.Start(
                    new ProcessStartInfo(Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\UngoogledChromium\Ungoogled Uninstaller.exe"))
                        { UseShellExecute = false, CreateNoWindow = true })!.WaitForExit();
            }));

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetCheckConnection(string lpszUrl, int dwFlags, int dwReserved);

        private static bool CheckInternet()
        {
            try
            {
                try
                {
                    if (!InternetCheckConnection("http://archlinux.org", 1, 0))
                    {
                        if (!InternetCheckConnection("http://google.com", 1, 0))
                            return false;
                    }

                    return true;
                }
                catch
                {
                    var request = (HttpWebRequest)WebRequest.Create("http://google.com");
                    request.KeepAlive = false;
                    request.Timeout = 5000;
                    using (var response = (HttpWebResponse)request.GetResponse())
                        return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}