using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging
{
    [Serializable]
    public class SqlLogSetting
    {
        [Key, Column(Order = 0)]
        public long AppId { get; set; }

        [Key, Column(Order = 1), MaxLength(SqlDbContext.TinyLength)]
        public string LogType { get; set; }

        [Required, Column(Order = 2)]
        public int Enabled { get; set; }

        [Required, Column(Order = 3)]
        public int Days { get; set; }

        [Required, Column(Order = 4)]
        public int MaxEntries { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        #endregion Relationships
    }
}