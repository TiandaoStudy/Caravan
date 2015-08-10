using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Finsa.Caravan.ReportingService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Abilita l'abbellimento degli URL.
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Aggiunge la gestione di script JS e dei CSS tramite bundle. Aggiungere ai nuovi progetti.
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
