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

using Finsa.Caravan.Common.Identity;
using Finsa.Caravan.Common.Identity.Models;
using Finsa.Caravan.DataAccess.Sql.Identity.Extensions;
using IdentityServer3.Core.Services;
using PommaLabs.Thrower;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Stores
{
    public sealed class SqlIdnClientStore : ICaravanClientStore
    {
        private readonly SqlDbContext _context;

        public SqlIdnClientStore(SqlDbContext context)
        {
            RaiseArgumentNullException.IfIsNull(context, nameof(context));
            _context = context;
        }

        async Task<IdentityServer3.Core.Models.Client> IClientStore.FindClientByIdAsync(string clientId)
        {
            return await FindClientByIdAsync(clientId);
        }

        public async Task<IdnClient> FindClientByIdAsync(string clientId)
        {
            var client = await _context.IdnClients
                .Include(x => x.App)
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
