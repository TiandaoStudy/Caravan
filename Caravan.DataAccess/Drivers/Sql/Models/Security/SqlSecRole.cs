using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable, Table("CRVN_SEC_ROLES")]
    public class SqlSecRole
    {
        [Key, Column("CROL_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_ROLES", 0, IsUnique = true)]
        public int AppId { get; set; }

        [Required, Column("CROL_NAME", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_ROLES", 1, IsUnique = true)]
        public string Name { get; set; }

        [Required, Column("CROL_DESCR", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        [Column("CROL_NOTES", Order = 4)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Notes { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}