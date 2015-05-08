using System;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Microsoft.AspNet.Identity;

namespace Finsa.Caravan.Common.Security
{
    public sealed class CaravanUserStore : IUserStore<SecUser>
    {
        private readonly ICaravanSecurityRepository _securityRepository;

        public CaravanUserStore(ICaravanSecurityRepository securityRepository)
        {
            Raise<ArgumentNullException>.IfIsNull(securityRepository);
            _securityRepository = securityRepository;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(SecUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SecUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(SecUser user)
        {
            _securityRepository.RemoveUser(Properties.Settings.Default.ApplicationName, user.Login);
            return Task.FromResult(0);
        }

        public Task<SecUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(_securityRepository.User(Properties.Settings.Default.ApplicationName, userId));
        }

        public Task<SecUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
