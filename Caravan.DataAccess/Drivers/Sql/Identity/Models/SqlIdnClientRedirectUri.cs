using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientRedirectUri"/>.
    /// </summary>
    public class SqlIdnClientRedirectUri : ClientRedirectUri
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientRedirectUri"/>.
    /// </summary>
    public sealed class SqlIdnClientRedirectUriTypeConfiguration : EntityTypeConfiguration<SqlIdnClientRedirectUri>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientRedirectUri"/>.
        /// </summary>
        public SqlIdnClientRedirectUriTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_REDIRECT_URIS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCRU_ID");
        }
    }
}