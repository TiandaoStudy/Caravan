using System;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.RestService.Core;
using Nancy;
using Newtonsoft.Json;

namespace Finsa.Caravan.RestService
{
   public sealed class LoggerModule : CustomModule
   {
      public LoggerModule() : base("logger")
      {
         Post["/{appName}/entries"] = p =>
         {
            StartSafeResponse<dynamic>(NotCached);
            return Db.Logger.Logs((string) p.appName);
         };
         
         Post["/{appName}/entries/{logType}"] = p =>
         {
            StartSafeResponse<dynamic>(NotCached);
            return Db.Logger.Logs((string) p.appName, SafeParseLogType((string) p.logType));
         };
         
         Post["/entries"] = _ => Log(null, null);
         Post["/entries/{appName}"] = p => Log(p.appName, null);
         Post["/entries/{appName}/{logType}"] = p => Log(p.appName, ParseLogType((string) p.logType));

         Post["/settings"] = _ => Db.Logger.LogSettings();
         Post["/settings/{appName}"] = p => Db.Logger.LogSettings((string) p.appName);
         Post["/settings/{appName}/{logType}"] = p => Db.Logger.LogSettings((string) p.appName, SafeParseLogType((string) p.logType));
      }

      private Response Log(string appName, LogType? logType)
      {
         LogEntry e;
         try
         {
            e = PrepareEntry((string) Request.Form.entry, appName, logType);
         }
         catch (Exception ex)
         {
            return Response.AsJson(LogResult.Failure(ex));
         }
         var result = Db.Logger.Log(e.Type, e.App.Name, e.UserLogin, e.CodeUnit, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
         return Response.AsJson(result);
      }

      private static LogEntry PrepareEntry(string json, string appName, LogType? logType)
      {
         var entry = JsonConvert.DeserializeObject<LogEntry>(json);
         if (entry == null)
         {
            throw new Exception(ErrorMessages.LogsModule_InvalidEntry);
         }
         if (appName == null)
         {
            if (String.IsNullOrWhiteSpace(entry.App.Name))
            {
               throw new Exception(ErrorMessages.LogsModule_MissingAppName);
            }
         }
         else
         {
            entry.App = new SecApp {Name = appName};
         }
         if (logType == null)
         {
            if (String.IsNullOrWhiteSpace(entry.TypeId))
            {
               throw new Exception(ErrorMessages.LogsModule_MissingLogType);
            }
         }
         else
         {
            entry.Type = logType.Value;
         }
         return entry;
      }
   }
}