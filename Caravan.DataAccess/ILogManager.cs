﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Finsa.Caravan.DataModel.Logging;

namespace Finsa.Caravan.DataAccess
{
   /// <summary>
   ///   TODO
   /// </summary>
   public interface ILogManager
   {
      #region Logging Methods
      
      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="type"></param>
      /// <param name="appName"></param>
      /// <param name="userName"></param>
      /// <param name="codeUnit"></param>
      /// <param name="function"></param>
      /// <param name="shortMessage"></param>
      /// <param name="longMessage"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <returns></returns>
      LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null);
      
      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="type"></param>
      /// <param name="shortMessage"></param>
      /// <param name="longMessage"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      LogResult Log<TCodeUnit>(LogType type, string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      
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
      LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

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
      LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

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
      LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

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
      Task<LogResult> LogWarnAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

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
      LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

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
      Task<LogResult> LogErrorAsync<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

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
      LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null,
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      
      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="type"></param>
      /// <param name="appName"></param>
      /// <param name="userName"></param>
      /// <param name="codeUnit"></param>
      /// <param name="function"></param>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <returns></returns>
      LogResult Log(LogType type, string appName, string userName, string codeUnit, string function, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="type"></param>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      LogResult Log<TCodeUnit>(LogType type, Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);
      
      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      Task<LogResult> LogWarnAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      Task<LogResult> LogErrorAsync<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <typeparam name="TCodeUnit"></typeparam>
      /// <param name="exception"></param>
      /// <param name="context"></param>
      /// <param name="args"></param>
      /// <param name="applicationName"></param>
      /// <param name="userName"></param>
      /// <param name="function"></param>
      /// <returns></returns>
      LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<CKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      #endregion

      #region Logs Retrieval

      /// <summary>
      ///   TODO
      /// </summary>
      /// <returns></returns>
      IList<LogEntry> Logs();

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="appName"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      IList<LogEntry> Logs(string appName);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="logType"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="logType"/> is not a valid <see cref="LogType"/>.
      /// </exception>
      IList<LogEntry> Logs(LogType logType);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="logType"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      ///   <paramref name="logType"/> is not a valid <see cref="LogType"/>.
      /// </exception>
      IList<LogEntry> Logs(string appName, LogType logType);

      #endregion

      #region Log Settings

      /// <summary>
      ///   TODO
      /// </summary>
      /// <returns></returns>
      IList<LogSettings> LogSettings();

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="appName"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      /// </exception>
      IList<LogSettings> LogSettings(string appName);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="logType"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="logType"/> is not a valid <see cref="LogType"/>.
      /// </exception>
      IList<LogSettings> LogSettings(LogType logType);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="logType"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">
      ///   <paramref name="appName"/> is null or empty.
      ///   <paramref name="logType"/> is not a valid <see cref="LogType"/>.
      /// </exception>
      LogSettings LogSettings(string appName, LogType logType);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="logType"></param>
      /// <param name="settings"></param>
      void AddSettings(string appName, LogType logType, LogSettings settings);

      /// <summary>
      /// 
      /// </summary>
      /// <param name="appName"></param>
      /// <param name="logType"></param>
      /// <param name="settings"></param>
      void UpdateSettings(string appName, LogType logType, LogSettings settings);

      #endregion
   }
}