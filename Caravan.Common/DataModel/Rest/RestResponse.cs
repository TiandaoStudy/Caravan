using System;
using System.Net;
using Newtonsoft.Json;

namespace Finsa.Caravan.DataModel.Rest
{
   [Serializable, JsonObject(MemberSerialization.OptIn)]
   public class RestResponse<TBody>
   {
      [JsonProperty(Order = 0)]
      public HttpStatusCode StatusCode { get; set; }
      
      [JsonProperty(Order = 1)]
      public TBody Body { get; set; }
   }

   public static class RestResponse
   {
      public static RestResponse<TBody> Success<TBody>(TBody body)
      {
         return new RestResponse<TBody>
         {
            StatusCode = HttpStatusCode.OK,
            Body = body
         };
      }
   }
}
