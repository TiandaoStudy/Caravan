using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
   public abstract class LoggerBase : ILogger
   {
      #region ILogger Members

      public LogResult Log<TCodeUnit>(LogType type, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled,
         string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(type, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

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

      public LogResult Log<TCodeUnit>(LogType type, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(type, applicationName, userName, function, exception, context, args);
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

      public IEnumerable<LogEntry> Logs()
      {
         return GetLogs(null, null);
      }

      public IEnumerable<LogEntry> Logs(string applicationName)
      {
         Raise<ArgumentException>.IfIsEmpty(applicationName);
         return GetLogs(applicationName, null);
      }

      public IEnumerable<LogEntry> Logs(LogType logType)
      {
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogs(null, (LogType?) logType);
      }

      public IEnumerable<LogEntry> Logs(string applicationName, LogType logType)
      {
         Raise<ArgumentException>.IfIsEmpty(applicationName);
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogs(applicationName, (LogType?) logType);
      }

      public IList<LogSettings> LogSettings()
      {
         return GetLogSettings(null, null);
      }

      public IList<LogSettings> LogSettings(string applicationName)
      {
         Raise<ArgumentException>.IfIsEmpty(applicationName);
         return GetLogSettings(applicationName, null);
      }

      public IList<LogSettings> LogSettings(LogType logType)
      {
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogSettings(null, (LogType?) logType);
      }

      public LogSettings LogSettings(string applicationName, LogType logType)
      {
         Raise<ArgumentException>.IfIsEmpty(applicationName);
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogSettings(applicationName, (LogType?) logType).FirstOrDefault();
      }

      #endregion

      public abstract LogResult Log(LogType type, string applicationName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context,
         IEnumerable<GKeyValuePair<string, string>> args);

      protected abstract IEnumerable<LogEntry> GetLogs(string applicationName, LogType? logType);

      protected abstract IList<LogSettings> GetLogSettings(string applicationName, LogType? logType);

      #region Shortcuts

      private LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentException>.IfIsEmpty(shortMessage);
         }
         catch (Exception ex)
         {
            return LogResult.Failure(ex);
         }
         return Log(type, applicationName, userName, typeof(TCodeUnit).FullName, function, shortMessage, longMessage, context, args);
      }

      public LogResult Log(LogType type, string applicationName, string userName, string codeUnit, string function, Exception exception, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentNullException>.IfIsNull(exception);
            exception = FindInnermostException(exception);
         }
         catch (Exception ex)
         {
            return LogResult.Failure(ex);
         }
         return Log(type, applicationName, userName, codeUnit, function, exception.Message, exception.StackTrace, context, args);
      }

      private LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, Exception exception, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentNullException>.IfIsNull(exception);
            exception = FindInnermostException(exception);
         }
         catch (Exception ex)
         {
            return LogResult.Failure(ex);
         }
         return Log(type, applicationName, userName, typeof(TCodeUnit).FullName, function, exception.Message, exception.StackTrace, context, args);
      }

      private static Exception FindInnermostException(Exception exception)
      {
         while (exception.InnerException != null)
         {
            exception = exception.InnerException;
         }
         return exception;
      } 

      protected static string GetCurrentUserName(string userName)
      {
         if (!String.IsNullOrWhiteSpace(userName))
         {
            return userName;
         }
         if (HttpContext.Current == null || HttpContext.Current.User == null || HttpContext.Current.User.Identity == null)
         {
            return LogEntry.NotSpecified;
         }
         return HttpContext.Current.User.Identity.Name;
      }

      #endregion
   }
}