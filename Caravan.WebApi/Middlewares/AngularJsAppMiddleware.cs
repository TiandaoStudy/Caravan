using Common.Logging;
using Finsa.Caravan.WebApi.Middlewares.Models;
using Microsoft.Owin;
using PommaLabs.Thrower;
using System.Threading.Tasks;

namespace Finsa.Caravan.WebApi.Middlewares
{
    /// <summary>
    ///   Configura la pipeline di OWIN in modo da restituire i file necessari per un'applicazione
    ///   Web costruita con AngularJS. Inoltre, questo componente si occupa anche di redirigere alla
    ///   pagina iniziale, di default index.html, tutte quelle richieste che non sono gestite da
    ///   altri componenti di middleware.
    /// </summary>
    public sealed class AngularJsAppMiddleware : OwinMiddleware
    {
        private readonly Settings _settings;
        private readonly ILog _log;

        /// <summary>
        ///   Inizializza il componente usato per AngularJS.
        /// </summary>
        /// <param name="next">Il componente successivo nella catena.</param>
        /// <param name="settings">Le impostazioni del componente.</param>
        /// <param name="log">Il log su cui scrivere eventuali messaggi.</param>
        public AngularJsAppMiddleware(OwinMiddleware next, Settings settings, ILog log)
            : base(next)
        {
            RaiseArgumentNullException.IfIsNull(settings, nameof(settings));
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            _settings = settings;
            _log = log;
        }

        /// <summary>
        ///   Redirige le richieste non valide verso la radice del sito.
        /// </summary>
        /// <param name="context">Il contesto di OWIN.</param>
        /// <returns>Task per proseguire nella catena di middleware.</returns>
        public override Task Invoke(IOwinContext context)
        {
            context.Response.Redirect("/app/index.html");
            return Task.FromResult(0);
        }

        /// <summary>
        ///   Impostazioni del componente di middleware.
        /// </summary>
        public sealed class Settings : AbstractMiddlewareSettings
        {
        }
    }
}
