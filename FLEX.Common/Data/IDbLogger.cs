using System;
using System.Collections.Generic;
using System.Data;

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

      void LogDebug<TCodeUnit>(string function, object shortMessage, object longMessage = null, string context = null, IDictionary<string, object> args = null);
      void LogInfo<TCodeUnit>(string function, object shortMessage, object longMessage = null, string context = null, IDictionary<string, object> args = null);
      void LogWarning<TCodeUnit>(string function, object shortMessage, object longMessage = null, string context = null, IDictionary<string, object> args = null);
      void LogError<TCodeUnit>(string function, object shortMessage, object longMessage = null, string context = null, IDictionary<string, object> args = null);
      void LogFatal<TCodeUnit>(string function, object shortMessage, object longMessage = null, string context = null, IDictionary<string, object> args = null);

      void LogDebug<TCodeUnit>(string function, Exception exception, string context = null, IDictionary<string, object> args = null);
      void LogInfo<TCodeUnit>(string function, Exception exception, string context = null, IDictionary<string, object> args = null);
      void LogWarning<TCodeUnit>(string function, Exception exception, string context = null, IDictionary<string, object> args = null);
      void LogError<TCodeUnit>(string function, Exception exception, string context = null, IDictionary<string, object> args = null);
      void LogFatal<TCodeUnit>(string function, Exception exception, string context = null, IDictionary<string, object> args = null);

      IEnumerable<DbLog> RetrieveAllLogs();
      IEnumerable<DbLog> RetrieveCurrentApplicationLogs();
      
      DataTable RetrieveAllLogsTable();
      DataTable RetrieveCurrentApplicationLogsTable();
   }

   public sealed class DbLog
   {
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
