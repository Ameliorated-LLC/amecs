using System;
using System.Threading;
using System.Threading.Tasks;

namespace amecs.Actions
{
    public class UsernameRequirement
    {
        public static Task<bool> Enable() => amecs.RunBasicActionTask("Enabling username login requirement", "The username login requirement is now enabled", new Action(() =>
        {
            new Reg.Value() { KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", ValueName = "dontdisplaylastusername", Data = 1, Type = Reg.RegistryValueType.REG_DWORD }.Apply();
            Thread.Sleep(1700);
        }));
        public static Task<bool> Disable() => amecs.RunBasicActionTask("Disabling username login requirement", "The username login requirement is now disabled", new Action(() =>
        {
            new Reg.Value() { KeyName = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", ValueName = "dontdisplaylastusername", Operation = Reg.RegistryValueOperation.Delete }.Apply();
            Thread.Sleep(1700);
        }));
    }
}