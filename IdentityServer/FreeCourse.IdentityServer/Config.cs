﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]{
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("photo_stock_catalog"){Scopes={"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name = "roles",DisplayName="roles",Description="İstifadəçi rolları",UserClaims=new[]{"role"} },
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Catalog API üçün full icazə"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API üçnü full icazə"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client
               {
                   ClientName="Asp.Net Core Mvc",
                   ClientId="WebMvcClient",
                   ClientSecrets={new Secret("secret".Sha512())},
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName}
               },
                new Client
               {
                   ClientName="Asp.Net Core Mvc",
                   ClientId="WebMvcClientForUser",
                   AllowOfflineAccess =true,
                   ClientSecrets={new Secret("secret".Sha512())},
                   AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                   AllowedScopes = {IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "roles"},
                   AccessTokenLifetime=1*60*60,
                   RefreshTokenExpiration= TokenExpiration.Absolute,
                   AbsoluteRefreshTokenLifetime=(int)(DateAndTime.Now.AddDays(60)-DateAndTime.Now).TotalSeconds,
                   RefreshTokenUsage = TokenUsage.ReUse
               }
            };
    }
}