using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="Consent"/>.
    /// </summary>
    public class SqlIdnConsent : Consent
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnConsent"/>.
    /// </summary>
    public sealed class SqlIdnConsentTypeConfiguration : EntityTypeConfiguration<SqlIdnConsent>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnConsent"/>.
        /// </summary>
        public SqlIdnConsentTypeConfiguration()
        {
            ToTable("CRVN_IDN_CONSENTS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.ClientId).HasColumnName("CCLI_CLIENT_ID");
        }
    }
}