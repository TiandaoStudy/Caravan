using System;

namespace Finsa.Caravan.DataAccess.Sql.Models.Security
{
    [Serializable]
    public class SqlSecEntry
    {
        public long Id { get; set; }

        public long AppId { get; set; }

        public SqlSecApp App { get; set; }

        public long? UserId { get; set; }

        public SqlSecUser User { get; set; }

        public long? GroupId { get; set; }

        public SqlSecGroup Group { get; set; }

        public long ContextId { get; set; }

        public SqlSecContext Context { get; set; }

        public long ObjectId { get; set; }

        public SqlSecObject Object { get; set; }
    }
}