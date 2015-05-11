using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Microsoft.AspNet.Identity;

namespace Finsa.Caravan.Common.Security
{
    public sealed class CaravanUserStore : IUserStore<SecUser>, IUserLoginStore<SecUser>, IUserPasswordStore<SecUser>
    {
        private readonly ICaravanSecurityRepository _securityRepository;

        public CaravanUserStore(ICaravanSecurityRepository securityRepository)
        {
            Raise<ArgumentNullException>.IfIsNull(securityRepository);
            _securityRepository = securityRepository;
        }

        #region IUserStore members

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
            return Task.FromResult(_securityRepository.User(Properties.Settings.Default.ApplicationName, userName));
        }

        #endregion

        #region IUserLoginStore members

        public Task AddLoginAsync(SecUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(SecUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(SecUser user)
        {
            throw new NotImplementedException();
        }

        public Task<SecUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserPasswordStore members

        public Task SetPasswordHashAsync(SecUser user, string passwordHash)
        {
            return Task.Run(() =>
            {
                user.HashedPassword = passwordHash;
            });
        }

        public Task<string> GetPasswordHashAsync(SecUser user)
        {
            return Task.FromResult(user.HashedPassword);
        }

        public Task<bool> HasPasswordAsync(SecUser user)
        {
            return Task.FromResult(String.IsNullOrWhiteSpace(user.HashedPassword));
        }

        #endregion
    }
}
