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

using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework.Entities;
using IdentityServer3.EntityFramework.Serialization;
using Newtonsoft.Json;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Stores
{
    internal abstract class AbstractSqlTokenStore<T> where T : class
    {
        protected readonly SqlDbContext _context;
        protected readonly TokenType _tokenType;
        protected readonly IScopeStore _scopeStore;
        protected readonly IClientStore _clientStore;

        protected AbstractSqlTokenStore(SqlDbContext context, TokenType tokenType, IScopeStore scopeStore, IClientStore clientStore)
        {
            RaiseArgumentNullException.IfIsNull(context, nameof(context));
            RaiseArgumentNullException.IfIsNull(scopeStore, nameof(scopeStore));
            RaiseArgumentNullException.IfIsNull(clientStore, nameof(clientStore));
            _context = context;
            _tokenType = tokenType;
            _scopeStore = scopeStore;
            _clientStore = clientStore;
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            settings.Converters.Add(new ClaimsPrincipalConverter());
            settings.Converters.Add(new ClientConverter(_clientStore));
            settings.Converters.Add(new ScopeConverter(_scopeStore));
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
            var token = await _context.IdnTokens.FindAsync(key, _tokenType);

            if (token == null || token.Expiry < DateTimeOffset.UtcNow)
            {
                return null;
            }

            return ConvertFromJson(token.JsonCode);
        }

        public async Task RemoveAsync(string key)
        {
            var token = await _context.IdnTokens.FindAsync(key, _tokenType);

            if (token != null)
            {
                _context.IdnTokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            var tokens = await _context.IdnTokens.Where(x =>
                x.SubjectId == subject &&
                x.TokenType == _tokenType).ToArrayAsync();

            var results = tokens.Select(x => ConvertFromJson(x.JsonCode)).ToArray();
            return results.Cast<ITokenMetadata>();
        }

        public async Task RevokeAsync(string subject, string client)
        {
            _context.IdnTokens.RemoveRange(_context.IdnTokens.Where(x =>
                x.SubjectId == subject &&
                x.ClientId == client &&
                x.TokenType == _tokenType));

            await _context.SaveChangesAsync();
        }

        public abstract Task StoreAsync(string key, T value);
    }
}