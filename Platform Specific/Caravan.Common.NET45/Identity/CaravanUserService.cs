/*
 * Copyright 2015 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Finsa.Caravan.Common.Identity.Models;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Security.Models;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;
using Microsoft.AspNet.Identity;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Constants = IdentityServer3.Core.Constants;

namespace Finsa.Caravan.Common.Identity
{
    /// <summary>
    ///   Caravan implementation of IUserService with standard method implementations.
    /// </summary>
    public class CaravanUserService : UserServiceBase
    {
        /// <summary>
        ///   </summary>
        /// <param name="userManagerFactory">La gestione degli utenti.</param>
        /// <param name="clientStore">Lo store dei client di OAuth2.</param>
        /// <param name="allowedApps">
        ///   Le APP di Caravan che possono sfrutturare questo servizio. Questo paramentro serve per
        ///   limitare le applicazioni che possono utilizzare il servizio di autenticazione e di
        ///   autorizzazione, al fine di aumentare la sicurezza complessiva del processo.
        /// </param>
        public CaravanUserService(ICaravanUserManagerFactory userManagerFactory, ICaravanClientStore clientStore, CaravanAllowedAppsCollection allowedApps)
        {
            RaiseArgumentNullException.IfIsNull(userManagerFactory, nameof(userManagerFactory));
            RaiseArgumentNullException.IfIsNull(clientStore, nameof(clientStore));
            RaiseArgumentNullException.IfIsNull(allowedApps, nameof(allowedApps));
            UserManagerFactory = userManagerFactory;
            ClientStore = clientStore;
            AllowedApps = allowedApps;
            ConvertSubjectToKey = IdnUserKey.FromString;
            EnableSecurityStamp = true;
        }

        public string DisplayNameClaimType { get; set; }

        public bool EnableSecurityStamp { get; set; }

        protected ICaravanUserManagerFactory UserManagerFactory { get; }

        protected ICaravanClientStore ClientStore { get; }

        /// <summary>
        ///   Le applicazioni Caravan che possono effettuare il processo di autorizzazione e autenticazione.
        /// </summary>
        protected CaravanAllowedAppsCollection AllowedApps { get; }

        /// <summary>
        ///   La funzione utilizzata per leggere il subject.
        /// </summary>
        protected Func<string, IdnUserKey> ConvertSubjectToKey { get; }

        /// <summary>
        ///   This method is called whenever claims about the user are requested (e.g. during token
        ///   creation or via the userinfo endpoint).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns/>
        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            RaiseArgumentNullException.IfIsNull(context.Subject, nameof(context.Subject));

            var subject = context.Subject;
            var requestedClaimTypes = context.RequestedClaimTypes;
            var clientId = context.Client.ClientId;

            var appName = await FindAppNameByClientIdAsync(clientId);
            using (var userManager = await UserManagerFactory.CreateAsync(appName))
            {
                var idnUserKey = ConvertSubjectToKey(subject.GetSubjectId());
                RaiseArgumentException.If(appName != idnUserKey.AppName, "Invalid application name");
                var user = await userManager.FindByIdAsync(idnUserKey.UserId);
                RaiseArgumentException.If(user == null, "Invalid subject identifier");

                var claims = await GetClaimsFromAccount(userManager, user);
                if (requestedClaimTypes != null && requestedClaimTypes.Any())
                {
                    claims = claims.Where(x => requestedClaimTypes.Contains(x.Type));
                }

                context.IssuedClaims = claims;
            }
        }

        protected virtual async Task<IEnumerable<Claim>> GetClaimsFromAccount(CaravanUserManager userManager, SecUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(Constants.ClaimTypes.Subject, IdnUserKey.ToString(user.AppName, user.Id)),
                new Claim(Constants.ClaimTypes.PreferredUserName, user.UserName),
            };

            if (userManager.SupportsUserEmail)
            {
                var email = await userManager.GetEmailAsync(user.Id);
                if (!string.IsNullOrWhiteSpace(email))
                {
                    claims.Add(new Claim(Constants.ClaimTypes.Email, email));
                    var verified = await userManager.IsEmailConfirmedAsync(user.Id);
                    claims.Add(new Claim(Constants.ClaimTypes.EmailVerified, verified ? "true" : "false"));
                }
            }

            if (userManager.SupportsUserPhoneNumber)
            {
                var phone = await userManager.GetPhoneNumberAsync(user.Id);
                if (!string.IsNullOrWhiteSpace(phone))
                {
                    claims.Add(new Claim(Constants.ClaimTypes.PhoneNumber, phone));
                    var verified = await userManager.IsPhoneNumberConfirmedAsync(user.Id);
                    claims.Add(new Claim(Constants.ClaimTypes.PhoneNumberVerified, verified ? "true" : "false"));
                }
            }

            if (userManager.SupportsUserClaim)
            {
                claims.AddRange(await userManager.GetClaimsAsync(user.Id));
            }

            if (userManager.SupportsUserRole)
            {
                var roleClaims =
                    from role in await userManager.GetRolesAsync(user.Id)
                    select new Claim(Constants.ClaimTypes.Role, role);
                claims.AddRange(roleClaims);
            }

            return claims;
        }

        /// <summary>
        ///   This method gets called for local authentication (whenever the user uses the username
        ///   and password dialog).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns/>
        public override async Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var userName = context.UserName;
            var password = context.Password;
            var message = context.SignInMessage;

            context.AuthenticateResult = null;

            var appName = await FindAppNameByClientIdAsync(message.ClientId);
            using (var userManager = await UserManagerFactory.CreateAsync(appName))
            {
                if (userManager.SupportsUserPassword)
                {
                    var user = await FindUserAsync(userManager, userName);
                    if (user != null)
                    {
                        if (userManager.SupportsUserLockout &&
                            await userManager.IsLockedOutAsync(user.Id))
                        {
                            return;
                        }

                        if (await userManager.CheckPasswordAsync(user, password))
                        {
                            if (userManager.SupportsUserLockout)
                            {
                                await userManager.ResetAccessFailedCountAsync(user.Id);
                            }

                            var result = await PostAuthenticateLocalAsync(user, message);
                            if (result == null)
                            {
                                var claims = await GetClaimsForAuthenticateResult(userManager, user);
                                result = new AuthenticateResult(IdnUserKey.ToString(user.AppName, user.Id), await GetDisplayNameForAccountAsync(userManager, user.Id), claims);
                            }

                            context.AuthenticateResult = result;
                        }
                        else if (userManager.SupportsUserLockout)
                        {
                            await userManager.AccessFailedAsync(user.Id);
                        }
                    }
                }
            }
        }

        protected virtual async Task<string> GetDisplayNameForAccountAsync(CaravanUserManager userManager, long userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var claims = await GetClaimsFromAccount(userManager, user);

            Claim nameClaim = null;
            if (DisplayNameClaimType != null)
            {
                nameClaim = claims.FirstOrDefault(x => x.Type == DisplayNameClaimType);
            }
            if (nameClaim == null) nameClaim = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Name);
            if (nameClaim == null) nameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            if (nameClaim != null) return nameClaim.Value;

            return user.UserName;
        }

        protected virtual async Task<SecUser> FindUserAsync(CaravanUserManager userManager, string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        protected virtual Task<AuthenticateResult> PostAuthenticateLocalAsync(SecUser user, SignInMessage message)
        {
            return Task.FromResult<AuthenticateResult>(null);
        }

        protected virtual async Task<IEnumerable<Claim>> GetClaimsForAuthenticateResult(CaravanUserManager userManager, SecUser user)
        {
            var claims = new List<Claim>();
            if (EnableSecurityStamp && userManager.SupportsUserSecurityStamp)
            {
                var stamp = await userManager.GetSecurityStampAsync(user.Id);
                if (!string.IsNullOrWhiteSpace(stamp))
                {
                    claims.Add(new Claim("security_stamp", stamp));
                }
            }
            return claims;
        }

        /// <summary>
        ///   This method gets called when the user uses an external identity provider to
        ///   authenticate. The user's identity from the external provider is passed via the
        ///   `externalUser` parameter which contains the provider identifier, the provider's
        ///   identifier for the user, and the claims from the provider for the external user.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns/>
        public override async Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            RaiseArgumentNullException.IfIsNull(context.ExternalIdentity, nameof(context.ExternalIdentity));

            var externalUser = context.ExternalIdentity;
            var message = context.SignInMessage;

            var appName = await FindAppNameByClientIdAsync(message.ClientId);
            using (var userManager = await UserManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindAsync(new UserLoginInfo(externalUser.Provider, externalUser.ProviderId));
                if (user == null)
                {
                    context.AuthenticateResult = await ProcessNewExternalAccountAsync(userManager, externalUser.Provider, externalUser.ProviderId, externalUser.Claims);
                }
                else
                {
                    context.AuthenticateResult = await ProcessExistingExternalAccountAsync(userManager, user.Id, externalUser.Provider, externalUser.ProviderId, externalUser.Claims);
                }
            }
        }

        protected virtual async Task<AuthenticateResult> ProcessNewExternalAccountAsync(CaravanUserManager userManager, string provider, string providerId, IEnumerable<Claim> claims)
        {
            var user = await TryGetExistingUserFromExternalProviderClaimsAsync(provider, claims);
            if (user == null)
            {
                user = await InstantiateNewUserFromExternalProviderAsync(provider, providerId, claims);
                if (user == null)
                    throw new InvalidOperationException("CreateNewAccountFromExternalProvider returned null");

                var createResult = await userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return new AuthenticateResult(createResult.Errors.First());
                }
            }

            var externalLogin = new UserLoginInfo(provider, providerId);
            var addExternalResult = await userManager.AddLoginAsync(user.Id, externalLogin);
            if (!addExternalResult.Succeeded)
            {
                return new AuthenticateResult(addExternalResult.Errors.First());
            }

            var result = await AccountCreatedFromExternalProviderAsync(userManager, user.Id, provider, providerId, claims);
            if (result != null) return result;

            return await SignInFromExternalProviderAsync(userManager, user.Id, provider);
        }

        protected virtual Task<SecUser> InstantiateNewUserFromExternalProviderAsync(string provider, string providerId, IEnumerable<Claim> claims)
        {
            var user = new SecUser { UserName = Guid.NewGuid().ToString("N") };
            return Task.FromResult(user);
        }

        protected virtual Task<SecUser> TryGetExistingUserFromExternalProviderClaimsAsync(string provider, IEnumerable<Claim> claims)
        {
            return Task.FromResult<SecUser>(null);
        }

        protected virtual async Task<AuthenticateResult> AccountCreatedFromExternalProviderAsync(CaravanUserManager userManager, long userId, string provider, string providerId, IEnumerable<Claim> claims)
        {
            claims = await SetAccountEmailAsync(userManager, userId, claims);
            claims = await SetAccountPhoneAsync(userManager, userId, claims);

            return await UpdateAccountFromExternalClaimsAsync(userManager, userId, provider, providerId, claims);
        }

        protected virtual async Task<AuthenticateResult> SignInFromExternalProviderAsync(CaravanUserManager userManager, long userId, string provider)
        {
            var user = await userManager.FindByIdAsync(userId);
            var claims = await GetClaimsForAuthenticateResult(userManager, user);

            return new AuthenticateResult(
                userId.ToString(),
                await GetDisplayNameForAccountAsync(userManager, userId),
                claims,
                authenticationMethod: Constants.AuthenticationMethods.External,
                identityProvider: provider);
        }

        protected virtual async Task<AuthenticateResult> UpdateAccountFromExternalClaimsAsync(CaravanUserManager userManager, long userId, string provider, string providerId, IEnumerable<Claim> claims)
        {
            var existingClaims = await userManager.GetClaimsAsync(userId);
            var intersection = existingClaims.Intersect(claims, new ClaimComparer());
            var newClaims = claims.Except(intersection, new ClaimComparer());

            foreach (var claim in newClaims)
            {
                var result = await userManager.AddClaimAsync(userId, claim);
                if (!result.Succeeded)
                {
                    return new AuthenticateResult(result.Errors.First());
                }
            }

            return null;
        }

        protected virtual async Task<AuthenticateResult> ProcessExistingExternalAccountAsync(CaravanUserManager userManager, long userId, string provider, string providerId, IEnumerable<Claim> claims)
        {
            return await SignInFromExternalProviderAsync(userManager, userId, provider);
        }

        protected virtual async Task<IEnumerable<Claim>> SetAccountEmailAsync(CaravanUserManager userManager, long userId, IEnumerable<Claim> claims)
        {
            var email = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Email);
            if (email != null)
            {
                var userEmail = await userManager.GetEmailAsync(userId);
                if (userEmail == null)
                {
                    // if this fails, then presumably the email is already associated with another
                    // account so ignore the error and let the claim pass thru
                    var result = await userManager.SetEmailAsync(userId, email.Value);
                    if (result.Succeeded)
                    {
                        var emailVerified = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.EmailVerified);
                        if (emailVerified != null && emailVerified.Value == "true")
                        {
                            var token = await userManager.GenerateEmailConfirmationTokenAsync(userId);
                            await userManager.ConfirmEmailAsync(userId, token);
                        }

                        var emailClaims = new[] { Constants.ClaimTypes.Email, Constants.ClaimTypes.EmailVerified };
                        return claims.Where(x => !emailClaims.Contains(x.Type));
                    }
                }
            }

            return claims;
        }

        protected virtual async Task<IEnumerable<Claim>> SetAccountPhoneAsync(CaravanUserManager userManager, long userID, IEnumerable<Claim> claims)
        {
            var phone = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.PhoneNumber);
            if (phone != null)
            {
                var userPhone = await userManager.GetPhoneNumberAsync(userID);
                if (userPhone == null)
                {
                    // if this fails, then presumably the phone is already associated with another
                    // account so ignore the error and let the claim pass thru
                    var result = await userManager.SetPhoneNumberAsync(userID, phone.Value);
                    if (result.Succeeded)
                    {
                        var phoneVerified = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.PhoneNumberVerified);
                        if (phoneVerified != null && phoneVerified.Value == "true")
                        {
                            var token = await userManager.GenerateChangePhoneNumberTokenAsync(userID, phone.Value);
                            await userManager.ChangePhoneNumberAsync(userID, phone.Value, token);
                        }

                        var phoneClaims = new[] { Constants.ClaimTypes.PhoneNumber, Constants.ClaimTypes.PhoneNumberVerified };
                        return claims.Where(x => !phoneClaims.Contains(x.Type));
                    }
                }
            }

            return claims;
        }

        /// <summary>
        ///   This method gets called whenever identity server needs to determine if the user is
        ///   valid or active (e.g. if the user's account has been deactivated since they logged in,
        ///   or e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns/>
        public override async Task IsActiveAsync(IsActiveContext context)
        {
            RaiseArgumentNullException.IfIsNull(context.Subject, nameof(context.Subject));

            var subject = context.Subject;

            var id = subject.GetSubjectId();
            var idnUserKey = ConvertSubjectToKey(id);

            var appName = await FindAppNameByClientIdAsync(context.Client.ClientId);
            RaiseArgumentException.If(appName != idnUserKey.AppName, "Invalid application name");
            using (var userManager = await UserManagerFactory.CreateAsync(appName))
            {
                var user = await userManager.FindByIdAsync(idnUserKey.UserId);

                context.IsActive = false;

                if (user != null)
                {
                    if (EnableSecurityStamp && userManager.SupportsUserSecurityStamp)
                    {
                        var securityStamp = subject.Claims.Where(x => x.Type == "security_stamp").Select(x => x.Value).SingleOrDefault();
                        if (securityStamp != null)
                        {
                            var dbSecurityStamp = await userManager.GetSecurityStampAsync(idnUserKey.UserId);
                            if (dbSecurityStamp != securityStamp)
                            {
                                return;
                            }
                        }
                    }

                    context.IsActive = true;
                }
            }
        }

        private async Task<string> FindAppNameByClientIdAsync(string clientId)
        {
            var client = await ClientStore.FindClientByIdAsync(clientId);
            RaiseArgumentException.If(client == null, "Invalid client identifier");

            var appName = client.AppName;
            RaiseInvalidOperationException.IfNot(AllowedApps.Contains(appName), $"Application {appName} has not been allowed");
            return appName;
        }

        sealed class ClaimComparer : IEqualityComparer<Claim>
        {
            public bool Equals(Claim x, Claim y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null || x.Type != y.Type)
                    return false;
                return x.Value == y.Value;
            }

            public int GetHashCode(Claim claim)
            {
                if (ReferenceEquals(claim, null))
                    return 0;
                return (claim.Type?.GetHashCode() ?? 0) ^ (claim.Value?.GetHashCode() ?? 0);
            }
        }
    }
}
