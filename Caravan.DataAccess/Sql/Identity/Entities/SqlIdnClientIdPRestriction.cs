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
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientIdPRestriction"/>.
    /// </summary>
    public class SqlIdnClientIdPRestriction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string Provider { get; set; }

        public virtual int ClientId { get; set; }

        public virtual SqlIdnClient Client { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientIdPRestriction"/>.
    /// </summary>
    public sealed class SqlIdnClientIdPRestrictionTypeConfiguration : EntityTypeConfiguration<SqlIdnClientIdPRestriction>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientIdPRestriction"/>.
        /// </summary>
        public SqlIdnClientIdPRestrictionTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_IDPR_RESTRS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCPR_ID");
            Property(x => x.ClientId).HasColumnName("CCLI_ID");
            Property(x => x.Provider).HasColumnName("CCPR_PROVIDER");

            // SqlIdnClientIdPRestriction(N) <-> SqlIdnClient(1)
            HasRequired(x => x.Client)
                .WithMany(x => x.IdentityProviderRestrictions)
                .HasForeignKey(x => x.ClientId)
                .WillCascadeOnDelete();
        }
    }
}
