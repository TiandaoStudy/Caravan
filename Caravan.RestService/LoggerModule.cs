using System;
using Finsa.Caravan.Common.DataModel.Exceptions;
using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.RestService.Core;
using Finsa.Caravan.RestService.Properties;
using Nancy;

namespace Finsa.Caravan.RestService
{
   public sealed class LoggerModule : CustomModule
   {
      public LoggerModule() : base("logger")
      {
         /*
          * Entries
          */

         Post["/{appName}/entries"] = p => SafeResponse<dynamic>(p, NotCached, (Func<dynamic, dynamic, dynamic>) GetEntriesAll);
         Post["/{appName}/entries/{logType}"] = p => SafeResponse<dynamic>(p, NotCached, (Func<dynamic, dynamic, dynamic>) GetEntriesForType);
         Put["/{appName}/entries"] = p => SafeResponse<LogEntrySingle>(p, NotCached, (Func<dynamic, LogEntrySingle, dynamic>) AddEntry);
         Put["/{appName}/entries/{logType}"] = p => SafeResponse<LogEntrySingle>(p, NotCached, (Func<dynamic, LogEntrySingle, dynamic>) AddEntryForType);
         
         /*
          * Settings
          */

         Post["/{appName}/settings"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetSettingsList);
         Post["/{appName}/settings/{logType}"] = p => SafeResponse<dynamic>(p, Settings.Default.LongCacheTimeoutInSeconds, (Func<dynamic, dynamic, dynamic>) GetSettingsSingle);
         Put["/{appName}/settings/{logType}"] =
            p => SafeResponse<LogSettingsSingle>(p, NotCached, (Func<dynamic, LogSettingsSingle, dynamic>) AddSetting);
         Patch["/{appName}/settings/{logType}"] = p => SafeResponse<LogSettingsSingle>(p, NotCached, (Func<dynamic, LogSettingsSingle, dynamic>) UpdateSetting);
      }


      private static dynamic AddSetting(dynamic p, LogSettingsSingle body)
      {
         try
         {
            return Db.Logger.AddSettings(p.appName, ParseLogType(p.logType), body.Settings);
         }
         catch (Exception exception)
         {
            if (exception.Message == AppExistingException.TheMessage)
               return ErrorResponse(HttpStatusCode.Conflict, AppExistingException.TheMessage);
            if (exception.Message == SettingsExistingException.TheMessage)
               return ErrorResponse(HttpStatusCode.Conflict, SettingsExistingException.TheMessage);
            return ErrorResponse(HttpStatusCode.BadRequest, exception.Message);
         }
         
      }

      private static dynamic UpdateSetting(dynamic p, LogSettingsSingle body)
      {
         try
         {
            return Db.Logger.UpdateSettings(p.appName, ParseLogType(p.logType), body.Settings);
         }
         catch (Exception exception)
         {
            if (exception.Message == AppNotFoundException.TheMessage)
               return ErrorResponse(HttpStatusCode.NotFound, AppNotFoundException.TheMessage);
            if(exception.Message == SettingsNotFoundException.TheMessage)
               return ErrorResponse(HttpStatusCode.NotFound, SettingsNotFoundException.TheMessage);
            if (exception.Message == SettingsExistingException.TheMessage)
               return ErrorResponse(HttpStatusCode.Conflict, SettingsExistingException.TheMessage);
         }
         return null; // DA FARE?!?
      }

      private static dynamic GetEntriesAll(dynamic p, dynamic body)
      {
         var entries = Db.Logger.Logs(p.appName);
         return new LogEntryList {Entries = entries};
      }
      
      private static dynamic GetEntriesForType(dynamic p, dynamic body)
      {
         var entries = Db.Logger.Logs(p.appName, SafeParseLogType(p.logType));
         return new LogEntryList {Entries = entries};
      }
      
      private static dynamic AddEntry(dynamic p, LogEntrySingle body)
      {
         return Log(body.Entry, p.appName, null);
      }
      
      private static dynamic AddEntryForType(dynamic p, LogEntrySingle body)
      {
         return Log(body.Entry, p.appName, ParseLogType(p.logType));
      }

      private static dynamic GetSettingsList(dynamic p, dynamic body)
      {
         var settings = Db.Logger.LogSettings(p.appName);
         return new LogSettingsList {Settings = settings};
      }

      private static dynamic GetSettingsSingle(dynamic p, dynamic body)
      {
         var settings = Db.Logger.LogSettings(p.appName, SafeParseLogType(p.logType));
         return new LogSettingsSingle {Settings = settings};
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
         return Db.Logger.LogRaw(e.Type, e.App.Name, e.UserLogin, e.CodeUnit, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
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

      private static Response ErrorResponse(HttpStatusCode statusCode, string errorMessage)
      {
         var response = (Response)errorMessage;
         response.StatusCode = statusCode;
         return response;
      }
   }
}