using Common.Logging;
using Finsa.CodeServices.Common;
using IdentityModel.Client;
using Microsoft.Owin;
using PommaLabs.Thrower;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Finsa.Caravan.WebApi.Middlewares
{
    public sealed class OAuth2HandlerMiddleware : OwinMiddleware
    {
        public const string ProxyPath = "/oauth2";
        public const string LoginPath = "/login";
        public const string LogoutPath = "/logout";
        public const string RedirectPath = "/redirect";

        private readonly ILog _log;
        private readonly Settings _settings;

        /// <summary>
        ///   Inizializza il componente usato per la gestione di OAuth2.
        /// </summary>
        /// <param name="next">Il componente successivo nella catena.</param>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        /// <param name="settings">Le impostazioni del componente.</param>
        public OAuth2HandlerMiddleware(OwinMiddleware next, ILog log, Settings settings)
            : base(next)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            RaiseArgumentNullException.IfIsNull(settings, nameof(settings));
            RaiseArgumentException.IfIsNullOrWhiteSpace(settings.ClientId, nameof(settings.ClientId));
            RaiseArgumentException.IfIsNullOrWhiteSpace(settings.RedirectUri.ToString(), nameof(settings.RedirectUri));
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
            var request = context.Request;
            var response = context.Response;

            try
            {
                // Recupero la parte di indirizzo dopo ProxyPath.
                var requestUrl = request.Uri.AbsoluteUri.ToLowerInvariant();
                var proxyPathIndex = requestUrl.IndexOf(ProxyPath, 0, StringComparison.Ordinal);
                var actionPathIndex = proxyPathIndex + ProxyPath.Length;

                // Verifico, azione per azione, se vi sia un match.
                if (ActionMatches(requestUrl, LoginPath, actionPathIndex))
                {
                    // Gestione login.
                    await HandleLoginAsync(request, response);
                    return;
                }
                if (ActionMatches(requestUrl, RedirectPath, actionPathIndex))
                {
                    // Gestione redirect.
                    await HandleRedirectAsync(request, response);
                    return;
                }
            }
            catch (HttpException ex)
            {
                _log.Error(ex);

                // Preparo la risposta di errore.
                response.StatusCode = (int) ex.HttpStatusCode;
                response.ContentType = "text/plain";

                // Preparo il body con l'errore.
                var stringBuilder = new StringBuilder("OAUTH2 PROXY ERROR:");
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
                var stringBuilder = new StringBuilder("OAUTH2 PROXY ERROR:");
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
        ///   Verifica, in modo rapido, se vi sia un match tra l'azione data e la richiesta effettuata.
        /// </summary>
        /// <param name="requestUrl">L'indirizzo della richiesta.</param>
        /// <param name="actionPath">L'azione che deve essere verificata.</param>
        /// <param name="actionPathIndex">Il punto dell'indirizzo da cui la verifica deve iniziare.</param>
        /// <returns>
        ///   Vero se è presente un match tra l'azione data e la richiesta effettuata, falso altrimenti.
        /// </returns>
        private static bool ActionMatches(string requestUrl, string actionPath, int actionPathIndex)
            => requestUrl.IndexOf(actionPath, actionPathIndex, StringComparison.Ordinal) == actionPathIndex;

        /// <summary>
        ///   I caratteri usati per fare pulizia all'inizio e alla fine degli URL.
        /// </summary>
        private static readonly char[] UrlTrimChars = { '/', '\\' };

        /// <summary>
        ///   Gestisce l'handler di tipo LOGIN, che redirige tutte le chiamate verso il portale di OAuth2.
        /// </summary>
        /// <param name="owinRequest">La richiesta OWIN.</param>
        /// <param name="owinResponse">La risposta OWIN.</param>
        /// <returns>Un task.</returns>
        private Task HandleLoginAsync(IOwinRequest owinRequest, IOwinResponse owinResponse)
        {
            // Informazioni sul server di OAuth2.
            var site = _settings.Site.AbsoluteUri.TrimEnd(UrlTrimChars);
            var authorizePath = _settings.AuthorizePath.Value.TrimStart(UrlTrimChars);

            // Informazioni sul client.
            var clientId = _settings.ClientId;
            var scope = string.Join("%20", _settings.Scopes);
            var redirectUri = HttpUtility.UrlEncode(_settings.RedirectUri.AbsoluteUri);
            var responseType = "code";

            // Costruisce l'indirizzo a cui fare redirect.
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(site);
            urlBuilder.Append('/');
            urlBuilder.Append(authorizePath);
            urlBuilder.Append("?client_id=");
            urlBuilder.Append(clientId);
            if (scope.Length > 0)
            {
                urlBuilder.Append("&scope=");
                urlBuilder.Append(scope);
            }
            urlBuilder.Append("&redirect_uri=");
            urlBuilder.Append(redirectUri);
            urlBuilder.Append("&response_type=");
            urlBuilder.Append(responseType);
            if (_settings.NonceGenerationMode == NonceGenerationMode.Auto)
            {
                urlBuilder.Append("&nonce=");
                urlBuilder.Append(UniqueIdGenerator.NewBase32());
            }

            // Effettuo il redirect ed esco dal componente di middleware.
            owinResponse.Redirect(urlBuilder.ToString());
            return Task.FromResult(0);
        }

        /// <summary>
        ///   Gestisce l'handler di tipo REDIRECT, che si occupa di generare i token dopo la login.
        /// </summary>
        /// <param name="owinRequest">La richiesta OWIN.</param>
        /// <param name="owinResponse">La risposta OWIN.</param>
        /// <returns>Un task.</returns>
        private async Task HandleRedirectAsync(IOwinRequest owinRequest, IOwinResponse owinResponse)
        {
            // Visto che il flusso utilizzato è di tipo "code grant", mi aspetto che nel query
            // string sia presenta il codice "code" per proseguire il flusso.
            var authCode = owinRequest.Query["code"];

            // Attivo il client per il recupero dei token.
            var tokenClient = new TokenClient("https://server/token", "client_id", "secret");
            var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(authCode, owinRequest.Uri.AbsoluteUri);


            var accessToken = tokenResponse.AccessToken;

            // Effettuo il redirect verso la home ed esco dal componente di middleware.
            owinResponse.Redirect(_settings.RedirectUri.AbsoluteUri);
        }

        public sealed class Settings
        {
            /// <summary>
            ///   Il percorso a cui effettuare la parte di autorizzazione.
            /// </summary>
            public PathString AuthorizePath { get; set; }

            /// <summary>
            ///   Il client ID da utilizzare con OAuth2.
            /// </summary>
            public string ClientId { get; set; }

            /// <summary>
            ///   La lista di scope richiesti dal client.
            /// </summary>
            public IEnumerable<string> Scopes { get; set; }

            /// <summary>
            ///   L'indirizzo a cui fare ritorno ad autenticazione avvenuta.
            /// </summary>
            public Uri RedirectUri { get; set; }

            /// <summary>
            ///   L'indirizzo base del portale di OAuth2.
            /// </summary>
            public Uri Site { get; set; }

            /// <summary>
            ///   La modalità di generazione del parametro "nonce". Viene impostato di default a
            ///   <see cref="NonceGenerationMode.Auto"/>.
            /// </summary>
            public NonceGenerationMode NonceGenerationMode { get; set; } = NonceGenerationMode.Auto;
        }

        /// <summary>
        ///   Descrive la modalità di generazione del parametro "nonce".
        /// </summary>
        public enum NonceGenerationMode
        {
            /// <summary>
            ///   Il parametro "nonce" non verrà inserito nel redirect alla login.
            /// </summary>
            None,

            /// <summary>
            ///   Il parametro "nonce" verrà valorizzato con un identificatore casuale.
            /// </summary>
            Auto
        }

        #region IDisposable Support

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

        #endregion IDisposable Support

        private static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
    }
}