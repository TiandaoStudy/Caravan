using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Security.Models
{
    [Serializable]
    public class SqlSecObject
    {
        [Key, Column("COBJ_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("CCTX_ID", Order = 1)]
        [Index("UK_CRVN_SEC_OBJECTS", 0, IsUnique = true)]
        public int ContextId { get; set; }

        [Required, Column("COBJ_NAME", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_OBJECTS", 1, IsUnique = true)]
        public string Name { get; set; }

        [Column("COBJ_DESCR", Order = 4)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Description { get; set; }

        [Required, Column("COBJ_TYPE", Order = 5)]
        [MaxLength(SqlDbContext.TinyLength)]
        public string Type { get; set; }

        #region Relationships

        public SqlSecContext Context { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion Relationships
    }

    public sealed class SqlSecObjectTypeConfiguration : EntityTypeConfiguration<SqlSecObject>
    {
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
