using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Finsa.Caravan;
using FLEX.Common.DataModel;

namespace FLEX.DataAccess
{
   /// <summary>
   ///   TODO
   /// </summary>
   public interface ILogger
   {
      #region Logging Methods

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
      LogResult LogDebug<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
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
      LogResult LogInfo<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
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
      LogResult LogWarn<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
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
      LogResult LogError<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
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
      LogResult LogFatal<TCodeUnit>(string shortMessage, string longMessage = LogEntry.NotSpecified, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null,
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
      LogResult LogDebug<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, 
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
      LogResult LogInfo<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, 
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
      LogResult LogWarn<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, 
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
      LogResult LogError<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, 
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
      LogResult LogFatal<TCodeUnit>(Exception exception, string context = LogEntry.NotSpecified, IEnumerable<GKeyValuePair<string, string>> args = null, 
         string applicationName = LogEntry.AutomaticallyFilled, string userName = LogEntry.AutomaticallyFilled, [CallerMemberName] string function = LogEntry.AutomaticallyFilled);

      #endregion

      #region Logs Retrieval

      /// <summary>
      ///   TODO
      /// </summary>
      /// <returns></returns>
      IEnumerable<LogEntry> GetAllLogs();

      /// <summary>
      ///   TODO
      /// </summary>
      /// <returns></returns>
      IEnumerable<LogEntry> GetCurrentApplicationLogs();

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="applicationName"></param>
      /// <returns></returns>
      IEnumerable<LogEntry> GetApplicationLogs(string applicationName);

      #endregion

      #region Log Settings

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="logType"></param>
      /// <returns></returns>
      IList<LogSettings> GetAllSettings(LogType logType);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="logType"></param>
      /// <returns></returns>
      LogSettings GetCurrentApplicationSettings(LogType logType);

      /// <summary>
      ///   TODO
      /// </summary>
      /// <param name="logType"></param>
      /// <param name="applicationName"></param>
      /// <returns></returns>
      LogSettings GetApplicationSettings(LogType logType, string applicationName);

      #endregion
   }
}