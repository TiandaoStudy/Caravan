using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Logging.Exceptions;
using Finsa.Caravan.Common.Utilities.Diagnostics;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class LoggerBase<TLog> : ILogger where TLog : LoggerBase<TLog>
    {
        #region ILogger Members

        public Task<LogResult> LogRawAsync(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function,
            string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            return Task.Run(() => LogRaw(logLevel, appName, userLogin, codeUnit, function, shortMessage, longMessage, context, args));
        }

        public LogResult Log<TCodeUnit>(LogLevel logLevel, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(logLevel, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogAsync<TCodeUnit>(LogLevel logLevel, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(logLevel, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult Log<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(e.LogLevel, e.AppName, e.UserLogin, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(e.LogLevel, e.AppName, e.UserLogin, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Debug, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogDebugAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Debug, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogDebug<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogLevel.Debug, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogDebugAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Debug, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogTrace<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Trace, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogTraceAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Trace, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogTrace<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogLevel.Trace, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogTraceAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Trace, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Info, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogInfoAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Info, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogInfo<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogLevel.Info, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogInfoAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Info, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Warn, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogWarnAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Warn, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogWarn<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogLevel.Warn, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogWarnAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Warn, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Error, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogErrorAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
           string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Error, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogError<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogLevel.Error, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogErrorAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Error, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Fatal, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogFatalAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Fatal, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogFatal<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogLevel.Fatal, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogFatalAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Fatal, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public Task<LogResult> LogRawAsync(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function,
            Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            return Task.Run(() => LogRaw(logLevel, appName, userLogin, codeUnit, function, exception, context, args));
        }

        public LogResult Log<TCodeUnit>(LogLevel logLevel, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(logLevel, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogAsync<TCodeUnit>(LogLevel logLevel, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(logLevel, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Debug, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogDebugAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Debug, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogTrace<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Trace, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogTraceAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Trace, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Info, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogInfoAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Info, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Warn, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogWarnAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Warn, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Error, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogErrorAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Error, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogLevel.Fatal, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogFatalAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogLevel.Fatal, appName, userLogin, function, exception, context, args));
        }

        public IList<LogEntry> Entries()
        {
            return GetEntries(null, null);
        }

        public IList<LogEntry> Entries(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetEntries(appName.ToLower(), null);
        }

        public IList<LogEntry> Entries(LogLevel logLevel)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetEntries(null, logLevel);
        }

        public IList<LogEntry> Entries(string appName, LogLevel logLevel)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetEntries(appName.ToLower(), logLevel);
        }

        public void RemoveEntry(string appName, int id)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentNullException>.IfIsNull(id);
            if (!DoRemoveEntry(appName, id))
            {
                throw new LogEntryNotFoundException();
            }
        }

        public IList<LogSetting> Settings()
        {
            return GetSettings(null, null);
        }

        public IList<LogSetting> Settings(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetSettings(appName.ToLower(), null);
        }

        public IList<LogSetting> Settings(LogLevel logLevel)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetSettings(null, logLevel);
        }

        public LogSetting Settings(string appName, LogLevel logLevel)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetSettings(appName.ToLower(), logLevel).FirstOrDefault();
        }

        public void AddSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            Raise<ArgumentNullException>.IfIsNull(setting);
            Raise<ArgumentOutOfRangeException>.If(setting.Days < 1 || setting.MaxEntries < 1);
            if (!DoAddSetting(appName.ToLower(), logLevel, setting))
            {
                throw new LogSettingExistingException();
            }
        }

        public void UpdateSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            Raise<ArgumentNullException>.IfIsNull(setting);
            Raise<ArgumentOutOfRangeException>.If(setting.Days < 1 || setting.MaxEntries < 1);
            if (!DoUpdateSetting(appName.ToLower(), logLevel, setting))
            {
                throw new LogSettingNotFoundException();
            }
        }

        public void RemoveSetting(string appName, LogLevel logLevel)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            if (!DoRemoveSetting(appName, logLevel))
            {
                throw new LogSettingNotFoundException();
            }
        }

        #endregion ILogger Members

        protected abstract LogResult DoLogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage, string context,
            IEnumerable<KeyValuePair<string, string>> args);

        protected abstract IList<LogEntry> GetEntries(string appName, LogLevel? logLevel);

        protected abstract IList<LogSetting> GetSettings(string appName, LogLevel? logLevel);

        protected abstract bool DoRemoveEntry(string appName, int logId);

        protected abstract bool DoAddSetting(string appName, LogLevel logLevel, LogSetting setting);

        protected abstract bool DoUpdateSetting(string appName, LogLevel logLevel, LogSetting setting);

        protected abstract bool DoRemoveSetting(string appName, LogLevel logLevel);

        #region Shortcuts

        public LogResult LogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage,
            string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentNullException>.IfIsNull(shortMessage);
                AuxCommonLogging(logLevel, shortMessage, context);
            }
            catch (Exception ex)
            {
                try
                {
                    CommonLogging.Error(ex.Message);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }
                return LogResult.Failure(ex);
            }
            return DoLogRaw(logLevel, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), codeUnit, function, shortMessage, longMessage, context, args);
        }

        private LogResult Log<TCodeUnit>(LogLevel logLevel, string appName, string userLogin, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentException>.IfIsEmpty(shortMessage);
                AuxCommonLogging(logLevel, shortMessage, context);
            }
            catch (Exception ex)
            {
                try
                {
                    CommonLogging.Error(ex.Message);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }
                return LogResult.Failure(ex);
            }
            return DoLogRaw(logLevel, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), typeof(TCodeUnit).FullName, function, shortMessage, longMessage, context, args);
        }

        public LogResult LogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, Exception exception, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentNullException>.IfIsNull(exception);
                exception = FindInnermostException(exception);
                AuxCommonLogging(logLevel, exception.Message, context);
            }
            catch (Exception ex)
            {
                try
                {
                    CommonLogging.Error(ex.Message);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }
                return LogResult.Failure(ex);
            }
            return DoLogRaw(logLevel, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), codeUnit, function, exception.Message, exception.StackTrace, context, args);
        }

        private LogResult Log<TCodeUnit>(LogLevel logLevel, string appName, string userLogin, string function, Exception exception, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentNullException>.IfIsNull(exception);
                exception = FindInnermostException(exception);
                AuxCommonLogging(logLevel, exception.Message, context);
            }
            catch (Exception ex)
            {
                try
                {
                    CommonLogging.Error(ex.Message);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }
                return LogResult.Failure(ex);
            }
            return DoLogRaw(logLevel, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), typeof(TCodeUnit).FullName, function, exception.Message, exception.StackTrace, context, args);
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
            if (String.IsNullOrWhiteSpace(appName) || appName == LogEntry.AutoFilled)
            {
                return Common.Properties.Settings.Default.ApplicationName;
            }
            return appName;
        }

        private static string GetCurrentuserLogin(string userLogin)
        {
            if (!String.IsNullOrWhiteSpace(userLogin) && userLogin != LogEntry.AutoFilled)
            {
                return userLogin;
            }
            if (HttpContext.Current == null || HttpContext.Current.User == null || HttpContext.Current.User.Identity == null)
            {
                return LogEntry.NotSpecified;
            }
            return HttpContext.Current.User.Identity.Name;
        }

        private static void CheckAndFillEntry(LogEntry entry, string function)
        {
            Raise<ArgumentNullException>.IfIsNull(entry);
            entry.Function = String.IsNullOrWhiteSpace(entry.Function) ? function : entry.Function;
        }

        #endregion Shortcuts

        #region Common.Logging

        private static readonly ILog CommonLogging = LogManager.GetLogger<TLog>();

        private static void AuxCommonLogging(LogLevel logLevel, string shortMessage, string context)
        {
            var msg = shortMessage;
            if (!String.IsNullOrWhiteSpace(context))
            {
                msg += " @ " + context;
            }
            switch (logLevel)
            {
                case LogLevel.Debug:
                    CommonLogging.Debug(msg);
                    break;

                case LogLevel.Error:
                    CommonLogging.Error(msg);
                    break;

                case LogLevel.Fatal:
                    CommonLogging.Fatal(msg);
                    break;

                case LogLevel.Info:
                    CommonLogging.Info(msg);
                    break;

                case LogLevel.Trace:
                    CommonLogging.Trace(msg);
                    break;

                case LogLevel.Warn:
                    CommonLogging.Warn(msg);
                    break;
            }
        }

        #endregion Common.Logging
    }
}