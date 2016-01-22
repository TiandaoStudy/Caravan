/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using AutoMapper;
using Finsa.Caravan.DataAccess.Sql.Identity.Entities;
using IdentityServer3.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Extensions
{
    public static class ModelsMap
    {
        static ModelsMap()
        {
            Mapper.CreateMap<Scope, SqlIdnScope>(MemberList.Source)
                .ForSourceMember(x => x.Claims, opts => opts.Ignore())
                .ForMember(x => x.ScopeClaims, opts => opts.MapFrom(src => src.Claims.Select(x => x)))
                .ForMember(x => x.ScopeSecrets, opts => opts.MapFrom(src => src.ScopeSecrets.Select(x => x)));

            Mapper.CreateMap<ScopeClaim, SqlIdnScopeClaim>(MemberList.Source);

            Mapper.CreateMap<Secret, SqlIdnClientSecret>(MemberList.Source);

            Mapper.CreateMap<Client, SqlIdnClient>(MemberList.Source)
                .ForMember(x => x.UpdateAccessTokenOnRefresh, opt => opt.MapFrom(src => src.UpdateAccessTokenClaimsOnRefresh))
                .ForMember(x => x.AllowAccessToAllCustomGrantTypes, opt => opt.MapFrom(src => src.AllowAccessToAllCustomGrantTypes))
                .ForMember(x => x.AllowedCustomGrantTypes, opt => opt.MapFrom(src => src.AllowedCustomGrantTypes.Select(x => new SqlIdnClientCustomGrantType { GrantType = x })))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => new SqlIdnClientRedirectUri { Uri = x })))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => new SqlIdnClientPostLogoutRedirectUri { Uri = x })))
                .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => new SqlIdnClientIdPRestriction { Provider = x })))
                .ForMember(x => x.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.Select(x => new SqlIdnClientScope { Scope = x })))
                .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => new SqlIdnClientCorsOrigin { Origin = x })))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new SqlIdnClientClaim { Type = x.Type, Value = x.Value })));
        }

        public static SqlIdnScope ToEntity(this Scope s)
        {
            if (s == null) return null;

            if (s.Claims == null)
            {
                s.Claims = new List<ScopeClaim>();
            }
            if (s.ScopeSecrets == null)
            {
                s.ScopeSecrets = new List<Secret>();
            }

            return Mapper.Map<Scope, SqlIdnScope>(s);
        }

        public static SqlIdnClient ToEntity(this Client s)
        {
            if (s == null) return null;

            if (s.ClientSecrets == null)
            {
                s.ClientSecrets = new List<Secret>();
            }
            if (s.RedirectUris == null)
            {
                s.RedirectUris = new List<string>();
            }
            if (s.PostLogoutRedirectUris == null)
            {
                s.PostLogoutRedirectUris = new List<string>();
            }
            if (s.AllowedScopes == null)
            {
                s.AllowedScopes = new List<string>();
            }
            if (s.IdentityProviderRestrictions == null)
            {
                s.IdentityProviderRestrictions = new List<string>();
            }
            if (s.Claims == null)
            {
                s.Claims = new List<Claim>();
            }
            if (s.AllowedCustomGrantTypes == null)
            {
                s.AllowedCustomGrantTypes = new List<string>();
            }
            if (s.AllowedCorsOrigins == null)
            {
                s.AllowedCorsOrigins = new List<string>();
            }

            return Mapper.Map<Client, SqlIdnClient>(s);
        }
    }
}
