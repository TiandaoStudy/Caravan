using Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.CodeServices.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Handles logging and log settings.
    /// </summary>
    public interface ICaravanLogRepository
    {
        #region Logging

        Task<LogResult> AddEntriesAsync(string appName, IEnumerable<LogEntry> logEntries);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        LogResult LogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        Task<LogResult> LogRawAsync(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogLevel logLevel, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogLevel logLevel, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogTrace<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogTraceAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogTrace<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogTraceAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        LogResult LogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        Task<LogResult> LogRawAsync(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogLevel logLevel, Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogLevel logLevel, Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogTrace<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogTraceAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">
        ///   The class, module or package in which this log was produced.
        /// </typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">
        ///   Contextual information which may add more meaning to the log message.
        /// </param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">
        ///   The method, procedure or function from which the log was called.
        /// </param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IList<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        #endregion Logging

        #region Entries

        /// <summary>
        ///   TODO
        /// </summary>
        /// <returns></returns>
        IList<LogEntry> GetEntries();

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        IList<LogEntry> GetEntries(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="logLevel"/> is not a valid <see cref="logLevel"/>.
        /// </exception>
        IList<LogEntry> GetEntries(LogLevel logLevel);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> is null or empty. <paramref name="logLevel"/> is not a
        ///   valid <see cref="logLevel"/>.
        /// </exception>
        IList<LogEntry> GetEntries(string appName, LogLevel logLevel);

        /// <summary>
        ///   Interroga direttamente la sorgente dati, tramite i dati opzionali passati in input.
        /// </summary>
        /// <param name="logEntryQuery">I parametri con cui eseguire la query.</param>
        /// <returns>Tutte le righe che rispettano i parametri passati come argomento.</returns>
        IList<LogEntry> QueryEntries(LogEntryQuery logEntryQuery);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <returns></returns>
        [Pure]
        Option<LogEntry> GetEntry(string appName, long logId);

        /// <summary>
        ///   Removes a log entry. Use it to delete logs that contain sensitive information!
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logId"></param>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        void RemoveEntry(string appName, int logId);

        /// <summary>
        ///   Applica le impostazioni di log e pulisce i log più vecchi, quelli che superano il
        ///   limite di cardinalità, ecc ecc.
        /// </summary>
        void CleanUpEntries();

        /// <summary>
        ///   Applica le impostazioni di log e pulisce i log più vecchi, quelli che superano il
        ///   limite di cardinalità, ecc ecc, ma solo per l'applicazione data.
        /// </summary>
        void CleanUpEntries(string appName);

        #endregion Entries

        #region Settings

        /// <summary>
        ///   TODO
        /// </summary>
        /// <returns></returns>
        IList<LogSetting> GetSettings();

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
        IList<LogSetting> GetSettings(string appName);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="logLevel"/> is not a valid <see cref="logLevel"/>.
        /// </exception>
        IList<LogSetting> GetSettings(LogLevel logLevel);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///   <paramref name="appName"/> is null or empty. <paramref name="logLevel"/> is not a
        ///   valid <see cref="logLevel"/>.
        /// </exception>
        LogSetting GetSettings(string appName, LogLevel logLevel);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="setting"></param>
        void AddSetting(string appName, LogLevel logLevel, LogSetting setting);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="setting"></param>
        void UpdateSetting(string appName, LogLevel logLevel, LogSetting setting);

        /// <summary>
        ///   Removes a setting with a specificed log type in a specified application.
        /// </summary>
        /// <param name="appName">The application name.</param>
        /// <param name="logLevel">The log type: INFO, DEBUG, etc etc.</param>
        void RemoveSetting(string appName, LogLevel logLevel);

        #endregion Settings
    }
}
