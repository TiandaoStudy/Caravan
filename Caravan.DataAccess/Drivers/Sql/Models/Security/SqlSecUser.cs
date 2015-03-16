using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecUser
    {
        [Key, Column("CUSR_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_USERS", 0, IsUnique = true)]
        public int AppId { get; set; }

        [Required, Column("CUSR_LOGIN", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_USERS", 1, IsUnique = true)]
        public string Login { get; set; }

        [Column("CUSR_HASHED_PWD", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string HashedPassword { get; set; }

        [Required, Column("CUSR_ACTIVE", Order = 4)]
        public bool Active { get; set; }

        [Column("CUSR_FIRST_NAME", Order = 5)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string FirstName { get; set; }

        [Column("CUSR_LAST_NAME", Order = 6)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string LastName { get; set; }

        [Column("CUSR_EMAIL", Order = 7)]
        [MaxLength(SqlDbContext.MediumLength)]
        public string Email { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecRole> Roles { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }

    public sealed class SqlSecUserTypeConfiguration : EntityTypeConfiguration<SqlSecUser>
    {
        public SqlSecUserTypeConfiguration()
        {
            ToTable("CRVN_SEC_USERS", Properties.Settings.Default.SqlSchema);
        }
    }
}