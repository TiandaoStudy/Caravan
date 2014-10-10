using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Finsa.Caravan;
using Finsa.Caravan.Helpers;
using FLEX.Common.DataModel;

namespace FLEX.Common.Data
{
   /// <summary>
   ///   TODO
   /// </summary>
   public interface IDbLogger
   {
      /* Test if a level is enabled for logging */
      bool IsDebugEnabled { get; }
      bool IsInfoEnabled { get; }
      bool IsWarningEnabled { get; }
      bool IsErrorEnabled { get; }
      bool IsFatalEnabled { get; }

      LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogWarning<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogWarning<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, ICollection<GKeyValuePair<string, string>> args = null, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      IEnumerable<LogEntry> RetrieveAllLogs();
      IEnumerable<LogEntry> RetrieveCurrentApplicationLogs();

      DataTable RetrieveAllLogsTable();
      DataTable RetrieveCurrentApplicationLogsTable();
   }
}