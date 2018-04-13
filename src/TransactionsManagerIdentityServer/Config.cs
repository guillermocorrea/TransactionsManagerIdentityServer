using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace TransactionsManagerIdentityServer
{
    public class Config
    {
        private const string API_RESOURCE_NAME = "transactions-api";
        private IConfigurationRoot _configuration;

        public Config(IConfigurationRoot configutarion)
        {
            _configuration = configutarion;
        }

        public IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = _configuration.GetSection("Clients:ApiClient:ClientId").Value,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(_configuration.GetSection("Clients:ApiClient:ClientSecret").Value.Sha256())
                    },

                    AllowedScopes = { API_RESOURCE_NAME }
                },
                new Client
                {
                    ClientId = _configuration.GetSection("Clients:MvcClient:ClientId").Value,
                    ClientName = _configuration.GetSection("Clients:MvcClient:ClientName").Value,
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(_configuration.GetSection("Clients:MvcClient:ClientSecret").Value.Sha256())
                    },
                    RedirectUris = { _configuration.GetSection("Clients:MvcClient:RedirectUris").Value },
                    PostLogoutRedirectUris = { _configuration.GetSection("Clients:MvcClient:PostLogoutRedirectUris").Value },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        API_RESOURCE_NAME
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource(API_RESOURCE_NAME, "Transactions API")
            };
        }

        public List<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "assistant",
                    Password = _configuration.GetSection("Users:Assistant:Password").Value,
                    Claims = new List<Claim> {
                        new Claim("name", "Assistant"),
                        new Claim(JwtClaimTypes.Email, "assistant@email.com"),
                        new Claim(JwtClaimTypes.Role, "assistant")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "manager",
                    Password = _configuration.GetSection("Users:Manager:Password").Value,
                    Claims = new List<Claim> {
                        new Claim("name", "Manager"),
                        new Claim(JwtClaimTypes.Email, "manager@email.com"),
                        new Claim(JwtClaimTypes.Role, "manager")
                    }
                },
                new TestUser
                {
                    SubjectId = "3",
                    Username = "administrator",
                    Password = _configuration.GetSection("Users:Administrator:Password").Value,
                    Claims = new List<Claim> {
                        new Claim("name", "Administrator"),
                        new Claim(JwtClaimTypes.Email, "administrator@email.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
        }
    }
}
