using System;
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

         Get["/settings/{applicationName}/{logType}"] = p => GetSettings(p.applicationName, p.logType);

         Post["/"] = _ => Log(null, null);
         Post["/{applicationName}"] = p => Log(p.applicationName, null);
         Post["/{applicationName}/{logType}"] = p => Log(p.applicationName, p.logType);
      }

      private Response GetSettings(string applicationName, string logTypeString)
      {
         LogType logType;
         if (!Enum.TryParse(logTypeString, true, out logType))
         {
            return HttpStatusCode.NotFound;
         }
         return Response.AsJson(Logger.Instance.GetApplicationSettings(applicationName, logType));
      }

      private Response Log(string applicationName, string logType)
      {
         return Response.AsJson(LogResult.Successful);
      }
   }
}