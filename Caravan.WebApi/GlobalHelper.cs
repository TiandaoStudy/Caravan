using System.Web.Http;
using Newtonsoft.Json;

namespace Finsa.Caravan.Mvc.Core
{
   public sealed class GlobalHelper
   {
      public static void Application_Start()
      {
         // Personalizzo le impostazioni del serializzatore JSON.
         GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
         {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
         };
      }
   }
}
