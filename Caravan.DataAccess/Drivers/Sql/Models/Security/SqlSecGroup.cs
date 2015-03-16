using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecGroup
    {
        [Key, Column("CGRP_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_GROUPS", 0, IsUnique = true)]
        public int AppId { get; set; }

        [Required, Column("CGRP_NAME", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_GROUPS", 1, IsUnique = true)]
        public string Name { get; set; }

        [Column("CGRP_DESCR", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        [Column("CGRP_NOTES", Order = 4)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Notes { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecRole> Roles { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }

    public sealed class SqlSecGroupTypeConfiguration : EntityTypeConfiguration<SqlSecGroup>
    {
        public SqlSecGroupTypeConfiguration()
        {
            ToTable("CRVN_SEC_GROUPS", Properties.Settings.Default.SqlSchema);
        }
    }
}