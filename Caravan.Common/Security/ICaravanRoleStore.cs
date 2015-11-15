﻿// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using System.Threading.Tasks;
using Finsa.Caravan.Common.Security.Models;
using Microsoft.AspNet.Identity;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   RoleStore personalizzato per Caravan.
    /// </summary>
    public interface ICaravanRoleStore : IQueryableRoleStore<SecRole, int>
    {
        /// <summary>
        ///   Il nome dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        string AppName { get; }

        /// <summary>
        ///   Il repository della sicurezza di Caravan.
        /// </summary>
        ICaravanSecurityRepository SecurityRepository { get; }

        /// <summary>
        ///   Finds a role by Caravan group name and role name.
        /// </summary>
        /// <param name="groupName"/>
        /// <param name="roleName"/>
        /// <returns/>
        Task<SecRole> FindByNameAsync(string groupName, string roleName);
    }
}