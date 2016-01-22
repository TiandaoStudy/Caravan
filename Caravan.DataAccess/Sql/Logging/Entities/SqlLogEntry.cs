using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Finsa.Caravan.DataAccess.Sql.Attributes;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;

namespace Finsa.Caravan.DataAccess.Sql.Logging.Entities
{
    [Serializable]
    public class SqlLogEntry
    {
        [Key, Column("CLOG_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("IX_CRVN_LOG_TYPE", 0), Index("IX_CRVN_LOG_DATE", 0)]
        public virtual int AppId { get; set; }

        [Required, Column("CLOS_TYPE", Order = 2)]
        [MaxLength(SqlDbContext.TinyLength)]
        [Index("IX_CRVN_LOG_TYPE", 1)]
        public virtual string LogLevel { get; set; }

        [Required, Column("CLOG_DATE", Order = 3)]
        [Index("IX_CRVN_LOG_DATE", 1)]
        [DateTimeKind(DateTimeKind.Utc)]
        public virtual DateTime Date { get; set; }

        [Column("CUSR_LOGIN", Order = 4)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string UserLogin { get; set; }

        [Column("CLOG_CODE_UNIT", Order = 5)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string CodeUnit { get; set; }

        [Column("CLOG_FUNCTION", Order = 6)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string Function { get; set; }

        [Required, Column("CLOG_SHORT_MSG", Order = 7)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string ShortMessage { get; set; }

        [Column("CLOG_LONG_MSG", Order = 8)] /* Should be a CLOB/TEXT */
        public virtual string LongMessage { get; set; }

        [Column("CLOG_CONTEXT", Order = 9)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string Context { get; set; }

        [Column("CLOG_KEY_0", Order = 10)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key0 { get; set; }

        [Column("CLOG_VALUE_0", Order = 11)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value0 { get; set; }

        [Column("CLOG_KEY_1", Order = 12)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key1 { get; set; }

        [Column("CLOG_VALUE_1", Order = 13)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value1 { get; set; }

        [Column("CLOG_KEY_2", Order = 14)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key2 { get; set; }

        [Column("CLOG_VALUE_2", Order = 15)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value2 { get; set; }

        [Column("CLOG_KEY_3", Order = 16)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key3 { get; set; }

        [Column("CLOG_VALUE_3", Order = 17)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value3 { get; set; }

        [Column("CLOG_KEY_4", Order = 18)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key4 { get; set; }

        [Column("CLOG_VALUE_4", Order = 19)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value4 { get; set; }

        [Column("CLOG_KEY_5", Order = 20)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key5 { get; set; }

        [Column("CLOG_VALUE_5", Order = 21)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value5 { get; set; }

        [Column("CLOG_KEY_6", Order = 22)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key6 { get; set; }

        [Column("CLOG_VALUE_6", Order = 23)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value6 { get; set; }

        [Column("CLOG_KEY_7", Order = 24)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key7 { get; set; }

        [Column("CLOG_VALUE_7", Order = 25)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value7 { get; set; }

        [Column("CLOG_KEY_8", Order = 26)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key8 { get; set; }

        [Column("CLOG_VALUE_8", Order = 27)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value8 { get; set; }

        [Column("CLOG_KEY_9", Order = 28)]
        [MaxLength(SqlDbContext.SmallLength)]
        public virtual string Key9 { get; set; }

        [Column("CLOG_VALUE_9", Order = 29)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Value9 { get; set; }

        #region Relationships

        public virtual SqlSecApp App { get; set; }

        public virtual SqlLogSetting LogSetting { get; set; }

        #endregion Relationships
    }

    public sealed class SqlLogEntryTypeConfiguration : EntityTypeConfiguration<SqlLogEntry>
    {
        public SqlLogEntryTypeConfiguration()
        {
            ToTable("CRVN_LOG_ENTRIES", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlLogEntry(N) <-> SqlSecApp(1)
            HasRequired(x => x.App)
                .WithMany(x => x.LogEntries)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);

            // SqlLogEntry(N) <-> SqlLogSettings(1)
            HasRequired(x => x.LogSetting)
                .WithMany(x => x.LogEntries)
                .HasForeignKey(x => new { x.AppId, x.LogLevel })
                .WillCascadeOnDelete(true);
        }
    }
}
