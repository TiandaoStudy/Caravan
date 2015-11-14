// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
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

using Finsa.Caravan.Common.Security.Models;
using Finsa.CodeServices.Common;
using Microsoft.AspNet.Identity;
using PommaLabs.Thrower;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    public sealed class CaravanRoleStore : IQueryableRoleStore<SecGroup, long>
    {
        private readonly ICaravanSecurityRepository _securityRepository;

        public CaravanRoleStore(ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            _securityRepository = securityRepository;
        }

        #region IQueryableRoleStore members

        /// <summary>
        ///   IQueryable Roles.
        /// </summary>
        public IQueryable<SecGroup> Roles
        {
            get
            {
                var appName = CaravanCommonConfiguration.Instance.AppName;
                return _securityRepository.GetGroupsAsync(appName).Result.AsQueryable();
            }
        }

        public void Dispose()
        {
            // Nulla, perché questo store non apre connessioni.
        }

        public async Task CreateAsync(SecGroup role)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            await _securityRepository.AddGroupAsync(appName, role);
        }

        public async Task UpdateAsync(SecGroup role)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            await _securityRepository.UpdateGroupByIdAsync(appName, role.Id, new SecGroupUpdates
            {
                Name = Option.Some(role.Name)
            });
        }

        public async Task DeleteAsync(SecGroup role)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            await _securityRepository.RemoveGroupAsync(appName, role.Name);
        }

        public async Task<SecGroup> FindByIdAsync(long roleId)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            return await _securityRepository.GetGroupByIdAsync(appName, roleId);
        }

        public async Task<SecGroup> FindByNameAsync(string roleName)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            return await _securityRepository.GetGroupByNameAsync(appName, roleName);
        }

        #endregion IQueryableRoleStore members
    }
}
