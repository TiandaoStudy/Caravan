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

using IdentityServer3.Core.Services;
using PommaLabs.Thrower;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Services
{
    internal sealed class SqlIdnCorsPolicyService : ICorsPolicyService
    {
        private readonly IDbContextFactory<SqlDbContext> _dbContextFactory;

        public SqlIdnCorsPolicyService(IDbContextFactory<SqlDbContext> dbContextFactory)
        {
            RaiseArgumentNullException.IfIsNull(dbContextFactory, nameof(dbContextFactory));
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var query = from client in ctx.IdnClients
                            from allowed in client.AllowedCorsOrigins
                            select allowed.Origin;

                var urls = await query.ToArrayAsync();

                var origins = urls.Select(GetOrigin).Where(x => x != null);

                var result = origins.Contains(origin, StringComparer.OrdinalIgnoreCase);

                return result;
            }
        }

        private static string GetOrigin(string url)
        {
            if (url == null)
            {
                // Non è possibile fare confronti con un indirizzo nullo.
                return null;
            }

            // Il tipo di confronto applicato di default nei passi successvi.
            const StringComparison cmp = StringComparison.OrdinalIgnoreCase;

            if ((url.StartsWith("http://", cmp) || url.StartsWith("https://", cmp)))
            {
                var idx = url.IndexOf("//", cmp);
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2, cmp);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            if (url.Equals("file://", cmp))
            {
                // Gestione aggiuntiva per consentire richieste da parte di applicazioni mobile
                // ibride, costruite con Cordova o PhoneGap.
                return url;
            }

            return null;
        }
    }
}
