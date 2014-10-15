using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Finsa.Caravan;
using Finsa.Caravan.Collections;
using Finsa.Caravan.Common.DataModel;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.Oracle
{
   public sealed class OracleLogger : LoggerBase
   {
      #region Constants

      private const int MaxApplicationNameLength = 30;
      private const int MaxUserNameLength = 30;
      private const int MaxCodeUnitLength = 100;
      private const int MaxFunctionLength = 100;
      private const int MaxShortMessageLength = 400;
      private const int MaxLongMessageLength = 4000;
      private const int MaxContextLength = 400;
      private const int MaxKeyLength = 100;
      private const int MaxValueLength = 400;
      private const int MaxArgumentCount = 10;

      #endregion

      #region Logging Methods

      protected override LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context,
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
               p.Add("p_type", type.ToString(), DbType.AnsiString);
               p.Add("p_application", Common.Configuration.Instance.ApplicationName.Truncate(MaxApplicationNameLength), DbType.AnsiString);
               p.Add("p_user", GetCurrentUserName().Truncate(MaxUserNameLength), DbType.AnsiString);
               p.Add("p_code_unit", typeof(TCodeUnit).FullName.Truncate(MaxCodeUnitLength), DbType.AnsiString);
               p.Add("p_function", function.Truncate(MaxFunctionLength), DbType.AnsiString);
               p.Add("p_short_msg", shortMessage == null ? LogEntry.NotSpecified : shortMessage.Truncate(MaxShortMessageLength), DbType.String);
               p.Add("p_long_msg", longMessage == null ? LogEntry.NotSpecified : longMessage.Truncate(MaxLongMessageLength), DbType.String);
               p.Add("p_context", context == null ? LogEntry.NotSpecified : context.Truncate(MaxContextLength), DbType.AnsiString);
               for (var i = 0; i < argsList.Count; i++)
               {
                  var arg = argsList[i];
                  p.Add(string.Format("p_key_{0}", i), arg.Key.Truncate(MaxKeyLength), DbType.AnsiString);
                  p.Add(string.Format("p_value_{0}", i), arg.Value == null ? null : arg.Value.Truncate(MaxValueLength), DbType.String);
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

      #endregion

      #region Logs Retrieval

      protected override IEnumerable<LogEntry> Logs(string applicationName, LogType? logType)
      {
         var query = @"
            select flog_date ""Date"", flos_type as TypeString, flos_application as ApplicationName, flog_user UserName, flog_code_unit as CodeUnit,
                   flog_function as Function, flog_short_msg as ShortMessage, flog_long_msg as LongMessage, flog_context as Context,
                   flog_key_0 as Key0, flog_value_0 as Value0, flog_key_1 as Key1, flog_value_1 as Value1, flog_key_2 as Key2, flog_value_2 as Value2,
                   flog_key_3 as Key3, flog_value_3 as Value3, flog_key_4 as Key4, flog_value_4 as Value4, flog_key_5 as Key5, flog_value_5 as Value5,
                   flog_key_6 as Key6, flog_value_6 as Value6, flog_key_7 as Key7, flog_value_7 as Value7, flog_key_8 as Key8, flog_value_8 as Value8,
                   flog_key_9 as Key9, flog_value_9 as Value9
              from {0}flex_log
             where (:applicationName is null or lower(flos_application) = lower(:applicationName))
               and (:logType is null or lower(flos_type) = lower(:logType))
             order by flog_date desc
         ";
         query = string.Format(query, Configuration.Instance.OracleRunner);
         var logTypeString = (logType == null) ? null : logType.ToString();
         using (var connection = QueryExecutor.Instance.OpenConnection())
         {
            return connection.Query<LogEntry>(query, new {applicationName, logType = logTypeString});
         }
      }

      #endregion

      #region Log Settings

      protected override IList<LogSettings> LogSettings(string applicationName, LogType? logType)
      {
         var query = @"
            select flos_application ApplicationName, flos_type TypeString, flos_enabled Enabled,
                   flos_days Days, flos_max_entries MaxEntries
              from {0}flex_log_settings
             where (:applicationName is null or lower(flos_application) = lower(:applicationName))
               and (:logType is null or lower(flos_type) = lower(:logType))
             order by flos_application, flos_type
         ";
         query = string.Format(query, Configuration.Instance.OracleRunner);
         var logTypeString = (logType == null) ? null : logType.ToString();
         using (var connection = QueryExecutor.Instance.OpenConnection())
         {
            return connection.Query<LogSettings>(query, new {applicationName, logType = logTypeString}).ToList();
         }
      }

      #endregion
   }
}