using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecObject
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long ContextId { get; set; }

        [Required]
        public long AppId { get; set; }

        [Required, MaxLength(SqlDbContext.SmallLength)]
        public string Name { get; set; }

        [Required, MaxLength(SqlDbContext.LargeLength)]
        public string Description { get; set; }

        [Required, MaxLength(SqlDbContext.TinyLength)]
        public string Type { get; set; }

        #region Relationships

        public SqlSecContext Context { get; set; }

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}