using Newtonsoft.Json;

namespace Finsa.Caravan.RestService.Core
{
   internal sealed class CustomJsonSerializer : Newtonsoft.Json.JsonSerializer
   {
      public CustomJsonSerializer()
      {
         Formatting = Formatting.None;
         NullValueHandling = NullValueHandling.Ignore;
         ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      }
   }
}