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

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}