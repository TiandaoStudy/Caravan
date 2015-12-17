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
using Finsa.Caravan.Common;
using Finsa.Caravan.WebApi.Models;
using PommaLabs.Thrower;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Finsa.Caravan.WebApi.Filters
{
    /// <summary>
    ///   Gestisce le autorizzazioni per i servizi di Caravan. Di default tutti i servizi sono disabilitati.
    /// </summary>
    public sealed class AuthorizeForCaravanAttribute : ActionFilterAttribute
    {
        private static readonly ILog Log = CaravanServiceProvider.FetchLog<AuthorizeForCaravanAttribute>();

        /// <summary>
        ///   La funzione da modificare per gestire le autorizzazioni.
        /// </summary>
        public static Func<HttpActionContext, CancellationToken, ILog, Task<AuthorizationResult>> AuthorizationGranted { get; set; } = (actionContext, cancellationToken, log) =>
        {
            const string className = nameof(AuthorizeForCaravanAttribute);
            const string propName = nameof(AuthorizationGranted);
            var errorMessage = $"Caravan actions are disabled by default, you can enabled them by changing {className}.{propName}";
            log.Warn(errorMessage);
            return Task.FromResult(new AuthorizationResult
            {
                Authorized = false,
                AuthorizationDeniedReason = errorMessage
            });
        };

        /// <summary>
        ///   Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var authorizationResult = await AuthorizationGranted(actionContext, cancellationToken, Log);
            if (authorizationResult.Authorized)
            {
                return;
            }
            var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = actionContext.ActionDescriptor.ActionName;
            var errorMessage = $"Access denied to Caravan action {controllerName}.{actionName}";
            Log.Warn(errorMessage);
            throw new HttpException(HttpStatusCode.Unauthorized, errorMessage, new HttpExceptionInfo
            {
                ErrorCode = CaravanErrorCodes.CVE00000,
                UserMessage = authorizationResult.AuthorizationDeniedReason
            });
        }
    }
}
