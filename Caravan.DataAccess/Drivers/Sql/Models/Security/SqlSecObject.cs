using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecObject
    {
        [Key, Index("UK_CARAVAN_SEC_OBJECT", IsUnique = true)]
        public long AppId { get; set; }

        [Key, Index("UK_CARAVAN_SEC_OBJECT", IsUnique = true)]
        public long ContextId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Index("UK_CARAVAN_SEC_OBJECT", IsUnique = true), MaxLength(SqlDbContext.SmallLength)]
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