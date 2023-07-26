using System;
using System.Threading;

namespace amecs.Actions
{
    public class UsernameRequirement
    {
        public static bool Enable() => amecs.RunBasicAction("Enabling username login requirement", "The username login requirement is now enabled", new Action(() =>
        {
            new Reg.Value() { KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", ValueName = "dontdisplaylastusername", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
            Thread.Sleep(1700);
        }));
        public static bool Disable() => amecs.RunBasicAction("Disabling username login requirement", "The username login requirement is now disabled", new Action(() =>
        {
            new Reg.Value() { KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", ValueName = "dontdisplaylastusername", Operation = Reg.RegistryValueOperation.Delete }.Apply();
            Thread.Sleep(1700);
        }));
    }
}