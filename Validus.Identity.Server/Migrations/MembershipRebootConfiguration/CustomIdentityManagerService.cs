using IdentityManager.MembershipReboot;

namespace Validus.Identity.Server.MembershipRebootConfiguration
{
    public class CustomIdentityManagerService : MembershipRebootIdentityManagerService<CustomUser, CustomGroup>
    {
        public CustomIdentityManagerService(CustomUserAccountService userSvc, CustomGroupService groupSvc)
            : base(userSvc, groupSvc)
        { }
    }
}
