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

using Finsa.Caravan.DataAccess.Sql.Identity.Entities;
using Finsa.Caravan.DataAccess.Sql.Logging.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Security.Entities
{
    [Serializable]
    public class SqlSecApp
    {
        [Key, Column("CAPP_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required, Column("CAPP_NAME", Order = 1)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_APPS", 0, IsUnique = true)]
        public virtual string Name { get; set; }

        [Column("CAPP_DESCR", Order = 2)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string Description { get; set; }

        [Column("CAPP_PWD_HASHER", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string PasswordHasher { get; set; }

        #region Relationships

        public virtual ICollection<SqlSecUser> Users { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecContext> Contexts { get; set; }

        public virtual ICollection<SqlLogSetting> LogSettings { get; set; }

        public virtual ICollection<SqlLogEntry> LogEntries { get; set; }

        public virtual ICollection<SqlIdnClient> Clients { get; set; }

        #endregion Relationships
    }

    /// <summary>
    ///   Configurazione della mappatura per <see cref="SqlSecApp"/>.
    /// </summary>
    public sealed class SqlSecAppTypeConfiguration : EntityTypeConfiguration<SqlSecApp>
    {
        /// <summary>
        ///   Configurazione della mappatura per <see cref="SqlSecApp"/>.
        /// </summary>
        public SqlSecAppTypeConfiguration()
        {
            ToTable("CRVN_SEC_APPS", CaravanDataAccessConfiguration.Instance.SqlSchema);
        }
    }
}
