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

using Finsa.CodeServices.Clock;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework.Entities;
using IdentityServer3.EntityFramework.Serialization;
using Newtonsoft.Json;
using PommaLabs.Thrower;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Stores
{
    public abstract class AbstractSqlIdnTokenStore<T>
        where T : class
    {
        protected readonly IDbContextFactory<SqlDbContext> DbContextFactory;
        protected readonly string TokenTypeString;
        protected readonly IScopeStore ScopeStore;
        protected readonly IClientStore ClientStore;
        protected readonly IClock Clock;

        protected AbstractSqlIdnTokenStore(IDbContextFactory<SqlDbContext> dbContextFactory, TokenType tokenType, IScopeStore scopeStore, IClientStore clientStore, IClock clock)
        {
            RaiseArgumentNullException.IfIsNull(dbContextFactory, nameof(dbContextFactory));
            RaiseArgumentNullException.IfIsNull(scopeStore, nameof(scopeStore));
            RaiseArgumentNullException.IfIsNull(clientStore, nameof(clientStore));
            RaiseArgumentNullException.IfIsNull(clock, nameof(clock));
            DbContextFactory = dbContextFactory;
            TokenTypeString = tokenType.ToString().ToLowerInvariant();
            ScopeStore = scopeStore;
            ClientStore = clientStore;
            Clock = clock;
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            settings.Converters.Add(new ClaimsPrincipalConverter());
            settings.Converters.Add(new ClientConverter(ClientStore));
            settings.Converters.Add(new ScopeConverter(ScopeStore));
            return settings;
        }

        protected string ConvertToJson(T value)
        {
            return JsonConvert.SerializeObject(value, GetJsonSerializerSettings());
        }

        protected T ConvertFromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, GetJsonSerializerSettings());
        }

        public async Task<T> GetAsync(string key)
        {
            using (var ctx = DbContextFactory.Create())
            {
                var token = await ctx.IdnTokens.FindAsync(key, TokenTypeString);

                if (token == null || token.Expiry < Clock.UtcNowOffset)
                {
                    return null;
                }

                return ConvertFromJson(token.JsonCode);
            }                
        }

        public async Task RemoveAsync(string key)
        {
            using (var ctx = DbContextFactory.Create())
            {
                var token = await ctx.IdnTokens.FindAsync(key, TokenTypeString);

                if (token != null)
                {
                    ctx.IdnTokens.Remove(token);
                    await ctx.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            using (var ctx = DbContextFactory.Create())
            {
                var tokens = await ctx.IdnTokens.Where(x =>
                x.SubjectId == subject &&
                x.TokenTypeString == TokenTypeString).ToArrayAsync();

                var results = tokens.Select(x => ConvertFromJson(x.JsonCode)).ToArray();
                return results.Cast<ITokenMetadata>();
            }
        }

        public async Task RevokeAsync(string subject, string client)
        {
            using (var ctx = DbContextFactory.Create())
            {
                ctx.IdnTokens.RemoveRange(ctx.IdnTokens.Where(x =>
                x.SubjectId == subject &&
                x.ClientId == client &&
                x.TokenTypeString == TokenTypeString));

                await ctx.SaveChangesAsync();
            }
        }

        public abstract Task StoreAsync(string key, T value);
    }
}
