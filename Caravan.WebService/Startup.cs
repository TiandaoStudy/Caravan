using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataAccess.Sql.Logging;
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
using PommaLabs.Thrower;
using Swashbuckle.Application;
using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Finsa.Caravan.WebApi.Filters;
using Finsa.Caravan.WebApi.Models;

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
            // IMPORTANTE: Inizializzo anche il kernel di Caravan nel metodo di Create.
            var config = new HttpConfiguration();
            var kernel = CreateKernel();

            // Inizializzatore per Caravan.
            CaravanWebServiceHelper.OnStart(config);
            DbInterception.Add(kernel.Get<SqlDbCommandLogger>());
            app.Use(kernel.Get<HttpLoggingMiddleware>());

            // Inizializzatore per Ninject.
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            // Inizializzatori specifici del servizio.
            ConfigureAdminPages(app);
            ConfigureHelpPages(config);
            ConfigureFormatters(config);
            AuthorizeForCaravanAttribute.AuthorizationGranted = (context, token, log) => Task.FromResult(new AuthorizationResult
            {
                // Liberi tutti, per ora...
                Authorized = true
            });

            // Inizializzazione gestione identità.
            IdentityConfig.Build(app);
        }

        private static IKernel CreateKernel() =>
            CaravanServiceProvider.NinjectKernel ??
            (CaravanServiceProvider.NinjectKernel = new StandardKernel(
                new NinjectConfig(),
                new CaravanCommonNinjectConfig(DependencyHandling.Default, "wsCaravan"),
                new CaravanDataAccessNinjectConfig(DependencyHandling.Default)));

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
