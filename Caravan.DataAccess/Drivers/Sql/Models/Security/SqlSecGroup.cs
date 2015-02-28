using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecGroup
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long AppId { get; set; }

        [Required, MaxLength(SqlDbContext.SmallLength)]
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