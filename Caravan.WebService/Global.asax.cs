using Finsa.Caravan.Mvc.Core;
using System.Web;
using System.Web.Http;

namespace Caravan.WebService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Inizializzatore di default.
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Inizializzatore per Caravan.
            GlobalHelper.Application_Start();
        }
    }
}