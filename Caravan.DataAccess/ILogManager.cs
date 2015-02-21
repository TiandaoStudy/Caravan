using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.DataAccess
{
    /// <summary>
    ///   Handles logging and log settings.
    /// </summary>
    public interface ILogManager
    {
        #region Logging Methods

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="type"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="codeUnit"></param>
        /// <param name="function"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="type"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogType type, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="type"></param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogType type, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="applicationName"></param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string applicationName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="applicationName"></param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string applicationName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="applicationName"></param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string applicationName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="applicationName"></param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string applicationName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="applicationName"></param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string applicationName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="type"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="codeUnit"></param>
        /// <param name="function"></param>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="type"></param>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogType type, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="type"></param>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogType type, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit"></typeparam>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        /// <param name="args"></param>
        /// <param name="appName">The application name.</param>
        /// <param name="userName"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
           string appName = LogEntry.AutoFilled, string userName = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        #endregion Logging Methods

        #region Entries

        /// <summary>
        ///   TODO
        /// </summary>
        /// <returns></returns>
        IList<LogEntry> Entries();

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        IList<LogEntry> Entries(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="logType"/> is not a valid <see cref="LogType"/>.
        /// </exception>
        IList<LogEntry> Entries(LogType logType);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> is null or empty. <paramref name="logType"/> is not a valid <see cref="LogType"/>.
        /// </exception>
        IList<LogEntry> Entries(string appName, LogType logType);

        /// <summary>
        ///   Removes a log entry. Use it to delete logs that contain sensitive information!
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logId"></param>
        void RemoveEntry(string appName, int logId);

        #endregion Entries

        #region Settings

        /// <summary>
        ///   TODO
        /// </summary>
        /// <returns></returns>
        IList<LogSetting> Settings();

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        IList<LogSetting> Settings(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="logType"/> is not a valid <see cref="LogType"/>.
        /// </exception>
        IList<LogSetting> Settings(LogType logType);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> is null or empty. <paramref name="logType"/> is not a valid <see cref="LogType"/>.
        /// </exception>
        LogSetting Settings(string appName, LogType logType);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="setting"></param>
        void AddSetting(string appName, LogType logType, LogSetting setting);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="setting"></param>
        void UpdateSetting(string appName, LogType logType, LogSetting setting);

        /// <summary>
        ///   Removes a setting with a specificed log type in a specified application.
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        void RemoveSetting(string appName, LogType logType);

        #endregion Settings
    }
}