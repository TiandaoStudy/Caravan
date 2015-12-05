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

using Finsa.Caravan.Common.Core;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using Finsa.CodeServices.Serialization;
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
    sealed class CaravanUserStore : ICaravanUserStore
    {
        /// <summary>
        ///   Inizializza lo store.
        /// </summary>
        /// <param name="appName">Nome dell'applicativo Caravan.</param>
        /// <param name="securityRepository">Il repository della sicurezza di Caravan.</param>
        public CaravanUserStore(string appName, ICaravanSecurityRepository securityRepository)
        {
            RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), ErrorMessages.NullOrWhiteSpaceAppName, nameof(appName));
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
        public Task CreateAsync(SecUser user) => SecurityRepository.AddUserAsync(AppName, user);

        /// <summary>
        ///   Updates a user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task UpdateAsync(SecUser user) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            Login = user.UserName, // Lo UserName di Identity è la Login di Caravan.
            FirstName = user.FirstName,
            LastName = user.LastName
        });

        /// <summary>
        ///   Deletes a user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task DeleteAsync(SecUser user) => SecurityRepository.RemoveUserAsync(AppName, user.Login);

        /// <summary>
        ///   Finds a user by ID.
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        public async Task<SecUser> FindByIdAsync(long userId)
        {
            try
            {
                return await SecurityRepository.GetUserByIdAsync(AppName, userId);
            }
            catch (SecUserNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        ///   Finds a user by name.
        /// </summary>
        /// <param name="userName"/>
        /// <returns/>
        public async Task<SecUser> FindByNameAsync(string userName)
        {
            try
            {
                return await SecurityRepository.GetUserByLoginAsync(AppName, userName);
            }
            catch (SecUserNotFoundException)
            {
                return null;
            }
        }

        #endregion IUserStore members

        #region IUserEmailStore members

        /// <summary>
        ///   Sets the user email.
        /// </summary>
        /// <param name="user"/>
        /// <param name="email"/>
        /// <returns/>
        public Task SetEmailAsync(SecUser user, string email) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            Email = email
        });

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
        public Task SetEmailConfirmedAsync(SecUser user, bool confirmed) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            EmailConfirmed = confirmed
        });

        /// <summary>
        ///   Returns the user associated with this email.
        /// </summary>
        /// <param name="email"/>
        /// <returns/>
        public async Task<SecUser> FindByEmailAsync(string email)
        {
            try
            {
                return await SecurityRepository.GetUserByEmailAsync(AppName, email);
            }
            catch (SecUserNotFoundException)
            {
                return null;
            }
        }

        #endregion IUserEmailStore members

        #region IUserPhoneNumberStore members

        /// <summary>
        ///   Sets the user phone number.
        /// </summary>
        /// <param name="user"/>
        /// <param name="phoneNumber"/>
        /// <returns/>
        public Task SetPhoneNumberAsync(SecUser user, string phoneNumber) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            PhoneNumber = phoneNumber
        });

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
        public Task SetPhoneNumberConfirmedAsync(SecUser user, bool confirmed) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            PhoneNumberConfirmed = confirmed
        });

        #endregion IUserPhoneNumberStore members

        #region IUserPasswordStore members

        /// <summary>
        ///   Sets the user password hash.
        /// </summary>
        /// <param name="user"/>
        /// <param name="passwordHash"/>
        /// <returns/>
        public Task SetPasswordHashAsync(SecUser user, string passwordHash)
        {
            return SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
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
        ///   Serializzatore binario usato per serializzare/deserializzare i claim degli utenti.
        /// 
        ///   Non viene passato al costruttore perché è importante che rimanga bloccato nel tempo.
        /// </summary>
        private static readonly BinarySerializer BinarySerializer = new BinarySerializer();

        /// <summary>
        ///   Returns the claims for the user with the issuer set.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<IList<Claim>> GetClaimsAsync(SecUser user)
        {
            // TODO
            if (user.Claims == null)
            {
                user.Claims = new SecClaim[0];
            }

            IList<Claim> claims = new Claim[user.Claims.Length];
            for (var i = 0; i < claims.Count; ++i)
            {
                var serializedClaim = user.Claims[i].Claim;
                claims[i] = BinarySerializer.DeserializeFromString<Claim>(serializedClaim);
            }
            return Task.FromResult(claims);
        }

        /// <summary>
        ///   Adds a new user claim.
        /// </summary>
        /// <param name="user"/>
        /// <param name="claim"/>
        /// <returns/>
        public Task AddClaimAsync(SecUser user, Claim claim)
        {
            var serializedClaim = BinarySerializer.SerializeToString(claim);
            return SecurityRepository.AddUserClaimAsync(AppName, user.Login, new SecClaim
            {
                Claim = serializedClaim
            });
        }

        /// <summary>
        ///   Removes a user claim.
        /// </summary>
        /// <param name="user"/>
        /// <param name="claim"/>
        /// <returns/>
        public Task RemoveClaimAsync(SecUser user, Claim claim)
        {
            var serializedClaim = BinarySerializer.SerializeToString(claim);
            return SecurityRepository.RemoveUserClaimAsync(AppName, user.Login, serializedClaim);
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
        public Task SetLockoutEndDateAsync(SecUser user, DateTimeOffset lockoutEnd) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            LockoutEndDate = lockoutEnd
        });

        /// <summary>
        ///   Used to record when an attempt to access the user has failed.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public async Task<int> IncrementAccessFailedCountAsync(SecUser user)
        {
            var newAccessFailedCount = user.AccessFailedCount + 1;
            await SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
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
        public Task ResetAccessFailedCountAsync(SecUser user) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            AccessFailedCount = 0
        });

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
        public Task SetLockoutEnabledAsync(SecUser user, bool enabled) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            LockoutEnabled = enabled
        });

        #endregion IUserLockoutStore members

        #region IUserRoleStore members

        /// <summary>
        ///   Adds a user to a role.
        /// </summary>
        /// <param name="user"/>
        /// <param name="identityRoleName"/>
        /// <returns/>
        public Task AddToRoleAsync(SecUser user, string identityRoleName)
        {
            var tuple = SecRole.FromIdentityRoleName(identityRoleName);
            return SecurityRepository.AddUserToRoleAsync(AppName, user.Login, tuple.Item1, tuple.Item2);
        }

        /// <summary>
        ///   Adds a user to a role.
        /// </summary>
        /// <param name="user"/>
        /// <param name="groupName"/>
        /// <param name="roleName"/>
        /// <returns/>
        public Task AddToRoleAsync(SecUser user, string groupName, string roleName) => SecurityRepository.AddUserToRoleAsync(AppName, user.Login, groupName, roleName);

        /// <summary>
        ///   Removes the role for the user.
        /// </summary>
        /// <param name="user"/>
        /// <param name="identityRoleName"/>
        /// <returns/>
        public Task RemoveFromRoleAsync(SecUser user, string identityRoleName)
        {
            var tuple = SecRole.FromIdentityRoleName(identityRoleName);
            return SecurityRepository.RemoveUserFromRoleAsync(AppName, user.Login, tuple.Item1, tuple.Item2);
        }

        /// <summary>
        ///   Removes the role for the user.
        /// </summary>
        /// <param name="user"/>
        /// <param name="groupName"/>
        /// <param name="roleName"/>
        /// <returns/>
        public Task RemoveFromRoleAsync(SecUser user, string groupName, string roleName) => SecurityRepository.RemoveUserFromRoleAsync(AppName, user.Login, groupName, roleName);

        /// <summary>
        ///   Returns the roles for this user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<IList<string>> GetRolesAsync(SecUser user) => Task.FromResult(user.Roles
            .Select(r => SecRole.ToIdentityRoleName(r.GroupName, r.Name))
            .ToArray() as IList<string>);

        /// <summary>
        ///   Returns true if a user is in the role.
        /// </summary>
        /// <param name="user"/>
        /// <param name="identityRoleName"/>
        /// <returns/>
        public Task<bool> IsInRoleAsync(SecUser user, string identityRoleName)
        {
            var tuple = SecRole.FromIdentityRoleName(identityRoleName);
            return Task.FromResult(user.Roles.Any(r => r.GroupName == tuple.Item1 && r.Name == tuple.Item2));
        }

        /// <summary>
        ///   Returns true if a user is in the role.
        /// </summary>
        /// <param name="user"/>
        /// <param name="groupName"/>
        /// <param name="roleName"/>
        /// <returns/>
        public Task<bool> IsInRoleAsync(SecUser user, string groupName, string roleName) => Task.FromResult(user.Roles.Any(r => r.GroupName == groupName && r.Name == roleName));

        #endregion IUserRoleStore members

        #region IUserSecurityStampStore members

        /// <summary>
        ///   Sets the security stamp for the user.
        /// </summary>
        /// <param name="user"/>
        /// <param name="stamp"/>
        /// <returns/>
        public Task SetSecurityStampAsync(SecUser user, string stamp) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            SecurityStamp = stamp
        });

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
        public Task SetTwoFactorEnabledAsync(SecUser user, bool enabled) => SecurityRepository.UpdateUserAsync(AppName, user.Login, new SecUserUpdates
        {
            TwoFactorAuthenticationEnabled = enabled
        });

        /// <summary>
        ///   Returns whether two factor authentication is enabled for the user.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public Task<bool> GetTwoFactorEnabledAsync(SecUser user) => Task.FromResult(user.TwoFactorAuthenticationEnabled);

        #endregion IUserTwoFactorStore members
    }
}
