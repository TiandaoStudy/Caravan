// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Security.Entities
{
    /// <summary>
    ///   Tabella che censisce i claim degli utenti delle applicazioni FINSA.
    /// </summary>
    [Serializable]
    public class SqlSecClaim
    {
        /// <summary>
        ///   Identificativo riga, è una sequenza autoincrementale.
        /// </summary>
        [Key, Column("CCLA_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        /// <summary>
        ///   Identificativo dell'utente a cui un certo claim appartiene.
        /// </summary>
        [Required, Column("CUSR_ID", Order = 1)]
        [Index("UK_CRVN_SEC_CLAIMS", 0, IsUnique = true)]
        public virtual long UserId { get; set; }

        /// <summary>
        ///   Hash prodotto dopo la serializzazione del claim.
        /// </summary>
        [Required, Column("CCLA_HASH", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_CLAIMS", 1, IsUnique = true)]
        public virtual string Hash { get; set; }

        /// <summary>
        ///   Il claim serializzato.
        /// </summary>
        [Column("CCLA_CLAIM", Order = 3)]
        [DataType(DataType.Text)]
        public virtual string Claim { get; set; }

        #region Relationships

        /// <summary>
        ///   L'utente a cui appartiene il claim.
        /// </summary>
        public virtual SqlSecUser User { get; set; }

        #endregion Relationships
    }

    /// <summary>
    ///   Configurazione della mappatura per <see cref="SqlSecClaim"/>.
    /// </summary>
    public sealed class SqlSecClaimTypeConfiguration : EntityTypeConfiguration<SqlSecClaim>
    {
        /// <summary>
        ///   Configurazione della mappatura per <see cref="SqlSecClaim"/>.
        /// </summary>
        public SqlSecClaimTypeConfiguration()
        {
            ToTable("CRVN_SEC_CLAIMS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlSecClaim(N) <-> SqlSecUser(1)
            HasRequired(x => x.User)
                .WithMany(x => x.Claims)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}
