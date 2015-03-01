using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecGroup
    {
        [Key, Column(Order = 0)]
        [Index("UK_CARAVAN_SEC_GROUP", 0, IsUnique = true)]
        public long AppId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(Order = 2), MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CARAVAN_SEC_GROUP", 1, IsUnique = true)]
        public string Name { get; set; }

        [Required, Column(Order = 3), MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        [Column(Order = 4), MaxLength(SqlDbContext.LargeLength)]
        public string Notes { get; set; }

        [Required, Column(Order = 5)] // <-- USARE RUOLI???
        public int IsAdmin { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}