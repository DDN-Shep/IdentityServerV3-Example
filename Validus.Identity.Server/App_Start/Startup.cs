//using IdentityAdmin.Configuration;
using IdentityManager.Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Pysco68.Owin.Authentication.Ntlm;
using System.Collections.Generic;
using System.Linq;
//using Validus.Identity.Server.AdminConfiguration;
using Validus.Identity.Server.MembershipRebootConfiguration;

using IdentityAuthenticationOptions = IdentityServer3.Core.Configuration.AuthenticationOptions;
using OwinAuthenticationDescription = Microsoft.Owin.Security.AuthenticationDescription;

namespace Validus.Identity.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "Cookie",
            //    /* TODO: Add admin security
            //    LoginPath = new PathString("/core/login")
            //    */
            //});

            //var options = new NtlmAuthenticationOptions
            //{
            //    SignInAsAuthenticationType = "Ntlm",
            //    AuthenticationMode = AuthenticationMode.Active,
            //    Description = new OwinAuthenticationDescription
            //    {
            //        AuthenticationType = NtlmAuthenticationDefaults.AuthenticationType,
            //        Caption = "NTLM"
            //    },
            //    CallbackPath = new PathString("/membership/ntlm-signin")
            //};

            //app.UseNtlmAuthentication(options);

            //app.Map("/admin", admin =>
            //{
            //    var factory = new IdentityAdminServiceFactory();

            //    factory.Configure();

            //    admin.UseIdentityAdmin(new IdentityAdminOptions
            //    {
            //        Factory = factory
            //    });
            //});

            app.Map("/membership", membership =>
            {
                var factory = new IdentityManagerServiceFactory();

                factory.Configure(Config.MembershipDb);

                membership.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = factory,
                    /* TODO: Add admin security
                    SecurityConfiguration = new HostSecurityConfiguration
                    {
                        HostAuthenticationType = "Ntlm",
                        NameClaimType = "name",
                        RoleClaimType = "role",
                        AdminRoleName = "Admin"
                    }
                    */
                });
            });

            app.Map("/core", core =>
            {
                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(InMemory.Clients)
                    .UseInMemoryScopes(InMemory.Scopes);

                var ef = new EntityFrameworkServiceOptions
                {
                    ConnectionString = Config.HostDb
                };

                ConfigureClients(InMemory.Clients, ef);
                ConfigureScopes(InMemory.Scopes, ef);

                factory.ConfigureCustomUserService(Config.MembershipDb);

                factory.RegisterConfigurationServices(ef);
                factory.RegisterOperationalServices(ef);

                factory.ConfigureClientStoreCache();
                factory.ConfigureScopeStoreCache();
                factory.ConfigureUserServiceCache();

                core.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Validus Identity Server",
                    SigningCertificate = Certificate.Get(),
                    Factory = factory,
                    Endpoints = new EndpointOptions
                    {
                        EnableAccessTokenValidationEndpoint = true
                    },
                    AuthenticationOptions = new IdentityAuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders,
                        EnablePostSignOutAutoRedirect = false
                    }
                });
            });
        }

        public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            using (var db = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Clients.Any())
                {
                    foreach (var c in clients)
                    {
                        var e = c.ToEntity();
                        db.Clients.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }

        public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            using (var db = new ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Scopes.Any())
                {
                    foreach (var s in scopes)
                    {
                        var e = s.ToEntity();
                        db.Scopes.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }

        public static void ConfigureIdentityProviders(IAppBuilder app, string type)
        {
            app.UseNtlmAuthentication(new NtlmAuthenticationOptions
            {
                SignInAsAuthenticationType = type,
                AuthenticationMode = AuthenticationMode.Active,
                Description = new OwinAuthenticationDescription
                {
                    AuthenticationType = NtlmAuthenticationDefaults.AuthenticationType,
                    Caption = "NTLM"
                },
                CallbackPath = new PathString("/core/ntlm-signin")
            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                SignInAsAuthenticationType = type,
                ClientId = "372935815617-f58vm1itfkperuh5bunfhcm433dountq.apps.googleusercontent.com",
                ClientSecret = "VjPn0EA2J2INqCDsnVRxMSLd"
            });
        }
    }
}
