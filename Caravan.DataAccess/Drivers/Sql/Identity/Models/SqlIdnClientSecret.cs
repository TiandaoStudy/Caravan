using IdentityServer3.EntityFramework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientSecret"/>.
    /// </summary>
    public class SqlIdnClientSecret
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(250)]
        public virtual string Value { get; set; }

        [StringLength(250)]
        public string Type { get; set; }

        [StringLength(2000)]
        public virtual string Description { get; set; }

        public virtual DateTimeOffset? Expiration { get; set; }

        public virtual SqlIdnClient Client { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientSecret"/>.
    /// </summary>
    public sealed class SqlIdnClientSecretTypeConfiguration : EntityTypeConfiguration<SqlIdnClientSecret>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientSecret"/>.
        /// </summary>
        public SqlIdnClientSecretTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_SECRETS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCSE_ID");
            Property(x => x.Value).HasColumnName("CCSE_VALUE");
            Property(x => x.Type).HasColumnName("CCSE_TYPE");
            Property(x => x.Description).HasColumnName("CCSE_DESCRIPTION");
            Property(x => x.Expiration).HasColumnName("CCSE_EXPIRATION");

            // SqlIdnClientSecret(N) <-> SqlIdnClient(1)
            HasRequired(x => x.Client)
                .WithMany(x => x.ClientSecrets)
                .WillCascadeOnDelete();
        }
    }
}
