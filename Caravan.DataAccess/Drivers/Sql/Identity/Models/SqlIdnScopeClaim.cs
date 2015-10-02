using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ScopeClaim"/>.
    /// </summary>
    public class SqlIdnScopeClaim : ScopeClaim
    {
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

            Property(x => x.Id).HasColumnName("CSCO_ID");
        }
    }
}