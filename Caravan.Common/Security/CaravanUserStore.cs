using Finsa.Caravan.Common.Models.Security;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Diagnostics;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    public class CaravanUserStore : IUserStore<SecUser>,
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
            var appName = Properties.Settings.Default.ApplicationName;
            _securityRepository.AddUser(appName, user);
            return Task.FromResult(0);
        }

        public Task UpdateAsync(SecUser user)
        {
            var appName = Properties.Settings.Default.ApplicationName;
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
            var appName = Properties.Settings.Default.ApplicationName;
            _securityRepository.RemoveUser(appName, user.Login);
            return Task.FromResult(0);
        }

        public Task<SecUser> FindByIdAsync(string userId)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            return Task.FromResult(_securityRepository.GetUserByLogin(appName, userId));
        }

        public Task<SecUser> FindByNameAsync(string userName)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            return Task.FromResult(_securityRepository.GetUserByLogin(appName, userName));
        }

        #endregion IUserStore members

        #region IUserEmailStore members

        public Task SetEmailAsync(SecUser user, string email)
        {
            var appName = Properties.Settings.Default.ApplicationName;
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
            var appName = Properties.Settings.Default.ApplicationName;
            _securityRepository.UpdateUser(appName, user.Login, new SecUserUpdates
            {
                EmailConfirmed = Option.Some(confirmed)
            });
            return Task.FromResult(0);
        }

        public Task<SecUser> FindByEmailAsync(string email)
        {
            var appName = Properties.Settings.Default.ApplicationName;
            return Task.FromResult(_securityRepository.GetUserByEmail(appName, email));
        }

        #endregion IUserEmailStore members

        #region IUserPhoneNumberStore members

        public Task SetPhoneNumberAsync(SecUser user, string phoneNumber)
        {
            var appName = Properties.Settings.Default.ApplicationName;
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
            var appName = Properties.Settings.Default.ApplicationName;
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
            var appName = Properties.Settings.Default.ApplicationName;
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
            return Task.FromResult(String.IsNullOrWhiteSpace(user.PasswordHash));
        }

        #endregion IUserPasswordStore members
    }
}