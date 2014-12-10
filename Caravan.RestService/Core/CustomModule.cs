using System;
using System.IO;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.DataModel.Rest;
using Nancy;
using Newtonsoft.Json;
using PommaLabs.KVLite.Nancy;

namespace Finsa.Caravan.RestService.Core
{
   public abstract class CustomModule : NancyModule
   {
      protected const int NotCached = -1;
      protected const string Success = "OK";

      protected CustomModule()
      {
      }

      protected CustomModule(string modulePath) : base(modulePath)
      {
      }

      protected dynamic SafeResponse<TBody>(dynamic parameters, int cacheSeconds, Func<dynamic, TBody, dynamic> handler)
      {
         try
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
            return handler(parameters, parsedBody.Body);
         }
         catch (Exception ex)
         {
            return RestResponse.Failure(ex);
         }
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

      private void ApplySecurity(dynamic auth)
      {
         if (auth == null || auth.Length == 0)
         {
            throw new Exception("INVALID AUTH");
         }
      }
   }

   internal sealed class TestAuthManager : IAuthManager
   {
      public AuthResult Authenticate(dynamic authObject)
      {
         if (authObject == null || authObject.Length == 0)
         {
            return new AuthResult {IsValid = false, Message = "INVALID AUTH"};
         }
         if (authObject == DataAccess.Properties.Settings.Default.RestTestAuthObject)
         {
            // Only for unit tests!!!
            Db.ChangeDataAccessKindUseOnlyForUnitTestsPlease();
         }
         return new AuthResult {IsValid = true, Message = "Authenticated"};
      }
   }
}