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
using Finsa.Caravan.Common.Security.Models;
using Microsoft.AspNet.Identity;
using PommaLabs.Thrower;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security
{
    /// <summary>
    ///   Exposes user related APIs which will automatically save changes to the UserStore.
    /// </summary>
    public sealed class CaravanUserManager : UserManager<SecUser, long>
    {
        private ICaravanUserStore CaravanUserStore => Store as ICaravanUserStore;

        /// <summary>
        ///   Inizializza il gestore personalizzato.
        /// </summary>
        /// <param name="userStore">Lo store per gli utenti.</param>
        /// <param name="passwordHasher"></param>
        public CaravanUserManager(ICaravanUserStore userStore, IPasswordHasher passwordHasher)
            : base(userStore)
        {
            RaiseArgumentNullException.IfIsNull(passwordHasher, nameof(passwordHasher));
            PasswordHasher = passwordHasher;
        }

        /// <summary>
        ///   Creates a user with no password.
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        public override async Task<IdentityResult> CreateAsync(SecUser user)
        {
            ThrowIfDisposed();
            var result = await UserValidator.ValidateAsync(user).WithCurrentCulture();
            if (!result.Succeeded)
            {
                return result;
            }
            if (UserLockoutEnabledByDefault && SupportsUserLockout)
            {
                await CaravanUserStore.SetLockoutEnabledAsync(user, true).WithCurrentCulture();
            }
            await Store.CreateAsync(user).WithCurrentCulture();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>
        ///   Creates a user with the given password.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> CreateAsync(SecUser user, string password)
        {
            ThrowIfDisposed();
            var pwdResult = await PasswordValidator.ValidateAsync(password);
            if (!pwdResult.Succeeded)
            {
                return pwdResult;
            }
            var createResult = await CreateAsync(user);
            if (!createResult.Succeeded)
            {
                return createResult;
            }
            return await UpdatePassword(CaravanUserStore, user, password);
        }

        #region Role management

        /// <summary>
        ///   Adds a user to a role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AddToRoleAsync(long userId, string groupName, string roleName)
        {
            ThrowIfDisposed();
            var userRoleStore = CaravanUserStore;
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ErrorMessages.CaravanUserManager_UserIdNotFound, userId));
            }
            var userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
            var identityRoleName = SecRole.ToIdentityRoleName(groupName, roleName);
            if (userRoles.Contains(identityRoleName))
            {
                return new IdentityResult(ErrorMessages.CaravanUserManager_UserAlreadyInRole);
            }
            await userRoleStore.AddToRoleAsync(user, groupName, roleName).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>
        ///   Returns true if the user is in the specified role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<bool> IsInRoleAsync(long userId, string groupName, string roleName)
        {
            ThrowIfDisposed();
            var userRoleStore = CaravanUserStore;
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ErrorMessages.CaravanUserManager_UserIdNotFound, userId));
            }
            return await userRoleStore.IsInRoleAsync(user, groupName, roleName).WithCurrentCulture();
        }

        /// <summary>
        ///   Removes a user from a role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RemoveFromRoleAsync(long userId, string groupName, string roleName)
        {
            ThrowIfDisposed();
            var userRoleStore = CaravanUserStore;
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, ErrorMessages.CaravanUserManager_UserIdNotFound, userId));
            }
            if (!await userRoleStore.IsInRoleAsync(user, groupName, roleName).WithCurrentCulture())
            {
                return new IdentityResult(ErrorMessages.CaravanUserManager_UserNotInRole);
            }
            await userRoleStore.RemoveFromRoleAsync(user, groupName, roleName).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        #endregion Role management

        #region Dispose handling

        private bool _disposed;

        /// <summary>
        ///   When disposing, actually dipose the store.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion Dispose handling

        #region Security stamp handling

        private async Task UpdateSecurityStampInternal(SecUser user)
        {
            await CaravanUserStore.SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
        }

        private static string NewSecurityStamp() => Guid.NewGuid().ToString();

        #endregion
    }
}
