using Finsa.Caravan.Mvc.Core;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Caravan.WebService
{
   public class WebApiApplication : HttpApplication
   {
      protected void Application_Start()
      {
         // Inizializzatori di default.
         AreaRegistration.RegisterAllAreas();
         GlobalConfiguration.Configure(WebApiConfig.Register);
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);

         // Inizializzatore per Caravan.
         GlobalHelper.Application_Start();
      }
   }
}