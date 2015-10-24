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

using Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Entities;
using Finsa.CodeServices.Clock;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework.Entities;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Stores
{
    internal sealed class SqlIdnRefreshTokenStore : AbstractSqlIdnTokenStore<RefreshToken>, IRefreshTokenStore
    {
        public SqlIdnRefreshTokenStore(SqlDbContext context, IScopeStore scopeStore, IClientStore clientStore, IClock clock)
            : base(context, TokenType.RefreshToken, scopeStore, clientStore, clock)
        {
        }

        public override async Task StoreAsync(string key, RefreshToken value)
        {
            var token = await Context.IdnTokens.FindAsync(key, TokenTypeString);
            if (token == null)
            {
                token = new SqlIdnToken
                {
                    Key = key,
                    SubjectId = value.SubjectId,
                    ClientId = value.ClientId,
                    JsonCode = ConvertToJson(value),
                    TokenTypeString = TokenTypeString
                };
                Context.IdnTokens.Add(token);
            }

            token.Expiry = Clock.UtcNow.AddSeconds(value.LifeTime);
            await Context.SaveChangesAsync();
        }
    }
}
