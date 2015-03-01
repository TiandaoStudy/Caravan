using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecUser
    {
        [Key, Index("UK_CARAVAN_SEC_USER", IsUnique = true)]
        public long AppId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public int Active { get; set; }

        [Required, Index("UK_CARAVAN_SEC_USER", IsUnique = true), MaxLength(SqlDbContext.SmallLength)]
        public string Login { get; set; }

        [Required, MaxLength(SqlDbContext.MediumLength)]
        public string HashedPassword { get; set; }

        [Required, MaxLength(SqlDbContext.MediumLength)]
        public string FirstName { get; set; }

        [Required, MaxLength(SqlDbContext.MediumLength)]
        public string LastName { get; set; }

        [Required, MaxLength(SqlDbContext.MediumLength)]
        public string Email { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}