using System.Collections.Generic;
using System.Linq;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;

namespace Validus.Identity.Server
{
    internal static class InMemory
    {
        public static List<Client> Clients => new List<Client>
        {
            new Client
            {
                Enabled = true,
                ClientName = "Systems Administrator Client",
                ClientId = "SystemAdmin",
                AccessTokenType = AccessTokenType.Jwt, // NOTE: Was "Reference"
                Flow = Flows.ClientCredentials,
                ClientSecrets = new List<Secret>
                {
                    new Secret("3537604D-13FB-432A-9942-4B7860B09FD0".Sha256())
                },
                AllowedScopes = StandardScopes.All.Select(o => o.Name).Concat(new[] { "read", "write", "webclaim" }).ToList(),
                AllowAccessToAllScopes = true
            }
        };

        public static List<Scope> Scopes => StandardScopes.All.Concat(new List<Scope>
        {
            new Scope
            {
                Name = "read"
            },
            new Scope
            {
                Name = "write"
            },
            new Scope
            {
                Name = "webclaim"
            }
        }).ToList();

        public static List<InMemoryUser> Users => new List<InMemoryUser>
        {
            new InMemoryUser
            {
                Username = "SystemAdmin",
                Password = "Talbot01",
                Subject = "1" // TODO: What is this?
            }
        };
    }
}