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

using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Identity.Models;
using Finsa.Caravan.WebApi.Identity;
using Finsa.Caravan.WebApi.Identity.Models;
using Finsa.Caravan.WebApi.Models;
using Finsa.Caravan.WebApi.Models.Identity;
using Ninject;
using RestSharp;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Finsa.Caravan.WebApi.Filters
{
    /// <summary>
    ///   Gestisce l'autorizzazione comunicando con un server OAuth2.
    /// </summary>
    public sealed class OAuth2AuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///   Le impostazioni legate al server di OAuth2. Iniettate in automatico da Ninject.
        /// </summary>
        [Inject]
        public OAuth2AuthorizationSettings AuthorizationSettings { get; set; }

        /// <summary>
        ///   L'oggetto che implementa l'interfaccia <see cref="IAccessTokenExtractor"/> da usare
        ///   per recuperare gli access token dalle richieste HTTP. Iniettato in automatico da Ninject.
        /// </summary>
        [Inject]
        public IAccessTokenExtractor AccessTokenExtractor { get; set; }

        /// <summary>
        ///   L'oggetto che implementa l'interfaccia <see cref="IAuthorizationErrorHandler"/> da
        ///   usare per gestire eventuali errori riscontrati in fase di autorizzazione. Iniettato in
        ///   automatico da Ninject.
        /// </summary>
        [Inject]
        public IAuthorizationErrorHandler AuthorizationErrorHandler { get; set; }

        /// <summary>
        ///   Il tipo che valida in modo definito l'autorizzazione alla richiesta HTTP.
        /// </summary>
        public Type AuthorizationValidatorType { get; set; } = typeof(CaravanAuthorizationValidator);

        /// <summary>
        ///   Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            try
            {
                string accessToken;
                if (!await AccessTokenExtractor.ExtractFromRequestAsync(actionContext.Request, out accessToken))
                {
                    var message = $"{AccessTokenExtractor.GetType().Name} could not extract access token from HTTP request";
                    await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.MissingAccessToken, message);
                    return;
                }

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
                    var message = $"Access token validation request was aborted";
                    await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.InvalidAccessToken, message);
                    return;
                }
                if (restResponse.ResponseStatus == ResponseStatus.TimedOut)
                {
                    var message = $"Access token validation request timed out";
                    await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.InvalidAccessToken, message);
                    return;
                }
                if (restResponse.ErrorException != null)
                {
                    var message = restResponse.ErrorException.Message;
                    await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.InvalidAccessToken, restResponse.ErrorException);
                    return;
                }
                if (restResponse.StatusCode != HttpStatusCode.OK)
                {
                    var message = $"[StatusCode: {restResponse.StatusCode}, Content: '{restResponse.Content}']";
                    await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.InvalidAccessToken, message);
                    return;
                }

                var authorizationValidator = CaravanServiceProvider.NinjectKernel.Get(AuthorizationValidatorType) as IAuthorizationValidator;
                AuthorizationResult authorizationResult = await authorizationValidator.ValidateRequestAsync(actionContext, restResponse.Data);
                if (!authorizationResult.Authorized)
                {
                    var exception = authorizationResult.AuthorizationDeniedException;
                    if (exception != null)
                    {
                        await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.InvalidRequest, exception);
                    }
                    else
                    {
                        var reason = authorizationResult.AuthorizationDeniedReason;
                        await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.InvalidRequest, reason);
                    }
                }
                
                var principal = new IdnPrincipal(authorizationResult.User);
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;
            }
            catch (Exception ex) when (!(ex is HttpException))
            {
                await AuthorizationErrorHandler.HandleErrorAsync(actionContext, AuthorizationErrorContext.MissingAccessToken, ex);
            }
        }
    }
}
