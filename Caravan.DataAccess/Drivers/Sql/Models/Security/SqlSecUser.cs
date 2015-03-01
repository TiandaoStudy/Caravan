using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Models.Security
{
    [Serializable]
    public class SqlSecUser
    {
        [Key, Column("CAPP_ID", Order = 0)]
        [Index("UK_CARAVAN_SEC_USER", 0, IsUnique = true)]
        public long AppId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, Column(Order = 2), MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CARAVAN_SEC_USER", 1, IsUnique = true)]
        public string Login { get; set; }

        [Required, Column(Order = 3)]
        public int Active { get; set; }

        [Required, Column(Order = 4), MaxLength(SqlDbContext.MediumLength)]
        public string HashedPassword { get; set; }

        [Required, Column(Order = 5), MaxLength(SqlDbContext.MediumLength)]
        public string FirstName { get; set; }

        [Required, Column(Order = 6), MaxLength(SqlDbContext.MediumLength)]
        public string LastName { get; set; }

        [Required, Column(Order = 7), MaxLength(SqlDbContext.MediumLength)]
        public string Email { get; set; }

        #region Relationships

        public SqlSecApp App { get; set; }

        public virtual ICollection<SqlSecGroup> Groups { get; set; }

        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        #endregion
    }
}