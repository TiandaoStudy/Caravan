using System.Web;
using System.Web.Http;
using Finsa.Caravan.WebApi;

namespace Caravan.WebService
{
    /// <summary>
    ///   Contiene varie configurazioni del servizio.
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        ///   Invocato all'avvio.
        /// </summary>
        protected void Application_Start()
        {
            // Inizializzatore di default.
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Inizializzatore per Caravan.
            GlobalHelper.Application_Start();
        }
    }
}