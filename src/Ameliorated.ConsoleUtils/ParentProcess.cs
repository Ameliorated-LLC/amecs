using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Ameliorated.ConsoleUtils
{
    public static class ParentProcess
    {
        private static readonly uint TH32CS_SNAPPROCESS = 2;

        public static string ProcessName = Get().ProcessName;

        public static Process Get()
        {
            try
            {
                var iParentPid = 0;
                var iCurrentPid = Process.GetCurrentProcess().Id;

                var oHnd = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

                if (oHnd == IntPtr.Zero)
                    return null;

                var oProcInfo = new PROCESSENTRY32();

                oProcInfo.dwSize =
                    (uint)Marshal.SizeOf(typeof(PROCESSENTRY32));

                if (Process32First(oHnd, ref oProcInfo) == false)
                    return null;

                do
                {
                    if (iCurrentPid == oProcInfo.th32ProcessID)
                        iParentPid = (int)oProcInfo.th32ParentProcessID;
                } while (iParentPid == 0 && Process32Next(oHnd, ref oProcInfo));

                if (iParentPid > 0)
                    return Process.GetProcessById(iParentPid);
                return null;
            } catch (Exception e)
            {
                return null;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll")]
        private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll")]
        private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }
    }
}