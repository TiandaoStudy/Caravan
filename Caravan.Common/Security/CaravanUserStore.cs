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

using Finsa.Caravan.Common.Security.Models;
using Microsoft.AspNet.Identity;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Exposes basic user management APIs.
    /// </summary>
    /// <remarks>ASP.NET Identity USERS are mapped to Caravan security USERS.</remarks>
    public sealed class CaravanUserStore : IQueryableUserStore<SecUser, long>,
        IUserEmailStore<SecUser, long>,
        IUserPhoneNumberStore<SecUser, long>,
        IUserPasswordStore<SecUser, long>,
        IUserClaimStore<SecUser, long>,
        IUserLockoutStore<SecUser, long>,
        IUserRoleStore<SecUser, long>,
        IUserSecurityStampStore<SecUser, long>,
        IUserTwoFactorStore<SecUser, long>
    //NOT IMPLEMENTED:
    //IUserLoginStore<SecUser, long>
    {
        /// <summary>
        ///   Inizializza lo store.
        /// </summary>
        /// <param name="appName">Nome dell'applicativo Caravan.</param>
        /// <param name="securityRepository">Il repository della sicurezza di Caravan.</param>
        public CaravanUserStore(string appName, ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), "Application name cannot be null, empty or blank", nameof(appName));
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            AppName = appName;
            SecurityRepository = securityRepository;
        }

        /// <summary>
        ///   Il nome dell'applicativo Caravan per cui lo store è stato istanziato.
        /// </summary>
        public string AppName { get; }

        /// <summary>
        ///   Il repository della sicurezza di Caravan.
        /// </summary>
        public ICaravanSecurityRepository SecurityRepository { get; }

        #region IUserStore members

        /// <summary>
        ///   IQueryable users.
        /// </summary>
        public IQueryable<SecUser> Users => SecurityRepository.GetUsersAsync(AppName).Result.AsQueryable();

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting
        ///   unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Nulla, perché questo store non apre connessioni.
        }

        /// <summary>
        ///   Inserts a new user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public async Task CreateAsync(SecUser user)
        {
            await SecurityRepository.AddUserAsync(AppName, user);
        }

        /// <summary>
        ///   Updates a user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public async Task UpdateAsync(SecUser user)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                Login = user.UserName // Lo UserName di Identity è la Login di Caravan.
            });
        }

        /// <summary>
        ///   Deletes a user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public async Task DeleteAsync(SecUser user)
        {
            await SecurityRepository.RemoveUserByIdAsync(AppName, user.Id);
        }

        /// <summary>
        ///   Finds a user by ID.
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        public async Task<SecUser> FindByIdAsync(long userId) => await SecurityRepository.GetUserByIdAsync(AppName, userId);

        /// <summary>
        ///   Finds a user by name.
        /// </summary>
        /// <param name="userName"/>
        /// <returns/>
        public async Task<SecUser> FindByNameAsync(string userName) => await SecurityRepository.GetUserByLoginAsync(AppName, userName);

        #endregion IUserStore members

        #region IUserEmailStore members

        /// <summary>
        ///   Sets the user email.
        /// </summary>
        /// <param name="user"/>
        /// <param name="email"/>
        /// <returns/>
        public async Task SetEmailAsync(SecUser user, string email)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                Email = email
            });
        }

        /// <summary>
        ///   Gets the user email.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<string> GetEmailAsync(SecUser user) => Task.FromResult(user.Email);

        /// <summary>
        ///   Returns true if the user email is confirmed.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<bool> GetEmailConfirmedAsync(SecUser user) => Task.FromResult(user.EmailConfirmed);

        /// <summary>
        ///   Sets whether the user email is confirmed.
        /// </summary>
        /// <param name="user"/>
        /// <param name="confirmed"/>
        /// <returns/>
        public async Task SetEmailConfirmedAsync(SecUser user, bool confirmed)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                EmailConfirmed = confirmed
            });
        }

        /// <summary>
        ///   Returns the user associated with this email.
        /// </summary>
        /// <param name="email"/>
        /// <returns/>
        public async Task<SecUser> FindByEmailAsync(string email) => await SecurityRepository.GetUserByEmailAsync(AppName, email);

        #endregion IUserEmailStore members

        #region IUserPhoneNumberStore members

        /// <summary>
        ///   Sets the user phone number.
        /// </summary>
        /// <param name="user"/>
        /// <param name="phoneNumber"/>
        /// <returns/>
        public async Task SetPhoneNumberAsync(SecUser user, string phoneNumber)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                PhoneNumber = phoneNumber
            });
        }

        /// <summary>
        ///   Gets the user phone number.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<string> GetPhoneNumberAsync(SecUser user) => Task.FromResult(user.PhoneNumber);

        /// <summary>
        ///   Returns true if the user phone number is confirmed.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<bool> GetPhoneNumberConfirmedAsync(SecUser user) => Task.FromResult(user.PhoneNumberConfirmed);

        /// <summary>
        ///   Sets whether the user phone number is confirmed.
        /// </summary>
        /// <param name="user"/>
        /// <param name="confirmed"/>
        /// <returns/>
        public async Task SetPhoneNumberConfirmedAsync(SecUser user, bool confirmed)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                PhoneNumberConfirmed = confirmed
            });
        }

        #endregion IUserPhoneNumberStore members

        #region IUserPasswordStore members

        /// <summary>
        ///   Sets the user password hash.
        /// </summary>
        /// <param name="user"/>
        /// <param name="passwordHash"/>
        /// <returns/>
        public async Task SetPasswordHashAsync(SecUser user, string passwordHash)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                PasswordHash = passwordHash
            });
        }

        /// <summary>
        ///   Gets the user password hash.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<string> GetPasswordHashAsync(SecUser user) => Task.FromResult(user.PasswordHash);

        /// <summary>
        ///   Returns true if a user has a password set.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<bool> HasPasswordAsync(SecUser user) => Task.FromResult(string.IsNullOrWhiteSpace(user.PasswordHash));

        #endregion IUserPasswordStore members

        #region IUserClaimStore members

        /// <summary>
        ///   Returns the claims for the user with the issuer set.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<IList<Claim>> GetClaimsAsync(SecUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Adds a new user claim.
        /// </summary>
        /// <param name="user"/>
        /// <param name="claim"/>
        /// <returns/>
        public Task AddClaimAsync(SecUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Removes a user claim.
        /// </summary>
        /// <param name="user"/>
        /// <param name="claim"/>
        /// <returns/>
        public Task RemoveClaimAsync(SecUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        #endregion IUserClaimStore members

        #region IUserLockoutStore members

        /// <summary>
        ///   Returns the DateTimeOffset that represents the end of a user's lockout, any time in
        ///   the past should be considered not locked out.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(SecUser user) => Task.FromResult(user.LockoutEndDate);

        /// <summary>
        ///   Locks a user out until the specified end date (set to a past date, to unlock a user).
        /// </summary>
        /// <param name="user"/>
        /// <param name="lockoutEnd"/>
        /// <returns/>
        public async Task SetLockoutEndDateAsync(SecUser user, DateTimeOffset lockoutEnd)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                LockoutEndDate = lockoutEnd
            });
        }

        /// <summary>
        ///   Used to record when an attempt to access the user has failed.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public async Task<int> IncrementAccessFailedCountAsync(SecUser user)
        {
            var newAccessFailedCount = user.AccessFailedCount + 1;
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                AccessFailedCount = newAccessFailedCount
            });
            return newAccessFailedCount;
        }

        /// <summary>
        ///   Used to reset the access failed count, typically after the account is successfully accessed.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public async Task ResetAccessFailedCountAsync(SecUser user)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                AccessFailedCount = 0
            });
        }

        /// <summary>
        ///   Returns the current number of failed access attempts. This number usually will be
        ///   reset whenever the password is verified or the account is locked out.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<int> GetAccessFailedCountAsync(SecUser user) => Task.FromResult(user.AccessFailedCount);

        /// <summary>
        ///   Returns whether the user can be locked out.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<bool> GetLockoutEnabledAsync(SecUser user) => Task.FromResult(user.LockoutEnabled);

        /// <summary>
        ///   Sets whether the user can be locked out.
        /// </summary>
        /// <param name="user"/>
        /// <param name="enabled"/>
        /// <returns/>
        public async Task SetLockoutEnabledAsync(SecUser user, bool enabled)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                LockoutEnabled = enabled
            });
        }

        #endregion IUserLockoutStore members

        #region IUserRoleStore members

        /// <summary>
        ///   Adds a user to a role.
        /// </summary>
        /// <param name="user"/>
        /// <param name="roleName"/>
        /// <returns/>
        public async Task AddToRoleAsync(SecUser user, string roleName)
        {
            await SecurityRepository.AddUserToGroupAsync(AppName, user.Login, roleName);
        }

        /// <summary>
        ///   Removes the role for the user.
        /// </summary>
        /// <param name="user"/>
        /// <param name="roleName"/>
        /// <returns/>
        public async Task RemoveFromRoleAsync(SecUser user, string roleName)
        {
            await SecurityRepository.RemoveUserFromGroupAsync(AppName, user.Login, roleName);
        }

        /// <summary>
        ///   Returns the roles for this user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<IList<string>> GetRolesAsync(SecUser user) => Task.FromResult(user.Groups.Select(g => g.Name).ToArray() as IList<string>);

        /// <summary>
        ///   Returns true if a user is in the role.
        /// </summary>
        /// <param name="user"/>
        /// <param name="roleName"/>
        /// <returns/>
        public Task<bool> IsInRoleAsync(SecUser user, string roleName) => Task.FromResult(user.Groups.Any(g => g.Name == roleName));

        #endregion IUserRoleStore members

        #region IUserSecurityStampStore members

        /// <summary>
        ///   Sets the security stamp for the user.
        /// </summary>
        /// <param name="user"/>
        /// <param name="stamp"/>
        /// <returns/>
        public async Task SetSecurityStampAsync(SecUser user, string stamp)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                SecurityStamp = stamp
            });
        }

        /// <summary>
        ///   Gets the user security stamp.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<string> GetSecurityStampAsync(SecUser user) => Task.FromResult(user.SecurityStamp);

        #endregion IUserSecurityStampStore members

        #region IUserTwoFactorStore members

        /// <summary>
        ///   Sets whether two factor authentication is enabled for the user.
        /// </summary>
        /// <param name="user"/>
        /// <param name="enabled"/>
        /// <returns/>
        public async Task SetTwoFactorEnabledAsync(SecUser user, bool enabled)
        {
            await SecurityRepository.UpdateUserByIdAsync(AppName, user.Id, new SecUserUpdates
            {
                TwoFactorAuthenticationEnabled = enabled
            });
        }

        /// <summary>
        ///   Returns whether two factor authentication is enabled for the user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<bool> GetTwoFactorEnabledAsync(SecUser user) => Task.FromResult(user.TwoFactorAuthenticationEnabled);

        #endregion IUserTwoFactorStore members

        #region IUserLoginStore members - NOT IMPLEMENTED!

        /// <summary>
        ///   Adds a user login with the specified provider and key.
        /// </summary>
        /// <param name="user"/>
        /// <param name="login"/>
        /// <returns/>
        public Task AddLoginAsync(SecUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Removes the user login with the specified combination if it exists.
        /// </summary>
        /// <param name="user"/>
        /// <param name="login"/>
        /// <returns/>
        public Task RemoveLoginAsync(SecUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Returns the linked accounts for this user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(SecUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Returns the user associated with this login.
        /// </summary>
        /// <returns/>
        public Task<SecUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        #endregion IUserLoginStore members - NOT IMPLEMENTED!
    }
}
