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
using Finsa.Caravan.WebApi.Filters.Models;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Finsa.Caravan.WebApi.Filters
{
    /// <summary>
    ///   Filtro che si occupa della gestione degli errori del servizio.
    /// </summary>
    public class HttpExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        ///   Un log configurato per questo filtro.
        /// </summary>
        protected static readonly ILog Log = CaravanServiceProvider.FetchLog<AuthorizeForCaravanAttribute>();

        /// <summary>
        ///   La mappa contenente tutti i gestori degli errori. Può essere arricchita dai filtri che
        ///   estendono questo filtro.
        /// </summary>
        protected static readonly IDictionary<Type, Action<HttpActionExecutedContext, Exception>> ExceptionHandlers = new Dictionary<Type, Action<HttpActionExecutedContext, Exception>>
        {
            [typeof(HttpException)] = HandleHttpException
        };

        /// <summary>
        ///   Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            // Verifico che qualche altro filtro non abbia già analizzato l'eccezione.
            var ex = actionExecutedContext.Exception;
            if (ex == null)
            {
                Log.Debug("Exception has been already handled by another filter");
                return Task.FromResult(0);
            }

            // Recupero l'eccezione più interna, perché è quella che contiene il vero errore.
            // Annullo l'eccezione nel context così che non venga elaborata da altri filtri.
            ex = ex.GetBaseException();
            var exType = ex.GetType();
            actionExecutedContext.Exception = null;

            // Verifico se esiste un gestore specifico per quella eccezione. Se si, lo applico immediatamente.
            Action<HttpActionExecutedContext, Exception> handler;
            if (ExceptionHandlers.TryGetValue(exType, out handler))
            {
                handler(actionExecutedContext, ex);
                return Task.FromResult(0);
            }

            Log.Debug($"No handler was found for exception type {exType.FullName}");
            Log.Error($"Unhandled exception of type {exType.FullName}", ex);

            CreateExceptionResponse(actionExecutedContext, new HttpExceptionResponse
            {
                HttpStatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message,
                UserMessage = ex.Message,
                ErrorCode = CaravanErrorCodes.CVE99999.ToString("G")
            });
            return Task.FromResult(0);
        }

        /// <summary>
        ///   Crea una risposta di errore partendo dall'oggetto dato.
        /// </summary>
        /// <param name="actionExecutedContext">Il contesto per l'azione in corso.</param>
        /// <param name="response">L'oggetto su cui costruire la risposta.</param>
        protected static void CreateExceptionResponse(HttpActionExecutedContext actionExecutedContext, HttpExceptionResponse response)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(response.HttpStatusCode, response);
        }

        /// <summary>
        ///   Gestore per <see cref="HttpException"/>.
        /// </summary>
        /// <param name="actionExecutedContext">Il contesto per l'azione in corso.</param>
        /// <param name="exception">L'eccezione di tipo <see cref="HttpException"/>.</param>
        protected static void HandleHttpException(HttpActionExecutedContext actionExecutedContext, Exception exception)
        {
            var httpEx = exception as HttpException;
            // ReSharper disable once PossibleNullReferenceException

            Log.Error($"{httpEx.ErrorCode} - {httpEx.UserMessage ?? httpEx.Message}", httpEx);

            CreateExceptionResponse(actionExecutedContext, new HttpExceptionResponse
            {
                HttpStatusCode = httpEx.HttpStatusCode,
                Message = httpEx.Message,
                UserMessage = httpEx.UserMessage,
                ErrorCode = httpEx.ErrorCode
            });
        }
    }
}
