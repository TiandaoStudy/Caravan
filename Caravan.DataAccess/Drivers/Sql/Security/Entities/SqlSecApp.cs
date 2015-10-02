using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Finsa.Caravan.DataAccess.Drivers.Sql.Logging.Entities;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Security.Entities
{
    [Serializable]
    public class SqlSecApp
    {
        [Key, Column("CAPP_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("CAPP_NAME", Order = 1)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_APPS", 0, IsUnique = true)]
        public string Name { get; set; }

        [Column("CAPP_DESCR", Order = 2)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        #region Relationships

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecContext> Contexts { get; set; }

        public virtual ICollection<SqlLogSetting> LogSettings { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        #endregion Relationships
    }

    public sealed class SqlSecAppTypeConfiguration : EntityTypeConfiguration<SqlSecApp>
    {
        public SqlSecAppTypeConfiguration()
        {
            ToTable("CRVN_SEC_APPS", CaravanDataAccessConfiguration.Instance.SqlSchema);
        }
    }
}
