using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataAccess.Sql.Models.Security
{
    [Serializable]
    public class SqlSecGroup
    {
        public long Id { get; set; }

        public long AppId { get; set; }

        public SqlSecApp App { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int IsAdmin { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }
    }
}