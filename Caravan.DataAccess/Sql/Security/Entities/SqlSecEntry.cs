using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Security.Entities
{
    [Serializable]
    public class SqlSecEntry
    {
        [Key, Column("CSEC_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Required, Column("COBJ_ID", Order = 1)]
        public virtual long ObjectId { get; set; }

        [Column("CUSR_ID", Order = 2)]
        public virtual long? UserId { get; set; }

        [Column("CGRP_ID", Order = 3)]
        public virtual int? GroupId { get; set; }

        [Column("CROL_ID", Order = 4)]
        public virtual int? RoleId { get; set; }

        #region Relationships

        public virtual SqlSecGroup Group { get; set; }

        public virtual SqlSecObject Object { get; set; }

        public virtual SqlSecRole Role { get; set; }

        public virtual SqlSecUser User { get; set; }

        #endregion Relationships
    }

    /// <summary>
    ///   Configurazione della mappatura per <see cref="SqlSecEntry"/>.
    /// </summary>
    public sealed class SqlSecEntryTypeConfiguration : EntityTypeConfiguration<SqlSecEntry>
    {
        /// <summary>
        ///   Configurazione della mappatura per <see cref="SqlSecEntry"/>.
        /// </summary>
        public SqlSecEntryTypeConfiguration()
        {
            ToTable("CRVN_SEC_ENTRIES", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlSecEntry(N) <-> SqlSecUser(1)
            HasOptional(x => x.User)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(true);

            // SqlSecEntry(N) <-> SqlSecGroup(1)
            HasOptional(x => x.Group)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.GroupId)
                .WillCascadeOnDelete(true);

            // SqlSecEntry(N) <-> SqlSecRole(1)
            HasOptional(x => x.Role)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.RoleId)
                .WillCascadeOnDelete(true);

            // SqlSecEntry(N) <-> SqlSecObject(1)
            HasRequired(x => x.Object)
                .WithMany(x => x.SecEntries)
                .HasForeignKey(x => x.ObjectId)
                .WillCascadeOnDelete(true);
        }
    }
}
