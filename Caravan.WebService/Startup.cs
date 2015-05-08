using Finsa.Caravan.DataAccess.Drivers.Sql;
using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebApi.DelegatingHandlers;
using Finsa.Caravan.WebApi.Middlewares;
using Finsa.Caravan.WebService;
using Finsa.WebApi.HelpPage.AnyHost;
using Microsoft.Owin;
using Ninject;
using Ninject.Parameters;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using PommaLabs.KVLite;
using System.Data.Entity.Infrastructure.Interception;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace Finsa.Caravan.WebService
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var kernel = CreateKernel();

            // Inizializzatore per Caravan.
            DbInterception.Add(kernel.Get<SqlDbCommandLogger>());
            app.Use(kernel.Get<HttpLoggingMiddleware>());
            ServiceHelper.ConfigureFormatters(config);
            ServiceHelper.ConfigureOutputCache(config, kernel.Get<ICache>());

            // Inizializzatore per Ninject.
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            // Inizializzatori di default per Web API.
            ConfigureWebApi(app, config);

            // Inizializzazione gestione identità.
            IdentityConfig.Build(app);
        }

        private static IKernel CreateKernel()
        {
            return new StandardKernel(new NinjectConfig());
        }

        private static void ConfigureWebApi(IAppBuilder app, HttpConfiguration config)
        {
            // REQUIRED TO ENABLE HELP PAGES :)
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            var xmlDocPath = HttpContext.Current.Server.MapPath(@"~/App_Data/HelpPages/WebServiceHelp.xml");
            config.SetDocumentationProvider(new XmlDocumentationProvider(xmlDocPath));
        }
    }
}