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
        [Column("CGRP_DESCR", Order = 3)]
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
