// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Finsa.Caravan.Common;
using Finsa.Caravan.DataAccess.Sql.Logging;
using Finsa.Caravan.DataAccess.Sql.Oracle;
using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebApi.Filters;
using Finsa.Caravan.WebApi.Models;
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
            CaravanWebServiceHelper.OnStartAsync(app, config, new CaravanWebServiceHelper.Settings
            {
                EnableHttpCompressionMiddleware = true,
                EnableHttpLoggingMiddleware = true
            }).Wait();
            DbInterception.Add(kernel.Get<SqlDbCommandLogger>());

            // Inizializzatore per Ninject.
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            // Inizializzatori specifici del servizio.
            ConfigureAdminPages(app);
            ConfigureHelpPages(config);
            ConfigureFormatters(config);
            ConfigureFilters(config);

            // Inizializzazione gestione identità.
            IdentityConfig.Build(app);
        }

        private static IKernel CreateKernel() =>
            CaravanServiceProvider.NinjectKernel ??
            (CaravanServiceProvider.NinjectKernel = new StandardKernel(
                new NinjectConfig(),
                new CaravanCommonNinjectConfig(DependencyHandling.Default, "wsCaravan"),
                new CaravanOracleDataAccessNinjectConfig(DependencyHandling.Default),
                new CaravanWebApiNinjectConfig(DependencyHandling.Default)));

        private static void ConfigureAdminPages(IAppBuilder app)
        {
            // Controlli di integrità.
            RaiseArgumentNullException.IfIsNull(app, nameof(app));

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
            // Controlli di integrità.
            RaiseArgumentNullException.IfIsNull(config, nameof(config));

            // REQUIRED TO ENABLE HELP PAGES :)
            config.MapHttpAttributeRoutes();
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "wsCaravan");
                c.IncludeXmlComments(PortableEnvironment.MapPath(@"~/App_Data/HelpPages/WebServiceHelp.xml"));
                c.RootUrl(req => req.RequestUri.GetLeftPart(UriPartial.Authority) + req.GetRequestContext().VirtualPathRoot.TrimEnd('/'));

                c.OAuth2("wsCaravan")
                 .Description("boh")
                 .Flow("implicit")
                 .AuthorizationUrl("")
                 .TokenUrl("")
                 .Scopes(cfg => { });
            }).EnableSwaggerUi(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.DisableValidator();
            });
        }

        private static void ConfigureFormatters(HttpConfiguration config)
        {
            // Controlli di integrità.
            RaiseArgumentNullException.IfIsNull(config, nameof(config));

            // Personalizzo le impostazioni del serializzatore JSON.
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Personalizzo le impostazioni del serializzatore XML.
            var xml = config.Formatters.XmlFormatter;
            xml.Indent = false;
        }

        private static void ConfigureFilters(HttpConfiguration config)
        {
            // Controlli di integrità.
            RaiseArgumentNullException.IfIsNull(config, nameof(config));

            config.Filters.Add(new HttpExceptionFilterAttribute());
            AuthorizeForCaravanAttribute.AuthorizationGranted = (context, token, log) => Task.FromResult(new AuthorizationResult
            {
                // Liberi tutti, per ora...
                Authorized = CaravanWebServiceConfiguration.Instance.Security_EnableCaravanServices
            });
        }
    }
}
