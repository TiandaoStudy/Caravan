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
using Finsa.Caravan.WebApi.Identity.Models;
using PommaLabs.Thrower;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Validatore per gli access token calibrato per IdentityServer.
    /// </summary>
    public sealed class IdentityAccessTokenValidator : IAccessTokenValidator
    {
        /// <summary>
        ///   Inietta la dipendenze necessarie.
        /// </summary>
        /// <param name="authorizationSettings">Le impostazioni legate al server di OAuth2.</param>
        /// <param name="log">Istanza del log per questo filtro.</param>
        public IdentityAccessTokenValidator(OAuth2AuthorizationSettings authorizationSettings, ILog log)
        {
            RaiseArgumentNullException.IfIsNull(authorizationSettings, nameof(authorizationSettings));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            AuthorizationSettings = authorizationSettings;
            Log = log;
        }

        /// <summary>
        ///   Le impostazioni legate al server di OAuth2.
        /// </summary>
        public OAuth2AuthorizationSettings AuthorizationSettings { get; }

        /// <summary>
        ///   Istanza del log per questo filtro.
        /// </summary>
        public ILog Log { get; }

        /// <summary>
        ///   Inoltra l'access token al server di OAuth2 al fine di validarlo.
        /// </summary>
        /// <param name="actionContext">La richiesta HTTP.</param>
        /// <param name="accessToken">L'access token da validare.</param>
        /// <returns>Vero se il token è stato validato, falso altrimenti.</returns>
        public async Task<AuthorizationResult<dynamic>> ValidateAccessTokenAsync(HttpActionContext actionContext, string accessToken)
        {
            try
            {
                var restClient = new RestClient(AuthorizationSettings.AccessTokenValidationUrl);
                var restRequest = new RestRequest(Method.POST);
                restRequest.AddParameter(new Parameter
                {
                    Name = "token",
                    Type = ParameterType.GetOrPost,
                    Value = accessToken
                });

                var restResponse = await restClient.ExecuteTaskAsync<dynamic>(restRequest);
                if (restResponse.ResponseStatus == ResponseStatus.Aborted)
                {
                    return new AuthorizationResult<dynamic>
                    {
                        Authorized = false,
                        AuthorizationDeniedReason = $"Access token validation request was aborted"
                    };
                }
                if (restResponse.ResponseStatus == ResponseStatus.TimedOut)
                {
                    return new AuthorizationResult<dynamic>
                    {
                        Authorized = false,
                        AuthorizationDeniedReason = $"Access token validation request timed out"
                    };
                }
                if (restResponse.ErrorException != null)
                {
                    return new AuthorizationResult<dynamic>
                    {
                        Authorized = false,
                        AuthorizationDeniedException = restResponse.ErrorException
                    };
                }
                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    return new AuthorizationResult<dynamic>
                    {
                        Authorized = false,
                        AuthorizationDeniedReason = $"[StatusCode: {restResponse.StatusCode}, Content: '{restResponse.Content}']"
                    };
                }
                
                return new AuthorizationResult<dynamic>
                {
                    Authorized = true,
                    Payload = restResponse.Data
                };
            }
            catch (Exception ex)
            {
                Log.Error("Caught an exception while validating an access token", ex);
                return new AuthorizationResult<dynamic>
                {
                    Authorized = false,
                    AuthorizationDeniedException = ex
                };
            }
        }
    }
}
