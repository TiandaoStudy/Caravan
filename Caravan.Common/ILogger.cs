using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Models.Logging;
using PommaLabs;

namespace Finsa.Caravan.Common
{
    /// <summary>
    ///   Handles logging and log settings.
    /// </summary>
    public interface ILogger
    {
        #region Logging

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        LogResult LogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        Task<LogResult> LogRawAsync(LogType logType, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogType logType, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogType logType, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogTrace<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogTraceAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogTrace<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogTraceAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName"></param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="shortMessage"></param>
        /// <param name="longMessage"></param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logEntry"></param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(LogEntry logEntry, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        LogResult LogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="codeUnit">The class, module or package in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <returns></returns>
        Task<LogResult> LogRawAsync(LogType logType, string appName, string userLogin, string codeUnit, string function, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult Log<TCodeUnit>(LogType logType, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="logType">The log type: INFO, DEBUG, etc etc.</param>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogAsync<TCodeUnit>(LogType logType, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogDebugAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogTrace<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogTraceAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogInfoAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogWarnAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogErrorAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <typeparam name="TCodeUnit">The class, module or package in which this log was produced.</typeparam>
        /// <param name="exception">The exception to be logged.</param>
        /// <param name="context">Contextual information which may add more meaning to the log message.</param>
        /// <param name="args">Further information that should be logged.</param>
        /// <param name="appName">The application name.</param>
        /// <param name="userLogin">The user logged in at the moment in which this log was produced.</param>
        /// <param name="function">The method, procedure or function from which the log was called.</param>
        /// <returns></returns>
        Task<LogResult> LogFatalAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<KeyValuePair<string, string>> args = null,
            string appName = LogEntry.AutoFilled, string userLogin = LogEntry.AutoFilled, [CallerMemberName] string function = LogEntry.AutoFilled);

        #endregion Logging

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
        /// <exception cref="ArgumentException"><paramref name="appName"/> is null or empty.</exception>
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

    /// <summary>
    ///   Convenient if it is in the same namespace as <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        ///   Converts given object in a very compact JSON format. If given object is null, an empty string is returned.
        /// </summary>
        /// <typeparam name="TObj">The type of the object. Used to avoid boxing.</typeparam>
        /// <param name="obj">The object that should be converted.</param>
        /// <returns>A very compact JSON corresponding to given object.</returns>
        public static string LogAsJson<TObj>(this TObj obj)
        {
            return ReferenceEquals(obj, null) ? Constants.EmptyString : Core.ServiceProvider.JsonSerializer.SerializeObject(obj);
        }
    }
}