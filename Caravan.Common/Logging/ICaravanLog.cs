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

        #region Catching

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        void Catching(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void Catching(Func<LogMessage> logMessageCallback);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Catching(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Catching(object message, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Catching(Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Catching(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        void CatchingFormat(string format, Exception exception, params object[] args);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        void CatchingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

        #endregion Catching

        #region Throwing

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        void Throwing(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        void Throwing(Func<LogMessage> logMessageCallback);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Throwing(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Throwing(object message, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Throwing(Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void Throwing(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        void ThrowingFormat(string format, Exception exception, params object[] args);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        void ThrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

        #endregion Throwing

        #region Rethrowing

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessage"></param>
        bool Rethrowing(LogMessage logMessage);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="logMessageCallback"></param>
        bool Rethrowing(Func<LogMessage> logMessageCallback);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        bool Rethrowing(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        bool Rethrowing(object message, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        bool Rethrowing(Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="formatMessageCallback"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        bool Rethrowing(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool RethrowingFormat(string format, Exception exception, params object[] args);

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="format"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool RethrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args);

        #endregion Rethrowing
    }
}
