﻿using IdentityManager;
using IdentityManager.Configuration;

namespace Validus.Identity.Server.MembershipRebootConfiguration
{
    public static class IdentityManagerServiceExtensions
    {
        public static void Configure(this IdentityManagerServiceFactory factory, string connectionString)
        {
            factory.IdentityManagerService = new Registration<IIdentityManagerService, CustomIdentityManagerService>();
            factory.Register(new Registration<CustomUserAccountService>());
            factory.Register(new Registration<CustomGroupService>());
            factory.Register(new Registration<CustomUserRepository>());
            factory.Register(new Registration<CustomGroupRepository>());
            factory.Register(new Registration<CustomDatabase>(resolver => new CustomDatabase(connectionString)));
            factory.Register(new Registration<CustomConfig>(CustomConfig.Config));
        }
    }
}
