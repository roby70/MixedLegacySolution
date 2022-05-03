using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using MLS;

namespace MSL.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new("myApiScope", "My API")
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            // oldApp interactive ASP.NET Core MVC client
            new()
            {
                ClientName = MlsEnvironment.LegacyAppInfo.ClientName,
                ClientId = MlsEnvironment.LegacyAppInfo.ClientId,
                ClientSecrets = { new Secret(MlsEnvironment.LegacyAppInfo.ClientSecret.Sha256()) },
                AllowedGrantTypes = GrantTypes.Implicit,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { "https://localhost:44360/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:44360" },
                AllowedScopes =new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "myApiScope"
                }
            },
            new ()
            {
                ClientName = "Angular Single Page Application",
                ClientId = "AngularApp",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "myApiScope"
                },
                RedirectUris = { "http://localhost:4200/auth-callback" },
                PostLogoutRedirectUris = { "http://localhost:4200/" },
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowAccessTokensViaBrowser = true
            }
        };

        public static List<TestUser> TestUsers => new List<TestUser>
        {
            new()
            {
                SubjectId = "71A932C4-C9A6-4B4D-81CF-C8409B98EA4F", Username = "roby", Password = "secret",
                Claims = new []
                {
                    new Claim("name", "Roberto Ferraris")
                },
                
            }
        };
    }
}
