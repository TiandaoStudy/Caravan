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

using Finsa.Caravan.DataAccess.Sql.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="IdentityServer3.EntityFramework.Entities.ScopeSecret"/>.
    /// </summary>
    public class SqlIdnScopeSecret
    {
        [Key]
        public virtual int Id { get; set; }

        [StringLength(2000)]
        public virtual string Description { get; set; }

        [DateTimeKind(DateTimeKind.Utc)]
        public virtual DateTime? Expiration { get; set; }

        [StringLength(250)]
        public virtual string Type { get; set; }

        [Required]
        [StringLength(250)]
        public virtual string Value { get; set; }

        public virtual int ScopeId { get; set; }

        public virtual SqlIdnScope Scope { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnScopeSecret"/>.
    /// </summary>
    public sealed class SqlIdnScopeSecretTypeConfiguration : EntityTypeConfiguration<SqlIdnScopeSecret>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnScopeSecret"/>.
        /// </summary>
        public SqlIdnScopeSecretTypeConfiguration()
        {
            ToTable("CRVN_IDN_SCO_SECRETS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CSSE_ID");
            Property(x => x.ScopeId).HasColumnName("CSCO_ID");
            Property(x => x.Description).HasColumnName("CSSE_DESCR");
            Property(x => x.Expiration).HasColumnName("CSSE_EXPIRATION");
            Property(x => x.Type).HasColumnName("CSSE_TYPE");
            Property(x => x.Value).HasColumnName("CSSE_VALUE");

            // SqlIdnScopeClaim(N) <-> SqlIdnScope(1)
            HasRequired(x => x.Scope)
                .WithMany(x => x.ScopeSecrets)
                .HasForeignKey(x => x.ScopeId)
                .WillCascadeOnDelete();
        }
    }
}
