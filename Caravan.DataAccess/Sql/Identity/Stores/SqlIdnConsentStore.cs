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

using Finsa.Caravan.DataAccess.Sql.Identity.Entities;
using IdentityServer3.Core.Services;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Stores
{
    internal sealed class SqlIdnConsentStore : IConsentStore
    {
        private readonly IDbContextFactory<SqlDbContext> _dbContextFactory;

        public SqlIdnConsentStore(IDbContextFactory<SqlDbContext> dbContextFactory)
        {
            RaiseArgumentNullException.IfIsNull(dbContextFactory, nameof(dbContextFactory));
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IdentityServer3.Core.Models.Consent> LoadAsync(string subject, string client)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var found = await ctx.IdnConsents.FindAsync(subject, client);
                if (found == null)
                {
                    return null;
                }

                var result = new IdentityServer3.Core.Models.Consent
                {
                    Subject = found.Subject,
                    ClientId = found.ClientId,
                    Scopes = ParseScopes(found.Scopes)
                };

                return result;
            }
        }

        public async Task UpdateAsync(IdentityServer3.Core.Models.Consent consent)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var item = await ctx.IdnConsents.FindAsync(consent.Subject, consent.ClientId);

                if (item == null)
                {
                    item = new SqlIdnConsent
                    {
                        Subject = consent.Subject,
                        ClientId = consent.ClientId
                    };
                    ctx.IdnConsents.Add(item);
                }

                if (consent.Scopes == null || !consent.Scopes.Any())
                {
                    ctx.IdnConsents.Remove(item);
                }

                item.Scopes = StringifyScopes(consent.Scopes);

                await ctx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IdentityServer3.Core.Models.Consent>> LoadAllAsync(string subject)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var found = await ctx.IdnConsents.Where(x => x.Subject == subject).ToArrayAsync();

                return found.Select(x => new IdentityServer3.Core.Models.Consent
                {
                    Subject = x.Subject,
                    ClientId = x.ClientId,
                    Scopes = ParseScopes(x.Scopes)
                }).ToArray();
            }
        }

        public async Task RevokeAsync(string subject, string client)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var found = await ctx.IdnConsents.FindAsync(subject, client);

                if (found != null)
                {
                    ctx.IdnConsents.Remove(found);
                    await ctx.SaveChangesAsync();
                }
            }
        }

        private static IEnumerable<string> ParseScopes(string scopes) => string.IsNullOrWhiteSpace(scopes)
            ? Enumerable.Empty<string>()
            : scopes.Split(',');

        private static string StringifyScopes(IEnumerable<string> scopes)
        {
            if (scopes == null || !scopes.Any())
            {
                return null;
            }

            return scopes.Aggregate((s1, s2) => s1 + "," + s2);
        }
    }
}
