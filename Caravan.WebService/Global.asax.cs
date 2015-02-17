using Finsa.Caravan.Mvc.Core;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Caravan.WebService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Inizializzatori di default.
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Inizializzatore per Caravan.
            GlobalHelper.Application_Start();
        }
    }
}