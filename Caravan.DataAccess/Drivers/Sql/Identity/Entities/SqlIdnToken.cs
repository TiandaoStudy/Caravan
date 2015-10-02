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

using System.Data.Entity.ModelConfiguration;
using IdentityServer3.EntityFramework.Entities;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Entities
{
    /// <summary>
    ///   Riferimento interno per <see cref="Token"/>.
    /// </summary>
    public class SqlIdnToken : Token
    {
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

            Property(x => x.ClientId).HasColumnName("CCLI_CLIENT_ID");
        }
    }
}