using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientPostLogoutRedirectUri"/>.
    /// </summary>
    public class SqlIdnClientPostLogoutRedirectUri : ClientPostLogoutRedirectUri
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientPostLogoutRedirectUri"/>.
    /// </summary>
    public sealed class SqlIdnClientPostLogoutRedirectUriTypeConfiguration : EntityTypeConfiguration<SqlIdnClientPostLogoutRedirectUri>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientPostLogoutRedirectUri"/>.
        /// </summary>
        public SqlIdnClientPostLogoutRedirectUriTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_PST_LGT_RDR_URIS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CPLR_ID");
        }
    }
}