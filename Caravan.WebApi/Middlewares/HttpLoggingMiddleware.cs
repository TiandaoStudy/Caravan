using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Diagnostics;
using Finsa.CodeServices.Common.Extensions;
using Microsoft.Owin;
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
        private const int MinBufferSize = 512;

        private readonly ICaravanLog _log;

        private AppFunc _next;
        private bool _disposed;

        public HttpLoggingMiddleware(ICaravanLog log)
        {
            Raise<ArgumentNullException>.IfIsNull(log);
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
            if (owinContext.Request.Uri.SafeToString().Contains("logger"))
            {
                await _next.Invoke(environment);
                return;
            }

            // Utilizzato per associare request e response nel log.
            var requestId = Guid.NewGuid();
            _log.GlobalVariablesContext.Set("request_id", requestId);

            // Log request
            if (!_disposed)
            {
                try
                {
                    var body = String.Empty;
                    if (Filter.HasFlag(HttpLoggingFilter.LogRequestBody))
                    {
                        body = await FormatBodyStreamAsync(request.Body);
                    }

                    _log.TraceArgs(() => new LogMessage
                    {
                        ShortMessage = String.Format("Request \"{0}\" at \"{1}\"", requestId, request.Uri.SafeToString()),
                        LongMessage = body,
                        Context = "Logging request",
                        Arguments = new[]
                        {
                            KeyValuePair.Create("from", request.RemoteIpAddress.SafeToString()),
                            KeyValuePair.Create("user_agent", request.Headers.Get("User-Agent").SafeToString()),
                            KeyValuePair.Create("uri", request.Uri.SafeToString()),
                            KeyValuePair.Create("method", request.Method.SafeToString()),
                            KeyValuePair.Create("headers", request.Headers.SafeToString())
                        }
                    });
                }
                catch (Exception ex)
                {
                    _log.ErrorArgs(() => new LogMessage
                    {
                        Exception = ex,
                        Context = "Logging request"
                    });
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
            catch (Exception ex)
            {
                _log.FatalArgs(() => new LogMessage
                {
                    Exception = ex,
                    Context = "Processing response"
                });

                // Should return a custom message?
                throw;
            }

            // Log response
            if (!_disposed)
            {
                try
                {
                    var body = String.Empty;
                    if (Filter.HasFlag(HttpLoggingFilter.LogResponseBody))
                    {
                        body = await FormatBodyStreamAsync(response.Body);

                        // You need to do this so that the response we buffered is flushed out to
                        // the client application.
                        await responseBuffer.CopyToAsync(responseStream);
                        response.Body = responseStream;
                    }

                    _log.TraceArgs(() => new LogMessage
                    {
                        ShortMessage = String.Format("Response \"{0}\" for \"{1}\"", requestId, request.Uri.SafeToString()),
                        LongMessage = body,
                        Context = "Logging response",
                        Arguments = new[]
                        {
                            KeyValuePair.Create("status_code", response.StatusCode.SafeToString())
                        }
                    });
                }
                catch (Exception ex)
                {
                    _log.ErrorArgs(() => new LogMessage
                    {
                        Exception = ex,
                        Context = "Logging response"
                    });
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
        private static async Task<string> FormatBodyStreamAsync(Stream bodyStream)
        {
            if ((bodyStream == null) || !bodyStream.CanRead || !bodyStream.CanSeek)
            {
                return null;
            }

            var body = String.Empty;

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