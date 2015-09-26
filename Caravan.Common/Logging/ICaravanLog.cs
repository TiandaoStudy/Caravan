using Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using System;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   A log customized for Caravan.
    /// </summary>
    public interface ICaravanLog : ILog
    {
        #region Trace

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Trace(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Trace(Func<LogMessage> logMessageCallback);

        #endregion Trace

        #region Debug

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Debug(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Debug(Func<LogMessage> logMessageCallback);

        #endregion Debug

        #region Info

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Info(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Info(Func<LogMessage> logMessageCallback);

        #endregion Info

        #region Warn

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Warn(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Warn(Func<LogMessage> logMessageCallback);

        #endregion Warn

        #region Error

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Error(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Error(Func<LogMessage> logMessageCallback);

        #endregion Error

        #region Fatal

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Fatal(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Fatal(Func<LogMessage> logMessageCallback);

        #endregion Fatal

        #region Exception

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Exception(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Exception(Func<LogMessage> logMessageCallback);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        bool Exception(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        bool Exception(object message, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        bool Exception(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool ExceptionFormat(string format, Exception exception, params object[] args);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool ExceptionFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

        #endregion Exception
    }
}
