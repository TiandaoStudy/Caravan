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
        [Key, Column("CLOS_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_LOG_SETTINGS", 0, IsUnique = true)]
        public int AppId { get; set; }

        [Required, Column("CLOS_TYPE", Order = 2)]
        [MaxLength(SqlDbContext.TinyLength)]
        [Index("UK_CRVN_LOG_SETTINGS", 1, IsUnique = true)]
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