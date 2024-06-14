using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace amecs.Actions
{
    public class Elevation
    {
        public static Task<bool> Elevate() => amecs.RunBasicActionTask("Disabling enhanced security", "Enhanced security is now disabled", new Action(() =>
        {
            Globals.Administrators.Members.Add(Globals.User);
            Globals.Administrators.Save();
            Thread.Sleep(1000);
        }), true);
        public static Task<bool> DeElevate() => amecs.RunBasicActionTask("Enabling enhanced security", "Enhanced security is now enabled", new Action(() =>
        {
            using PrincipalContext context = new PrincipalContext(ContextType.Machine);

            using PrincipalSearcher userPrincipalSearcher = new PrincipalSearcher(new UserPrincipal(context));
            using var administrator = userPrincipalSearcher.FindAll().FirstOrDefault(x => (x is UserPrincipal) && x.Sid.IsWellKnown(WellKnownSidType.AccountAdministratorSid)) as UserPrincipal;
            administrator!.Enabled = true;
            administrator!.Save();
            Globals.Administrators.Members.Remove(Globals.User);
            Globals.Administrators.Save();
            Thread.Sleep(1000);
        }), false, true);
    }
}