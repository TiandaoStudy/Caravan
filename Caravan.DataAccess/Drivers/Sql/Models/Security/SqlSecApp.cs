using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecApp
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(Order = 1), MaxLength(SqlDbContext.SmallLength)]
        public string Name { get; set; }

        [Required, Column(Order = 2), MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        #region Relationships

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecContext> Contexts { get; set; }

        public virtual ICollection<SqlSecObject> Objects { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        public virtual ICollection<SqlLogSetting> LogSettings { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        #endregion Relationships
    }
}