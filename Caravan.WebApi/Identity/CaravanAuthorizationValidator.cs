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

using Common.Logging;
using Finsa.Caravan.Common.Identity;
using Finsa.Caravan.Common.Identity.Models;
using Finsa.Caravan.Common.Security;
using Finsa.Caravan.Common.Security.Exceptions;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.WebApi.Models;
using PommaLabs.Thrower;
using System;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Classe base che gestisce il caricamento di un utente Caravan al fine della validazione
    ///   della richiesta HTTP. Può essere estesa per supportare logiche più raffinate.
    /// </summary>
    public class CaravanAuthorizationValidator : IAuthorizationValidator
    {
        /// <summary>
        ///   Gestisce le dipendenze del modulo.
        /// </summary>
        /// <param name="log">Il log su cui annotare eventuali messaggi.</param>
        /// <param name="securityRepository">Il repository con cui gestire gli utenti.</param>
        /// <param name="allowedApps">Le applicazioni Caravan che possono usufruire del servizio.</param>
        public CaravanAuthorizationValidator(ILog log, ICaravanSecurityRepository securityRepository, CaravanAllowedAppsCollection allowedApps)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            RaiseArgumentNullException.IfIsNull(securityRepository, nameof(securityRepository));
            RaiseArgumentNullException.IfIsNull(allowedApps, nameof(allowedApps));
            Log = log;
            SecurityRepository = securityRepository;
            AllowedApps = allowedApps;
        }

        /// <summary>
        ///   Il log a disposizione del validatore.
        /// </summary>
        protected ILog Log { get; }

        /// <summary>
        ///   Il repository della sicurezza di Caravan.
        /// </summary>
        protected ICaravanSecurityRepository SecurityRepository { get; }

        /// <summary>
        ///   Le applicazioni che possono usufruire del servizio di autorizzazione.
        /// </summary>
        protected CaravanAllowedAppsCollection AllowedApps { get; }

        /// <summary>
        ///   Valida definitivamente l'accesso da parte di un dato utente. Se ritorna vero, l'utente
        ///   può accedere al servizio; se ritorna falso, l'utente non può accedere ed eventuali indicazioni
        /// </summary>
        /// <param name="actionContext">Il contesto HTTP da validare.</param>
        /// <param name="userClaims">I claim restituiti dal servizio che gestisce l'identità.</param>
        /// <returns>Vero se l'utente è stato autorizzato, falso altrimenti.</returns>
        public async Task<AuthorizationResult> ValidateRequestAsync(HttpActionContext actionContext, dynamic userClaims)
        {
            try
            {
                SecUserKey idnUserKey = SecUserKey.FromString(userClaims["sub"]);

                if (!AllowedApps.Contains(idnUserKey.AppName))
                {
                    return new AuthorizationResult
                    {
                        Authorized = false,
                        AuthorizationDeniedReason = $"Application '{idnUserKey.AppName}' has not been allowed"
                    };
                }

                var user = await SecurityRepository.GetUserByLoginAsync(idnUserKey.AppName, idnUserKey.UserLogin);
                AuthorizationResult authorizationResult = await ValidateRequestAsync(actionContext, userClaims, user);

                if (authorizationResult.Authorized)
                {
                    authorizationResult.Principal = new IdnPrincipal(user);
                }

                return authorizationResult;
            }
            catch (SecAppNotFoundException aex)
            {
                Log.Warn(aex.Message);
                return new AuthorizationResult
                {
                    Authorized = false,
                    AuthorizationDeniedReason = aex.Message
                };
            }
            catch (SecUserNotFoundException uex)
            {
                Log.Warn(uex.Message);
                return new AuthorizationResult
                {
                    Authorized = false,
                    AuthorizationDeniedReason = uex.Message
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new AuthorizationResult
                {
                    Authorized = false,
                    AuthorizationDeniedReason = ex.Message,
                    AuthorizationDeniedException = ex
                };
            }
        }

        /// <summary>
        ///   Valida definitivamente l'accesso da parte di un dato utente. Se ritorna vero, l'utente
        ///   può accedere al servizio; se ritorna falso, l'utente non può accedere ed eventuali indicazioni
        /// </summary>
        /// <param name="actionContext">La richiesta HTTP da validare.</param>
        /// <param name="userClaims">I claim restituiti dal servizio che gestisce l'identità.</param>
        /// <param name="user">L'utente caricato da Caravan.</param>
        /// <returns>Vero se l'utente è stato autorizzato, falso altrimenti.</returns>
        protected virtual Task<AuthorizationResult> ValidateRequestAsync(HttpActionContext actionContext, dynamic userClaims, SecUser user) => Task.FromResult(new AuthorizationResult
        {
            Authorized = true
        });
    }
}
