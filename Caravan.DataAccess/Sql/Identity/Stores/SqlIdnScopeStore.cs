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

using Finsa.Caravan.DataAccess.Sql.Identity.Extensions;
using IdentityServer3.Core.Services;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql.Identity.Stores
{
    public sealed class SqlIdnScopeStore : IScopeStore
    {
        private readonly SqlDbContext _context;

        public SqlIdnScopeStore(SqlDbContext context)
        {
            Raise<ArgumentNullException>.IfIsNull(context, nameof(context));
            _context = context;
        }

        public async Task<IEnumerable<IdentityServer3.Core.Models.Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            var scopes =
                from s in _context.IdnScopes.Include(x => x.ScopeClaims)
                select s;

            if (scopeNames != null && scopeNames.Any())
            {
                scopes = from s in scopes
                         where scopeNames.Contains(s.Name)
                         select s;
            }

            var list = await scopes.ToListAsync();
            return list.Select(x => x.ToModel());
        }

        public async Task<IEnumerable<IdentityServer3.Core.Models.Scope>> GetScopesAsync(bool publicOnly = true)
        {
            var scopes =
                from s in _context.IdnScopes.Include(s => s.ScopeClaims)
                select s;

            if (publicOnly)
            {
                scopes = from s in scopes
                         where s.ShowInDiscoveryDocument
                         select s;
            }

            var list = await scopes.ToListAsync();
            return list.Select(x => x.ToModel());
        }
    }
}
