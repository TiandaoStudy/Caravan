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

        public Task<LogResult> LogRawAsync(LogType logType, string appName, string userLogin, string codeUnit, string function,
            string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            return Task.Run(() => LogRaw(logType, appName, userLogin, codeUnit, function, shortMessage, longMessage, context, args));
        }

        public LogResult Log<TCodeUnit>(LogType logType, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(logType, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogAsync<TCodeUnit>(LogType logType, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(logType, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult Log<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(e.LogType, e.AppName, e.UserLogin, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(e.LogType, e.AppName, e.UserLogin, e.Function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Debug, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogDebugAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Debug, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogDebug<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogType.Debug, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogDebugAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogType.Debug, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogTrace<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Trace, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogTraceAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Trace, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogTrace<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogType.Trace, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogTraceAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogType.Trace, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Info, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogInfoAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Info, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogInfo<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogType.Info, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogInfoAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogType.Info, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Warn, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogWarnAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Warn, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogWarn<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogType.Warn, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogWarnAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogType.Warn, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Error, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogErrorAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
           string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Error, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogError<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogType.Error, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogErrorAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogType.Error, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Fatal, appName, userLogin, function, shortMessage, longMessage, context, args);
        }

        public Task<LogResult> LogFatalAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled,
            string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Fatal, appName, userLogin, function, shortMessage, longMessage, context, args));
        }

        public LogResult LogFatal<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Log<TCodeUnit>(LogType.Fatal, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments);
        }

        public Task<LogResult> LogFatalAsync<TCodeUnit>(LogEntry e, string function = LogEntry.AutoFilled)
        {
            CheckAndFillEntry(e, function);
            return Task.Run(() => Log<TCodeUnit>(LogType.Fatal, e.AppName, e.UserLogin, function, e.ShortMessage, e.LongMessage, e.Context, e.Arguments));
        }

        public Task<LogResult> LogRawAsync(LogType logType, string appName, string userLogin, string codeUnit, string function,
            Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            return Task.Run(() => LogRaw(logType, appName, userLogin, codeUnit, function, exception, context, args));
        }

        public LogResult Log<TCodeUnit>(LogType logType, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(logType, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogAsync<TCodeUnit>(LogType logType, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(logType, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Debug, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogDebugAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Debug, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogTrace<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Trace, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogTraceAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Trace, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Info, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogInfoAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Info, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Warn, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogWarnAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Warn, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Error, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogErrorAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Error, appName, userLogin, function, exception, context, args));
        }

        public LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled,
            string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled)
        {
            return Log<TCodeUnit>(LogType.Fatal, appName, userLogin, function, exception, context, args);
        }

        public Task<LogResult> LogFatalAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null, string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, string function = LogEntry.AutoFilled)
        {
            return Task.Run(() => Log<TCodeUnit>(LogType.Fatal, appName, userLogin, function, exception, context, args));
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

        public IList<LogEntry> Entries(LogType logType)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
            return GetEntries(null, logType);
        }

        public IList<LogEntry> Entries(string appName, LogType logType)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
            return GetEntries(appName.ToLower(), logType);
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

        public IList<LogSetting> Settings(LogType logType)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
            return GetSettings(null, logType);
        }

        public LogSetting Settings(string appName, LogType logType)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
            return GetSettings(appName.ToLower(), logType).FirstOrDefault();
        }

        public void AddSetting(string appName, LogType logType, LogSetting setting)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
            Raise<ArgumentNullException>.IfIsNull(setting);
            Raise<ArgumentOutOfRangeException>.If(setting.Days < 1 || setting.MaxEntries < 1);
            if (!DoAddSetting(appName.ToLower(), logType, setting))
            {
                throw new LogSettingExistingException();
            }
        }

        public void UpdateSetting(string appName, LogType logType, LogSetting setting)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
            Raise<ArgumentNullException>.IfIsNull(setting);
            Raise<ArgumentOutOfRangeException>.If(setting.Days < 1 || setting.MaxEntries < 1);
            if (!DoUpdateSetting(appName.ToLower(), logType, setting))
            {
                throw new LogSettingNotFoundException();
            }
        }

        public void RemoveSetting(string appName, LogType logType)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogType), logType));
            if (!DoRemoveSetting(appName, logType))
            {
                throw new LogSettingNotFoundException();
            }
        }

        #endregion ILogger Members

        protected abstract LogResult DoLogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage, string context,
            IEnumerable<KeyValuePair<string, string>> args);

        protected abstract IList<LogEntry> GetEntries(string appName, LogType? logType);

        protected abstract IList<LogSetting> GetSettings(string appName, LogType? logType);

        protected abstract bool DoRemoveEntry(string appName, int logId);

        protected abstract bool DoAddSetting(string appName, LogType logType, LogSetting setting);

        protected abstract bool DoUpdateSetting(string appName, LogType logType, LogSetting setting);

        protected abstract bool DoRemoveSetting(string appName, LogType logType);

        #region Shortcuts

        public LogResult LogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage,
            string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentNullException>.IfIsNull(shortMessage);
                AuxCommonLogging(logType, shortMessage, context);
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
            return DoLogRaw(logType, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), codeUnit, function, shortMessage, longMessage, context, args);
        }

        private LogResult Log<TCodeUnit>(LogType logType, string appName, string userLogin, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentException>.IfIsEmpty(shortMessage);
                AuxCommonLogging(logType, shortMessage, context);
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
            return DoLogRaw(logType, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), typeof(TCodeUnit).FullName, function, shortMessage, longMessage, context, args);
        }

        public LogResult LogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function, Exception exception, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentNullException>.IfIsNull(exception);
                exception = FindInnermostException(exception);
                AuxCommonLogging(logType, exception.Message, context);
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
            return DoLogRaw(logType, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), codeUnit, function, exception.Message, exception.StackTrace, context, args);
        }

        private LogResult Log<TCodeUnit>(LogType logType, string appName, string userLogin, string function, Exception exception, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentNullException>.IfIsNull(exception);
                exception = FindInnermostException(exception);
                AuxCommonLogging(logType, exception.Message, context);
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
            return DoLogRaw(logType, GetCurrentAppName(appName), GetCurrentuserLogin(userLogin), typeof(TCodeUnit).FullName, function, exception.Message, exception.StackTrace, context, args);
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

        private static void AuxCommonLogging(LogType logType, string shortMessage, string context)
        {
            var msg = shortMessage;
            if (!String.IsNullOrWhiteSpace(context))
            {
                msg += " @ " + context;
            }
            switch (logType)
            {
                case LogType.Debug:
                    CommonLogging.Debug(msg);
                    break;

                case LogType.Error:
                    CommonLogging.Error(msg);
                    break;

                case LogType.Fatal:
                    CommonLogging.Fatal(msg);
                    break;

                case LogType.Info:
                    CommonLogging.Info(msg);
                    break;

                case LogType.Trace:
                    CommonLogging.Trace(msg);
                    break;

                case LogType.Warn:
                    CommonLogging.Warn(msg);
                    break;
            }
        }

        #endregion Common.Logging
    }
}