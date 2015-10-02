using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="Scope"/>.
    /// </summary>
    public class SqlIdnScope : Scope
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnScope"/>.
    /// </summary>
    public sealed class SqlIdnScopeTypeConfiguration : EntityTypeConfiguration<SqlIdnScope>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnScope"/>.
        /// </summary>
        public SqlIdnScopeTypeConfiguration()
        {
            ToTable("CRVN_IDN_SCOPES", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CSCO_ID");
        }
    }
}