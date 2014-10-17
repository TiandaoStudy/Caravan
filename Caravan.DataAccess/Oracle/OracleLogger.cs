using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Dapper;
using Finsa.Caravan.Collections;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;
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

      protected override IEnumerable<LogEntry> GetLogs(string appName, LogType? logType)
      {
         using (var ctx = new OracleDbContext())
         {
            return (from s in ctx.LogEntry.Include(s => s.App)
                    where appName == null || s.App.Name == appName.ToLower()
                    where logType == null || s.TypeString == logType.ToString().ToLower()
                    orderby s.App.Name, s.TypeString, s.Date descending
                    select s).ToList();
         }
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         using (var ctx = new OracleDbContext())
         {
            return (from s in ctx.LogSettings.Include(s => s.App)
                    where appName == null || s.App.Name == appName.ToLower()
                    where logType == null || s.TypeString == logType.ToString().ToLower()
                    orderby s.App.Name, s.TypeString
                    select s).ToList();
         }
      }
   }
}