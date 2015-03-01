using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable, Table("CRVN_SEC_ENTRIES")]
    public class SqlSecEntry
    {
        [Key, Column("CSEC_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column("COBJ_ID", Order = 1)]
        public int ObjectId { get; set; }

        [Column("CUSR_ID", Order = 2)]
        public long? UserId { get; set; }

        [Column("CGRP_ID", Order = 3)]
        public int? GroupId { get; set; }

        [Column("CROL_ID", Order = 4)]
        public int? RoleId { get; set; }

        #region Relationships

        public SqlSecGroup Group { get; set; }

        public SqlSecObject Object { get; set; }

        public SqlSecRole Role { get; set; }

        public SqlSecUser User { get; set; }

        #endregion
    }
}