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

using IdentityServer3.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="IdentityServer3.EntityFramework.Entities.Client"/>.
    /// </summary>
    public class SqlIdnClient
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual bool Enabled { get; set; }

        [Required]
        [StringLength(200)]
        [Index(IsUnique = true)]
        public virtual string ClientId { get; set; }

        public virtual ICollection<SqlIdnClientSecret> ClientSecrets { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string ClientName { get; set; }

        [StringLength(2000)]
        public virtual string ClientUri { get; set; }

        public virtual string LogoUri { get; set; }

        public virtual bool RequireConsent { get; set; }
        public virtual bool AllowRememberConsent { get; set; }

        public virtual Flows Flow { get; set; }
        public virtual bool AllowClientCredentialsOnly { get; set; }

        public virtual ICollection<SqlIdnClientRedirectUri> RedirectUris { get; set; }
        public virtual ICollection<SqlIdnClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        public virtual bool AllowAccessToAllScopes { get; set; }
        public virtual ICollection<SqlIdnClientScope> AllowedScopes { get; set; }

        // in seconds
        [Range(0, int.MaxValue)]
        public virtual int IdentityTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int AccessTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int AuthorizationCodeLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int AbsoluteRefreshTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int SlidingRefreshTokenLifetime { get; set; }

        public virtual TokenUsage RefreshTokenUsage { get; set; }
        public virtual bool UpdateAccessTokenOnRefresh { get; set; }

        public virtual TokenExpiration RefreshTokenExpiration { get; set; }

        public virtual AccessTokenType AccessTokenType { get; set; }

        public virtual bool EnableLocalLogin { get; set; }
        public virtual ICollection<SqlIdnClientIdPRestriction> IdentityProviderRestrictions { get; set; }

        public virtual bool IncludeJwtId { get; set; }

        public virtual ICollection<SqlIdnClientClaim> Claims { get; set; }
        public virtual bool AlwaysSendClientClaims { get; set; }
        public virtual bool PrefixClientClaims { get; set; }

        public virtual bool AllowAccessToAllGrantTypes { get; set; }

        public virtual ICollection<SqlIdnClientCustomGrantType> AllowedCustomGrantTypes { get; set; }
        public virtual ICollection<SqlIdnClientCorsOrigin> AllowedCorsOrigins { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClient"/>.
    /// </summary>
    public sealed class SqlIdnClientTypeConfiguration : EntityTypeConfiguration<SqlIdnClient>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClient"/>.
        /// </summary>
        public SqlIdnClientTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLIENTS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCLI_ID");
            Property(x => x.Enabled).HasColumnName("CCLI_ENABLED");
            Property(x => x.ClientId).HasColumnName("CCLI_CLIENT_ID");
            Property(x => x.ClientName).HasColumnName("CCLI_CLIENT_NAME");
            Property(x => x.LogoUri).HasColumnName("CCLI_LOGO_URI");
            Property(x => x.RequireConsent).HasColumnName("CCLI_REQUIRE_CONSENT");
            Property(x => x.AllowRememberConsent).HasColumnName("CCLI_ALLOW_REMEMBER_CONSENT");
            Property(x => x.Flow).HasColumnName("CCLI_FLOW");
            Property(x => x.AllowClientCredentialsOnly).HasColumnName("CCLI_ALLOW_CLIENT_CREDS_ONLY");
            Property(x => x.AllowAccessToAllScopes).HasColumnName("CCLI_ALLOW_ACCESS_TO_ALL_SCPS");
            Property(x => x.IdentityTokenLifetime).HasColumnName("CCLI_IDENTITY_TOKEN_LIFETIME");
            Property(x => x.AccessTokenLifetime).HasColumnName("CCLI_ACCESS_TOKEN_LIFETIME");
            Property(x => x.AuthorizationCodeLifetime).HasColumnName("CCLI_AUTH_CODE_LIFETIME");
            Property(x => x.AbsoluteRefreshTokenLifetime).HasColumnName("CCLI_ABS_REFR_TOKEN_LIFETIME");
            Property(x => x.SlidingRefreshTokenLifetime).HasColumnName("CCLI_SLID_REFR_TOKEN_LIFETIME");
            Property(x => x.RefreshTokenUsage).HasColumnName("CCLI_REFRESH_TOKEN_USAGE");
            Property(x => x.UpdateAccessTokenOnRefresh).HasColumnName("CCLI_UPD_ACCESS_TOKEN_ON_REFR");
            Property(x => x.RefreshTokenExpiration).HasColumnName("CCLI_REFRESH_TOKEN_EXPIRATION");
            Property(x => x.AccessTokenType).HasColumnName("CCLI_ACCESS_TOKEN_TYPE");
            Property(x => x.EnableLocalLogin).HasColumnName("CCLI_ENABLE_LOCAL_LOGIN");
            Property(x => x.IncludeJwtId).HasColumnName("CCLI_INCLUDE_JWT_ID");
            Property(x => x.AlwaysSendClientClaims).HasColumnName("CCLI_ALWAYS_SEND_CLIENT_CLAIMS");
            Property(x => x.PrefixClientClaims).HasColumnName("CCLI_PREFIX_CLIENT_CLAIMS");
            Property(x => x.AllowAccessToAllGrantTypes).HasColumnName("CCLI_ALLOW_ACCESS_TO_ALL_GRTP");

            // SqlLogEntry(N) <-> SqlSecApp(1)
            //HasRequired(x => x.App)
            //    .WithMany(x => x.LogEntries)
            //    .HasForeignKey(x => x.AppId)
            //    .WillCascadeOnDelete(true);

            // SqlLogEntry(N) <-> SqlLogSettings(1)
            //HasRequired(x => x.LogSetting)
            //    .WithMany(x => x.LogEntries)
            //    .HasForeignKey(x => new { x.AppId, x.LogLevel })
            //    .WillCascadeOnDelete(true);
        }
    }
}
