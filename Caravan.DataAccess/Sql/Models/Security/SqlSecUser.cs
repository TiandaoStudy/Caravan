using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataAccess.Sql.Models.Security
{
    [Serializable]
    public class SqlSecUser
    {
        public long Id { get; set; }

        public long AppId { get; set; }

        public SqlSecApp App { get; set; }

        public int Active { get; set; }

        public string Login { get; set; }

        public string HashedPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }
    }
}