using System;
using System.ComponentModel.DataAnnotations;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecEntry
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long AppId { get; set; }

        [Required]
        public long ContextId { get; set; }

        [Required]
        public long ObjectId { get; set; }

        public long? UserId { get; set; }

        public long? GroupId { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public SqlSecUser User { get; set; }

        public SqlSecGroup Group { get; set; }

        public SqlSecContext Context { get; set; }

        public SqlSecObject Object { get; set; }

        #endregion
    }
}