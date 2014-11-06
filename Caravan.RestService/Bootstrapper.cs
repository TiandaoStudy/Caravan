using Finsa.Caravan.RestService.Core;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class Bootstrapper : CachingBootstrapper
   {
      // The bootstrapper enables you to reconfigure the composition of the framework,
      // by overriding the various methods and properties.
      // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

      protected override void ConfigureApplicationContainer(TinyIoCContainer container)
      {
         base.ConfigureApplicationContainer(container);
         container.Register<JsonSerializer, CustomJsonSerializer>();
      }
   }
}