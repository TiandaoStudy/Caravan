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

using Finsa.CodeServices.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using IdentityServer3.Core.Models;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="IdentityServer3.EntityFramework.Entities.Scope"/>.
    /// </summary>
    public class SqlIdnScope
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual bool Enabled { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }

        [StringLength(200)]
        public virtual string DisplayName { get; set; }

        [StringLength(1000)]
        public virtual string Description { get; set; }

        public virtual bool Required { get; set; }
        public virtual bool Emphasize { get; set; }

        [NotMapped]
        public ScopeType Type { get; set; }

        [StringLength(100)]
        public virtual string TypeString
        {
            get { return Type.ToString().ToLowerInvariant(); }
            set { Type = value.ToEnum<ScopeType>(); }
        }

        public virtual ICollection<SqlIdnScopeClaim> ScopeClaims { get; set; }

        public virtual bool IncludeAllClaimsForUser { get; set; }

        [StringLength(200)]
        public virtual string ClaimsRule { get; set; }

        public virtual bool ShowInDiscoveryDocument { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnScope"/>.
    /// </summary>
    public sealed class SqlIdnScopeTypeConfiguration : EntityTypeConfiguration<SqlIdnScope>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnScope"/>.
        /// </summary>
        public SqlIdnScopeTypeConfiguration()
        {
            ToTable("CRVN_IDN_SCOPES", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CSCO_ID");
            Property(x => x.Enabled).HasColumnName("CSCO_ENABLED");
            Property(x => x.Name).HasColumnName("CSCO_SCOPE_NAME");
            Property(x => x.DisplayName).HasColumnName("CSCO_DISPLAY_NAME");
            Property(x => x.Description).HasColumnName("CSCO_DESCR");
            Property(x => x.Required).HasColumnName("CSCO_REQUIRED");
            Property(x => x.Emphasize).HasColumnName("CSCO_EMPHASIZE");
            Property(x => x.TypeString).HasColumnName("CSCO_TYPE");
            Property(x => x.IncludeAllClaimsForUser).HasColumnName("CSCO_INCL_ALL_CLAIMS_FOR_USER");
            Property(x => x.ClaimsRule).HasColumnName("CSCO_CLAIMS_RULE");
            Property(x => x.ShowInDiscoveryDocument).HasColumnName("CSCO_SHOW_IN_DISCOVERY_DOC");
        }
    }
}