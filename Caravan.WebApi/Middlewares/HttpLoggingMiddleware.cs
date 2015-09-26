using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.WebApi.Core;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Serialization;
using Microsoft.Owin;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Finsa.Caravan.WebApi.Middlewares
{
    [Flags]
    public enum HttpLoggingFilter
    {
        LogResponseBody = 1,
        LogRequestBody = 2
    }

    /// <summary>
    ///   Middleware that logs HTTP requests and responses
    /// </summary>
    public sealed class HttpLoggingMiddleware : IDisposable
    {
        const int MinBufferSize = 512;

        readonly ICaravanLog _log;
        AppFunc _next;
        bool _disposed;

        public HttpLoggingMiddleware(ICaravanLog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _log = log;
            Filter = HttpLoggingFilter.LogRequestBody | HttpLoggingFilter.LogResponseBody;
        }

        public HttpLoggingFilter Filter { get; set; }

        public void Initialize(AppFunc next)
        {
            _next = next;
        }

        public void Dispose()
        {
            _disposed = true;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var owinContext = new OwinContext(environment);
            var request = owinContext.Request;
            var response = owinContext.Response;

            // Indica se è una richiesta per il logger: non vogliamo loggare le chiamate al log.
            // Inoltre, non devono finire nel log neanche le chiamate alle pagine di help di Swagger.
            var owinRequestUri = owinContext.Request.Uri.SafeToString().ToLowerInvariant();
            if (owinRequestUri.Contains("logger") || owinRequestUri.Contains("swagger"))
            {
                await _next.Invoke(environment);
                return;
            }

            // Utilizzato per associare request e response nel log.
            var requestId = UniqueIdGenerator.NewBase32("-");
            _log.ThreadVariablesContext.Set(Constants.RequestId, requestId);

            try
            {
                // Aggiungo l'ID della request agli header della response, in modo che sia più
                // facile il rintracciamento dei log su Caravan.
                response.Headers.Append(Constants.RequestIdHeader, requestId);
            }
            catch (Exception ex)
            {
                // Non è un problema se questa operazione fallisce, ma devo comunque registrare lo
                // strano evento.
                _log.Error("Could not add the Caravan request ID header...", ex);
            }

            // Log request
            if (!_disposed)
            {
                try
                {
                    var body = string.Empty;
                    if (Filter.HasFlag(HttpLoggingFilter.LogRequestBody))
                    {
                        body = await FormatBodyStreamAsync(request.Body);
                    }

                    _log.Trace(new LogMessage
                    {
                        ShortMessage = $"Request '{requestId}' at '{request.Uri.SafeToString()}'",
                        LongMessage = body,
                        Context = "Logging request",
                        Arguments = new[]
                        {
                            KeyValuePair.Create<string, object>("request_remote_ip_address", request.RemoteIpAddress),
                            KeyValuePair.Create<string, object>("request_user_agent", request.Headers.Get("User-Agent")),
                            KeyValuePair.Create<string, object>("request_uri", request.Uri),
                            KeyValuePair.Create<string, object>("request_method", request.Method),
                            KeyValuePair.Create<string, object>("request_headers", request.Headers.ToYamlString(LogMessage.ReadableYamlSettings))
                        }
                    });
                }
                catch (Exception ex)
                {
                    // Eccezione NON rilanciata.
                    _log.Exception(new LogMessage { Context = "Logging request", Exception = ex });
                }
            }

            // Buffer the response
            var responseStream = response.Body;
            var responseBuffer = new MemoryStream(MinBufferSize);
            response.Body = responseBuffer;

            try
            {
                // Run inner handlers
                await _next.Invoke(environment);
            }
            catch (Exception ex) when (_log.Fatal(new LogMessage { Context = "Processing request", Exception = ex }))
            {
                // Eccezione rilanciata in automatico, la funzione di log ritorna sempre FALSE.
            }

            // Log response
            if (!_disposed)
            {
                try
                {
                    var body = string.Empty;
                    if (Filter.HasFlag(HttpLoggingFilter.LogResponseBody))
                    {
                        body = await FormatBodyStreamAsync(response.Body);

                        // You need to do this so that the response we buffered is flushed out to
                        // the client application.
                        await responseBuffer.CopyToAsync(responseStream);
                        response.Body = responseStream;
                    }

                    _log.Trace(new LogMessage
                    {
                        ShortMessage = $"Response '{requestId}' for '{request.Uri.SafeToString()}'",
                        LongMessage = body,
                        Context = "Logging response",
                        Arguments = new[]
                        {
                            KeyValuePair.Create<string, object>("response_status_code", response.StatusCode),
                            KeyValuePair.Create<string, object>("response_headers", response.Headers.ToYamlString(LogMessage.ReadableYamlSettings))
                        }
                    });
                }
                catch (Exception ex)
                {
                    // Eccezione NON rilanciata.
                    _log.Exception(new LogMessage { Context = "Logging response", Exception = ex });
                }
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
                // Append the body contents to the StringBuilder
                body = await bodyReader.ReadToEndAsync();
                bodyStream.Seek(0, SeekOrigin.Begin);
            }

            return body;
        }
    }
}
