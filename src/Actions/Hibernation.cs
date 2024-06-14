using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace amecs.Actions
{
    public static class Hibernation
    {
        public static Task<bool> EnableHibernation() =>amecs.RunBasicActionTask("Enabling hibernation","Enabled hibernation successfully",() => 
        { 
            Thread.Sleep(1600); 
            
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = "powercfg.exe";
            proc.StartInfo.Arguments = "/HIBERNATE /TYPE FULL";
            proc.Start();
            proc.WaitForExit(20000);

            if (proc.ExitCode != 0)
                throw new Exception("powercfg exited with a non-zero exitcode.\r\nHibernation may not be supported by your hardware.");
        });

        public static Task<bool> DisableHibernation() =>amecs.RunBasicActionTask("Disabling hibernation","Disabled hibernation successfully",() => 
        { 
            Thread.Sleep(1600); 
            
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = "powercfg.exe";
            proc.StartInfo.Arguments = "/HIBERNATE OFF";
            proc.Start();
            proc.WaitForExit(20000);

            if (proc.ExitCode != 0)
                throw new Exception("powercfg exited with a non-zero exitcode.");
            Thread.Sleep(1600); 
        });
    }
}