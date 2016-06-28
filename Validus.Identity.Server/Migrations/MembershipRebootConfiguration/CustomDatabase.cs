using BrockAllen.MembershipReboot.Ef;

namespace Validus.Identity.Server.MembershipRebootConfiguration
{
    public class CustomDatabase : MembershipRebootDbContext<CustomUser, CustomGroup>
    {
        public CustomDatabase(string name)
            : base(name)
        { }
    }
}
