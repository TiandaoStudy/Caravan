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

using Finsa.Caravan.DataAccess.Sql.Identity.Entities;
using Finsa.CodeServices.Clock;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework.Entities;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Stores
{
    internal sealed class SqlIdnAuthorizationCodeStore : AbstractSqlIdnTokenStore<AuthorizationCode>, IAuthorizationCodeStore
    {
        public SqlIdnAuthorizationCodeStore(SqlDbContext context, IScopeStore scopeStore, IClientStore clientStore, IClock clock)
            : base(context, TokenType.AuthorizationCode, scopeStore, clientStore, clock)
        {
        }

        public override async Task StoreAsync(string key, AuthorizationCode value)
        {
            Context.IdnTokens.Add(new SqlIdnToken
            {
                Key = key,
                SubjectId = value.SubjectId,
                ClientId = value.ClientId,
                JsonCode = ConvertToJson(value),
                Expiry = Clock.UtcNow.AddSeconds(value.Client.AuthorizationCodeLifetime),
                TokenTypeString = TokenTypeString
            });

            await Context.SaveChangesAsync();
        }
    }
}
