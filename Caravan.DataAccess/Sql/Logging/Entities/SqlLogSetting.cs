using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;

namespace Finsa.Caravan.DataAccess.Sql.Logging.Entities
{
    [Serializable]
    public class SqlLogSetting
    {
        [Key, Column("CAPP_ID", Order = 0)]
        public virtual int AppId { get; set; }

        [Key, Column("CLOS_TYPE", Order = 1)]
        [MaxLength(SqlDbContext.TinyLength)]
        public virtual string LogLevel { get; set; }

        [Required, Column("CLOS_ENABLED", Order = 3)]
        public virtual bool Enabled { get; set; }

        [Required, Column("CLOS_DAYS", Order = 4)]
        public virtual short Days { get; set; }

        [Required, Column("CLOS_MAX_ENTRIES", Order = 5)]
        public virtual int MaxEntries { get; set; }

        #region Relationships

        public virtual SqlSecApp App { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        #endregion Relationships
    }

    public sealed class SqlLogSettingTypeConfiguration : EntityTypeConfiguration<SqlLogSetting>
    {
        public SqlLogSettingTypeConfiguration()
        {
            ToTable("CRVN_LOG_SETTINGS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlLogSettings(N) <-> SqlSecApp(1)
            HasRequired(x => x.App)
                .WithMany(x => x.LogSettings)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);
        }
    }
}
