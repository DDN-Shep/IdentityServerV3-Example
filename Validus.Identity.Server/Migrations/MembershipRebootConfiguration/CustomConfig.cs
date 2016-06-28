using BrockAllen.MembershipReboot;

namespace Validus.Identity.Server.MembershipRebootConfiguration
{
    public class CustomConfig : MembershipRebootConfiguration<CustomUser>
    {
        public static readonly CustomConfig Config = new CustomConfig();

        public CustomConfig()
        {
            PasswordHashingIterationCount = 10000;
            RequireAccountVerification = false;
            //EmailIsUsername = true;
        }
    }
}
