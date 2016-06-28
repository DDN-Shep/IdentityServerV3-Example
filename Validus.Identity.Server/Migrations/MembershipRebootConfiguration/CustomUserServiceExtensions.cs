using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;

namespace Validus.Identity.Server.MembershipRebootConfiguration
{
    public static class CustomUserServiceExtensions
    {
        public static void ConfigureCustomUserService(this IdentityServerServiceFactory factory, string connectionString)
        {
            factory.UserService = new Registration<IUserService, CustomUserService>();
            factory.Register(new Registration<CustomUserAccountService>());
            factory.Register(new Registration<CustomConfig>(CustomConfig.Config));
            factory.Register(new Registration<CustomUserRepository>());
            factory.Register(new Registration<CustomDatabase>(resolver => new CustomDatabase(connectionString)));
        }
    }
}