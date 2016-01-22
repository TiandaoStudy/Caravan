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
    public class SqlSecRole
    {
        [Key, Column("CROL_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required, Column("CGRP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_ROLES", 0, IsUnique = true)]
        public virtual int GroupId { get; set; }

        [Required, Column("CROL_NAME", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_ROLES", 1, IsUnique = true)]
        public virtual string Name { get; set; }

        [Column("CROL_DESCR", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string Description { get; set; }

        [Column("CROL_NOTES", Order = 4)]
        [MaxLength(SqlDbContext.LargeLength)]
        public virtual string Notes { get; set; }

        #region Relationships

        public virtual SqlSecGroup Group { get; set; }

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion Relationships
    }

    /// <summary>
    ///   Configurazione della mappatura per <see cref="SqlSecRole"/>.
    /// </summary>
    public sealed class SqlSecRoleTypeConfiguration : EntityTypeConfiguration<SqlSecRole>
    {
        /// <summary>
        ///   Configurazione della mappatura per <see cref="SqlSecRole"/>.
        /// </summary>
        public SqlSecRoleTypeConfiguration()
        {
            ToTable("CRVN_SEC_ROLES", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlSecRole(N) <-> SqlSecGroup(1)
            HasRequired(x => x.Group)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.GroupId)
                .WillCascadeOnDelete(true);

            // SqlSecRole(N) <-> SqlSecUser(N)
            HasMany(x => x.Users)
                .WithMany(x => x.Roles)
                .Map(x => x.MapLeftKey("CROL_ID")
                           .MapRightKey("CUSR_ID")
                           .ToTable("CRVN_SEC_USER_ROLES", CaravanDataAccessConfiguration.Instance.SqlSchema));
        }
    }
}
