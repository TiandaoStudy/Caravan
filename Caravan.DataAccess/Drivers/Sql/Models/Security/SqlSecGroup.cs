using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecGroup
    {
        [Key, Index("UK_CARAVAN_SEC_GROUP", IsUnique = true)]
        public long AppId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Index("UK_CARAVAN_SEC_GROUP", IsUnique = true), MaxLength(SqlDbContext.SmallLength)]
        public string Name { get; set; }

        [Required, MaxLength(SqlDbContext.LargeLength)]
        public string Description { get; set; }

        [Required]
        public int IsAdmin { get; set; }

        [MaxLength(SqlDbContext.LargeLength)]
        public string Notes { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}