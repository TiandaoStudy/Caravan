﻿using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Sql.Models.Logging;

namespace Finsa.Caravan.DataAccess.Sql.Models.Security
{
    [Serializable]
    public class SqlSecApp
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecContext> Contexts { get; set; }

        public virtual ICollection<SqlSecObject> Objects { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        public virtual ICollection<SqlLogSetting> LogSettings { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }
    }
}