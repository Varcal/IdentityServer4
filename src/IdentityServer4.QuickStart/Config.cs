using System.Collections.Generic;
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
                    Password = "123456"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "varcal",
                    Password = "123456"
                }
            };
        }
    }
}
