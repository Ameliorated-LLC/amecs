using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace amecs
{
    public enum RegistryOperation
    {
        Delete = 0,
        Add = 1
    }

    public class Reg
    {
        public class Key
        {
            public string KeyName { get; set; }

            //public Scope Scope { get; set; } = Scope.AllUsers;
            public RegistryOperation Operation { get; set; } = RegistryOperation.Delete;

            private List<RegistryKey> GetRoots()
            {
                var hive = KeyName.Split('\\').GetValue(0).ToString().ToUpper();
                var list = new List<RegistryKey>();

                list.Add(hive switch
                {
                    "HKCU" => RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default),
                    "HKEY_CURRENT_USER" => RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default),
                    "HKLM" => RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default),
                    "HKEY_LOCAL_MACHINE" => RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default),
                    "HKCR" => RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default),
                    "HKEY_CLASSES_ROOT" => RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default),
                    "HKU" => RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default),
                    "HKEY_USERS" => RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default),
                    _ => throw new ArgumentException($"Key '{KeyName}' does not specify a valid registry hive.")
                });
                return list;
            }

            public string GetSubKey() => KeyName.Substring(KeyName.IndexOf('\\') + 1);

            public bool IsEqual()
            {
                try
                {
                    var roots = GetRoots();

                    foreach (var _root in roots)
                    {
                        var root = _root;
                        var subKey = GetSubKey();
                        var openedSubKey = root.OpenSubKey(subKey);

                        if (Operation == RegistryOperation.Delete && openedSubKey != null)
                        {
                            return false;
                        }

                        if (Operation == RegistryOperation.Add && openedSubKey == null)
                        {
                            return false;
                        }
                    }
                } catch (Exception e)
                {
                    return false;
                }

                return true;
            }

            public bool Apply()
            {
                var roots = GetRoots();

                foreach (var _root in roots)
                {
                    var root = _root;
                    var subKey = GetSubKey();
                    var openedSubKey = root.OpenSubKey(subKey);
                    if (openedSubKey != null) openedSubKey.Close();
                    

                    if (Operation == RegistryOperation.Add && openedSubKey == null)
                    {
                        root.CreateSubKey(subKey)?.Close();
                    }

                    if (Operation == RegistryOperation.Delete)
                    {
                        root.DeleteSubKeyTree(subKey, false);
                    }

                    root.Close();
                }

                return true;
            }
        }


        public enum RegistryValueOperation
        {
            Delete = 0,
            Add = 1,

            // This indicates to skip the action if the specified value does not already exist
            Set = 2
        }

        public enum RegistryValueType
        {
            REG_SZ = RegistryValueKind.String,
            REG_MULTI_SZ = RegistryValueKind.MultiString,
            REG_EXPAND_SZ = RegistryValueKind.ExpandString,
            REG_DWORD = RegistryValueKind.DWord,
            REG_QWORD = RegistryValueKind.QWord,
            REG_BINARY = RegistryValueKind.Binary,
            REG_NONE = RegistryValueKind.None,
            REG_UNKNOWN = RegistryValueKind.Unknown
        }

        public class Value
        {
            public string KeyName { get; set; }

            public string ValueName { get; set; } = "";

            public object? Data { get; set; }

            public RegistryValueType Type { get; set; }

            //public Scope Scope { get; set; } = Scope.AllUsers;

            public RegistryValueOperation Operation { get; set; } = RegistryValueOperation.Add;

            private List<RegistryKey> GetRoots()
            {
                var hive = KeyName.Split('\\').GetValue(0).ToString().ToUpper();
                var list = new List<RegistryKey>();

                list.Add(hive switch
                {
                    "HKCU" => RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default),
                    "HKEY_CURRENT_USER" => RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default),
                    "HKLM" => RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default),
                    "HKEY_LOCAL_MACHINE" => RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default),
                    "HKCR" => RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default),
                    "HKEY_CLASSES_ROOT" => RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default),
                    "HKU" => RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default),
                    "HKEY_USERS" => RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default),
                    _ => throw new ArgumentException($"Key '{KeyName}' does not specify a valid registry hive.")
                });
                return list;
            }

            public string GetSubKey() => KeyName.Substring(KeyName.IndexOf('\\') + 1);

            public object? GetCurrentValue(RegistryKey root)
            {
                var subkey = GetSubKey();
                return Registry.GetValue(root.Name + "\\" + subkey, ValueName, null);
            }

            public static byte[] StringToByteArray(string hex)
            {
                return Enumerable.Range(0, hex.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                    .ToArray();
            }

            public bool IsEqual()
            {
                try
                {
                    var roots = GetRoots();

                    foreach (var _root in roots)
                    {
                        var root = _root;
                        var subKey = GetSubKey();

                        var openedSubKey = root.OpenSubKey(subKey);

                        if (openedSubKey == null && (Operation == RegistryValueOperation.Set || Operation == RegistryValueOperation.Delete))
                            continue;
                        if (openedSubKey == null) return false;

                        var value = openedSubKey.GetValue(ValueName);

                        if (value == null)
                        {
                            if (Operation == RegistryValueOperation.Set || Operation == RegistryValueOperation.Delete)
                                continue;

                            return false;
                        }

                        if (Operation == RegistryValueOperation.Delete) return false;

                        if (Data == null) return false;


                        bool matches;
                        try
                        {
                            matches = Type switch
                            {
                                RegistryValueType.REG_SZ =>
                                    Data.ToString() == value.ToString(),
                                RegistryValueType.REG_EXPAND_SZ =>
                                    // RegistryValueOptions.DoNotExpandEnvironmentNames above did not seem to work.
                                    Environment.ExpandEnvironmentVariables(Data.ToString()) == value.ToString(),
                                RegistryValueType.REG_MULTI_SZ =>
                                    Data.ToString() == "" ? ((string[])value).SequenceEqual(new string[] { }) : ((string[])value).SequenceEqual(Data.ToString().Split(new string[] { "\\0" }, StringSplitOptions.None)),
                                RegistryValueType.REG_DWORD =>
                                    unchecked((int)Convert.ToUInt32(Data)) == (int)value,
                                RegistryValueType.REG_QWORD =>
                                    Convert.ToUInt64(Data) == (ulong)value,
                                RegistryValueType.REG_BINARY =>
                                    ((byte[])value).SequenceEqual(StringToByteArray(Data.ToString())),
                                RegistryValueType.REG_NONE =>
                                    ((byte[])value).SequenceEqual(new byte[0]),
                                RegistryValueType.REG_UNKNOWN =>
                                    Data.ToString() == value.ToString(),
                                _ => throw new ArgumentException("Impossible.")
                            };
                        } catch (InvalidCastException)
                        {
                            matches = false;
                        }

                        if (!matches) return false;
                    }
                } catch (Exception e)
                {
                    return false;
                }

                return true;
            }

            public bool Apply()
            {
                var roots = GetRoots();

                foreach (var _root in roots)
                {
                    var root = _root;
                    var subKey = GetSubKey();

                    if (GetCurrentValue(root) == Data) continue;

                    var opened = root.OpenSubKey(subKey);
                    if (opened == null && Operation == RegistryValueOperation.Set) continue;
                    if (opened == null && Operation == RegistryValueOperation.Add) root.CreateSubKey(subKey)?.Close();
                    if (opened != null) opened.Close();

                    if (Operation == RegistryValueOperation.Delete)
                    {
                        var key = root.OpenSubKey(subKey, true);
                        key?.DeleteValue(ValueName);
                        key?.Close();
                        root.Close();
                        continue;
                    }

                    if (Type == RegistryValueType.REG_BINARY)
                    {
                        var data = StringToByteArray(Data.ToString());

                        Registry.SetValue(root.Name + "\\" + subKey, ValueName, data, (RegistryValueKind)Type);
                    }
                    else if (Type == RegistryValueType.REG_DWORD)
                    {
                        // DWORD values using the highest bit set fail without this, for example '2962489444'.
                        // See https://stackoverflow.com/questions/6608400/how-to-put-a-dword-in-the-registry-with-the-highest-bit-set;
                        var value = unchecked((int)Convert.ToUInt32(Data));
                        Registry.SetValue(root.Name + "\\" + subKey, ValueName, value, (RegistryValueKind)Type);
                    }
                    else if (Type == RegistryValueType.REG_QWORD)
                    {
                        Registry.SetValue(root.Name + "\\" + subKey, ValueName, Convert.ToUInt64(Data), (RegistryValueKind)Type);
                    }
                    else if (Type == RegistryValueType.REG_NONE)
                    {
                        byte[] none = new byte[0];

                        Registry.SetValue(root.Name + "\\" + subKey, ValueName, none, (RegistryValueKind)Type);
                    }
                    else if (Type == RegistryValueType.REG_MULTI_SZ)
                    {
                        string[] data;
                        if (Data.ToString() == "") data = new string[] { };
                        else data = Data.ToString().Split(new string[] { "\\0" }, StringSplitOptions.None);

                        Registry.SetValue(root.Name + "\\" + subKey, ValueName, data, (RegistryValueKind)Type);
                    }
                    else
                    {
                        Registry.SetValue(root.Name + "\\" + subKey, ValueName, Data, (RegistryValueKind)Type);
                    }

                    root.Close();
                }

                return true;
            }
        }


        [DllImport("advapi32.dll", SetLastError = true)]
        static extern int RegLoadKey(IntPtr hKey, string lpSubKey, string lpFile);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern int RegSaveKey(IntPtr hKey, string lpFile, uint securityAttrPtr = 0);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern int RegUnLoadKey(IntPtr hKey, string lpSubKey);

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern IntPtr RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("advapi32.dll")]
        static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, ref UInt64 lpLuid);

        [DllImport("advapi32.dll")]
        static extern bool LookupPrivilegeValue(IntPtr lpSystemName, string lpName, ref UInt64 lpLuid);

        public static void LoadDefaultUserHive()
        {
            var parentKey = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default);

            IntPtr parentHandle = parentKey.Handle.DangerousGetHandle();
            AcquirePrivileges();
            RegLoadKey(parentHandle, "DefaultUserHive", Environment.ExpandEnvironmentVariables(@"%SYSTEMDRIVE%\Users\Default\NTUSER.dat"));
            parentKey.Close();
        }

        public static void UnloadDefaultUserHive()
        {
            var parentKey = RegistryKey.OpenBaseKey(RegistryHive.Users, RegistryView.Default);
            AcquirePrivileges();
            RegUnLoadKey(parentKey.Handle.DangerousGetHandle(), "DefaultUserHive");
            parentKey.Close();
        }

        public static void AcquirePrivileges()
        {
            ulong luid = 0;
            bool throwaway;
            LookupPrivilegeValue(IntPtr.Zero, "SeRestorePrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, true, true, out throwaway);
            LookupPrivilegeValue(IntPtr.Zero, "SeBackupPrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, true, true, out throwaway);
        }

        public static void ReturnPrivileges()
        {
            ulong luid = 0;
            bool throwaway;
            LookupPrivilegeValue(IntPtr.Zero, "SeRestorePrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, false, true, out throwaway);
            LookupPrivilegeValue(IntPtr.Zero, "SeBackupPrivilege", ref luid);
            RtlAdjustPrivilege((int)luid, false, true, out throwaway);
        }
    }
}