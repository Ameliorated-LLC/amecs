namespace amecs.Extra
{
    public class WSH
    {
        public static bool Enable() => amecs.RunBasicAction("Enabling Windows Script Host", "Enabled WSH successfully", i =>
        {
            new Reg.Value()
            {
                KeyName = Globals.UserHive + @"\SOFTWARE\Microsoft\Windows Script Host\Settings",
                ValueName = "Enabled",
                Data = 1,
                Type = Reg.RegistryValueType.REG_DWORD
            }.Apply();
            new Reg.Value()
            {
                KeyName = @"HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings",
                ValueName = "Enabled",
                Data = 1,
                Type = Reg.RegistryValueType.REG_DWORD
            }.Apply();
        });
        public static bool Disable() => amecs.RunBasicAction("Disabling Windows Script Host", "Disabled WSH successfully", i =>
        {
            new Reg.Value()
            {
                KeyName = Globals.UserHive + @"\SOFTWARE\Microsoft\Windows Script Host\Settings",
                ValueName = "Enabled",
                Data = 0,
                Type = Reg.RegistryValueType.REG_DWORD
            }.Apply();
            new Reg.Value()
            {
                KeyName = @"HKLM\SOFTWARE\Microsoft\Windows Script Host\Settings",
                ValueName = "Enabled",
                Data = 0,
                Type = Reg.RegistryValueType.REG_DWORD
            }.Apply();
        });
    }
}