using System;
using System.IO;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.DataModel.Rest;
using Finsa.Caravan.Extensions;
using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService.Core
{
   public abstract class CustomModule : NancyModule
   {
      protected const int NotCached = -1;

      protected CustomModule()
      {
      }

      protected CustomModule(string modulePath) : base(modulePath)
      {
      }

      protected RestRequest<TBody> ParseBody<TBody>()
      {
         using (var streamReader = new StreamReader(Context.Request.Body))
         {
            return JsonConvert.DeserializeObject<RestRequest<TBody>>(streamReader.ReadToEnd());
         }
      }

      protected static LogType? ParseLogType(string logTypeString, bool fallback = true)
      {
         LogType logType;
         return Enum.TryParse(logTypeString, true, out logType) ? logType : new LogType?();
      }

      protected static LogType SafeParseLogType(string logTypeString, bool fallback = true)
      {
         LogType logType;
         return Enum.TryParse(logTypeString, true, out logType) ? logType : LogType.Info;
      }

      protected TBody StartSafeResponse<TBody>(string appName, int cacheSeconds)
      {
         var parsedBody = ParseBody<TBody>();
         ApplySecurity(parsedBody.Auth);
         if (cacheSeconds != NotCached)
         {
            Context.EnableOutputCache(cacheSeconds);
         }
         else
         {
            Context.DisableOutputCache();
         }
         return parsedBody.Body;
      }

      private void ApplySecurity(dynamic auth)
      {
         if (auth == null || auth.Length == 0)
         {
            throw new Exception("INVALID AUTH");
         }
      }
   }
}