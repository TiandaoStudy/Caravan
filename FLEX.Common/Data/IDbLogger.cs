using System;
using System.Collections.Generic;

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
   }
}
