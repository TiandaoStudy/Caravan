﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecContext
    {
        [Key, Column(Order = 0)]
        [Index("UK_CARAVAN_SEC_CONTEXT", 0, IsUnique = true)]
        public long AppId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(Order = 2), MaxLength(SqlDbContext.MediumLength /* Might be autogenerated */)]
        [Index("UK_CARAVAN_SEC_CONTEXT", 1, IsUnique = true)]
        public string Name { get; set; }

        [Required, Column(Order = 3), MaxLength(SqlDbContext.LargeLength)]
        public string Description { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecObject> Objects { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion Relationships
    }
}