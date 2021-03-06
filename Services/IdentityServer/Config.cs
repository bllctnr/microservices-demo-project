// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {

        // Api resources for AUD
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){ Scopes = {"catalog_fullpermission"}},
            new ApiResource("resource_shoppingcart"){ Scopes = {"shoppingcart_fullpermission"}},
            new ApiResource("resource_order"){ Scopes = {"order_fullpermission"}},
            new ApiResource("resource_payment"){ Scopes = {"payment_fullpermission"}},
            new ApiResource("resource_gateway"){ Scopes = {"gateway_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       // Informations accessible by client
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(), // Sub
                       new IdentityResources.Profile(),
                       new IdentityResource(){ Name="roles", DisplayName="Roles", Description="User Roles", UserClaims = new[]{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Full permission for Catalog API"),
                new ApiScope("shoppingcart_fullpermission", "Full permission for shoppingcart"),
                new ApiScope("order_fullpermission", "Full permission for order API"),
                new ApiScope("payment_fullpermission", "Full permission for payment API"),
                new ApiScope("gateway_fullpermission", "Full permission for gateway"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Client",
                    ClientId = "WebClient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        "catalog_fullpermission", 
                        "gateway_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName,

                    }
                },
                new Client
                {
                    ClientName = "Client",
                    ClientId = "WebClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Client credentials does not include refresh token
                    AllowedScopes={
                        "catalog_fullpermission",
                        "shoppingcart_fullpermission",
                        "order_fullpermission",
                        "gateway_fullpermission",
                        IdentityServerConstants.StandardScopes.Email, 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile, 
                        IdentityServerConstants.StandardScopes.OfflineAccess, 
                        IdentityServerConstants.LocalApi.ScopeName,"roles" 
                    }, 
                    AccessTokenLifetime = 1*60*60, // 1 hour
                    RefreshTokenExpiration = TokenExpiration.Absolute, 
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
                new Client
                {
                    ClientName = "Token Exchange Client",
                    ClientId = "TokenExchangeClient",
                    AllowOfflineAccess = true,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = new[]{"urn:ietf:params:oauth:grant-type:token-exchange"}, // Client credentials does not include refresh token
                    AllowedScopes={
                        "payment_fullpermission",
                        IdentityServerConstants.StandardScopes.OpenId
                    },
                    AccessTokenLifetime = 1*60*60, // 1 hour
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }

            };
    }
}