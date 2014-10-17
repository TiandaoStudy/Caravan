﻿using System;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.RestService.Core;
using Nancy;
using Newtonsoft.Json;

namespace Finsa.Caravan.RestService
{
   public sealed class LogsModule : CustomModule
   {
      public LogsModule() : base("logs")
      {
         Get["/"] = _ => Db.Logger.Logs();
         Get["/{applicationName}"] = p => Db.Logger.Logs((string) p.applicationName);
         Get["/{applicationName}/{logType}"] = p => Db.Logger.Logs((string) p.applicationName, SafeParseLogType((string) p.logType));
         
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
         var result = Db.Logger.Log(e.Type, e.App.Name, e.UserLogin, e.CodeUnit, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
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
            if (String.IsNullOrWhiteSpace(entry.App.Name))
            {
               throw new Exception(ErrorMessages.LogsModule_MissingAppName);
            }
         }
         else
         {
            entry.App = new SecApp {Name = applicationName};
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

   public sealed class LogSettingsModule : CustomModule
   {
      public LogSettingsModule() : base("/logSettings")
      {
         Get["/"] = _ => Db.Logger.LogSettings();
         Get["/{applicationName}"] = p => Db.Logger.LogSettings((string) p.applicationName);
         Get["/{applicationName}/{logType}"] = p => Db.Logger.LogSettings((string) p.applicationName, SafeParseLogType((string) p.logType));
      }
   }
}