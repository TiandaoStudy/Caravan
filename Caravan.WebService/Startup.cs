﻿using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebApi.DelegatingHandlers;
using Finsa.Caravan.WebService;
using Finsa.WebApi.HelpPage.AnyHost;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using PommaLabs.KVLite;
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

            // Inizializzatore per Ninject.
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);

            // Inizializzatori di default per Web API.
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            var xmlDocPath = HttpContext.Current.Server.MapPath(@"~/App_Data/HelpPage/Finsa.Caravan.WebService.xml");
            config.SetDocumentationProvider(new XmlDocumentationProvider(xmlDocPath));
            app.UseWebApi(config);

            // Inizializzatore per Caravan.
            config.MessageHandlers.Add(kernel.Get<LoggingDelegatingHandler>());
            ServiceHelper.ConfigureFormatters(config);
            ServiceHelper.ConfigureOutputCache(config, kernel.Get<ICache>());
        }

        private static IKernel CreateKernel()
        {
            return new StandardKernel(new NinjectConfig());
        }
    }
}