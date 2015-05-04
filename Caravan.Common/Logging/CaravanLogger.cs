﻿using Common.Logging;
using Common.Logging.Factory;
using Common.Logging.NLog;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Serialization;
using Finsa.Caravan.Common.Utilities;
using Finsa.Caravan.Common.Utilities.Collections.ReadOnly;
using NLog;
using System;
using System.Collections.Generic;
using LogLevel = Common.Logging.LogLevel;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Concrete implementation of <see cref="ILog"/> interface specific to Caravan.
    /// </summary>
    /// <author>Alessio Parma</author>
    public class CaravanLogger : AbstractLogger, ICaravanLog
    {
        #region Fields

        /// <summary>
        ///   The prefix used to identify complex messages.
        /// </summary>
        public const string JsonMessagePrefix = "#JSON#";

        private static readonly IJsonSerializer CachedJsonSerializer = new JsonNetSerializer();

        private readonly Logger _logger;

        #endregion Fields

        #region Construction

        /// <summary>
        ///   Builds an instance of the Caravan logger.
        /// </summary>
        /// <param name="logger">The <see cref="Logger"/> that will be used as backend.</param>
        public CaravanLogger(Logger logger)
        {
            _logger = logger;
        }

        #endregion Construction

        #region Properties

        /// <summary>
        ///   The JSON serializer used to pack messages.
        /// </summary>
        public static IJsonSerializer JsonSerializer
        {
            get { return CachedJsonSerializer; }
        }

        #endregion Properties

        #region ILog Members

        /// <summary>
        ///   Gets a value indicating whether this instance is trace enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is trace enabled; otherwise, <c>false</c>.</value>
        public override bool IsTraceEnabled
        {
            get
            {
                return _logger.IsTraceEnabled;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.</value>
        public override bool IsDebugEnabled
        {
            get
            {
                return _logger.IsDebugEnabled;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is info enabled; otherwise, <c>false</c>.</value>
        public override bool IsInfoEnabled
        {
            get
            {
                return _logger.IsInfoEnabled;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.</value>
        public override bool IsWarnEnabled
        {
            get
            {
                return _logger.IsWarnEnabled;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is error enabled; otherwise, <c>false</c>.</value>
        public override bool IsErrorEnabled
        {
            get
            {
                return _logger.IsErrorEnabled;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.</value>
        public override bool IsFatalEnabled
        {
            get
            {
                return _logger.IsFatalEnabled;
            }
        }

        /// <summary>
        ///   Returns the global context for variables
        /// </summary>
        public override IVariablesContext GlobalVariablesContext
        {
            get
            {
                return new NLogGlobalVariablesContext();
            }
        }

        /// <summary>
        ///   Returns the thread-specific context for variables
        /// </summary>
        public override IVariablesContext ThreadVariablesContext
        {
            get
            {
                return new NLogThreadVariablesContext();
            }
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Trace"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public override void Trace(object message)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, message, null);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Trace"/> level including the stack
        ///   trace of the <see cref="T:System.Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public override void Trace(object message, Exception exception)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, message, exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public override void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args"/>
        public override void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of format arguments</param>
        public override void TraceFormat(string format, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of format arguments</param>
        public new virtual void TraceFormat(string format, Exception exception, params object[] args)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public override void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Trace"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public override void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsTraceEnabled)
                return;
            WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Debug"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public override void Debug(object message)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, message, null);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Debug"/> level including the stack
        ///   Debug of the <see cref="T:System.Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack Debug.</param>
        public override void Debug(object message, Exception exception)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, message, exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public override void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args"/>
        public override void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of format arguments</param>
        public override void DebugFormat(string format, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of format arguments</param>
        public override void DebugFormat(string format, Exception exception, params object[] args)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Debug.</param>
        public override void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Debug"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Debug.</param>
        public override void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsDebugEnabled)
                return;
            WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Info"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public override void Info(object message)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, message, null);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Info"/> level including the stack
        ///   Info of the <see cref="T:System.Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack Info.</param>
        public override void Info(object message, Exception exception)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, message, exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public override void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args"/>
        public override void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of format arguments</param>
        public override void InfoFormat(string format, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(null, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of format arguments</param>
        public override void InfoFormat(string format, Exception exception, params object[] args)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(null, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Info.</param>
        public override void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Info"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Info.</param>
        public override void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsInfoEnabled)
                return;
            WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Warn"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public override void Warn(object message)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, message, null);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Warn"/> level including the stack
        ///   Warn of the <see cref="T:System.Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack Warn.</param>
        public override void Warn(object message, Exception exception)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, message, exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Warnrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public override void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Warnrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args"/>
        public override void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of format arguments</param>
        public override void WarnFormat(string format, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of format arguments</param>
        public override void WarnFormat(string format, Exception exception, params object[] args)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Warn.</param>
        public override void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Warn"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Warn.</param>
        public override void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsWarnEnabled)
                return;
            WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Error"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public override void Error(object message)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, message, null);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Error"/> level including the stack
        ///   Error of the <see cref="T:System.Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack Error.</param>
        public override void Error(object message, Exception exception)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, message, exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Errorrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public override void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Errorrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args"/>
        public override void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of format arguments</param>
        public override void ErrorFormat(string format, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(null, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of format arguments</param>
        public override void ErrorFormat(string format, Exception exception, params object[] args)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(null, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Error.</param>
        public override void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Error"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Error.</param>
        public override void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsErrorEnabled)
                return;
            WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Fatal"/> level.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        public override void Fatal(object message)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, message, null);
        }

        /// <summary>
        ///   Log a message object with the <see cref="F:LogLevel.Fatal"/> level including the stack
        ///   Fatal of the <see cref="T:System.Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack Fatal.</param>
        public override void Fatal(object message, Exception exception)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, message, exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args"/>
        public override void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level.
        /// </summary>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting Fatalrmation.
        /// </param>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args"/>
        public override void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="args">the list of format arguments</param>
        public override void FatalFormat(string format, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level.
        /// </summary>
        /// <param name="format">The format of the message object to log. <see cref="M:System.String.Format(System.String,System.Object[])"/></param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="args">the list of format arguments</param>
        public override void FatalFormat(string format, Exception exception, params object[] args)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Fatal.</param>
        public override void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        public override void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
        }

        /// <summary>
        ///   Log a message with the <see cref="F:LogLevel.Fatal"/> level using a callback to obtain
        ///   the message
        /// </summary>
        /// <remarks>
        ///   Using this method avoids the cost of creating a message and evaluating message
        ///   arguments that probably won't be logged due to loglevel settings.
        /// </remarks>
        /// <param name="formatProvider">
        ///   An <see cref="T:System.IFormatProvider"/> that supplies culture-specific formatting information.
        /// </param>
        /// <param name="formatMessageCallback">
        ///   A callback used by the logger to obtain the message if log level is matched
        /// </param>
        /// <param name="exception">The exception to log, including its stack Fatal.</param>
        public override void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (!IsFatalEnabled)
                return;
            WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
        }

        #endregion ILog Members

        #region ICaravanLog Members

        #region Trace

        public void TraceArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void TraceArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion Trace

        #region Debug

        public void DebugArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void DebugArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion Debug

        #region Info

        public void InfoArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void InfoArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion Info

        #region Warn

        public void WarnArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void WarnArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion Warn

        #region Error

        public void ErrorArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void ErrorArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion Error

        #region Fatal

        public void FatalArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, LogMessageHandler(shortMessage, longMessage, context), null);
            }
        }

        public void FatalArgs(Func<LogMessage> logMessageCallback)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, LogMessageHandler(logMessageCallback), null);
            }
        }

        #endregion Fatal

        private static string LogMessageHandler(Func<LogMessage> logMessageCallback)
        {
            var safeCallback = logMessageCallback;
            return safeCallback == null ? Constants.EmptyString : SerializeJsonlogMessageCallback(safeCallback());
        }

        private static string LogMessageHandler(string shortMsg, string longMsg, string context)
        {
            return SerializeJsonlogMessageCallback(new LogMessage
            {
                ShortMessage = shortMsg,
                LongMessage = longMsg,
                Context = context
            });
        }

        private static string SerializeJsonlogMessageCallback(LogMessage logMessage)
        {
            if (logMessage.Exception != null)
            {
                var exception = logMessage.Exception;
                logMessage.Exception = null;
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
                logMessage.ShortMessage = exception.Message;
                logMessage.LongMessage = exception.StackTrace;
                // Keep aligned with Finsa.DataAccess.CaravanLoggerTarget.ParseMessage
                logMessage.Arguments = new List<KeyValuePair<string, string>>(logMessage.Arguments ?? ReadOnlyList<KeyValuePair<string, string>>.EmptyList)
                {
                    KeyValuePair.Create("exception_data", exception.Data.LogAsJson()),
                    KeyValuePair.Create("exception_source", exception.Source ?? Constants.EmptyString)
                };
            }
            return JsonMessagePrefix + CachedJsonSerializer.SerializeObject(logMessage);
        }

        #endregion ICaravanLog Members

        #region Private Methods

        /// <summary>
        ///   Actually sends the message to the underlying log system.
        /// </summary>
        /// <param name="logLevel">the level of this log event.</param>
        /// <param name="message">the message to log</param>
        /// <param name="exception">the exception to log (may be null)</param>
        protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
        {
            var level = GetLevel(logLevel);
            var logEvent = new LogEventInfo(level, _logger.Name, null, "{0}", new[] { message }, exception);
            _logger.Log(typeof(CaravanLogger), logEvent);
        }

        private static NLog.LogLevel GetLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.All:
                    return NLog.LogLevel.Trace;

                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;

                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;

                case LogLevel.Info:
                    return NLog.LogLevel.Info;

                case LogLevel.Warn:
                    return NLog.LogLevel.Warn;

                case LogLevel.Error:
                    return NLog.LogLevel.Error;

                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;

                case LogLevel.Off:
                    return NLog.LogLevel.Off;

                default:
                    throw new ArgumentOutOfRangeException("logLevel", logLevel, @"Unknown log level");
            }
        }

        #endregion Private Methods
    }
}