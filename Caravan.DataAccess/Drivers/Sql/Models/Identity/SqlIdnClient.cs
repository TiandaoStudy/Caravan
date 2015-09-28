using IdentityServer3.EntityFramework.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Identity
{
    /// <summary>
    ///   Riferimento interno per <see cref="Client"/>.
    /// </summary>
    public class SqlIdnClient : Client
    {
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClient"/>.
    /// </summary>
    public sealed class SqlIdnClientTypeConfiguration : EntityTypeConfiguration<SqlIdnClient>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClient"/>.
        /// </summary>
        public SqlIdnClientTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLIENTS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCLI_ID");
            Property(x => x.Enabled).HasColumnName("CCLI_ENABLED");
            Property(x => x.ClientId).HasColumnName("CCLI_CLIENT_ID");

            // SqlLogEntry(N) <-> SqlSecApp(1)
            //HasRequired(x => x.App)
            //    .WithMany(x => x.LogEntries)
            //    .HasForeignKey(x => x.AppId)
            //    .WillCascadeOnDelete(true);

            // SqlLogEntry(N) <-> SqlLogSettings(1)
            //HasRequired(x => x.LogSetting)
            //    .WithMany(x => x.LogEntries)
            //    .HasForeignKey(x => new { x.AppId, x.LogLevel })
            //    .WillCascadeOnDelete(true);
        }
    }
}