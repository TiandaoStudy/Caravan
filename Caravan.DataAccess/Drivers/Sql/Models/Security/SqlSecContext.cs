using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecContext
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long AppId { get; set; }

        [Required]
        public SqlSecApp App { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        #region Relationships

        public virtual ICollection<SqlSecObject> Objects { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}