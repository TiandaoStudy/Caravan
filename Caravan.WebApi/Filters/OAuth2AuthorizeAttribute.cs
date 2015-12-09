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

using Finsa.Caravan.Common;
using Finsa.Caravan.WebApi.Identity;
using Finsa.Caravan.WebApi.Identity.Models;
using Finsa.Caravan.WebApi.Models.Identity;
using Ninject;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Finsa.Caravan.WebApi.Filters
{
    public sealed class OAuth2AuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///   Il tipo che implementa l'interfaccia <see cref="IAccessTokenExtractor"/> da istanziare per
        ///   recuperare gli access token dalle richieste HTTP.
        /// </summary>
        public Type AccessTokenExtractorType { get; set; } = typeof(BearerAccessTokenExtractor);

        /// <summary>
        ///   Il tipo che implementa l'interfaccia <see cref="IAuthorizationErrorHandler"/> da istanziare per
        ///   gestire eventuali errori riscontrati in fase di autorizzazione.
        /// </summary>
        public Type AuthorizationErrorHandlerType { get; set; } = typeof(IAuthorizationErrorHandler);

        /// <summary>
        ///   Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override async void OnActionExecuting(HttpActionContext actionContext)
        {
            await OnActionExecutingAsync(actionContext, CancellationToken.None);
        }

        /// <summary>
        ///   Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            try
            {
                var authorizationSettings = CaravanServiceProvider.NinjectKernel.Get<OAuth2AuthorizationSettings>();

                string accessToken;
                var accessTokenExtractor = LoadAccessTokenExtractor();
                if (!accessTokenExtractor.ExtractFromRequest(actionContext.Request, out accessToken))
                {
                    LoadAuthorizationErrorHandler().HandleError(actionContext, AuthorizationErrorContext.MissingAccessToken);
                    return;
                }

                var restClient = new RestClient(authorizationSettings.AccessTokenValidationUrl);
                var restRequest = new RestRequest(Method.POST);
                restRequest.AddParameter(new Parameter
                {
                    Name = "token",
                    Type = ParameterType.GetOrPost,
                    Value = accessToken
                });

                var restResponse = await restClient.ExecuteTaskAsync(restRequest);
            }
            catch (Exception ex)
            {

            }            
        }

        /// <summary>
        ///   Carica dinamicamente l'oggetto che si occupa dell'estrazione dei token.
        /// </summary>
        /// <returns>L'oggetto che si occupa dell'estrazione dei token.</returns>
        IAccessTokenExtractor LoadAccessTokenExtractor() => Activator.CreateInstance(AccessTokenExtractorType) as IAccessTokenExtractor;

        /// <summary>
        ///   Carica dinamicamente l'oggetto che si occupa della gestione degli errori.
        /// </summary>
        /// <returns>L'oggetto che si occupa della gestione degli errori.</returns>
        IAuthorizationErrorHandler LoadAuthorizationErrorHandler() => Activator.CreateInstance(AuthorizationErrorHandlerType) as IAuthorizationErrorHandler;
    }
}
