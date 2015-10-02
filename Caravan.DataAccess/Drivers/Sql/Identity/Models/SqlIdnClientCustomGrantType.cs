using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Models
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientCustomGrantType"/>.
    /// </summary>
    public class SqlIdnClientCustomGrantType : ClientCustomGrantType
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientCustomGrantType"/>.
    /// </summary>
    public sealed class SqlIdnClientCustomGrantTypeTypeConfiguration : EntityTypeConfiguration<SqlIdnClientCustomGrantType>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientCustomGrantType"/>.
        /// </summary>
        public SqlIdnClientCustomGrantTypeTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_CUSTOM_GRNT_TYPES", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCGT_ID");
        }
    }
}