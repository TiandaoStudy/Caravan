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
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="Token"/>.
    /// </summary>
    public class SqlIdnToken
    {
        [Key, Column(Order = 0)]
        public string Key { get; set; }

        [Key, Column(Order = 1)]
        public TokenType TokenType { get; set; }

        [StringLength(200)]
        public string SubjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string JsonCode { get; set; }

        [Required]
        public DateTimeOffset Expiry { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnToken"/>.
    /// </summary>
    public sealed class SqlIdnTokenTypeConfiguration : EntityTypeConfiguration<SqlIdnToken>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnToken"/>.
        /// </summary>
        public SqlIdnTokenTypeConfiguration()
        {
            ToTable("CRVN_IDN_TOKENS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Key).HasColumnName("CTOK_KEY");
            Property(x => x.TokenType).HasColumnName("CTOK_TYPE");
            Property(x => x.SubjectId).HasColumnName("CTOK_SUBJECT_ID");
            Property(x => x.ClientId).HasColumnName("CCLI_CLIENT_ID");
            Property(x => x.JsonCode).HasColumnName("CTOK_JSON_CODE");
            Property(x => x.Expiry).HasColumnName("CTOK_EXPIRY");
        }
    }
}
