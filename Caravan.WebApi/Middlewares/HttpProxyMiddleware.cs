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
using Microsoft.Owin;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Finsa.Caravan.WebApi.Middlewares
{
    /// <summary>
    ///   Componente di middleware il cui unico scopo è dirottare le richieste verso un endpoint dato.
    /// </summary>
    public sealed class HttpProxyMiddleware : OwinMiddleware
    {
        private readonly ILog _log;
        private readonly Settings _settings;

        /// <summary>
        ///   Imposta gli elementi statici del componente di middleware.
        /// </summary>
        static HttpProxyMiddleware()
        {
            LoadHeaderProperties();
        }

        /// <summary>
        ///   Inizializza il componente usato per la gestione di OAuth2.
        /// </summary>
        /// <param name="next">Il componente successivo nella catena.</param>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        /// <param name="settings">Le impostazioni del componente.</param>
        public HttpProxyMiddleware(OwinMiddleware next, ILog log, Settings settings)
            : base(next)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            RaiseArgumentNullException.IfIsNull(settings, nameof(settings));
            RaiseArgumentException.IfIsNullOrWhiteSpace(settings.TargetEndpointUri.ToString(), nameof(settings.TargetEndpointUri));
            _log = log;
            _settings = settings;
        }

        /// <summary>
        ///   Applica una rapida compressione alla risposta HTTP se il client ha specificato
        ///   esplicitamente che è in grado di accettare risposte compresse.
        /// </summary>
        /// <param name="context">Il contesto di Owin.</param>
        /// <returns>Task per proseguire nella catena di middleware.</returns>
        public override async Task Invoke(IOwinContext context)
        {
            // Recupero le informazioni su request e response.
            var owinRequest = context.Request;
            var response = context.Response;

            try
            {
                await ProxyRequestAsync(owinRequest, response);
            }
            catch (HttpException ex)
            {
                _log.Error(ex);

                // Preparo la risposta di errore.
                response.StatusCode = (int) ex.HttpStatusCode;
                response.ContentType = "text/plain";

                // Preparo il body con l'errore.
                var stringBuilder = new StringBuilder("HTTP PROXY ERROR:");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.Append("Message: ");
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.Append("User message: ");
                stringBuilder.AppendLine(ex.UserMessage);
                stringBuilder.Append("Error code: ");
                stringBuilder.AppendLine(ex.ErrorCode?.ToString());
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("Stack trace: ");
                stringBuilder.AppendLine(ex.StackTrace);

                await response.WriteAsync(stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                // Preparo la risposta di errore.
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
                response.ContentType = "text/plain";

                // Preparo il body con l'errore.
                var stringBuilder = new StringBuilder("HTTP PROXY ERROR:");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                stringBuilder.Append("Message: ");
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.Append("Exception type: ");
                stringBuilder.AppendLine(ex.GetType().FullName);
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("Stack trace: ");
                stringBuilder.AppendLine(ex.StackTrace);

                await response.WriteAsync(stringBuilder.ToString());
            }
        }

        /// <summary>
        ///   Gestisce il proxy di tipo API, che redirige tutte le chiamate verso un servizio dato.
        /// </summary>
        /// <param name="owinRequest">La richiesta OWIN.</param>
        /// <param name="owinResponse">La risposta OWIN.</param>
        /// <returns>Un task.</returns>
        private async Task ProxyRequestAsync(IOwinRequest owinRequest, IOwinResponse owinResponse)
        {
            // Preparazione del client e della richiesta REST.
            var httpRequestUri = new Uri(_settings.TargetEndpointUri, owinRequest.Uri);
            var httpRequest = WebRequest.CreateHttp(httpRequestUri);

            // Configurazione della richiesta.
            httpRequest.AllowAutoRedirect = true;
            httpRequest.AutomaticDecompression = DecompressionMethods.None;
            httpRequest.Method = owinRequest.Method;

            // Copia degli header della richiesta.
            foreach (var header in owinRequest.Headers)
            {
                if (!_settings.FilteredHeaders.Contains(header.Key))
                {
                    SetRawHeader(httpRequest, header.Key, header.Value);
                }
            }

            // Copia del body della richiesta, tranne se la richiesta è in GET.
            if (!httpRequest.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                var httpRequestBody = await httpRequest.GetRequestStreamAsync();
                await owinRequest.Body.CopyToAsync(httpRequestBody);
            }

            // Esecuzione della richiesta.
            var httpResponse = (await httpRequest.GetResponseAsync()) as HttpWebResponse;

            // Copia del body della risposta.
            var httpResponseBody = httpResponse.GetResponseStream();
            await httpResponseBody.CopyToAsync(owinResponse.Body);

            // Copia del codice HTTP.
            owinResponse.StatusCode = (int) httpResponse.StatusCode;

            // Copia degli header della risposta.
            var httpResponseHeaders = httpResponse.Headers;
            foreach (string headerName in httpResponseHeaders)
            {
                owinResponse.Headers.Append(headerName, string.Join(", ", httpResponseHeaders.GetValues(headerName)));
            }
        }

        /// <summary>
        ///   Le impostazioni del componente di middleware.
        /// </summary>
        public sealed class Settings
        {
            /// <summary>
            ///   Determina se questo componente debba essere aggiunto alla pipeline.
            /// </summary>
            public bool Enabled { get; set; } = false;

            /// <summary>
            ///   Il percorso da cui le richieste vengono dirottate.
            /// </summary>
            public PathString SourceEndpointPath { get; set; } = new PathString("/source");

            /// <summary>
            ///   L'indirizzo a cui dirottare le richieste.
            /// </summary>
            public Uri TargetEndpointUri { get; set; } = new Uri("http://localhost/myApp/target/");

            /// <summary>
            ///   Gli header HTTP che non vengono inseriti nella richiesta in proxy.
            /// </summary>
            public HashSet<string> FilteredHeaders = new HashSet<string>
            {
                "Connection"
            };
        }

        #region HttpWebRequest extensions

        /// <summary>
        ///   Lista di header HTTP che non possono essere "aggiunti" a una <see
        ///   cref="HttpWebRequest"/>, in quanto possono solo essere impostati tramite le apposite proprietà.
        /// </summary>
        private static HashSet<string> RestrictedHeaders = new HashSet<string>
        {
            "Accept",
            "Connection",
            "Content-Length",
            "Content-Type",
            "Date",
            "Expect",
            "Host",
            "If-Modified-Since",
            "Keep-Alive",
            "Proxy-Connection",
            "Range",
            "Referer",
            "Transfer-Encoding",
            "User-Agent"
        };

        /// <summary>
        ///   La mappa delle proprietà da usare per impostare gli header della request.
        /// </summary>
        private static Dictionary<string, PropertyInfo> HeaderProperties = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        ///   Prepara l'oggetto utilizzato per impostare in modo efficiente gli header della request.
        /// </summary>
        private static void LoadHeaderProperties()
        {
            var type = typeof(HttpWebRequest);
            foreach (string header in RestrictedHeaders)
            {
                var propertyName = header.Replace("-", "");
                var headerProperty = type.GetProperty(propertyName);
                HeaderProperties[header] = headerProperty;
            }
        }

        /// <summary>
        ///   Imposta gli header "ristretti" attraverso le proprietà, mentre gli altri vengono
        ///   semplicemente aggiunti.
        /// </summary>
        /// <param name="request">La request.</param>
        /// <param name="name">Il nome dell'header.</param>
        /// <param name="values">I valori nell'header.</param>
        private static void SetRawHeader(HttpWebRequest request, string name, string[] values)
        {
            var mergedValues = (values.Length == 1)
                ? values[0]
                : string.Join(", ", values);

            if (HeaderProperties.ContainsKey(name))
            {
                var property = HeaderProperties[name];
                if (property.PropertyType == typeof(DateTime))
                    property.SetValue(request, DateTime.Parse(mergedValues), null);
                else if (property.PropertyType == typeof(bool))
                    property.SetValue(request, bool.Parse(mergedValues), null);
                else if (property.PropertyType == typeof(long))
                    property.SetValue(request, long.Parse(mergedValues), null);
                else
                    property.SetValue(request, mergedValues, null);
            }
            else
            {
                request.Headers[name] = mergedValues;
            }
        }

        #endregion HttpWebRequest extensions

        #region IDisposable support

        private bool disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free
        //       unmanaged resources. ~OAuth2ProxyMiddleware() { // Do not change this code. Put
        // cleanup code in Dispose(bool disposing) above. Dispose(false); }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above. GC.SuppressFinalize(this);
        }

        #endregion IDisposable support
    }
}