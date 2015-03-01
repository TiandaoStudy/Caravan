using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging
{
    [Serializable, Table("CRVN_LOG_ENTRIES")]
    public class SqlLogEntry
    {
        [Key, Column("CLOG_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("IX_CRVN_LOG_TYPE", 0), Index("IX_CRVN_LOG_DATE", 0)]
        public int AppId { get; set; }

        [Required, Column("CLOS_TYPE", Order = 2)]
        [MaxLength(SqlDbContext.TinyLength)]
        [Index("IX_CRVN_LOG_TYPE", 1)]
        public string LogType { get; set; }

        [Required, Column("CLOG_TYPE", Order = 3)]
        [Index("IX_CRVN_LOG_DATE", 1)]
        public DateTime Date { get; set; }

        [Column("CUSR_LOGIN", Order = 4)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string UserLogin { get; set; }

        [Column("CLOG_CODE_UNIT", Order = 5)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string CodeUnit { get; set; }

        [Column("CLOG_FUNCTION", Order = 6)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Function { get; set; }

        [Required, Column("CLOG_SHORT_MSG", Order = 7)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string ShortMessage { get; set; }

        [Column("CLOG_CONTEXT", Order = 8)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Context { get; set; }

        [Column("CLOG_KEY_0", Order = 9)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key0 { get; set; }

        [Column("CLOG_VALUE_0", Order = 10)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value0 { get; set; }

        [Column("CLOG_KEY_1", Order = 11)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key1 { get; set; }

        [Column("CLOG_VALUE_1", Order = 12)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value1 { get; set; }

        [Column("CLOG_KEY_2", Order = 13)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key2 { get; set; }

        [Column("CLOG_VALUE_2", Order = 14)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value2 { get; set; }

        [Column("CLOG_KEY_3", Order = 15)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key3 { get; set; }

        [Column("CLOG_VALUE_3", Order = 16)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value3 { get; set; }

        [Column("CLOG_KEY_4", Order = 17)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key4 { get; set; }

        [Column("CLOG_VALUE_4", Order = 18)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value4 { get; set; }

        [Column("CLOG_KEY_5", Order = 19)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key5 { get; set; }

        [Column("CLOG_VALUE_5", Order = 20)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value5 { get; set; }

        [Column("CLOG_KEY_6", Order = 21)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key6 { get; set; }

        [Column("CLOG_VALUE_6", Order = 22)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value6 { get; set; }

        [Column("CLOG_KEY_7", Order = 23)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key7 { get; set; }

        [Column("CLOG_VALUE_7", Order = 24)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value7 { get; set; }

        [Column("CLOG_KEY_8", Order = 25)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key8 { get; set; }

        [Column("CLOG_VALUE_8", Order = 26)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value8 { get; set; }

        [Column("CLOG_KEY_9", Order = 27)]
        [MaxLength(SqlDbContext.SmallLength)]
        public string Key9 { get; set; }

        [Column("CLOG_VALUE_9", Order = 28)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Value9 { get; set; }

        [Column("CLOG_LONG_MSG", Order = 29)] /* Should be a CLOB/TEXT */
        public string LongMessage { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public SqlLogSetting LogSetting { get; set; }

        #endregion Relationships
    }
}