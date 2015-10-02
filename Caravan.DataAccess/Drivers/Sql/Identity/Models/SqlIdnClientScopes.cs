using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientScope"/>.
    /// </summary>
    public class SqlIdnClientScope : ClientScope
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientScope"/>.
    /// </summary>
    public sealed class SqlIdnClientScopeTypeConfiguration : EntityTypeConfiguration<SqlIdnClientScope>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientScope"/>.
        /// </summary>
        public SqlIdnClientScopeTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_SCOPES", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCSC_ID");
        }
    }
}