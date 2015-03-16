using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebService;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(Startup))]

namespace Finsa.Caravan.WebService
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Inizializzatori di default per MVC & Web API.
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Inizializzatore per Caravan.
            ServiceHelper.OnStart(GlobalConfiguration.Configuration);
        }
    }
}