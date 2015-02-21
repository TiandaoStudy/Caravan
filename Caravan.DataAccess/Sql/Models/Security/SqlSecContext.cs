using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataAccess.Sql.Models.Security
{
    [Serializable]
    public class SqlSecContext
    {
        public long Id { get; set; }

        public long AppId { get; set; }

        public SqlSecApp App { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SqlSecObject> Objects { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }
    }
}