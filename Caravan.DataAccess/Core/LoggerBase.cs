using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;
using Finsa.Caravan;
using Finsa.Caravan.Diagnostics;
using FLEX.Common.DataModel;

namespace FLEX.DataAccess.Core
{
   public abstract class LoggerBase : ILogger
   {
      #region ILogger Members

      public LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Debug, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Info, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Warn, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Error, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Fatal, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Debug, applicationName, userName, function, exception, context, args);
      }

      public LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Info, applicationName, userName, function, exception, context, args);
      }

      public LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Warn, applicationName, userName, function, exception, context, args);
      }

      public LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Error, applicationName, userName, function, exception, context, args);
      }

      public LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Fatal, applicationName, userName, function, exception, context, args);
      }

      public abstract IEnumerable<LogEntry> GetAllLogs();

      public abstract IEnumerable<LogEntry> GetApplicationLogs(string applicationName);

      public IEnumerable<LogEntry> GetCurrentApplicationLogs()
      {
         return GetApplicationLogs(Common.Configuration.Instance.ApplicationName);
      }

      public abstract IList<LogSettings> GetAllSettings(LogType logType);

      public abstract LogSettings GetApplicationSettings(string applicationName, LogType logType);

      public LogSettings GetCurrentApplicationSettings(LogType logType)
      {
         return GetApplicationSettings(Common.Configuration.Instance.ApplicationName, logType);
      }

      #endregion

      protected abstract LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context,
         IEnumerable<GKeyValuePair<string, string>> args);

      protected static string GetCurrentUserName()
      {
         if (HttpContext.Current == null || HttpContext.Current.User == null || HttpContext.Current.User.Identity == null)
         {
            return LogEntry.NotSpecified;
         }
         return HttpContext.Current.User.Identity.Name;
      }

      #region Private Methods

      private LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, Exception exception, string context, IEnumerable<GKeyValuePair<string, string>> args)
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
         return Log<TCodeUnit>(type, applicationName, userName, function, exception.Message, exception.StackTrace, context, args);
      }

      private static Exception FindInnermostException(Exception exception)
      {
         while (exception.InnerException != null)
         {
            exception = exception.InnerException;
         }
         return exception;
      }

      #endregion
   }
}