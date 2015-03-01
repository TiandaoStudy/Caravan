using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging
{
    [Serializable, Table("CRVN_LOG_SETTINGS")]
    public class SqlLogSetting
    {
        [Key, Column("CAPP_ID", Order = 0)]
        public int AppId { get; set; }

        [Key, Column("CLOS_TYPE", Order = 1)]
        [MaxLength(SqlDbContext.TinyLength)]
        public string LogType { get; set; }

        [Required, Column("CLOS_ENABLED", Order = 3)]
        public bool Enabled { get; set; }

        [Required, Column("CLOS_DAYS", Order = 4)]
        public short Days { get; set; }

        [Required, Column("CLOS_MAX_ENTRIES", Order = 5)]
        public int MaxEntries { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        #endregion Relationships
    }
}