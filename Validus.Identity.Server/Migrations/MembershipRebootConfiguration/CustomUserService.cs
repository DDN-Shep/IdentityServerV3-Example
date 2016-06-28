using IdentityServer3.MembershipReboot;

namespace Validus.Identity.Server.MembershipRebootConfiguration
{
    public class CustomUserService : MembershipRebootUserService<CustomUser>
    {
        public CustomUserService(CustomUserAccountService userSvc)
            : base(userSvc)
        { }
    }
}
