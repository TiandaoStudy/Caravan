using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging
{
    [Serializable]
    public class SqlLogEntry
    {
        [Key, Column(Order = 0)]
        [Index("IDX_CARAVAN_LOG_TYPE", 0), Index("IDX_CARAVAN_LOG_DATE", 0)]
        public long AppId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(Order = 2), MaxLength(SqlDbContext.TinyLength)]
        [Index("IDX_CARAVAN_LOG_TYPE", 1)]
        public string LogType { get; set; }

        [Required, Column(Order = 3)]
        [Index("IDX_CARAVAN_LOG_DATE", 1)]
        public DateTime Date { get; set; }

        [Required, Column(Order = 4), MaxLength(SqlDbContext.MediumLength)]
        public string ShortMessage { get; set; }

        [Column(Order = 5), MaxLength(SqlDbContext.MediumLength)]
        public string UserLogin { get; set; }

        [Column(Order = 6), MaxLength(SqlDbContext.LargeLength)]
        public string CodeUnit { get; set; }

        [Column(Order = 7), MaxLength(SqlDbContext.MediumLength)]
        public string Function { get; set; }

        [Column(Order = 8), MaxLength(SqlDbContext.MediumLength)]
        public string Context { get; set; }

        [Column(Order = 9), MaxLength(SqlDbContext.MediumLength)]
        public string Key0 { get; set; }

        [Column(Order = 10), MaxLength(SqlDbContext.LargeLength)]
        public string Value0 { get; set; }

        [Column(Order = 11), MaxLength(SqlDbContext.MediumLength)]
        public string Key1 { get; set; }

        [Column(Order = 12), MaxLength(SqlDbContext.LargeLength)]
        public string Value1 { get; set; }

        [Column(Order = 13), MaxLength(SqlDbContext.MediumLength)]
        public string Key2 { get; set; }

        [Column(Order = 14), MaxLength(SqlDbContext.LargeLength)]
        public string Value2 { get; set; }

        [Column(Order = 15), MaxLength(SqlDbContext.MediumLength)]
        public string Key3 { get; set; }

        [Column(Order = 16), MaxLength(SqlDbContext.LargeLength)]
        public string Value3 { get; set; }

        [Column(Order = 17), MaxLength(SqlDbContext.MediumLength)]
        public string Key4 { get; set; }

        [Column(Order = 18), MaxLength(SqlDbContext.LargeLength)]
        public string Value4 { get; set; }

        [Column(Order = 19), MaxLength(SqlDbContext.MediumLength)]
        public string Key5 { get; set; }

        [Column(Order = 20), MaxLength(SqlDbContext.LargeLength)]
        public string Value5 { get; set; }

        [Column(Order = 21), MaxLength(SqlDbContext.MediumLength)]
        public string Key6 { get; set; }

        [Column(Order = 22), MaxLength(SqlDbContext.LargeLength)]
        public string Value6 { get; set; }

        [Column(Order = 23), MaxLength(SqlDbContext.MediumLength)]
        public string Key7 { get; set; }

        [Column(Order = 24), MaxLength(SqlDbContext.LargeLength)]
        public string Value7 { get; set; }

        [Column(Order = 25), MaxLength(SqlDbContext.MediumLength)]
        public string Key8 { get; set; }

        [Column(Order = 26), MaxLength(SqlDbContext.LargeLength)]
        public string Value8 { get; set; }

        [Column(Order = 27), MaxLength(SqlDbContext.MediumLength)]
        public string Key9 { get; set; }

        [Column(Order = 28), MaxLength(SqlDbContext.LargeLength)]
        public string Value9 { get; set; }

        [Column(Order = 29)] /* Should be a CLOB/TEXT */
        public string LongMessage { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public SqlLogSetting LogSetting { get; set; }

        #endregion Relationships
    }
}