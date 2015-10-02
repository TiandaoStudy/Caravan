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
using IdentityServer3.EntityFramework.Entities;
using PommaLabs.Thrower;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Drivers.Sql.Identity.Stores
{
    internal sealed class SqlIdnClientStore : IClientStore
    {
        private readonly SqlDbContext _context;

        public SqlIdnClientStore(SqlDbContext context)
        {
            RaiseArgumentNullException.IfIsNull(context, nameof(context));
            _context = context;
        }

        public async Task<IdentityServer3.Core.Models.Client> FindClientByIdAsync(string clientId)
        {
            var client = await _context.IdnClients
                .Include(x => x.ClientSecrets)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCustomGrantTypes)
                .Include(x => x.AllowedCorsOrigins)
                .SingleOrDefaultAsync(x => x.ClientId == clientId);

            return client.ToModel();
        }
    }
}
