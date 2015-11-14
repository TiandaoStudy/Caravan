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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    public sealed class CaravanUserStore : IUserStore<SecUser>,
        IUserEmailStore<SecUser>,
        IUserPhoneNumberStore<SecUser>,
        IUserLoginStore<SecUser>,
        IUserPasswordStore<SecUser>
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
            // Nulla, perché questo store non apre connessioni.
        }

        public Task CreateAsync(SecUser user)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.AddUser(appName, user);
            return Task.FromResult(0);
        }

        public Task UpdateAsync(SecUser user)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.UpdateUser(appName, user.Login, new SecUserUpdates
            {
                PasswordHash = Option.Some(user.PasswordHash),
                Active = Option.Some(user.Active),
                FirstName = Option.Some(user.FirstName),
                LastName = Option.Some(user.LastName),
                Email = Option.Some(user.Email),
                EmailConfirmed = Option.Some(user.EmailConfirmed),
                PhoneNumber = Option.Some(user.PhoneNumber),
                PhoneNumberConfirmed = Option.Some(user.PhoneNumberConfirmed)
            });
            return Task.FromResult(0);
        }

        public Task DeleteAsync(SecUser user)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.RemoveUser(appName, user.Login);
            return Task.FromResult(0);
        }

        public Task<SecUser> FindByIdAsync(string userId)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            return Task.FromResult(_securityRepository.GetUserByLogin(appName, userId));
        }

        public Task<SecUser> FindByNameAsync(string userName)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            return Task.FromResult(_securityRepository.GetUserByLogin(appName, userName));
        }

        #endregion IUserStore members

        #region IUserEmailStore members

        public Task SetEmailAsync(SecUser user, string email)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.UpdateUser(appName, user.Login, new SecUserUpdates
            {
                Email = Option.Some(email)
            });
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(SecUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(SecUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(SecUser user, bool confirmed)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.UpdateUser(appName, user.Login, new SecUserUpdates
            {
                EmailConfirmed = Option.Some(confirmed)
            });
            return Task.FromResult(0);
        }

        public Task<SecUser> FindByEmailAsync(string email)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            return Task.FromResult(_securityRepository.GetUserByEmail(appName, email));
        }

        #endregion IUserEmailStore members

        #region IUserPhoneNumberStore members

        public Task SetPhoneNumberAsync(SecUser user, string phoneNumber)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.UpdateUser(appName, user.Login, new SecUserUpdates
            {
                PhoneNumber = Option.Some(phoneNumber)
            });
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(SecUser user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(SecUser user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(SecUser user, bool confirmed)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.UpdateUser(appName, user.Login, new SecUserUpdates
            {
                PhoneNumberConfirmed = Option.Some(confirmed)
            });
            return Task.FromResult(0);
        }

        #endregion IUserPhoneNumberStore members

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

        #endregion IUserLoginStore members

        #region IUserPasswordStore members

        public Task SetPasswordHashAsync(SecUser user, string passwordHash)
        {
            var appName = CaravanCommonConfiguration.Instance.AppName;
            _securityRepository.UpdateUser(appName, user.Login, new SecUserUpdates
            {
                PasswordHash = Option.Some(passwordHash)
            });
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(SecUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(SecUser user)
        {
            return Task.FromResult(string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        #endregion IUserPasswordStore members
    }
}
