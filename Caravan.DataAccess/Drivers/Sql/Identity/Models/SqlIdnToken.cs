using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="Token"/>.
    /// </summary>
    public class SqlIdnToken : Token
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnToken"/>.
    /// </summary>
    public sealed class SqlIdnTokenTypeConfiguration : EntityTypeConfiguration<SqlIdnToken>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnToken"/>.
        /// </summary>
        public SqlIdnTokenTypeConfiguration()
        {
            ToTable("CRVN_IDN_TOKENS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.ClientId).HasColumnName("CCLI_CLIENT_ID");
        }
    }
}