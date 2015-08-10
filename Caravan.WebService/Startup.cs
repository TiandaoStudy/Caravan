using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Web;
using System.Web.Http;
using Common.Logging;
using Finsa.Caravan.DataAccess.Logging.Sql;
using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebApi.Middlewares;
using Finsa.Caravan.WebService;
using Finsa.CodeServices.Common.Diagnostics;
using Finsa.WebApi.HelpPage.AnyHost;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using PommaLabs.KVLite;

[assembly: OwinStartup(typeof(Startup))]

namespace Finsa.Caravan.WebService
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var kernel = CreateKernel();
            var cache = kernel.Get<ICache>();

            // Inizializzatore per Caravan.
            ServiceHelper.OnStart(config, LogManager.GetLogger<Startup>(), cache);
            DbInterception.Add(kernel.Get<SqlDbCommandLogger>());
            app.Use(kernel.Get<HttpLoggingMiddleware>());

            // Inizializzatore per Ninject.
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            // Inizializzatori specifici del servizio.
            ConfigureHelpPages(config);
            ConfigureFormatters(config);

            // Inizializzazione gestione identità.
            IdentityConfig.Build(app);
        }

        private static IKernel CreateKernel()
        {
            return new StandardKernel(new NinjectConfig());
        }

        private static void ConfigureHelpPages(HttpConfiguration config)
        {
            // REQUIRED TO ENABLE HELP PAGES :)
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            var xmlDocPath = HttpContext.Current.Server.MapPath(@"~/App_Data/HelpPages/WebServiceHelp.xml");
            config.SetDocumentationProvider(new XmlDocumentationProvider(xmlDocPath));
        }

        private static void ConfigureFormatters(HttpConfiguration configuration)
        {
            // Controlli di integrità.
            Raise<ArgumentNullException>.IfIsNull(configuration);

            // Personalizzo le impostazioni del serializzatore JSON.
            configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Personalizzo le impostazioni del serializzatore XML.
            var xml = configuration.Formatters.XmlFormatter;
            xml.Indent = false;
        }
    }
}
