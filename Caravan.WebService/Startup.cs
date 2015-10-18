using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Logging.Sql;
using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebApi.Middlewares;
using Finsa.Caravan.WebService;
using Finsa.CodeServices.Common.Portability;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
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
using System.Net.Http;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace Finsa.Caravan.WebService
{
    /// <summary>
    ///   Inizializza il servizio di Caravan.
    /// </summary>
    public sealed class Startup
    {
        /// <summary>
        ///   Inizializza il servizio di Caravan.
        /// </summary>
        /// <param name="app">Necessario per configurare il servizio.</param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // IMPORTANTE: Inizializzo anche il kernel di Caravan nel metodo di Create.
            var kernel = CreateKernel();
            var cache = kernel.Get<ICache>();

            // Inizializzatore per Caravan.
            CaravanWebApiHelper.OnStart(config, LogManager.GetLogger<Startup>(), cache);
            DbInterception.Add(kernel.Get<SqlDbCommandLogger>());
            app.Use(kernel.Get<HttpLoggingMiddleware>());

            // Inizializzatore per Ninject.
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            // Inizializzatori specifici del servizio.
            ConfigureAdminPages(app);
            ConfigureHelpPages(config);
            ConfigureFormatters(config);

            // Inizializzazione gestione identità.
            IdentityConfig.Build(app);
        }

        private static IKernel CreateKernel()
        {
            return CaravanServiceProvider.NinjectKernel ?? (CaravanServiceProvider.NinjectKernel = new StandardKernel(new NinjectConfig()));
        }

        private static void ConfigureAdminPages(IAppBuilder app)
        {
            var root = PortableEnvironment.MapPath("~/Caravan/Administration");
            var fileSystem = new PhysicalFileSystem(root);
            var options = new StaticFileOptions
            {
                RequestPath = new PathString("/admin"),
                FileSystem = fileSystem
            };
            app.UseStaticFiles(options);
        }

        private static void ConfigureHelpPages(HttpConfiguration config)
        {
            // REQUIRED TO ENABLE HELP PAGES :)
            config.MapHttpAttributeRoutes();
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "wsCaravan");
                c.IncludeXmlComments(PortableEnvironment.MapPath(@"~/App_Data/HelpPages/WebServiceHelp.xml"));
                c.RootUrl(req => req.RequestUri.GetLeftPart(UriPartial.Authority) + req.GetRequestContext().VirtualPathRoot.TrimEnd('/'));
            }).EnableSwaggerUi(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.DisableValidator();
            });
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