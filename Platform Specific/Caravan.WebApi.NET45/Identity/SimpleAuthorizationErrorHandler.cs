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
using Ninject;
using PommaLabs.Thrower;
using System;
using System.Net;
using System.Web.Http.Controllers;

namespace Finsa.Caravan.WebApi.Identity
{
    /// <summary>
    ///   Alza un'eccezione riportante il tipo di errore ricevuto, nulla di più.
    /// </summary>
    public sealed class SimpleAuthorizationErrorHandler : IAuthorizationErrorHandler
    {
        /// <summary>
        ///   Istanza del log per questo filtro.
        /// </summary>
        [Inject]
        public ILog Log { get; set; }

        /// <summary>
        ///   Gestisce un errore avvenuto durante la validazione dell'accesso.
        /// </summary>
        /// <param name="actionContext">L'azione per cui si stava validando l'accesso.</param>
        /// <param name="errorContext">Il tipo di errore riscontrato.</param>
        public void HandleError(HttpActionContext actionContext, AuthorizationErrorContext errorContext)
        {
            var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = actionContext.ActionDescriptor.ActionName;
            var errorMessage = $"Access denied to {controllerName}.{actionName}. Reason: {errorContext}";
            Log.Error(errorMessage);
            throw new HttpException(HttpStatusCode.Unauthorized, errorMessage, new HttpExceptionInfo
            {
                UserMessage = errorMessage,
                ErrorCode = CaravanErrorCodes.CVE00000
            });
        }

        /// <summary>
        ///   Gestisce un errore avvenuto durante la validazione dell'accesso. Questa variante deve
        ///   gestire anche un eventuale oggetto riportante ulteriori informazioni sull'errore.
        /// </summary>
        /// <param name="actionContext">L'azione per cui si stava validando l'accesso.</param>
        /// <param name="errorContext">Il tipo di errore riscontrato.</param>
        /// <param name="payload">Un oggetto contenente ulteriori informazioni.</param>
        public void HandleError(HttpActionContext actionContext, AuthorizationErrorContext errorContext, object payload)
        {
            var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = actionContext.ActionDescriptor.ActionName;
            var errorMessage = $"Access denied to {controllerName}.{actionName}. Reason: {errorContext}. Payload: {payload.ToString()}";
            Log.Error(errorMessage);
            throw new HttpException(HttpStatusCode.Unauthorized, errorMessage, new HttpExceptionInfo
            {
                UserMessage = errorMessage,
                ErrorCode = CaravanErrorCodes.CVE00000
            });
        }

        /// <summary>
        ///   Gestisce un errore avvenuto durante la validazione dell'accesso. Questa variante deve
        ///   gestire anche una eventuale eccezione.
        /// </summary>
        /// <param name="actionContext">L'azione per cui si stava validando l'accesso.</param>
        /// <param name="errorContext">Il tipo di errore riscontrato.</param>
        /// <param name="exception">L'eccezione che è stata lanciata.</param>
        public void HandleError(HttpActionContext actionContext, AuthorizationErrorContext errorContext, Exception exception)
        {
            var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = actionContext.ActionDescriptor.ActionName;
            exception = exception.GetBaseException();
            var errorMessage = $"Access denied to {controllerName}.{actionName}. Reason: {errorContext}. Exception: {exception.Message}";
            Log.Error(errorMessage, exception);
            throw new HttpException(HttpStatusCode.Unauthorized, errorMessage, exception, new HttpExceptionInfo
            {
                UserMessage = errorMessage,
                ErrorCode = CaravanErrorCodes.CVE00000
            });
        }
    }
}
