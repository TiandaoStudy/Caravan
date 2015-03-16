using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecRole
    {
        [Key, Column("CROL_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("CGRP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_ROLES", 0, IsUnique = true)]
        public int GroupId { get; set; }

        [Required, Column("CROL_NAME", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_ROLES", 1, IsUnique = true)]
        public string Name { get; set; }

        [Column("CROL_DESCR", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        [Column("CROL_NOTES", Order = 4)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Notes { get; set; }

        #region Relationships

        public SqlSecGroup Group { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }

    public sealed class SqlSecRoleTypeConfiguration : EntityTypeConfiguration<SqlSecRole>
    {
        public SqlSecRoleTypeConfiguration()
        {
            ToTable("CRVN_SEC_ROLES", Properties.Settings.Default.SqlSchema);
        }
    }
}