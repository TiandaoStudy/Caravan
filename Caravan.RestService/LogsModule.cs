using Finsa.Caravan.RestService.Core;
using FLEX.Common.DataModel;
using FLEX.DataAccess;
using Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class LogsModule : CaravanModule
   {
      public LogsModule() : base("logs")
      {
         Get["/"] = _ => Logger.Instance.GetAllLogs();
         Get["/{applicationName}"] = p => Logger.Instance.GetApplicationLogs(p.applicationName);
         Get["/{applicationName}/{logType}"] = p => Logger.Instance.GetApplicationLogs(p.applicationName);

         Post["/"] = _ => Log(null, null);
         Post["/{applicationName}"] = p => Log(p.applicationName, null);
         Post["/{applicationName}/{logType}"] = p => Log(p.applicationName, p.logType);
      }

      private Response Log(string applicationName, string logType)
      {
         return Response.AsJson(LogResult.Successful);
      }
   }
}