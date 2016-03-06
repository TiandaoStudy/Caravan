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
using Finsa.Caravan.WebApi.Middlewares.Models;
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
        private readonly Settings _settings;
        private readonly ILog _log;

        /// <summary>
        ///   Imposta gli elementi statici del componente di middleware.
        /// </summary>
        static HttpProxyMiddleware()
        {
            LoadHeaderProperties();
        }

        /// <summary>
        ///   Inizializza il componente usato per il proxy HTTP.
        /// </summary>
        /// <param name="next">Il componente successivo nella catena.</param>
        /// <param name="settings">Le impostazioni del componente.</param>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        public HttpProxyMiddleware(OwinMiddleware next, Settings settings, ILog log)
            : base(next)
        {
            RaiseArgumentNullException.IfIsNull(settings, nameof(settings));
            RaiseArgumentException.IfIsNullOrWhiteSpace(settings.TargetEndpointUri.ToString(), nameof(settings.TargetEndpointUri));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _settings = settings;
            _log = log;
        }

        /// <summary>
        ///   Redirige la request verso l'endpoint specificato e ritorna indietro la response ricevuta.
        /// </summary>
        /// <param name="context">Il contesto di Owin.</param>
        /// <returns>Task per proseguire nella catena di middleware.</returns>
        public override async Task Invoke(IOwinContext context)
        {
            // Recupero le informazioni su request e response.
            var owinRequest = context.Request;
            var owinResponse = context.Response;

            try
            {
                await ProxyRequestAsync(owinRequest, owinResponse);
            }
            catch (HttpException ex)
            {
                _log.Error(ex);

                // Preparo la risposta di errore.
                owinResponse.StatusCode = (int) ex.HttpStatusCode;
                owinResponse.ContentType = "text/plain";

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

                await owinResponse.WriteAsync(stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                // Preparo la risposta di errore.
                owinResponse.StatusCode = (int) HttpStatusCode.InternalServerError;
                owinResponse.ContentType = "text/plain";

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

                await owinResponse.WriteAsync(stringBuilder.ToString());
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
            var httpRequestUri = UriCombine(_settings.TargetEndpointUri, owinRequest.Path, owinRequest.QueryString);
            var httpRequest = WebRequest.CreateHttp(httpRequestUri);
            var requestHasBody = owinRequest.Body.Length != 0;

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

            // Copia del body della richiesta, solo se presente.
            if (requestHasBody)
            {
                // Registro la lunghezza del contenuto del body.
                httpRequest.ContentLength = owinRequest.Body.Length;

                // Avvio la copia dello stream.
                var httpRequestBody = await httpRequest.GetRequestStreamAsync();
                await owinRequest.Body.CopyToAsync(httpRequestBody);
            }

            // Esecuzione della richiesta.
            HttpWebResponse httpResponse;
            try
            {
                httpResponse = (await httpRequest.GetResponseAsync()) as HttpWebResponse;
            }
            catch (WebException ex) when (ex.Response is HttpWebResponse)
            {
                // Le response di errore arrivano all'interno di un'eccezione.
                httpResponse = ex.Response as HttpWebResponse;
            }

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
        ///   Unisce i due URI dati.
        /// </summary>
        /// <param name="baseUri">Il primo URI.</param>
        /// <param name="relativePath">Il secondo URI.</param>
        /// <param name="queryString">La componente della query string.</param>
        /// <returns>I due URI uniti.</returns>
        private static string UriCombine(Uri baseUri, PathString relativePath, QueryString queryString)
        {
	    var trimmedBaseUriValue = baseUri.AbsolutePath.TrimEnd('/\');
            var trimmedRelativePathValue = relativePath.HasValue ? relativePath.Value.TrimStart('/\') : string.Empty;
            return queryString.HasValue
                 ? string.Format("{0}/{1}?{2}", trimmedBaseUriValue, trimmedRelativePathValue, queryString.Value)
                 : string.Format("{0}/{1}", trimmedBaseUriValue, trimmedRelativePathValue);
        }

        /// <summary>
        ///   Le impostazioni del componente di middleware.
        /// </summary>
        public sealed class Settings : AbstractMiddlewareSettings
        {
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
                "Connection", // Causa errori vari - Va studiato.
                "Content-Length" // Viene impostato in automatico dal framework.
            };
        }

        #region HttpWebRequest extensions

        /// <summary>
        ///   Lista di header HTTP che non possono essere "aggiunti" a una
        ///   <see cref="HttpWebRequest"/>, in quanto possono solo essere impostati tramite le
        ///   apposite proprietà.
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
    }
}
