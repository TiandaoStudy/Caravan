using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Finsa.Caravan.Collections;
using Finsa.Caravan.Common.DataModel;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;

namespace Finsa.Caravan.DataAccess.Oracle
{
   public sealed class OracleLogger : LoggerBase
   {
      #region Constants

      private const int MaxApplicationNameLength = 30;
      private const int MaxUserNameLength = 30;
      private const int MaxCodeUnitLength = 100;
      private const int MaxFunctionLength = 100;
      private const int MaxShortMessageLength = 400;
      private const int MaxLongMessageLength = 2000;
      private const int MaxContextLength = 400;
      private const int MaxKeyLength = 100;
      private const int MaxValueLength = 400;
      private const int MaxArgumentCount = 10;

      #endregion

      #region Logging Methods

      public override LogResult Log(LogType type, string applicationName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context,
         IEnumerable<GKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentException>.IfIsEmpty(codeUnit);
            var argsList = (args == null) ? ReadOnlyList.Empty<GKeyValuePair<string, string>>() as IList<GKeyValuePair<string, string>> : args.ToList();
            Raise<ArgumentOutOfRangeException>.If(argsList.Count > MaxArgumentCount);

            using (var connection = QueryExecutor.Instance.OpenConnection())
            {
               var p = new DynamicParameters();
               p.Add("p_type", type.ToString(), DbType.AnsiString);
               p.Add("p_application", applicationName.Truncate(MaxApplicationNameLength), DbType.AnsiString);
               p.Add("p_user", GetCurrentUserName(userName).Truncate(MaxUserNameLength), DbType.AnsiString);
               p.Add("p_code_unit", codeUnit.Truncate(MaxCodeUnitLength), DbType.AnsiString);
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
               var procedure = string.Format("{0}pck_caravan_log.sp_log", Configuration.Instance.OracleRunner);
               connection.Execute(procedure, p, commandType: CommandType.StoredProcedure);
            }

            return LogResult.Success;
         }
         catch (Exception ex)
         {
            return LogResult.Failure(ex);
         }
      }

      #endregion

      #region Logs Retrieval

      protected override IEnumerable<LogEntry> Logs(string applicationName, LogType? logType)
      {
         var query = @"
            select clog_date ""Date"", clos_type as TypeString, capp_name as ApplicationName, clog_user UserName, clog_code_unit as CodeUnit,
                   clog_function as Function, clog_short_msg as ShortMessage, clog_long_msg as LongMessage, clog_context as Context,
                   clog_key_0 as Key0, clog_value_0 as Value0, clog_key_1 as Key1, clog_value_1 as Value1, clog_key_2 as Key2, clog_value_2 as Value2,
                   clog_key_3 as Key3, clog_value_3 as Value3, clog_key_4 as Key4, clog_value_4 as Value4, clog_key_5 as Key5, clog_value_5 as Value5,
                   clog_key_6 as Key6, clog_value_6 as Value6, clog_key_7 as Key7, clog_value_7 as Value7, clog_key_8 as Key8, clog_value_8 as Value8,
                   clog_key_9 as Key9, clog_value_9 as Value9
              from {0}caravan_log
             where (:applicationName is null or capp_name = lower(:applicationName))
               and (:logType is null or clos_type = lower(:logType))
             order by clog_date desc
         ";
         query = string.Format(query, Configuration.Instance.OracleRunner);
         using (var connection = QueryExecutor.Instance.OpenConnection())
         {
            var parameters = new DynamicParameters();
            parameters.Add("applicationName", applicationName, DbType.AnsiString);
            parameters.Add("logType", (logType == null) ? null : logType.ToString(), DbType.AnsiString);
            return connection.Query<LogEntry>(query, parameters);
         }
      }

      #endregion

      #region Log Settings

      protected override IList<LogSettings> LogSettings(string applicationName, LogType? logType)
      {
         var query = @"
            select capp_name ApplicationName, clos_type TypeString, clos_enabled Enabled,
                   clos_days Days, clos_max_entries MaxEntries
              from {0}caravan_log_settings
             where (:applicationName is null or capp_name = lower(:applicationName))
               and (:logType is null or clos_type = lower(:logType))
             order by capp_name, clos_type
         ";
         query = string.Format(query, Configuration.Instance.OracleRunner);
         using (var connection = QueryExecutor.Instance.OpenConnection())
         {
            var parameters = new DynamicParameters();
            parameters.Add("applicationName", applicationName, DbType.AnsiString);
            parameters.Add("logType", (logType == null) ? null : logType.ToString(), DbType.AnsiString);
            return connection.Query<LogSettings>(query, parameters).ToList();
         }
      }

      #endregion
   }
}