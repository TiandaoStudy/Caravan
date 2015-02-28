﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging
{
    [Serializable]
    public class SqlLogSetting
    {
        [Required]
        public long AppId { get; set; }

        [Required, MinLength(4), MaxLength(5)]
        public string LogType { get; set; }

        [Required]
        public int Enabled { get; set; }

        [Required]
        public int Days { get; set; }

        [Required]
        public int MaxEntries { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        #endregion
    }
}