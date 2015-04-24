using Common.Logging;
using Common.Logging.Simple;
using Finsa.Caravan.Common.Models.Logging;
using System;
using System.Collections.Generic;

namespace Finsa.Caravan.Common.Logging
{
    public sealed class NoOpCaravanLogger : ICaravanLog
    {
        #region ILog Members

        /// <summary>
        ///   Always returns <see langword="false"/>.
        /// </summary>
        public bool IsTraceEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///   Always returns <see langword="false"/>.
        /// </summary>
        public bool IsDebugEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///   Always returns <see langword="false"/>.
        /// </summary>
        public bool IsInfoEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///   Always returns <see langword="false"/>.
        /// </summary>
        public bool IsWarnEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///   Always returns <see langword="false"/>.
        /// </summary>
        public bool IsErrorEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///   Always returns <see langword="false"/>.
        /// </summary>
        public bool IsFatalEnabled
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///   Returns the global context for variables
        /// </summary>
        public IVariablesContext GlobalVariablesContext
        {
            get
            {
                return (IVariablesContext) new NoOpVariablesContext();
            }
        }

        /// <summary>
        ///   Returns the thread-specific context for variables
        /// </summary>
        public IVariablesContext ThreadVariablesContext
        {
            get
            {
                return (IVariablesContext) new NoOpVariablesContext();
            }
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        public void Trace(object message)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        /// <param name="e"/>
        public void Trace(object message, Exception e)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public void TraceFormat(string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void TraceFormat(string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of message format arguments</param>
        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        public void Debug(object message)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        /// <param name="e"/>
        public void Debug(object message, Exception e)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public void DebugFormat(string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void DebugFormat(string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of message format arguments</param>
        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Debug.</param>
        public void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Debug.</param>
        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        public void Info(object message)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        /// <param name="e"/>
        public void Info(object message, Exception e)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public void InfoFormat(string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void InfoFormat(string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of message format arguments</param>
        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Info.</param>
        public void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Info.</param>
        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        public void Warn(object message)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        /// <param name="e"/>
        public void Warn(object message, Exception e)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public void WarnFormat(string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void WarnFormat(string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Warnrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of message format arguments</param>
        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Warnrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Warn.</param>
        public void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Warn.</param>
        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        public void Error(object message)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        /// <param name="e"/>
        public void Error(object message, Exception e)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public void ErrorFormat(string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void ErrorFormat(string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Errorrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of message format arguments</param>
        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Errorrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Error.</param>
        public void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Error.</param>
        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        public void Fatal(object message)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="message"/>
        /// <param name="e"/>
        public void Fatal(object message, Exception e)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public void FatalFormat(string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void FatalFormat(string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of message format arguments</param>
        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of message format arguments</param>
        public void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Fatal.</param>
        public void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
        }

        /// <summary>
        ///   Ignores message.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Fatal.</param>
        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
        }

        #endregion ILog Members

        #region ICaravanLog Members

        public void TraceArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
        }

        public void TraceArgs(Func<LogMessage> logMessageCallback)
        {
        }

        public void DebugArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
        }

        public void DebugArgs(Func<LogMessage> logMessageCallback)
        {
        }

        public void InfoArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
        }

        public void InfoArgs(Func<LogMessage> logMessageCallback)
        {
        }

        public void WarnArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
        }

        public void WarnArgs(Func<LogMessage> logMessageCallback)
        {
        }

        public void ErrorArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
        }

        public void ErrorArgs(Func<LogMessage> logMessageCallback)
        {
        }

        public void FatalArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
        }

        public void FatalArgs(Func<LogMessage> logMessageCallback)
        {
        }

        #endregion ICaravanLog Members
    }
}