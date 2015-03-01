﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable, Table("CRVN_SEC_CONTEXTS")]
    public class SqlSecContext
    {
        [Key, Column("CCTX_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_CONTEXTS", 0, IsUnique = true)]
        public int AppId { get; set; }

        [Required, Column("CCTX_NAME", Order = 2)]
        [MaxLength(SqlDbContext.MediumLength /* Might be autogenerated */)]
        [Index("UK_CRVN_SEC_CONTEXTS", 1, IsUnique = true)]
        public string Name { get; set; }

        [Column("CCTX_DESCR", Order = 3)]
        [MaxLength(SqlDbContext.LargeLength)]
        public string Description { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecObject> Objects { get; set; }

        #endregion Relationships
    }
}