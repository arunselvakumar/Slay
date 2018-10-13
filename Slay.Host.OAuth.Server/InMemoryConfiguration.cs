namespace Slay.Host.OAuth.Server
{
    using System.Collections.Generic;

    using IdentityModel;

    using IdentityServer4;
    using IdentityServer4.Models;
    using IdentityServer4.Test;

    using ApiResource = IdentityServer4.Models.ApiResource;
    using Client = IdentityServer4.Models.Client;
    using Secret = IdentityServer4.Models.Secret;

    public static class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[] { new ApiResource { Name = "socialnetwork_fullaccess", DisplayName = "Social Network", Scopes = new List<Scope> { new Scope("socialnetwork_fullaccess") }} };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "socialnetwork",
                    AllowedGrantTypes = { GrantType.Implicit },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "socialnetwork_fullaccess" },
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = { "http://localhost:4200/assets/html/oidc/oidc-login-redirect.html" },
                    FrontChannelLogoutSessionRequired = false,
                    PostLogoutRedirectUris = { "http://localhost:4200/?postLogout=true" },
                    ClientSecrets = { new Secret("secret".ToSha256()) },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://127.0.0.1:4200",
                        "http://localhost:4200",
                        "*"
                    }
                }
            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[] { new TestUser { SubjectId = "1", Username = "arun", Password = "arun" } };
        }
    }
}
