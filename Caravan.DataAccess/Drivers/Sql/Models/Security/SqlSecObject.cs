using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecObject
    {
        [Key, Column(Order = 0), Index("UK_CARAVAN_SEC_OBJECT", 0, IsUnique = true)]
        public long AppId { get; set; }

        [Key, Column(Order = 1), Index("UK_CARAVAN_SEC_OBJECT", 1, IsUnique = true)]
        public long ContextId { get; set; }

        [Key, Column(Order = 2), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(Order = 3), Index("UK_CARAVAN_SEC_OBJECT", 2, IsUnique = true), MaxLength(SqlDbContext.SmallLength)]
        public string Name { get; set; }

        [Required, Column(Order = 4), MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        [Required, Column(Order = 5), MaxLength(SqlDbContext.TinyLength)]
        public string Type { get; set; }

        #region Relationships

        public SqlSecContext Context { get; set; }

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}