using System.Web.Http;
using Finsa.Caravan.WebApi;
using Finsa.Caravan.WebService;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Finsa.Caravan.WebService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Inizializzatore di default per le Web API.
            GlobalConfiguration.Configure(WebApiConfig.Register);
            app.UseWebApi(config);

            // Inizializzatore per Caravan.
            ServiceHelper.OnStart(config);
        }
    }
}