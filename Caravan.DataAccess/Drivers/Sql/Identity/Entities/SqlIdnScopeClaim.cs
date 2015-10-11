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

using IdentityServer3.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="ScopeClaim"/>.
    /// </summary>
    public class SqlIdnScopeClaim
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }

        [StringLength(1000)]
        public virtual string Description { get; set; }

        public virtual bool AlwaysIncludeInIdToken { get; set; }

        public virtual int ScopeId { get; set; }

        public virtual SqlIdnScope Scope { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnScopeClaim"/>.
    /// </summary>
    public sealed class SqlIdnScopeClaimTypeConfiguration : EntityTypeConfiguration<SqlIdnScopeClaim>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnScopeClaim"/>.
        /// </summary>
        public SqlIdnScopeClaimTypeConfiguration()
        {
            ToTable("CRVN_IDN_SCO_CLAIMS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CSCL_ID");
            Property(x => x.ScopeId).HasColumnName("CSCO_ID");
            Property(x => x.Name).HasColumnName("CSCL_NAME");
            Property(x => x.Description).HasColumnName("CSCL_DESCR");
            Property(x => x.AlwaysIncludeInIdToken).HasColumnName("CSCL_ALWAYS_INCL_IN_TOKEN");

            // SqlIdnScopeClaim(N) <-> SqlIdnScope(1)
            HasRequired(x => x.Scope)
                .WithMany(x => x.ScopeClaims)
                .HasForeignKey(x => x.ScopeId)
                .WillCascadeOnDelete();
        }
    }
}
