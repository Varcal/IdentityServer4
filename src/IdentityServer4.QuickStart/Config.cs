using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer4.QuickStart
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Api","Minha Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"Api"}
                },
                new Client
                {
                    ClientId = "var.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"Api"}
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "Mvc Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "cleber",
                    Password = "123456",
                    Claims = new[]
                    {
                        new Claim("name", "Cleber Varçal"),
                        new Claim("website","http://varcalsys.com.br"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "varcal",
                    Password = "123456",
                    Claims = new[]
                    {
                        new Claim("name", "Cleber Varçal"),
                        new Claim("website","http://varcalsys.com.br"),
                    }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }
}
