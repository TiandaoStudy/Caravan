using System;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.DataModel.Rest;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.RestService.Core;

namespace Finsa.Caravan.RestService
{
   public sealed class LoggerModule : CustomModule
   {
      public LoggerModule() : base("logger")
      {
         /*
          * Entries
          */

         Post["/{appName}/entries"] = p =>
         {
            StartSafeResponse<dynamic>(NotCached);
            var entries = Db.Logger.Logs(p.appName);
            return RestResponse.Success(new LogEntryList {Entries = entries});
         };
         
         Post["/{appName}/entries/{logType}"] = p =>
         {
            StartSafeResponse<dynamic>(NotCached);
            var entries = Db.Logger.Logs(p.appName, SafeParseLogType(p.logType));
            return RestResponse.Success(new LogEntryList {Entries = entries});
         };
         
         Put["/{appName}/entries"] = p =>
         {
            var entry = StartSafeResponse<LogEntrySingle>(NotCached);
            var result = Log(entry.Entry, p.appName, null);
            return RestResponse.FromLogResult(result);
         };

         Put["/{appName}/entries/{logType}"] = p =>
         {
            var entry = StartSafeResponse<LogEntrySingle>(NotCached);
            var result = Log(entry.Entry, p.appName, ParseLogType(p.logType));
            return RestResponse.FromLogResult(result);
         };
         
         /*
          * Settings
          */

         Post["/{appName}/settings"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var settings = Db.Logger.LogSettings(p.appName);
            return RestResponse.Success(new LogSettingsList {Settings = settings});
         };

         Post["/{appName}/settings/{logType}"] = p =>
         {
            StartSafeResponse<dynamic>(Configuration.LongCacheTimeoutInSeconds);
            var settings = Db.Logger.LogSettings(p.appName, SafeParseLogType(p.logType));
            return RestResponse.Success(new LogSettingsSingle {Settings = settings});
         };
      }

      private static LogResult Log(LogEntry e, string appName, LogType? logType)
      {
         try
         {
            e = PrepareEntry(e, appName, logType);
         }
         catch (Exception ex)
         {
            return LogResult.Failure(ex);
         }
         return Db.Logger.Log(e.Type, e.App.Name, e.UserLogin, e.CodeUnit, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
      }

      private static LogEntry PrepareEntry(LogEntry entry, string appName, LogType? logType)
      {
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