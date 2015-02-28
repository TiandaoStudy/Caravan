using System;
using System.ComponentModel.DataAnnotations;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging
{
    [Serializable]
    public class SqlLogEntry
    {
        [Required]
        public long AppId { get; set; }

        [Required]
        public long Id { get; set; }

        [Required, MinLength(4), MaxLength(5)]
        public string LogType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string UserLogin { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string CodeUnit { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Function { get; set; }

        [Required, MaxLength(SqlDbContext.MediumLength)]
        public string ShortMessage { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string LongMessage { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Context { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key0 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value0 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key1 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value1 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key2 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value2 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key3 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value3 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key4 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value4 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key5 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value5 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key6 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value6 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key7 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value7 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key8 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value8 { get; set; }

        [MaxLength(SqlDbContext.MediumLength)]
        public string Key9 { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Value9 { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public SqlLogSetting LogSetting { get; set; }

        #endregion
    }
}