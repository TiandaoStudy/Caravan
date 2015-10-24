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
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Services
{
    public sealed class SqlIdnClientConfigurationCorsPolicyService : ICorsPolicyService
    {
        readonly SqlDbContext _context;

        public SqlIdnClientConfigurationCorsPolicyService(SqlDbContext context)
        {
            RaiseArgumentNullException.IfIsNull(context);
            _context = context;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var query =
                from client in _context.IdnClients
                from allowed in client.AllowedCorsOrigins
                select allowed.Origin;

            var urls = await query.ToArrayAsync();

            var origins = urls.Select(GetOrigin).Where(x => x != null).Distinct();

            var result = origins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            return result;
        }

        private static string GetOrigin(string url)
        {
            if (url != null && (url.StartsWith("http://", StringComparison.Ordinal) || url.StartsWith("https://", StringComparison.Ordinal)))
            {
                var idx = url.IndexOf("//", StringComparison.Ordinal);
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2, StringComparison.Ordinal);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            return null;
        }
    }
}
