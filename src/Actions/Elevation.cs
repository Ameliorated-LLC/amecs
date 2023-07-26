using System;
using System.Threading;

namespace amecs.Actions
{
    public class Elevation
    {
        public static bool Elevate() => amecs.RunBasicAction("Elevating user to Administrator", "The current user is now an Administrator", new Action(() =>
        {
            Globals.Administrators.Members.Add(Globals.User);
            Globals.Administrators.Save();
            Thread.Sleep(1000);
        }), true);
        public static bool DeElevate() => amecs.RunBasicAction("Revoking Admin rights from the current user", "Admin rights have been revoked for the current user", new Action(() =>
        {
            Globals.Administrators.Members.Remove(Globals.User);
            Globals.Administrators.Save();
            Thread.Sleep(1000);
        }), true);
    }
}