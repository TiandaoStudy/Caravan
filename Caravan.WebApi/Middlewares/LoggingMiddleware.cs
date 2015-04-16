using System.Text;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Utilities;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Finsa.Caravan.Common.Utilities.Extensions;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Finsa.Caravan.WebApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly ICaravanLog _log;
        private AppFunc _next;

        public LoggingMiddleware(ICaravanLog log)
        {
            Raise<ArgumentNullException>.IfIsNull(log);
            _log = log;
        }

        public void Initialize(AppFunc next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            // Apre un contesto, dove è più facile recuperare le variabili.
            var context = new OwinContext(environment);

            // Utilizzato per associare request e response nel log.
            var requestId = Guid.NewGuid();
            _log.GlobalVariablesContext.Set("request_id", requestId);

            // Gestisco eventuali errori prodotti durante la registrazione della request.
            try
            {
                var requestBody = ReadAndResetStream(context.Request.Body);
                context.Request.Body = new MemoryStream(Encoding.Default.GetBytes(requestBody));
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Request \"{0}\" at \"{1}\"", requestId, context.Request.Uri.SafeToString()),
                    LongMessage = requestBody,
                    Context = "Logging request",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("from", context.Request.RemoteIpAddress),
                        KeyValuePair.Create("user_agent", context.Request.Headers["User-Agent"].SafeToString()),
                        KeyValuePair.Create("uri", context.Request.Uri.SafeToString()),
                        KeyValuePair.Create("method", context.Request.Method.SafeToString()),
                        KeyValuePair.Create("headers", context.Request.Headers.LogAsJson())
                    }
                });
            }
            catch (Exception ex)
            {
                _log.TraceArgs(() => new LogMessage
                {
                    Exception = ex,
                    Context = "Logging request"
                });
            }

            // Eseguo il prossimo componente.
            await _next.Invoke(environment);

            // Gestisco eventuali errori prodotti durante la registrazione della response.
            try
            {
                var responseBody = ReadAndResetStream(context.Response.Body);
                _log.TraceArgs(() => new LogMessage
                {
                    ShortMessage = String.Format("Response \"{0}\" for \"{1}\"", requestId, context.Request.Uri.SafeToString()),
                    LongMessage = responseBody,
                    Context = "Logging response",
                    Arguments = new[]
                    {
                        KeyValuePair.Create("status_code", context.Response.StatusCode.SafeToString())
                    }
                });
            }
            catch (Exception ex)
            {
                _log.TraceArgs(() => new LogMessage
                {
                    Exception = ex,
                    Context = "Logging response"
                });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string ReadAndResetStream(Stream stream)
        {
            if (stream == null)
            {
                return Constants.EmptyString;
            }
            using (var streamReader = new StreamReader(stream))
            {
                var result = streamReader.ReadToEnd();
                stream.Position = 0;
                return result;
            }
        }
    }
}