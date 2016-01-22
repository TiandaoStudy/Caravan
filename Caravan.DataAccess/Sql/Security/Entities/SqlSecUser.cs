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
using Finsa.Caravan.DataAccess.Sql.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Security.Entities
{
    /// <summary>
    ///   Tabella che censisce gli utenti delle applicazioni FINSA.
    /// </summary>
    [Serializable]
    public class SqlSecUser : SqlTrackedEntity
    {
        /// <summary>
        ///   Identificativo riga, è una sequenza autoincrementale.
        /// </summary>
        [Key, Column("CUSR_ID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        /// <summary>
        ///   Identificativo della applicazione a cui un certo utente appartiene.
        /// </summary>
        [Required, Column("CAPP_ID", Order = 1)]
        [Index("UK_CRVN_SEC_USERS", 0, IsUnique = true)]
        public virtual int AppId { get; set; }

        /// <summary>
        ///   La sigla usata per effettuare la login.
        /// </summary>
        [Required, Column("CUSR_LOGIN", Order = 2)]
        [MaxLength(SqlDbContext.SmallLength)]
        [Index("UK_CRVN_SEC_USERS", 1, IsUnique = true)]
        public virtual string Login { get; set; }

        /// <summary>
        ///   Hash della password fissata per un certo utente.
        /// </summary>
        [Column("CUSR_HASHED_PWD", Order = 3)]
        [MaxLength(SqlDbContext.MediumLength)]
        [DataType(DataType.Password)]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///   Indica se un certo utente sia attivo o meno.
        /// </summary>
        [Required, Column("CUSR_ACTIVE", Order = 4)]
        public virtual bool Active { get; set; }

        /// <summary>
        ///   Nome di un certo utente.
        /// </summary>
        [Column("CUSR_FIRST_NAME", Order = 5)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string FirstName { get; set; }

        /// <summary>
        ///   Cognome di un certo utente.
        /// </summary>
        [Column("CUSR_LAST_NAME", Order = 6)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string LastName { get; set; }

        /// <summary>
        ///   Indirizzo e-mail di un certo utente.
        /// </summary>
        [Column("CUSR_EMAIL", Order = 7)]
        [MaxLength(SqlDbContext.MediumLength)]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        /// <summary>
        ///   Indica se il dato indirizzo e-mail sia stato confermato.
        /// </summary>
        [Column("CUSR_EMAIL_CONFIRMED", Order = 8)]
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        ///   Numero di telefono di un certo utente.
        /// </summary>
        [Column("CUSR_PHONE", Order = 9)]
        [MaxLength(SqlDbContext.SmallLength)]
        [DataType(DataType.PhoneNumber)]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///   Indica se il dato numero di telefono sia stato confermato.
        /// </summary>
        [Column("CUSR_PHONE_CONFIRMED", Order = 10)]
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///   Rappresenta una sintesi delle informazioni di accesso per un certo utente.
        /// </summary>
        [Column("CUSR_SECURITY_STAMP", Order = 11)]
        [MaxLength(SqlDbContext.MediumLength)]
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///   Indica se ad un certo utente possa essere bloccata la login a fronte di un certo
        ///   numero di accessi falliti.
        /// 
        ///   Abilitato di default.
        /// </summary>
        [Column("CUSR_LOCKOUT_ENABLED", Order = 12)]
        public virtual bool LockoutEnabled { get; set; } = true;

        /// <summary>
        ///   Indica la data di fine del blocco della login, valorizzare nel passato in fase di
        ///   creazione utente.
        /// </summary>
        [Column("CUSR_LOCKOUT_END_DATE", Order = 13)]
        [DateTimeKind(DateTimeKind.Utc)]
        public virtual DateTime LockoutEndDate { get; set; }

        /// <summary>
        ///   Il numero di login fallite per un certo utente.
        /// </summary>
        [Column("CUSR_ACCESS_FAILED_COUNT", Order = 14)]
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        ///   Indica se un certo utente abbia abilitato la autenticazione a due fattori.
        /// </summary>
        [Column("CUSR_TWO_FACTOR_AUTH_ENABLED", Order = 15)]
        public virtual bool TwoFactorAuthenticationEnabled { get; set; }

        #region Relationships

        /// <summary>
        ///   Applicazione a cui un certo utente appartiene.
        /// </summary>
        public virtual SqlSecApp App { get; set; }

        /// <summary>
        ///   Ruoli ricoperti dall'utente.
        /// </summary>
        public virtual ICollection<SqlSecRole> Roles { get; set; }

        /// <summary>
        ///   Voci di sicurezza legate all'utente.
        /// </summary>
        public virtual ICollection<SqlSecEntry> SecEntries { get; set; }

        /// <summary>
        ///   I claim legati all'utente.
        /// </summary>
        public virtual ICollection<SqlSecClaim> Claims { get; set; }

        #endregion Relationships
    }

    /// <summary>
    ///   Configurazione della mappatura per <see cref="SqlSecUser"/>.
    /// </summary>
    public sealed class SqlSecUserTypeConfiguration : EntityTypeConfiguration<SqlSecUser>
    {
        /// <summary>
        ///   Configurazione della mappatura per <see cref="SqlSecUser"/>.
        /// </summary>
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
