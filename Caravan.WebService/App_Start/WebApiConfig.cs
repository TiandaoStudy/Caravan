using System.Web;
using System.Web.Http;
using Finsa.WebApi.HelpPage.AnyHost;

namespace Finsa.Caravan.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // REQUIRED TO ENABLE HELP PAGES :)
            config.MapHttpAttributeRoutes(new HelpDirectRouteProvider());
            var xmlDocPath = HttpContext.Current.Server.MapPath(@"~/App_Data/HelpPage/Finsa.Caravan.WebService.xml");
            config.SetDocumentationProvider(new XmlDocumentationProvider(xmlDocPath));
        }
    }
}