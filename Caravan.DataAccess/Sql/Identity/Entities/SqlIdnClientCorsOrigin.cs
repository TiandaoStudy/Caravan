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

using IdentityServer3.EntityFramework.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="ClientCorsOrigin"/>.
    /// </summary>
    public class SqlIdnClientCorsOrigin : ClientCorsOrigin
    {
        [Key]
        public override int Id { get; set; }

        [Required]
        [StringLength(150)]
        public override string Origin { get; set; }

        public virtual int ClientId { get; set; }

        public virtual new SqlIdnClient Client { get; set; }
    }

    /// <summary>
    ///   Mappatura SQL per la classe <see cref="SqlIdnClientCorsOrigin"/>.
    /// </summary>
    public sealed class SqlIdnClientCorsOriginTypeConfiguration : EntityTypeConfiguration<SqlIdnClientCorsOrigin>
    {
        /// <summary>
        ///   Mappa la classe <see cref="SqlIdnClientCorsOrigin"/>.
        /// </summary>
        public SqlIdnClientCorsOriginTypeConfiguration()
        {
            ToTable("CRVN_IDN_CLI_CORS_ORIGINS", CaravanDataAccessConfiguration.Instance.SqlSchema);

            Property(x => x.Id).HasColumnName("CCCO_ID");
            Property(x => x.ClientId).HasColumnName("CCLI_ID");
            Property(x => x.Origin).HasColumnName("CCCO_ORIGIN");

            // SqlIdnClientCorsOrigin(N) <-> SqlIdnClient(1)
            HasRequired(x => x.Client)
                .WithMany(x => x.AllowedCorsOrigins)
                .HasForeignKey(x => x.ClientId)
                .WillCascadeOnDelete();
        }
    }
}
