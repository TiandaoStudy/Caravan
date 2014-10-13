using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Dapper;
using Finsa.Caravan;
using Finsa.Caravan.Collections;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;
using FLEX.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.Oracle
{
   public sealed class OracleDbLogger : DbLoggerBase, IDbLogger
   {
      #region Public Properties

      public bool IsDebugEnabled
      {
         get { return IsLevelEnabled("DEBUG"); }
      }

      public bool IsInfoEnabled
      {
         get { return IsLevelEnabled("INFO"); }
      }

      public bool IsWarningEnabled
      {
         get { return IsLevelEnabled("WARNING"); }
      }

      public bool IsErrorEnabled
      {
         get { return IsLevelEnabled("ERROR"); }
      }

      public bool IsFatalEnabled
      {
         get { return IsLevelEnabled("FATAL"); }
      }

      #endregion

      #region Logging Methods

      public LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified,
         IEnumerable<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("DEBUG", function, shortMessage, longMessage, context, args);
      }

      public LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified,
         IEnumerable<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("INFO", function, shortMessage, longMessage, context, args);
      }

      public LogResult LogWarning<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified,
         IEnumerable<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("WARNING", function, shortMessage, longMessage, context, args);
      }

      public LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified,
         IEnumerable<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("ERROR", function, shortMessage, longMessage, context, args);
      }

      public LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified,
         IEnumerable<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("FATAL", function, shortMessage, longMessage, context, args);
      }

      public LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("DEBUG", function, exception, context, args);
      }

      public LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("INFO", function, exception, context, args);
      }

      public LogResult LogWarning<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("WARNING", function, exception, context, args);
      }

      public LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("ERROR", function, exception, context, args);
      }

      public LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>("FATAL", function, exception, context, args);
      }

      #endregion

      #region Logs Retrieval

      public IEnumerable<LogEntry> RetrieveAllLogs()
      {
         var query = @"
            select flog_entry_date as EntryDate, flos_type as Type, flog_Application as Application, flog_code_unit as CodeUnit,
                   flog_function as Function, flog_short_msg as ShortMessage, flog_long_msg as LongMessage, flog_context as Context,
                   flog_key_0 as Key0, flog_value_0 as Value0, flog_key_1 as Key1, flog_value_1 as Value1, flog_key_2 as Key2, flog_value_2 as Value2,
                   flog_key_3 as Key3, flog_value_3 as Value3, flog_key_4 as Key4, flog_value_4 as Value4, flog_key_5 as Key5, flog_value_5 as Value5,
                   flog_key_6 as Key6, flog_value_6 as Value6, flog_key_7 as Key7, flog_value_7 as Value7, flog_key_8 as Key8, flog_value_8 as Value8,
                   flog_key_9 as Key9, flog_value_9 as Value9
              from {0}flex_log
         ";
         query = string.Format(query, Configuration.Instance.OracleRunner);
         using (var connection = QueryExecutor.Instance.OpenConnection())
         {
            return connection.Query<LogEntry>(query);
         }
      }

      public IEnumerable<LogEntry> RetrieveCurrentApplicationLogs()
      {
         return RetrieveApplicationLogs(Common.Configuration.Instance.ApplicationName);
      }

      public IEnumerable<LogEntry> RetrieveApplicationLogs(string applicationName)
      {
         var query = @"
            select flog_entry_date as EntryDate, flos_type as Type, flog_Application as Application, flog_code_unit as CodeUnit,
                   flog_function as Function, flog_short_msg as ShortMessage, flog_long_msg as LongMessage, flog_context as Context,
                   flog_key_0 as Key0, flog_value_0 as Value0, flog_key_1 as Key1, flog_value_1 as Value1, flog_key_2 as Key2, flog_value_2 as Value2,
                   flog_key_3 as Key3, flog_value_3 as Value3, flog_key_4 as Key4, flog_value_4 as Value4, flog_key_5 as Key5, flog_value_5 as Value5,
                   flog_key_6 as Key6, flog_value_6 as Value6, flog_key_7 as Key7, flog_value_7 as Value7, flog_key_8 as Key8, flog_value_8 as Value8,
                   flog_key_9 as Key9, flog_value_9 as Value9
              from {0}flex_log
             where upper(flog_application) = upper(:applicationName)";
         query = string.Format(query, Configuration.Instance.OracleRunner);
         using (var connection = QueryExecutor.Instance.OpenConnection())
         {
            return connection.Query<LogEntry>(query, new {applicationName});
         }
      }

      #endregion

      #region "Private Methods"

      private static bool IsLevelEnabled(string type)
      {
         type = type.ToUpper();
         using (var connection = QueryExecutor.Instance.OpenConnection())
         {
            var query = string.Format("select flos_enabled from {0}flex_log_settings where flos_type = :type", Configuration.Instance.OracleRunner);
            var enabled = connection.Query<int>(query, new {type}).First();
            return enabled == 0;
         }
      }

      private static LogResult Log<TCodeUnit>(string type, string function, string shortMessage, string longMessage, string context,
         IEnumerable<GKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentNullException>.IfIsNull(shortMessage);
            var argsList = (args == null) ? ReadOnlyList.Empty<GKeyValuePair<string, string>>() as IList<GKeyValuePair<string, string>> : args.ToList();
            Raise<ArgumentOutOfRangeException>.If(argsList.Count > MaxArgumentCount);

            using (var connection = QueryExecutor.Instance.OpenConnection())
            {
               var p = new DynamicParameters();
               p.Add("p_type", type);
               p.Add("p_application", Common.Configuration.Instance.ApplicationName.Truncate(MaxApplicationNameLength));
               p.Add("p_code_unit", typeof (TCodeUnit).FullName.Truncate(MaxCodeUnitLength));
               p.Add("p_function", function.Truncate(MaxFunctionLength));
               p.Add("p_short_msg", shortMessage == null ? LogEntry.NotSpecified : shortMessage.Truncate(MaxShortMessageLength));
               p.Add("p_long_msg", longMessage == null ? LogEntry.NotSpecified : longMessage.Truncate(MaxLongMessageLength));
               p.Add("p_context", context == null ? LogEntry.NotSpecified : context.Truncate(MaxContextLength));
               for (var i = 0; i < argsList.Count; i++)
               {
                  var arg = argsList[i];
                  p.Add(string.Format("p_key_{0}", i), arg.Key.Truncate(MaxKeyLength));
                  p.Add(string.Format("p_value_{0}", i), arg.Value == null ? null : arg.Value.Truncate(MaxValueLength));
               }
               var procedure = string.Format("{0}pck_flex_log.sp_log", Configuration.Instance.OracleRunner);
               connection.Execute(procedure, p, commandType: CommandType.StoredProcedure);
            }

            return LogResult.Successful;
         }
         catch (Exception ex)
         {
            return new LogResult {Succeeded = false, Exception = ex};
         }
      }

      private static LogResult Log<TCodeUnit>(string type, string function, Exception exception, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentNullException>.IfIsNull(exception);
            exception = FindInnermostException(exception);
         }
         catch (Exception ex)
         {
            return new LogResult {Succeeded = false, Exception = ex};
         }
         return Log<TCodeUnit>(type, function, exception.Message, exception.StackTrace, context, args);
      }

      #endregion
   }
}