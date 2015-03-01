using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecEntry
    {
        [Key, Column(Order = 0), Index("IDX_CARAVAN_SECURITY_CTX", 0)]
        public long AppId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(Order = 2), Index("IDX_CARAVAN_SECURITY_CTX", 1)]
        public long ContextId { get; set; }

        [Required, Column(Order = 3)]
        public long ObjectId { get; set; }

        [Column(Order = 4)]
        public long? UserId { get; set; }

        [Column(Order = 5)]
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