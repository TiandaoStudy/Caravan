using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientIdPRestriction"/>.
    /// </summary>
    public class SqlIdnClientIdPRestriction : ClientIdPRestriction
    {
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
            ToTable("CRVN_IDN_CLI_PROV_RESTRICTIONS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCPR_ID");
        }
    }
}