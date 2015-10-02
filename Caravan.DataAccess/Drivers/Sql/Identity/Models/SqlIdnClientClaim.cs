using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientClaim"/>.
    /// </summary>
    public class SqlIdnClientClaim : ClientClaim
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientClaim"/>.
    /// </summary>
    public sealed class SqlIdnClientClaimTypeConfiguration : EntityTypeConfiguration<SqlIdnClientClaim>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientClaim"/>.
        /// </summary>
        public SqlIdnClientClaimTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_CLAIMS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCLM_ID");
        }
    }
}