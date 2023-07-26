using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace amecs
{
    [StructLayout(LayoutKind.Sequential)]

    public class NSudo
    {
        private struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public unsafe byte* lpSecurityDescriptor;
            public int bInheritHandle;
        }
        private enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }
        private enum TOKEN_TYPE {
            TokenPrimary = 1,
            TokenImpersonation
        }
        
        [StructLayout(LayoutKind.Sequential)]
        private struct LUID {
            public uint LowPart;
            public uint HighPart;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct LUID_AND_ATTRIBUTES {
            public LUID Luid;
            public UInt32 Attributes;
        }
        
        private struct TOKEN_PRIVILEGES {
            public int PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=1)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }
        
        
        private static UInt32 MAXIMUM_ALLOWED = (UInt32)TokenAccessLevels.MaximumAllowed;
        
        [DllImport("advapi32.dll", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);
        
        [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool DuplicateTokenEx(
            IntPtr hExistingToken,
            uint dwDesiredAccess,
            IntPtr lpTokenAttributes,
            SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
            TOKEN_TYPE TokenType,
            out IntPtr phNewToken );
        [DllImport("advapi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        private static extern bool DuplicateTokenEx(
            IntPtr hExistingToken,
            uint dwDesiredAccess,
            ref SECURITY_ATTRIBUTES lpTokenAttributes,
            SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
            TOKEN_TYPE TokenType,
            out IntPtr phNewToken );
        [DllImport("advapi32.dll")]
        static extern bool LookupPrivilegeValue(IntPtr lpSystemName, string lpName,
            ref LUID lpLuid);
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        
        // Use this signature if you do not want the previous state
        [DllImport("advapi32.dll", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            UInt32 Zero,
            IntPtr Null1,
            IntPtr Null2);
        
        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetThreadToken(IntPtr pHandle,
            IntPtr hToken);


        
        
        [DllImport("wtsapi32.dll", SetLastError=true)]
        static extern bool WTSQueryUserToken(UInt32 sessionId, out IntPtr Token);
        
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern Boolean SetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass,
            ref UInt32 TokenInformation, UInt32 TokenInformationLength);
        
        
        [DllImport("userenv.dll", SetLastError=true)]
        static extern bool CreateEnvironmentBlock(out IntPtr lpEnvironment, IntPtr hToken, bool bInherit );
        public static bool GetUserPrivilege(IntPtr Token)
        {
            IntPtr NewToken;
            DuplicateTokenEx(Token, MAXIMUM_ALLOWED, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenImpersonation, out NewToken);
            SetThreadToken(IntPtr.Zero, NewToken);
            return true;
        }

        [DllImport("advapi32.dll", SetLastError=true, CharSet=CharSet.Unicode)]
        static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [Flags]
        enum CreationFlags
        {
            CREATE_SUSPENDED = 0x00000004,
            CREATE_UNICODE_ENVIRONMENT = 0x00000400,
            CREATE_NO_WINDOW = 0x08000000,
            CREATE_NEW_CONSOLE = 0x00000010
        }
        [DllImport("advapi32.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LogonUser(
            [MarshalAs(UnmanagedType.LPStr)] string pszUserName,
            [MarshalAs(UnmanagedType.LPStr)] string pszDomain,
            [MarshalAs(UnmanagedType.LPStr)] string pszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        public static int? RunProcessAsUser(IntPtr Token, string Executable, string Arguments, uint timeout = 0xFFFFFFFF)
        {
            GetAssignPrivilege();
            GetQuotaPrivilege();

            var startupInfo = new STARTUPINFO();
            startupInfo.cb = Marshal.SizeOf(startupInfo);
            startupInfo.dwFlags = 0x00000001;
            startupInfo.wShowWindow = 1;


            var procAttrs = new SECURITY_ATTRIBUTES();
            var threadAttrs = new SECURITY_ATTRIBUTES();
            procAttrs.nLength = Marshal.SizeOf(procAttrs);
            threadAttrs.nLength = Marshal.SizeOf(threadAttrs);

            // Log on user temporarily in order to start console process in its security context.
            var hUserTokenDuplicate = IntPtr.Zero;
            var pEnvironmentBlock = IntPtr.Zero;

            DuplicateTokenEx(Token, MAXIMUM_ALLOWED, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityIdentification, TOKEN_TYPE.TokenPrimary, out hUserTokenDuplicate);
            
            CreateEnvironmentBlock(out pEnvironmentBlock, Token, false);

            PROCESS_INFORMATION _processInfo;
            if (!CreateProcessAsUser(hUserTokenDuplicate, null, String.IsNullOrEmpty(Arguments) ? $"\"{Executable}\"" : $"\"{Executable}\" {Arguments}",
                    ref procAttrs, ref threadAttrs, false, (uint)CreationFlags.CREATE_NO_WINDOW |
                                                           (uint)CreationFlags.CREATE_UNICODE_ENVIRONMENT,
                    pEnvironmentBlock, null, ref startupInfo, out _processInfo)) return null;

            uint exitCode;
            WaitForSingleObject(_processInfo.hProcess, timeout);
            GetExitCodeProcess(_processInfo.hProcess, out exitCode);

            return (int)exitCode;
            /*
            uint dwCreationFlags = (uint)CreationFlags.CREATE_UNICODE_ENVIRONMENT;


            startupInfo.cb = Marshal.SizeOf(startupInfo);



            SECURITY_ATTRIBUTES throwaway = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES throwaway2 = new SECURITY_ATTRIBUTES();
            Console.WriteLine(Marshal.GetLastWin32Error() + "-3");

            Console.WriteLine(CreateProcessAsUser(hUserToken, String.Empty, "\"C:\\Windows\\notepad.exe\"", ref throwaway, ref throwaway2, false, 0, IntPtr.Zero, String.Empty, ref StartupInfo, out ProcessInfo));
            Console.WriteLine(Marshal.GetLastWin32Error() + "-4");
            
            return Process.GetProcessById(ProcessInfo.dwProcessId);
            */
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
        [DllImport("kernel32.dll", SetLastError=true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public IntPtr lpReserved;

            public IntPtr lpDesktop;
            public IntPtr lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }
        public static IntPtr GetUserToken()
        {
            IntPtr Token;

            WTSQueryUserToken((uint)SessionID, out Token);
            return Token;
        }

        private static int SessionID = -1;
        public static bool GetSystemPrivilege()
        {
            IntPtr CurrentProcessToken;
            OpenProcessToken(Process.GetCurrentProcess().Handle, MAXIMUM_ALLOWED, out CurrentProcessToken);
            IntPtr DuplicatedCurrentProcessToken;
            DuplicateTokenEx(CurrentProcessToken, MAXIMUM_ALLOWED, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenImpersonation, out DuplicatedCurrentProcessToken);
            LUID_AND_ATTRIBUTES RawPrivilege = new LUID_AND_ATTRIBUTES();
            LookupPrivilegeValue(IntPtr.Zero, "SeDebugPrivilege", ref RawPrivilege.Luid);
            RawPrivilege.Attributes = SE_PRIVILEGE_ENABLED;

            TOKEN_PRIVILEGES TokenPrivilege = new TOKEN_PRIVILEGES();
            TokenPrivilege.Privileges = new LUID_AND_ATTRIBUTES[] { RawPrivilege };
            TokenPrivilege.PrivilegeCount = 1;
            AdjustTokenPrivileges(DuplicatedCurrentProcessToken, false, ref TokenPrivilege, 0, IntPtr.Zero, IntPtr.Zero);

            SetThreadToken(IntPtr.Zero, DuplicatedCurrentProcessToken);

            SessionID = GetActiveSession();

            IntPtr OriginalProcessToken = new IntPtr(-1);
            CreateSystemToken((int)MAXIMUM_ALLOWED, SessionID, ref OriginalProcessToken);

            IntPtr SystemToken;
            DuplicateTokenEx(OriginalProcessToken, MAXIMUM_ALLOWED, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenImpersonation, out SystemToken);

            SetThreadToken(IntPtr.Zero, SystemToken);

            return true;
        }

        [DllImport("advapi32.dll", SetLastError=true)]
        static extern bool GetTokenInformation(
            IntPtr TokenHandle,
            TOKEN_INFORMATION_CLASS TokenInformationClass,
            IntPtr TokenInformation,
            int TokenInformationLength,
            out int ReturnLength);

        enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin
        }
        
        private static int GetActiveSession()
        {
            IntPtr pSessionInfo = IntPtr.Zero;
            Int32 Count = 0;
            var retval = WTSEnumerateSessions((IntPtr)null, 0, 1, ref pSessionInfo, ref Count);
            Int32 dataSize = Marshal.SizeOf(typeof(WTS_SESSION_INFO));
            
            Int64 current = (Int64)pSessionInfo;

            int result = -1;
            if (retval != 0)
            {
                for (int i = 0; i < Count; i++)
                {
                    WTS_SESSION_INFO si = (WTS_SESSION_INFO)Marshal.PtrToStructure((System.IntPtr)current, typeof(WTS_SESSION_INFO));
                    current += dataSize;

                    if (si.State == WTS_CONNECTSTATE_CLASS.WTSActive)
                    {
                        result = si.SessionID;
                        break;
                    }
                }
                WTSFreeMemory(pSessionInfo);
            }

            return result;
        }
        
        private static void CreateSystemToken(int DesiredAccess, int dwSessionID, ref IntPtr TokenHandle)
        {
            int dwLsassPID = -1;
            int dwWinLogonPID = -1;
            WTS_PROCESS_INFO[] pProcesses;
            IntPtr pProcessInfo = IntPtr.Zero;

            int dwProcessCount = 0;

            if (WTSEnumerateProcesses((IntPtr)null, 0, 1, ref pProcessInfo, ref dwProcessCount))
            {
                IntPtr pMemory = pProcessInfo;
                pProcesses = new WTS_PROCESS_INFO[dwProcessCount];

                for (int i = 0; i < dwProcessCount; i++)
                {
                    pProcesses[i] = (WTS_PROCESS_INFO)Marshal.PtrToStructure(pProcessInfo, typeof(WTS_PROCESS_INFO));
                    pProcessInfo = (IntPtr)((long)pProcessInfo + Marshal.SizeOf(pProcesses[i]));

                    var processName = Marshal.PtrToStringAnsi(pProcesses[i].ProcessName);
                    ConvertSidToStringSid(pProcesses[i].UserSid, out string sid);

                    string strSid;
                    
                    if (processName == null || pProcesses[i].UserSid == default || sid != "S-1-5-18")
                        continue;

                    if ((-1 == dwLsassPID) && (0 == pProcesses[i].SessionID) && (processName == "lsass.exe"))
                    {
                        dwLsassPID = pProcesses[i].ProcessID;
                        continue;
                    }

                    if ((-1 == dwWinLogonPID) && (dwSessionID == pProcesses[i].SessionID) && (processName == "winlogon.exe"))
                    {
                        dwWinLogonPID = pProcesses[i].ProcessID;
                        continue;
                    }
                }

                WTSFreeMemory(pMemory);
            }

            bool Result = false;
            
            IntPtr SystemProcessHandle = IntPtr.Zero;


            try
            {
                SystemProcessHandle = Process.GetProcessById(dwLsassPID).Handle;
            } catch
            {
                SystemProcessHandle = Process.GetProcessById(dwWinLogonPID).Handle;
            }
            IntPtr SystemTokenHandle = IntPtr.Zero;
            if (OpenProcessToken(SystemProcessHandle, TOKEN_DUPLICATE, out SystemTokenHandle))
            {
                Result = DuplicateTokenEx(SystemTokenHandle, (uint)DesiredAccess, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityIdentification, TOKEN_TYPE.TokenPrimary, out TokenHandle);
                CloseHandle(SystemTokenHandle);
            }

            CloseHandle(SystemProcessHandle);

            // return Result;
            return;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
            uint processAccess,
            bool bInheritHandle,
            uint processId
        );
        public const UInt32 TOKEN_DUPLICATE = 0x0002;
        
        [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool ConvertSidToStringSid(IntPtr pSid, out string strSid);
        
        
        [DllImport("kernel32.dll", SetLastError=true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
        
        [DllImport("wtsapi32.dll", SetLastError=true)]
        static extern int WTSEnumerateSessions(
            System.IntPtr hServer,
            int Reserved,
            int Version,
            ref System.IntPtr ppSessionInfo,
            ref int pCount);
        
        
        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public Int32 SessionID;

            [MarshalAs(UnmanagedType.LPStr)]
            public String pWinStationName;

            public WTS_CONNECTSTATE_CLASS State;
        }
        public enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }
        [DllImport("wtsapi32.dll")]
        static extern void WTSFreeMemory(IntPtr pMemory);
        
        [DllImport("wtsapi32.dll", SetLastError=true)]
        static extern bool WTSEnumerateProcesses(
            IntPtr serverHandle, // Handle to a terminal server.
            Int32  reserved,     // must be 0
            Int32  version,      // must be 1
            ref IntPtr ppProcessInfo, // pointer to array of WTS_PROCESS_INFO
            ref Int32  pCount     // pointer to number of processes
        );
        struct WTS_PROCESS_INFO
        {
            public int SessionID;
            public int ProcessID;
            //This is a pointer to string...
            public IntPtr ProcessName;
            public IntPtr UserSid;
        }



        [DllImport("ntdll.dll", SetLastError = true)]
        static extern IntPtr RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);
        [DllImport("advapi32.dll")]
        static extern bool LookupPrivilegeValue(IntPtr lpSystemName, string lpName, ref UInt64 lpLuid);
        public static void GetOwnershipPrivilege()
        {
            ulong luid = 0;
            bool throwaway;
            LookupPrivilegeValue(IntPtr.Zero, "SeTakeOwnershipPrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, true, true, out throwaway);
        }
        public static void GetAssignPrivilege()
        {
            ulong luid = 0;
            bool throwaway;
            LookupPrivilegeValue(IntPtr.Zero, "SeAssignPrimaryTokenPrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, true, true, out throwaway);
        }
        public static void GetQuotaPrivilege()
        {
            ulong luid = 0;
            bool throwaway;
            LookupPrivilegeValue(IntPtr.Zero, "SeIncreaseQuotaPrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, true, true, out throwaway);
        }
        
        public static void GetShutdownPrivilege()
        {
            ulong luid = 0;
            bool throwaway;
            LookupPrivilegeValue(IntPtr.Zero, "SeShutdownPrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, true, true, out throwaway);
        }
        
        public class Win32 {
            
        }
    }
}