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

using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.WebApi.Core;
using Finsa.Caravan.WebApi.Middlewares.Models;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.IO.RecyclableMemoryStream;
using Finsa.CodeServices.Serialization;
using Microsoft.Owin;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.WebApi.Middlewares
{
    /// <summary>
    ///   Determina che cosa registrare nel log.
    /// </summary>
    [Flags]
    public enum HttpLoggingFilter
    {
        /// <summary>
        ///   Registra il body della request.
        /// </summary>
        LogResponseBody = 1,

        /// <summary>
        ///   Registra il body della response.
        /// </summary>
        LogRequestBody = 2
    }

    /// <summary>
    ///   Middleware that logs HTTP requests and responses.
    /// </summary>
    public sealed class HttpLoggingMiddleware : OwinMiddleware
    {
        private readonly Settings _settings;
        private readonly ICaravanLog _log;

        /// <summary>
        ///   Inizializza il componente usato per il logging.
        /// </summary>
        /// <param name="next">Un riferimento al prossimo componente della pipeline.</param>
        /// <param name="settings">Le impostazioni del componente.</param>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        public HttpLoggingMiddleware(OwinMiddleware next, Settings settings, ICaravanLog log)
            : base(next)
        {
            RaiseArgumentNullException.IfIsNull(settings, nameof(settings));
            RaiseArgumentNullException.IfIsNull(settings.IgnoredPaths, nameof(settings.IgnoredPaths));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _settings = settings;
            _log = log;
        }

        /// <summary>
        ///   Registra nel log delle informazioni relative a request e response.
        /// </summary>
        /// <param name="context">Il contesto di Owin.</param>
        /// <returns>Task per proseguire nella catena di middleware.</returns>
        public async override Task Invoke(IOwinContext context)
        {
            // Recupero le informazioni su request e response.
            var owinRequest = context.Request;
            var owinResponse = context.Response;
            var requestId = context.Environment[Constants.RequestIdHeader];

            // Indica se è una richiesta per il logger: non vogliamo loggare le chiamate al log.
            // Inoltre, non devono finire nel log neanche le chiamate alle pagine di help di Swagger.
            var owinRequestUri = owinRequest.Uri.SafeToString().ToLowerInvariant();
            var uriIsIgnored = _settings.IgnoredPaths.Any(iu => owinRequestUri.Contains(iu.Value));

            // Log request
            try
            {
                var body = string.Empty;
                if (_settings.Filter.HasFlag(HttpLoggingFilter.LogRequestBody) && !uriIsIgnored)
                {
                    body = await FormatBodyStreamAsync(owinRequest.Body);
                }

                _log.Trace(new LogMessage
                {
                    ShortMessage = $"Request '{requestId}' at '{owinRequest.Uri.SafeToString()}'",
                    LongMessage = body,
                    Context = "Logging request",
                    Arguments = new[]
                    {
                            KeyValuePair.Create<string, object>("request_remote_ip_address", owinRequest.RemoteIpAddress),
                            KeyValuePair.Create<string, object>("request_user_agent", owinRequest.Headers.Get("User-Agent")),
                            KeyValuePair.Create<string, object>("request_uri", owinRequest.Uri),
                            KeyValuePair.Create<string, object>("request_method", owinRequest.Method),
                            KeyValuePair.Create<string, object>("request_headers", owinRequest.Headers.ToYamlString(LogMessage.ReadableYamlSettings))
                        }
                });
            }
            catch (Exception ex)
            {
                // Eccezione NON rilanciata.
                _log.Catching(new LogMessage { Context = "Logging request", Exception = ex });
            }

            var responseStream = Stream.Null;
            var responseBuffer = RecyclableMemoryStreamManager.Instance.GetStream(Constants.ResponseBufferTag, Constants.MinResponseBufferSize);

            // Perform request - Buffer the response, only is the URI is not ignored
            if (!uriIsIgnored)
            {
                responseStream = owinResponse.Body;
                owinResponse.Body = responseBuffer;
            }

            try
            {
                // Run inner handlers.
                await Next.Invoke(context);
            }
            catch (Exception ex) when (_log.Fatal(new LogMessage { Context = "Processing request", Exception = ex }))
            {
                // Eccezione rilanciata in automatico, la funzione di log ritorna sempre FALSE.
            }

            // Log response
            try
            {
                var body = string.Empty;
                if (_settings.Filter.HasFlag(HttpLoggingFilter.LogResponseBody) && !uriIsIgnored)
                {
                    body = await FormatBodyStreamAsync(owinResponse.Body);

                    // You need to do this so that the response we buffered is flushed out to the
                    // client application.
                    Debug.Assert(responseStream != Stream.Null);
                    await responseBuffer.CopyToAsync(responseStream);
                    owinResponse.Body = responseStream;
                    responseBuffer.Dispose();
                }

                _log.Trace(new LogMessage
                {
                    ShortMessage = $"Response '{requestId}' for '{owinRequest.Uri.SafeToString()}'",
                    LongMessage = body,
                    Context = "Logging response",
                    Arguments = new[]
                    {
                            KeyValuePair.Create<string, object>("response_status_code", owinResponse.StatusCode),
                            KeyValuePair.Create<string, object>("response_headers", owinResponse.Headers.ToYamlString(LogMessage.ReadableYamlSettings))
                        }
                });
            }
            catch (Exception ex)
            {
                // Eccezione NON rilanciata.
                _log.Catching(new LogMessage { Context = "Logging response", Exception = ex });
            }
        }

        /// <summary>
        ///   Formats the contents of an HTTP request or response body into a string.
        /// </summary>
        /// <param name="bodyStream"></param>
        /// <returns>
        ///   The request or response body stream to use (must replace the current stream).
        /// </returns>
        static async Task<string> FormatBodyStreamAsync(Stream bodyStream)
        {
            if ((bodyStream == null) || !bodyStream.CanRead || !bodyStream.CanSeek)
            {
                return null;
            }

            var body = string.Empty;

            bodyStream.Seek(0, SeekOrigin.Begin);
            var bodyReader = new StreamReader(bodyStream);

            if (bodyReader.Peek() != -1)
            {
                // Append the body contents to the StringBuilder.
                body = await bodyReader.ReadToEndAsync();
                bodyStream.Seek(0, SeekOrigin.Begin);
            }

            return body;
        }

        /// <summary>
        ///   Impostazioni del componente di middleware.
        /// </summary>
        public sealed class Settings : AbstractMiddlewareSettings
        {
            /// <summary>
            ///   Determina se loggare la request, la response o entrambe.
            /// </summary>
            public HttpLoggingFilter Filter { get; set; } = HttpLoggingFilter.LogRequestBody | HttpLoggingFilter.LogResponseBody;

            /// <summary>
            ///   Percorsi che non devono essere loggati.
            /// </summary>
            public IList<PathString> IgnoredPaths { get; } = new List<PathString>
            {
                PathString.FromUriComponent("/logger"),
                PathString.FromUriComponent("/swagger"),
                PathString.FromUriComponent("/signalr")
            };
        }
    }
}
