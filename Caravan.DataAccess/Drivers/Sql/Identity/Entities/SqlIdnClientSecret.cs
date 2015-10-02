﻿/*
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

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using IdentityServer3.EntityFramework.Entities;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientSecret"/>.
    /// </summary>
    public class SqlIdnClientSecret
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Value { get; set; }

        [StringLength(250)]
        public string Type { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public DateTimeOffset? Expiration { get; set; }

        public SqlIdnClient Client { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientSecret"/>.
    /// </summary>
    public sealed class SqlIdnClientSecretTypeConfiguration : EntityTypeConfiguration<SqlIdnClientSecret>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientSecret"/>.
        /// </summary>
        public SqlIdnClientSecretTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_SECRETS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCSE_ID");
            Property(x => x.Value).HasColumnName("CCSE_VALUE");
            Property(x => x.Type).HasColumnName("CCSE_TYPE");
            Property(x => x.Description).HasColumnName("CCSE_DESCRIPTION");
            Property(x => x.Expiration).HasColumnName("CCSE_EXPIRATION");

            // SqlIdnClientSecret(N) <-> SqlIdnClient(1)
            HasRequired(x => x.Client)
                .WithMany(x => x.ClientSecrets)
                .WillCascadeOnDelete();
        }
    }
}
