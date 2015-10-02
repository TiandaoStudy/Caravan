using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientSecret"/>.
    /// </summary>
    public class SqlIdnClientSecret : ClientSecret
    {
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
        }
    }
}