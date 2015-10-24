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
    public abstract class SqlTrackedEntity
    {
        [Column("TRCK_INSERT_DATE")]
        [DateTimeKind(DateTimeKind.Utc)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTime InsertDate { get; set; }

        [Column("TRCK_INSERT_DB_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual string InsertDbUser { get; set; }

        [Column("TRCK_INSERT_APP_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        public virtual string InsertAppUser { get; set; }

        [Column("TRCK_UPDATE_DATE")]
        [DateTimeKind(DateTimeKind.Utc)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTime? UpdateDate { get; set; }

        [Column("TRCK_UPDATE_DB_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual string UpdateDbUser { get; set; }

        [Column("TRCK_UPDATE_APP_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        public virtual string UpdateAppUser { get; set; }
    }
}
