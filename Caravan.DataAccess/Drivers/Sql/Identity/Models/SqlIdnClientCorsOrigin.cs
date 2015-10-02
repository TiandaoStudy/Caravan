using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientCorsOrigin"/>.
    /// </summary>
    public class SqlIdnClientCorsOrigin : ClientCorsOrigin
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientCorsOrigin"/>.
    /// </summary>
    public sealed class SqlIdnClientCorsOriginTypeConfiguration : EntityTypeConfiguration<SqlIdnClientCorsOrigin>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientCorsOrigin"/>.
        /// </summary>
        public SqlIdnClientCorsOriginTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_CORS_ORIGINS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCCO_ID");
        }
    }
}