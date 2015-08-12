using Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Diagnostics;
using Finsa.CodeServices.Common.Extensions;
using Microsoft.Owin;
using System;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Finsa.Caravan.Common.Logging.Models;

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
            _log.GlobalVariablesContext.Set("request_id", requestId);

            try
            {
                var requestBody = (request.Content == null) ? string.Empty : request.Content.ReadAsStringAsync().Result;

                _log.Trace(new LogMessage
                {
                    ShortMessage = string.Format("Request \"{0}\" at \"{1}\"", requestId, request.RequestUri.SafeToString()),
                    LongMessage = requestBody,
                    Context = "Logging request",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("request_remote_ip_address", GetClientIP(request)),
                        KeyValuePair.Create("request_user_agent", request.Headers.UserAgent.SafeToString()),
                        KeyValuePair.Create("request_uri", request.RequestUri.SafeToString()),
                        KeyValuePair.Create("request_method", request.Method.SafeToString()),
                        KeyValuePair.Create("request_headers", request.Headers.SafeToString())
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
                            ShortMessage = string.Format("Response \"{0}\" for \"{1}\"", requestId, request.RequestUri.SafeToString()),
                            LongMessage = responseBody,
                            Context = "Logging response",
                            Arguments = new[]
                            {
                                KeyValuePair.Create("response_status_code", response.StatusCode.SafeToString()),
                                KeyValuePair.Create("response_headers", response.Headers.SafeToString())
                            }
                        });
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

        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        private static string GetClientIP(HttpRequestMessage request)
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