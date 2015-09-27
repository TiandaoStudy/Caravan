using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Logging.Sql;
using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebApi.Middlewares;
using Finsa.Caravan.WebService;
using Finsa.CodeServices.Common.Portability;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using PommaLabs.KVLite;
using PommaLabs.Thrower;
using Swashbuckle.Application;
using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace Finsa.Caravan.WebService
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // IMPORTANTE: Inizializzo anche il kernel di Caravan.
            var kernel = CaravanServiceProvider.NinjectKernel = CreateKernel();
            var cache = kernel.Get<ICache>();

            // Inizializzatore per Caravan.
            CaravanServiceHelper.OnStartAsync(config, LogManager.GetLogger<Startup>(), cache).Wait();
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

        static IKernel CreateKernel() => new StandardKernel(new NinjectConfig());

        static void ConfigureHelpPages(HttpConfiguration config)
        {
            // REQUIRED TO ENABLE HELP PAGES :)
            config.MapHttpAttributeRoutes();
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "wsCaravan");
                c.IncludeXmlComments(PortableEnvironment.MapPath(@"~/App_Data/HelpPages/WebServiceHelp.xml"));
            }).EnableSwaggerUi(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.DisableValidator();
            });
        }

        static void ConfigureFormatters(HttpConfiguration configuration)
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
