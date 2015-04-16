using System.Web;
using Common.Logging;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebApi.Middlewares;
using Finsa.Caravan.WebService;
using Finsa.WebApi.HelpPage.AnyHost;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System.Web.Http;
using PommaLabs.KVLite;

[assembly: OwinStartup(typeof(Startup))]

namespace Finsa.Caravan.WebService
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Inizializzatore per Ninject.
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            // Middleware definiti dentro Caravan, da porre prima delle Web API.
            app.Use(new ExceptionHandlingMiddleware(LogManager.GetLogger<ExceptionHandlingMiddleware>()));
            app.Use(new LoggingMiddleware(LogManager.GetLogger<ExceptionHandlingMiddleware>() as ICaravanLog));

            // Inizializzatori di default per Web API.
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            var xmlDocPath = HttpContext.Current.Server.MapPath(@"~/App_Data/HelpPage/Finsa.Caravan.WebService.xml");
            config.SetDocumentationProvider(new XmlDocumentationProvider(xmlDocPath));
            app.UseWebApi(config);

            // Inizializzatore per Caravan.
            ServiceHelper.ConfigureFormatters(config);
            ServiceHelper.ConfigurateOutputCache(config, PersistentCache.DefaultInstance);
        }

        private static IKernel CreateKernel()
        {
            return new StandardKernel(new NinjectConfig());
        }
    }
}