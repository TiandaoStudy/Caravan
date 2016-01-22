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
    public class SqlSecObject
    {
        [Key, Column("COBJ_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Required, Column("CCTX_ID", Order = 1)]
        [Index("UK_CRVN_SEC_OBJECTS", 0, IsUnique = true)]
        public virtual int ContextId { get; set; }

        [Required, Column("COBJ_NAME", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_OBJECTS", 1, IsUnique = true)]
        public virtual string Name { get; set; }

        [Column("COBJ_DESCR", Order = 4)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string Description { get; set; }

        [Required, Column("COBJ_TYPE", Order = 5)]
        [MaxLength(SqlDbContext.TinyLength)]
        public virtual string Type { get; set; }

        #region Relationships

        public virtual SqlSecContext Context { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion Relationships
    }

    /// <summary>
    ///   Configurazione della mappatura per <see cref="SqlSecObject"/>.
    /// </summary>
    public sealed class SqlSecObjectTypeConfiguration : EntityTypeConfiguration<SqlSecObject>
    {
        /// <summary>
        ///   Configurazione della mappatura per <see cref="SqlSecObject"/>.
        /// </summary>
        public SqlSecObjectTypeConfiguration()
        {
            ToTable("CRVN_SEC_OBJECTS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlSecObject(N) <-> SqlSecContext(1)
            HasRequired(x => x.Context)
                .WithMany(x => x.Objects)
                .HasForeignKey(x => x.ContextId)
                .WillCascadeOnDelete(true);
        }
    }
}
