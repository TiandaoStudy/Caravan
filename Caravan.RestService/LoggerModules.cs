using System;
using Finsa.Caravan.Common.DataModel;
using Finsa.Caravan.RestService.Core;
using FLEX.DataAccess;
using Nancy;
using Newtonsoft.Json;

namespace Finsa.Caravan.RestService
{
   public sealed class LogsModule : CaravanModule
   {
      public LogsModule() : base("logs")
      {
         Get["/"] = _ => Logger.Instance.Logs();
         Get["/{applicationName}"] = p => Logger.Instance.Logs((string) p.applicationName);
         Get["/{applicationName}/{logType}"] = p => Logger.Instance.Logs((string) p.applicationName, SafeParseLogType((string) p.logType));
         
         Post["/"] = _ => Log(null, null);
         Post["/{applicationName}"] = p => Log(p.applicationName, null);
         Post["/{applicationName}/{logType}"] = p => Log(p.applicationName, ParseLogType((string) p.logType));
      }

      private Response Log(string applicationName, LogType? logType)
      {
         LogEntry e;
         try
         {
            e = PrepareEntry((string) Request.Form.entry, applicationName, logType);
         }
         catch (Exception ex)
         {
            return Response.AsJson(LogResult.Failure(ex));
         }
         var result = Logger.Instance.Log(e.Type, e.ApplicationName, e.UserName, e.CodeUnit, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
         return Response.AsJson(result);
      }

      private static LogEntry PrepareEntry(string json, string applicationName, LogType? logType)
      {
         var entry = JsonConvert.DeserializeObject<LogEntry>(json);
         if (entry == null)
         {
            throw new Exception(ErrorMessages.LogsModule_InvalidEntry);
         }
         if (applicationName == null)
         {
            if (String.IsNullOrWhiteSpace(entry.ApplicationName))
            {
               throw new Exception(ErrorMessages.LogsModule_MissingAppName);
            }
         }
         else
         {
            entry.ApplicationName = applicationName;
         }
         if (logType == null)
         {
            if (String.IsNullOrWhiteSpace(entry.TypeString))
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

   public sealed class LogSettingsModule : CaravanModule
   {
      public LogSettingsModule() : base("/logSettings")
      {
         Get["/"] = _ => Logger.Instance.LogSettings();
         Get["/{applicationName}"] = p => Logger.Instance.LogSettings((string) p.applicationName);
         Get["/{applicationName}/{logType}"] = p => Logger.Instance.LogSettings((string) p.applicationName, SafeParseLogType((string) p.logType));
      }
   }
}