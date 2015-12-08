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

using Finsa.Caravan.DataAccess.Sql.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Sql.Entities
{
    /// <summary>
    ///   Contiene la sei colonne necessarie per il tracciamento delle modifiche alle righe di una
    ///   tabella: data e utente (DB e APP) di inserimento, data e utente (DB e APP) di modifica.
    /// </summary>
    [Serializable]
    public abstract class SqlTrackedEntity
    {
        [Column("TRCK_INSERT_DATE")]
        [DateTimeOffset(Hours = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTimeOffset InsertDate { get; set; }

        [Column("TRCK_INSERT_DB_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual string InsertDbUser { get; set; }

        [Column("TRCK_INSERT_APP_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        public virtual string InsertAppUser { get; set; }

        [Column("TRCK_UPDATE_DATE")]
        [DateTimeOffset(Hours = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTimeOffset? UpdateDate { get; set; }

        [Column("TRCK_UPDATE_DB_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual string UpdateDbUser { get; set; }

        [Column("TRCK_UPDATE_APP_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        public virtual string UpdateAppUser { get; set; }
    }
}
