// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Security.Entities
{
    [Serializable]
    public class SqlSecGroup
    {
        [Key, Column("CGRP_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_GROUPS", 0, IsUnique = true)]
        public virtual int AppId { get; set; }

        [Required, Column("CGRP_NAME", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_GROUPS", 1, IsUnique = true)]
        public virtual string Name { get; set; }

        [Column("CGRP_DESCR", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string Description { get; set; }

        [Column("CGRP_NOTES", Order = 4)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Notes { get; set; }

        #region Relationships

        public virtual SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecRole> Roles { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion Relationships
    }

    /// <summary>
    ///   Configurazione della mappatura per <see cref="SqlSecGroup"/>.
    /// </summary>
    public sealed class SqlSecGroupTypeConfiguration : EntityTypeConfiguration<SqlSecGroup>
    {
        /// <summary>
        ///   Configurazione della mappatura per <see cref="SqlSecGroup"/>.
        /// </summary>
        public SqlSecGroupTypeConfiguration()
        {
            ToTable("CRVN_SEC_GROUPS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlSecGroup(N) <-> SqlSecApp(1)
            HasRequired(x => x.App)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);
        }
    }
}
