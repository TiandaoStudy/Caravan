using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Entities
{
    /// <summary>
    ///   Contiene la quattro colonne necessarie per il tracciamento delle modifiche alle righe di
    ///   una tabella: data e utente di inserimento, data e utente di modifica.
    /// </summary>
    public abstract class SqlTrackedEntity
    {
        [Column("TRCK_INSERT_DATE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTime InsertDate { get; set; }

        [Column("TRCK_INSERT_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        public virtual string InsertUser { get; set; }

        [Column("TRCK_UPDATE_DATE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTime? UpdateDate { get; set; }

        [Column("TRCK_UPDATE_USER")]
        [StringLength(SqlDbContext.SmallLength)]
        public virtual string UpdateUser { get; set; }
    }
}