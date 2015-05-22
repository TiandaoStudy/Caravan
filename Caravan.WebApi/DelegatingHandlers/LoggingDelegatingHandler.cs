﻿using System;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Diagnostics;
using Finsa.CodeServices.Common.Extensions;
using Microsoft.Owin;

namespace Finsa.Caravan.WebApi.DelegatingHandlers
{
    /// <summary>
    ///   An handler to log both request and response information.
    /// </summary>
    public class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ICaravanLog _log;

        /// <summary>
        ///   Builds the handler using given log.
        /// </summary>
        /// <param name="log">The log used by the handler.</param>
        public LoggingDelegatingHandler(ICaravanLog log)
        {
            Raise<ArgumentNullException>.IfIsNull(log);
            _log = log;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Utilizzato per associare request e response nel log.
            var requestId = Guid.NewGuid();
            _log.GlobalVariablesContext.Set("request_id", requestId);

            try
            {
                var requestBody = (request.Content == null) ? String.Empty : request.Content.ReadAsStringAsync().Result;

                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Request \"{0}\" at \"{1}\"", requestId, request.RequestUri.SafeToString()),
                    LongMessage = requestBody,
                    Context = "Logging request",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("from", GetClientIP(request)),
                        KeyValuePair.Create("user_agent", request.Headers.UserAgent.SafeToString()),
                        KeyValuePair.Create("uri", request.RequestUri.SafeToString()),
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

            // Let other handlers process the request
            try
            {
                return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
                {
                    var response = task.Result;

                    try
                    {
                        var responseBody = (response.Content == null) ? String.Empty : response.Content.ReadAsStringAsync().Result;

                        _log.TraceArgs(() => new LogMessage
                        {
                            ShortMessage = String.Format("Response \"{0}\" for \"{1}\"", requestId, request.RequestUri.SafeToString()),
                            LongMessage = responseBody,
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

                    return response;
                }, cancellationToken);
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
                var ctx = (HttpContextWrapper) request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                var remoteEndpoint = (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin
            if (request.Properties.ContainsKey(OwinContext))
            {
                var owinContext = (OwinContext) request.Properties[OwinContext];
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
