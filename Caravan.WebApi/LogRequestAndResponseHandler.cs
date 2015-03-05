using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Properties;
using Finsa.Caravan.DataAccess;
using PommaLabs.Extensions;

namespace Finsa.Caravan.WebApi
{
    public sealed class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Utilizzato per associare request e response nel log.
            var requestId = Guid.NewGuid().SafeToString();
            var userLogin = "TODO!!!";

            try
            {
                var requestBody = (request.Content == null) ? String.Empty : request.Content.ReadAsStringAsync().Result;

                var args = new[]
                {
                    KeyValuePair.Create("request_id", requestId),
                    KeyValuePair.Create("host", request.Headers.Host),
                    KeyValuePair.Create("user_agent", request.Headers.UserAgent.SafeToString()),
                    KeyValuePair.Create("uri", request.RequestUri.SafeToString()),
                    KeyValuePair.Create("method", request.Method.SafeToString()),
                    KeyValuePair.Create("headers", request.Headers.SafeToString())
                };

                Db.Logger.LogRawAsync(
                    LogType.Debug,
                    Settings.Default.ApplicationName,
                    userLogin,
                    GetType().FullName,
                    "SendAsync",
                    "Incoming request",
                    requestBody,
                    "Logging request",
                    args
                );
            }
            catch (Exception ex)
            {
                Db.Logger.LogRawAsync(
                    LogType.Error,
                    Settings.Default.ApplicationName,
                    userLogin,
                    GetType().FullName,
                    "SendAsync",
                    ex,
                    "Logging request",
                    new[]
                    {
                        KeyValuePair.Create("request_id", requestId)
                    }
                );
            }

            // Let other handlers process the request
            return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;

                try
                {
                    var responseBody = (response.Content == null) ? String.Empty : response.Content.ReadAsStringAsync().Result;

                    var args = new[]
                    {
                        KeyValuePair.Create("request_id", requestId),
                        KeyValuePair.Create("status_code", response.StatusCode.SafeToString())
                    };

                    Db.Logger.LogRawAsync(
                        LogType.Debug,
                        Settings.Default.ApplicationName,
                        userLogin,
                        GetType().FullName,
                        "SendAsync",
                        "Outgoing response",
                        responseBody,
                        "Logging response",
                        args
                    );
                }
                catch (Exception ex)
                {
                    Db.Logger.LogRawAsync(
                        LogType.Error,
                        Settings.Default.ApplicationName,
                        userLogin,
                        GetType().FullName,
                        "SendAsync",
                        ex,
                        "Logging response",
                        new[]
                        {
                            KeyValuePair.Create("request_id", requestId)
                        }
                    );
                }

                return response;
            }, cancellationToken);
        }
    }
}