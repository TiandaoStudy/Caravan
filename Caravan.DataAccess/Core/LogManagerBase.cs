using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
   public abstract class LogManagerBase : ILogManager
   {
      #region ILogManager Members

      public LogResult Log<TCodeUnit>(LogType type, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled,
         string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(type, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Debug, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Info, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Warn, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public Task<LogResult> LogWarnAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled,
         string function = LogEntry.AutomaticallyFilled)
      {
         return Task.Factory.StartNew(() => LogWarn<TCodeUnit>(shortMessage, longMessage, context, args, applicationName, userName, function));
      }

      public LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Error, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Fatal, applicationName, userName, function, shortMessage, longMessage, context, args);
      }

      public LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Debug, applicationName, userName, function, exception, context, args);
      }

      public LogResult Log<TCodeUnit>(LogType type, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(type, applicationName, userName, function, exception, context, args);
      }

      public LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Info, applicationName, userName, function, exception, context, args);
      }

      public LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Warn, applicationName, userName, function, exception, context, args);
      }

      public Task<LogResult> LogWarnAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, string function = LogEntry.AutomaticallyFilled)
      {
         return Task.Factory.StartNew(() => LogWarn<TCodeUnit>(exception, context, args, applicationName, userName, function));
      }

      public LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Error, applicationName, userName, function, exception, context, args);
      }

      public LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, string applicationName = LogEntry.AutomaticallyFilled,
         string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled)
      {
         return Log<TCodeUnit>(LogType.Fatal, applicationName, userName, function, exception, context, args);
      }

      public IEnumerable<LogEntry> Logs()
      {
         return GetLogs(String.Empty, null);
      }

      public IEnumerable<LogEntry> Logs(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetLogs(appName.ToLowerOrEmpty(), null);
      }

      public IEnumerable<LogEntry> Logs(LogType logType)
      {
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogs(String.Empty, logType);
      }

      public IEnumerable<LogEntry> Logs(string appName, LogType logType)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogs(appName.ToLowerOrEmpty(), logType);
      }

      public IList<LogSettings> LogSettings()
      {
         return GetLogSettings(String.Empty, null);
      }

      public IList<LogSettings> LogSettings(string appName)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         return GetLogSettings(appName.ToLowerOrEmpty(), null);
      }

      public IList<LogSettings> LogSettings(LogType logType)
      {
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogSettings(String.Empty, logType);
      }

      public LogSettings LogSettings(string appName, LogType logType)
      {
         Raise<ArgumentException>.IfIsEmpty(appName);
         Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
         return GetLogSettings(appName.ToLowerOrEmpty(), logType).FirstOrDefault();
      }

      #endregion

      public abstract LogResult Log(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context,
         IEnumerable<CKeyValuePair<string, string>> args);

      protected abstract IEnumerable<LogEntry> GetLogs(string appName, LogType? logType);

      protected abstract IList<LogSettings> GetLogSettings(string appName, LogType? logType);

      #region Shortcuts

      private LogResult Log<TCodeUnit>(LogType type, string appName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<CKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentException>.IfIsEmpty(shortMessage);
         }
         catch (Exception ex)
         {
            return LogResult.Failure(ex);
         }
         return Log(type, GetCurrentAppName(appName), GetCurrentUserName(userName), typeof(TCodeUnit).FullName, function, shortMessage, longMessage, context, args);
      }

      public LogResult Log(LogType type, string appName, string userName, string codeUnit, string function, Exception exception, string context, IEnumerable<CKeyValuePair<string, string>> args)
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
         return Log(type, GetCurrentAppName(appName), GetCurrentUserName(userName), codeUnit, function, exception.Message, exception.StackTrace, context, args);
      }

      private LogResult Log<TCodeUnit>(LogType type, string appName, string userName, string function, Exception exception, string context, IEnumerable<CKeyValuePair<string, string>> args)
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
         return Log(type, GetCurrentAppName(appName), GetCurrentUserName(userName), typeof(TCodeUnit).FullName, function, exception.Message, exception.StackTrace, context, args);
      }

      private static Exception FindInnermostException(Exception exception)
      {
         while (exception.InnerException != null)
         {
            exception = exception.InnerException;
         }
         return exception;
      }

      private static string GetCurrentAppName(string appName)
      {
         if (String.IsNullOrWhiteSpace(appName) || appName == LogEntry.AutomaticallyFilled)
         {
            return Common.Configuration.Instance.ApplicationName;
         }
         return appName;
      }

      private static string GetCurrentUserName(string userName)
      {
         if (!String.IsNullOrWhiteSpace(userName) && userName != LogEntry.AutomaticallyFilled)
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