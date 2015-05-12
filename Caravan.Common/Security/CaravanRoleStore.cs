using Finsa.Caravan.Common.Models.Security;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Diagnostics;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    public class CaravanRoleStore : IRoleStore<SecGroup>
    {
        private readonly ICaravanSecurityRepository _securityRepository;

        public CaravanRoleStore(ICaravanSecurityRepository securityRepository)
        {
            Raise<ArgumentNullException>.IfIsNull(securityRepository);
            _securityRepository = securityRepository;
        }

        #region IRoleStore members

        public void Dispose()
        {
            // Nulla, perché questo store non apre connessioni.
        }

        public Task CreateAsync(SecGroup role)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            _securityRepository.AddGroup(appName, role);
            return Task.FromResult(0);
        }

        public Task UpdateAsync(SecGroup role)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            _securityRepository.UpdateGroup(appName, role.Name, new SecGroupUpdates
            {
                Description = Option.Some(role.Description),
                Notes = Option.Some(role.Notes)
            });
            return Task.FromResult(0);
        }

        public Task DeleteAsync(SecGroup role)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            _securityRepository.RemoveGroup(appName, role.Name);
            return Task.FromResult(0);
        }

        public Task<SecGroup> FindByIdAsync(string roleId)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            return Task.FromResult(_securityRepository.GetGroupByName(appName, roleId));
        }

        public Task<SecGroup> FindByNameAsync(string roleName)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            return Task.FromResult(_securityRepository.GetGroupByName(appName, roleName));
        }

        #endregion IRoleStore members
    }
}