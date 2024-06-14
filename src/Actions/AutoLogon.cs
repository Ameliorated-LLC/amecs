using System;
using System.DirectoryServices.AccountManagement;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ameliorated.ConsoleUtils;

namespace amecs.Actions
{
    public class AutoLogon
    {
        internal static class SafeNativeMethods
        {
            #region Structures

            [StructLayout(LayoutKind.Sequential)]
            public struct LSA_UNICODE_STRING : IDisposable
            {
                public UInt16 Length;
                public UInt16 MaximumLength;
                public IntPtr Buffer;

                public void Dispose()
                {
                    this = new LSA_UNICODE_STRING();
                }
            }

            public struct LSA_OBJECT_ATTRIBUTES
            {
                public int Length;
                public IntPtr RootDirectory;
                public LSA_UNICODE_STRING ObjectName;
                public UInt32 Attributes;
                public IntPtr SecurityDescriptor;
                public IntPtr SecurityQualityOfService;
            }

            public enum LSA_AccessPolicy : long
            {
                POLICY_VIEW_LOCAL_INFORMATION = 0x00000001L,
                POLICY_VIEW_AUDIT_INFORMATION = 0x00000002L,
                POLICY_GET_PRIVATE_INFORMATION = 0x00000004L,
                POLICY_TRUST_ADMIN = 0x00000008L,
                POLICY_CREATE_ACCOUNT = 0x00000010L,
                POLICY_CREATE_SECRET = 0x00000020L,
                POLICY_CREATE_PRIVILEGE = 0x00000040L,
                POLICY_SET_DEFAULT_QUOTA_LIMITS = 0x00000080L,
                POLICY_SET_AUDIT_REQUIREMENTS = 0x00000100L,
                POLICY_AUDIT_LOG_ADMIN = 0x00000200L,
                POLICY_SERVER_ADMIN = 0x00000400L,
                POLICY_LOOKUP_NAMES = 0x00000800L,
                POLICY_NOTIFICATION = 0x00001000L
            }

            #endregion

            #region DLL Imports

            [DllImport("advapi32")]
            public static extern IntPtr FreeSid(IntPtr pSid);

            [DllImport("advapi32.dll", PreserveSig = true)]
            public static extern UInt32 LsaOpenPolicy(
                ref LSA_UNICODE_STRING SystemName,
                ref LSA_OBJECT_ATTRIBUTES ObjectAttributes,
                Int32 DesiredAccess,
                out IntPtr PolicyHandle);

            [DllImport("advapi32.dll", SetLastError = true, PreserveSig = true)]
            public static extern uint LsaStorePrivateData(
                IntPtr PolicyHandle,
                LSA_UNICODE_STRING[] KeyName,
                LSA_UNICODE_STRING[] PrivateData);

            [DllImport("advapi32.dll", PreserveSig = true)]
            public static extern uint LsaRetrievePrivateData(
                IntPtr PolicyHandle,
                LSA_UNICODE_STRING[] KeyName,
                out IntPtr PrivateData);

            [DllImport("advapi32.dll", PreserveSig = true)]
            public static extern uint LsaNtStatusToWinError(uint status);

            [DllImport("advapi32.dll")]
            public static extern uint LsaClose(IntPtr ObjectHandle);

            #endregion
        }
        
        #region Functions

        /// <summary>
        /// Store Encrypted Data
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static long StoreData(String keyName, String Data)
        {
            long winErrorCode = 0; 
            IntPtr sid = IntPtr.Zero;
            int sidSize = 0;

            //allocate buffers
            sid = Marshal.AllocHGlobal(sidSize);

            //initialize an empty unicode-string
            SafeNativeMethods.LSA_UNICODE_STRING systemName = new SafeNativeMethods.LSA_UNICODE_STRING();
            
            //Set desired access rights (requested rights)
            int access = (int)(SafeNativeMethods.LSA_AccessPolicy.POLICY_CREATE_SECRET); 
            //initialize a pointer for the policy handle
            IntPtr policyHandle = IntPtr.Zero;

            //these attributes are not used, but LsaOpenPolicy wants them to exists
            SafeNativeMethods.LSA_OBJECT_ATTRIBUTES ObjectAttributes = new SafeNativeMethods.LSA_OBJECT_ATTRIBUTES();
            ObjectAttributes.Length = 0;
            ObjectAttributes.RootDirectory = IntPtr.Zero;
            ObjectAttributes.Attributes = 0;
            ObjectAttributes.SecurityDescriptor = IntPtr.Zero;
            ObjectAttributes.SecurityQualityOfService = IntPtr.Zero;

            //get a policy handle
            uint resultPolicy = SafeNativeMethods.LsaOpenPolicy(ref systemName, ref ObjectAttributes, access, out policyHandle);

            winErrorCode = SafeNativeMethods.LsaNtStatusToWinError(resultPolicy);

            if (winErrorCode != 0)
            {
                ConsoleTUI.OpenFrame.WriteCenteredLine("OpenPolicy failed: " + winErrorCode);
            }
            else
            {
                //initialize an unicode-string for the keyName
                SafeNativeMethods.LSA_UNICODE_STRING[] uKeyName = new SafeNativeMethods.LSA_UNICODE_STRING[1];
                uKeyName[0] = new SafeNativeMethods.LSA_UNICODE_STRING();
                uKeyName[0].Buffer = Marshal.StringToHGlobalUni(keyName);
                uKeyName[0].Length = (UInt16)(keyName.Length * UnicodeEncoding.CharSize);
                uKeyName[0].MaximumLength = (UInt16)((keyName.Length + 1) * UnicodeEncoding.CharSize);

                //initialize an unicode-string for the Data to encrypt
                SafeNativeMethods.LSA_UNICODE_STRING[] uData = new SafeNativeMethods.LSA_UNICODE_STRING[1];
                uData[0] = new SafeNativeMethods.LSA_UNICODE_STRING();
                uData[0].Buffer = Marshal.StringToHGlobalUni(Data);
                uData[0].Length = (UInt16)(Data.Length * UnicodeEncoding.CharSize);
                uData[0].MaximumLength = (UInt16)((Data.Length + 1) * UnicodeEncoding.CharSize);

                //Store Encrypted Data:
                SafeNativeMethods.LsaStorePrivateData(policyHandle, uKeyName, uData);

                //winErrorCode = LsaNtStatusToWinError(res);
                if (winErrorCode != 0)
                {
                    ConsoleTUI.OpenFrame.WriteCenteredLine("LsaStorePrivateData failed: " + winErrorCode);
                }

                SafeNativeMethods.LsaClose(policyHandle);
            }
            SafeNativeMethods.FreeSid(sid);
            return winErrorCode;
        }

        /// <summary>
        /// Retrieve Encrypted Data
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string RetrieveData(String keyName)
        {
            string sout = "";
            long winErrorCode = 0;
            IntPtr sid = IntPtr.Zero;
            int sidSize = 0;

            //allocate buffers
            sid = Marshal.AllocHGlobal(sidSize);

            //initialize an empty unicode-string
            SafeNativeMethods.LSA_UNICODE_STRING systemName = new SafeNativeMethods.LSA_UNICODE_STRING();

            //Set desired access rights (requested rights)
            int access = (int)(SafeNativeMethods.LSA_AccessPolicy.POLICY_CREATE_SECRET);
            //initialize a pointer for the policy handle
            IntPtr policyHandle = IntPtr.Zero;

            //these attributes are not used, but LsaOpenPolicy wants them to exists
            SafeNativeMethods.LSA_OBJECT_ATTRIBUTES ObjectAttributes = new SafeNativeMethods.LSA_OBJECT_ATTRIBUTES();
            ObjectAttributes.Length = 0;
            ObjectAttributes.RootDirectory = IntPtr.Zero;
            ObjectAttributes.Attributes = 0;
            ObjectAttributes.SecurityDescriptor = IntPtr.Zero;
            ObjectAttributes.SecurityQualityOfService = IntPtr.Zero;

            //get a policy handle
            uint resultPolicy = SafeNativeMethods.LsaOpenPolicy(ref systemName, ref ObjectAttributes, access, out policyHandle);

            winErrorCode = SafeNativeMethods.LsaNtStatusToWinError(resultPolicy);

            if (winErrorCode != 0)
            {
                ConsoleTUI.OpenFrame.WriteCenteredLine("OpenPolicy failed: " + winErrorCode);
            }
            else
            {
                //initialize an unicode-string for the keyName
                SafeNativeMethods.LSA_UNICODE_STRING[] uKeyName = new SafeNativeMethods.LSA_UNICODE_STRING[1];
                uKeyName[0] = new SafeNativeMethods.LSA_UNICODE_STRING();
                uKeyName[0].Buffer = Marshal.StringToHGlobalUni(keyName);
                uKeyName[0].Length = (UInt16)(keyName.Length * UnicodeEncoding.CharSize);
                uKeyName[0].MaximumLength = (UInt16)((keyName.Length + 1) * UnicodeEncoding.CharSize);

                //Store Encrypted Data:
                IntPtr pData;
                long result = SafeNativeMethods.LsaRetrievePrivateData(policyHandle, uKeyName, out pData);

                //winErrorCode = LsaNtStatusToWinError(res);
                if (winErrorCode != 0)
                {
                    ConsoleTUI.OpenFrame.WriteCenteredLine("LsaStorePrivateData failed: " + winErrorCode);
                }
                SafeNativeMethods.LSA_UNICODE_STRING ss = (SafeNativeMethods.LSA_UNICODE_STRING)Marshal.PtrToStructure(pData, typeof(SafeNativeMethods.LSA_UNICODE_STRING));
                sout = Marshal.PtrToStringAuto(ss.Buffer);

                SafeNativeMethods.LsaClose(policyHandle);
            }
            SafeNativeMethods.FreeSid(sid);


            return sout;
        }

        #endregion

        
        
        private const string LogonKey = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";

        public static bool Disable() => amecs.RunBasicAction("Disabling AutoLogon", "AutoLogon disabled successfully", new Action(() =>
        {
            new Reg.Value() { KeyName = LogonKey, ValueName = "DefaultUserName", Data = "", Type = Reg.RegistryValueType.REG_SZ}.Apply();
            new Reg.Value() { KeyName = LogonKey, ValueName = "AutoAdminLogon", Operation = Reg.RegistryValueOperation.Delete}.Apply();
            new Reg.Value() { KeyName = LogonKey, ValueName = "AutoLogonCount", Operation = Reg.RegistryValueOperation.Delete}.Apply();
            new Reg.Value() { KeyName = LogonKey, ValueName = "ForceAutoLogon", Operation = Reg.RegistryValueOperation.Delete}.Apply();
            new Reg.Value() { KeyName = LogonKey, ValueName = "DisableCAD", Operation = Reg.RegistryValueOperation.Delete}.Apply();
            new Reg.Value() { KeyName = LogonKey, ValueName = "DefaultPassword", Operation = Reg.RegistryValueOperation.Delete}.Apply();
            StoreData("DefaultPassword", "");
            
            Thread.Sleep(1700);
        }));
        
        public static bool Enable()
        {
            PrincipalContext context = new PrincipalContext(ContextType.Machine);

            string password = "";
            while (true)
            {
                password = new InputPrompt() { MaskInput = true, Text = "Enter your password, or press escape to quit: " }.Start();
                if (password == null)
                    return true;

                if (String.IsNullOrEmpty(password))
                {
                    try
                    {
                        Globals.User.ChangePassword("", "");
                        break;
                    } catch {}
                }
                else if (context.ValidateCredentials(Globals.Username, password))
                    break;
                
                ConsoleTUI.OpenFrame.WriteLine("Incorrect password.");
                Console.WriteLine();
            }
            try
            {
                ConsoleTUI.OpenFrame.WriteCentered("\r\nEnabling AutoLogon");
                using (new ConsoleUtils.LoadingIndicator(true))
                {
                    new Reg.Value() { KeyName = LogonKey, ValueName = "DefaultUserName", Data = Globals.Username, Type = Reg.RegistryValueType.REG_SZ}.Apply();
                    new Reg.Value() { KeyName = LogonKey, ValueName = "DefaultDomainName", Data = Environment.MachineName, Type = Reg.RegistryValueType.REG_SZ}.Apply();
                    new Reg.Value() { KeyName = LogonKey, ValueName = "AutoAdminLogon", Data = 1, Type = Reg.RegistryValueType.REG_DWORD}.Apply();
                    new Reg.Value() { KeyName = LogonKey, ValueName = "AutoLogonCount", Operation = Reg.RegistryValueOperation.Delete}.Apply();
                    new Reg.Value() { KeyName = LogonKey, ValueName = "DisableCAD", Data = 1, Type = Reg.RegistryValueType.REG_DWORD}.Apply();
                    new Reg.Value() { KeyName = LogonKey, ValueName = "DefaultPassword", Operation = Reg.RegistryValueOperation.Delete}.Apply();
                    StoreData("DefaultPassword", password);
            
                    Thread.Sleep(1700);
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
            ConsoleTUI.OpenFrame.Close($"AutoLogon enabled successfully", ConsoleColor.Green, Console.BackgroundColor, new ChoicePrompt()
            {
                AnyKey = true,
                Text = "Press any key to return to the Menu: "
            });
            return true;
            
        }
    }
}