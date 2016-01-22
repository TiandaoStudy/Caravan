using Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.WebApi.Core;
using Finsa.CodeServices.Common;
using Microsoft.Owin;
using PommaLabs.Thrower;
using System;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Finsa.Caravan.WebApi.DelegatingHandlers
{
    /// <summary>
    ///   An handler to log both request and response information.
    /// </summary>
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        readonly ILog _log;

        /// <summary>
        ///   Builds the handler using given log.
        /// </summary>
        /// <param name="log">The log used by the handler.</param>
        public LoggingDelegatingHandler(ILog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _log = log;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Utilizzato per associare request e response nel log.
            var requestId = UniqueIdGenerator.NewBase32("-");
            _log.ThreadVariablesContext.Set(Constants.RequestId, requestId);

            // Indica se è una richiesta per il logger: non vogliamo loggare le chiamate al log.
            // Inoltre, non devono finire nel log neanche le chiamate alle pagine di help di Swagger.
            var owinRequestUri = request.RequestUri.SafeToString().ToLowerInvariant();
            if (owinRequestUri.Contains("logger") || owinRequestUri.Contains("swagger"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            try
            {
                var requestBody = (request.Content == null) ? string.Empty : request.Content.ReadAsStringAsync().Result;

                _log.Trace(new LogMessage
                {
                    ShortMessage = string.Format("Request '{0}' at '{1}'", requestId, request.RequestUri.SafeToString()),
                    LongMessage = requestBody,
                    Context = "Logging request",
                    Arguments = new[]
                    {
                        KeyValuePair.Create<string, object>("request_remote_ip_address", GetClientIP(request)),
                        KeyValuePair.Create<string, object>("request_user_agent", request.Headers.UserAgent),
                        KeyValuePair.Create<string, object>("request_uri", request.RequestUri),
                        KeyValuePair.Create<string, object>("request_method", request.Method),
                        KeyValuePair.Create<string, object>("request_headers", request.Headers)
                    }
                });
            }
            catch (Exception ex)
            {
                _log.Error(new LogMessage
                {
                    Exception = ex,
                    Context = "Logging request"
                });
            }

            // Let other handlers process the request
            try
            {
                return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
                {
                    var response = task.Result;

                    try
                    {
                        var responseBody = (response.Content == null) ? string.Empty : response.Content.ReadAsStringAsync().Result;

                        _log.Trace(new LogMessage
                        {
                            ShortMessage = string.Format("Response '{0}' for '{1}'", requestId, request.RequestUri.SafeToString()),
                            LongMessage = responseBody,
                            Context = "Logging response",
                            Arguments = new[]
                            {
                                KeyValuePair.Create<string, object>("response_status_code", response.StatusCode),
                                KeyValuePair.Create<string, object>("response_headers", response.Headers)
                            }
                        });

                        // Aggiungo l'ID della request agli header della response, in modo che sia
                        // più facile il rintracciamento dei log su Caravan.
                        response.Headers.Add(Constants.RequestIdHeader, requestId);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(new LogMessage
                        {
                            Exception = ex,
                            Context = "Logging response"
                        });
                    }

                    return response;
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                _log.Fatal(new LogMessage
                {
                    Exception = ex,
                    Context = "Processing response"
                });

                // Should return a custom message?
                throw;
            }
        }

        #region Lettura IP della sorgente della richiesta

        const string HttpContext = "MS_HttpContext";
        const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        const string OwinContext = "MS_OwinContext";

        static string GetClientIP(HttpRequestMessage request)
        {
            // Web-hosting
            if (request.Properties.ContainsKey(HttpContext))
            {
                var ctx = (HttpContextWrapper)request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                var remoteEndpoint = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin
            if (request.Properties.ContainsKey(OwinContext))
            {
                var owinContext = (OwinContext)request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }

        #endregion Lettura IP della sorgente della richiesta
    }
}
