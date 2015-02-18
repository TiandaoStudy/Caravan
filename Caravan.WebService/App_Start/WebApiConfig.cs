using Finsa.Caravan.Mvc.Core.Enrichers;
using Finsa.WebApi.HelpPage.AnyHost;
using System.Web;
using System.Web.Http;

namespace Caravan.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // REQUIRED TO ENABLE HELP PAGES :)
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            var xmlDocPath = HttpContext.Current.Server.MapPath(@"~/App_Data/XmlDocument.xml");
            config.SetDocumentationProvider(new XmlDocumentationProvider(xmlDocPath));

            // Aggiunge i generatori di link, di modo che il servizio integri le risorse con le rispettive azioni.
            config.AddResponseEnrichers();
        }
    }
}