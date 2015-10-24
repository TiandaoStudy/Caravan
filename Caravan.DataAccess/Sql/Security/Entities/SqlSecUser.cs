﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Security.Entities
{
    [Serializable]
    public class SqlSecUser
    {
        [Key, Column("CUSR_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_USERS", 0, IsUnique = true)]
        public virtual int AppId { get; set; }

        [Required, Column("CUSR_LOGIN", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_USERS", 1, IsUnique = true)]
        public virtual string Login { get; set; }

        [Column("CUSR_HASHED_PWD", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string PasswordHash { get; set; }

        [Required, Column("CUSR_ACTIVE", Order = 4)]
        public virtual bool Active { get; set; }

        [Column("CUSR_FIRST_NAME", Order = 5)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string FirstName { get; set; }

        [Column("CUSR_LAST_NAME", Order = 6)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string LastName { get; set; }

        [Column("CUSR_EMAIL", Order = 7)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string Email { get; set; }

        #region Relationships

        public virtual SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecRole> Roles { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion Relationships
    }

    public sealed class SqlSecUserTypeConfiguration : EntityTypeConfiguration<SqlSecUser>
    {
        public SqlSecUserTypeConfiguration()
        {
            ToTable("CRVN_SEC_USERS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            // SqlSecUser(N) <-> SqlSecApp(1)
            HasRequired(x => x.App)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.AppId)
                .WillCascadeOnDelete(true);
        }
    }
}
