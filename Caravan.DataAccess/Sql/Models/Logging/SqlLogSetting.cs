using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Sql.Models.Security;

namespace Finsa.Caravan.DataAccess.Sql.Models.Logging
{
    [Serializable]
    public class SqlLogSetting
    {
        public long AppId { get; set; }

        public SqlSecApp App { get; set; }

        public int Enabled { get; set; }

        public int Days { get; set; }

        public int MaxEntries { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        public string TypeId { get; set; }
    }
}