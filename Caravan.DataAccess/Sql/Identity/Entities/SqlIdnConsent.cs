/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using IdentityServer3.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="Consent"/>.
    /// </summary>
    public class SqlIdnConsent
    {
        [Key, Column(Order = 0)]
        [StringLength(200)]
        public virtual string Subject { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(200)]
        public virtual string ClientId { get; set; }

        [Required]
        [StringLength(2000)]
        public virtual string Scopes { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnConsent"/>.
    /// </summary>
    public sealed class SqlIdnConsentTypeConfiguration : EntityTypeConfiguration<SqlIdnConsent>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnConsent"/>.
        /// </summary>
        public SqlIdnConsentTypeConfiguration()
        {
            ToTable("CRVN_IDN_CONSENTS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Subject).HasColumnName("CCON_SUBJECT");
            Property(x => x.ClientId).HasColumnName("CCLI_CLIENT_ID");
            Property(x => x.Scopes).HasColumnName("CSCO_SCOPE_NAMES");
        }
    }
}
