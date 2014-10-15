using Finsa.Caravan.Common.DataModel;
using Finsa.Caravan.RestService.Core;
using FLEX.DataAccess;
using Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class LogsModule : CaravanModule
   {
      public LogsModule() : base("logs")
      {
         Get["/"] = _ => Logger.Instance.Logs();
         Get["/{applicationName}"] = p => Logger.Instance.Logs((string) p.applicationName);
         Get["/{logType}"] = p => Logger.Instance.Logs(ParseLogType((string) p.logType));
         Get["/{applicationName}/{logType}"] = p => Logger.Instance.Logs((string) p.applicationName, ParseLogType((string) p.logType));


         Post["/"] = _ => Log(null, null);
         Post["/{applicationName}"] = p => Log(p.applicationName, null);
         Post["/{logType}"] = p => Log(null, p.logType);
         Post["/{applicationName}/{logType}"] = p => Log(p.applicationName, p.logType);
      }

      private Response Log(string applicationName, string logType)
      {
         return Response.AsJson(LogResult.Successful);
      }
   }

   public sealed class LogSettingsModule : CaravanModule
   {
      public LogSettingsModule() : base("/logSettings")
      {
         Get["/"] = _ => Logger.Instance.LogSettings();
         Get["/{applicationName}"] = p => Logger.Instance.LogSettings((string) p.applicationName);
         Get["/{logType}"] = p => Logger.Instance.LogSettings(ParseLogType((string) p.logType));
         Get["/{applicationName}/{logType}"] = p => Logger.Instance.LogSettings((string) p.applicationName, ParseLogType((string) p.logType));
      }
   }
}