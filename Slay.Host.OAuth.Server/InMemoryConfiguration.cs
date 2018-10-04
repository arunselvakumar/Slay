namespace Slay.Host.OAuth.Server
{
    using System.Collections.Generic;

    using IdentityModel;

    using IdentityServer4.Models;
    using IdentityServer4.Test;

    using ApiResource = IdentityServer4.Models.ApiResource;
    using Client = IdentityServer4.Models.Client;
    using Secret = IdentityServer4.Models.Secret;

    public static class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[] { new ApiResource { Name = "socialnetwork", DisplayName = "Social Network", Scopes = new List<Scope> { new Scope { Name = "socialnetwork_fullaccess" } } } };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[] { new Client
                               {
                                    ClientId = "socialnetwork",
                                    AllowedGrantTypes = { GrantType.Implicit },
                                    AllowedScopes = { "socialnetwork_fullaccess" },
                                    AccessTokenType = AccessTokenType.Jwt,
                                    RedirectUris = { "http://localhost:50366/signin-oidc" },
                                    PostLogoutRedirectUris = { "http://localhost:50366/signout-callback-oidc" },
                                    ClientSecrets = { new Secret("secret".ToSha256()) }
                               } };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[] { new TestUser { SubjectId = "1", Username = "arun", Password = "arun" } };
        }
    }
}
