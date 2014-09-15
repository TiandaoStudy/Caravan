using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using PommaLabs.GRAMPA;

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

      void LogDebug<TCodeUnit>(object shortMessage, object longMessage = null, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogInfo<TCodeUnit>(object shortMessage, object longMessage = null, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogWarning<TCodeUnit>(object shortMessage, object longMessage = null, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogError<TCodeUnit>(object shortMessage, object longMessage = null, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogFatal<TCodeUnit>(object shortMessage, object longMessage = null, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);

      void LogDebug<TCodeUnit>(Exception exception, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogInfo<TCodeUnit>(Exception exception, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogWarning<TCodeUnit>(Exception exception, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogError<TCodeUnit>(Exception exception, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);
      void LogFatal<TCodeUnit>(Exception exception, string context = null, IList<GPair<string, object>> args = null, [CallerMemberName] string function = DbLog.AutoFilledParameter);

      IEnumerable<DbLog> RetrieveAllLogs();
      IEnumerable<DbLog> RetrieveCurrentApplicationLogs();
      
      DataTable RetrieveAllLogsTable();
      DataTable RetrieveCurrentApplicationLogsTable();
   }

   [Serializable]
   public sealed class DbLog
   {
       public const string AutoFilledParameter = "Automatically filled parameter, please do not specify a value!";

      public DateTime EntryDate { get; set; }
      public string Type { get; set; }
      public string Application { get; set; }
      public string CodeUnit { get; set; }
      public string Function { get; set; }
      public string ShortMessage { get; set; }
      public string LongMessage { get; set; }
      public string Context { get; set; }
      public string Key0 { get; set; }
      public string Value0 { get; set; }
      public string Key1 { get; set; }
      public string Value1 { get; set; }
      public string Key2 { get; set; }
      public string Value2 { get; set; }
      public string Key3 { get; set; }
      public string Value3 { get; set; }
      public string Key4 { get; set; }
      public string Value4 { get; set; }
      public string Key5 { get; set; }
      public string Value5 { get; set; }
      public string Key6 { get; set; }
      public string Value6 { get; set; }
      public string Key7 { get; set; }
      public string Value7 { get; set; }
      public string Key8 { get; set; }
      public string Value8 { get; set; }
      public string Key9 { get; set; }
      public string Value9 { get; set; }
   }
}
