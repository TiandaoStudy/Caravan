using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecEntry
    {
        [Key, Index("IDX_CARAVAN_SECURITY_CTX")]
        public long AppId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Index("IDX_CARAVAN_SECURITY_CTX")]
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