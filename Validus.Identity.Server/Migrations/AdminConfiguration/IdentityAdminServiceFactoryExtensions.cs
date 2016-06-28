using IdentityAdmin.Configuration;
using IdentityAdmin.Core;

namespace Validus.Identity.Server.AdminConfiguration
{
    public static class IdentityAdminServiceFactoryExtensions
    {
        public static void Configure(this IdentityAdminServiceFactory factory)
        {
            factory.IdentityAdminService = new Registration<IIdentityAdminService, IdentityAdminManagerService>();
        }
    }
}