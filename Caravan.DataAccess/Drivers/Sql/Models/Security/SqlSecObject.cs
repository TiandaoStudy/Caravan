using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecObject
    {
        public long Id { get; set; }

        public long ContextId { get; set; }

        public SqlSecContext Context { get; set; }

        public long AppId { get; set; }

        public SqlSecApp App { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }
    }
}