﻿using IdentityServer4.Models;
using System.Collections.Generic;

namespace MyIdentityServer
{
  public static class Config
  {
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
      return new IdentityResource[]
      {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone()
      };
    }

    public static IEnumerable<ApiResource> GetApis()
    {
      return new ApiResource[]
      {
                new ApiResource("api1", "My API #1")
      };
    }

    public static IEnumerable<Client> GetClients()
    {
      return new[]
      {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("373f4671-0c18-48d6-9da3-962b1c81299a".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = { new Secret("373f4671-0c18-48d6-9da3-962b1c81299a".Sha256()) },

                    RedirectUris = { "http://localhost:5001/signin-oidc" },
                    LogoutUri = "http://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://localhost:3000/",
                    ClientSecrets = { new Secret("373f4671-0c18-48d6-9da3-962b1c81299a".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:3000/user/login"                        
                    },

                    PostLogoutRedirectUris = { "http://localhost:3000/index.html" },
                    AllowedCorsOrigins = { "http://localhost:3000" },

                    AllowedScopes = { "openid", "profile", "api1" }
                }
            };
    }
  }
}