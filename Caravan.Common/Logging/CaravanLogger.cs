using Common.Logging;
using Common.Logging.Factory;
using Finsa.Caravan.Common.Logging.Models;
using PommaLabs.Thrower;
using System;

namespace Finsa.Caravan.Common.Logging
{
    sealed class CaravanLogger : AbstractLogger, ICaravanLog
    {
        /// <summary>
        ///   Il valore di ritorno dei metodi di log, impostato a FALSE. Questo è stato deciso per
        ///   consentire l'uso dei metodi di log all'interno degli "exception filters" introdotti in C#6.
        /// </summary>
        private const bool DoNotFilterException = false;

        /// <summary>
        ///   The tag used when the exception received as argument is null.
        /// </summary>
        private const string UndefinedExceptionType = "?UndefinedException?";

        /// <summary>
        ///   Il tipo del logger, usato da NLog per capire meglio quale funzione abbia veramente
        ///   invocato la funzione di log. Tale operazione viene fatta navigando lo stack di chiamate.
        /// </summary>
        private static readonly Type DeclaringType = typeof(CaravanLogger);

        /// <summary>
        ///   Il logger di NLog, su cui vengono dirottati i messaggi personalizzati.
        /// </summary>
        private readonly NLog.Logger _logger;

        /// <summary>
        ///   Costruisce il log personalizzato a partire da un reale logger di NLog.
        /// </summary>
        /// <param name="logger">Il logger di NLog, su cui vengono dirottati i messaggi personalizzati.</param>
        public CaravanLogger(NLog.Logger logger)
        {
            RaiseArgumentNullException.IfIsNull(logger, nameof(logger));
            _logger = logger;
        }

        protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
        {
            var logEventInfo = new LogEventInfo(logLevel, _logger.Name, "{0}", new[] { message }, exception);
            _logger.Log(DeclaringType, logEventInfo);
        }

        private void WriteLogMessage(LogLevel logLevel, LogMessage message)
        {
            var logEventInfo = new LogEventInfo(logLevel, _logger.Name, "{0}", new[] { message }, null);
            _logger.Log(DeclaringType, logEventInfo);
        }

        #region ILog members

        /// <summary>
        ///   Gets a value indicating whether this instance is trace enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is trace enabled; otherwise, <c>false</c>.</value>
        public override bool IsTraceEnabled => _logger.IsTraceEnabled;

        /// <summary>
        ///   Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.</value>
        public override bool IsDebugEnabled => _logger.IsDebugEnabled;

        /// <summary>
        ///   Gets a value indicating whether this instance is info enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is info enabled; otherwise, <c>false</c>.</value>
        public override bool IsInfoEnabled => _logger.IsInfoEnabled;

        /// <summary>
        ///   Gets a value indicating whether this instance is warn enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.</value>
        public override bool IsWarnEnabled => _logger.IsWarnEnabled;

        /// <summary>
        ///   Gets a value indicating whether this instance is error enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is error enabled; otherwise, <c>false</c>.</value>
        public override bool IsErrorEnabled => _logger.IsErrorEnabled;

        /// <summary>
        ///   Gets a value indicating whether this instance is fatal enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.</value>
        public override bool IsFatalEnabled => _logger.IsFatalEnabled;

        /// <summary>
        ///   Returns the global context for variables.
        /// </summary>
        public override IVariablesContext GlobalVariablesContext => CaravanVariablesContext.GlobalVariables;

        /// <summary>
        ///   Returns the thread-specific context for variables
        /// </summary>
        public override IVariablesContext ThreadVariablesContext => CaravanVariablesContext.ThreadVariables;

        #endregion ILog members

        #region ICaravanLog members

        #region Standard members

        public bool Trace(LogMessage logMessage)
        {
            if (IsTraceEnabled)
            {
                WriteLogMessage(LogLevel.Trace, logMessage);
            }
            return DoNotFilterException;
        }

        public bool Trace(Func<LogMessage> logMessageCallback)
        {
            if (IsTraceEnabled)
            {
                WriteLogMessage(LogLevel.Trace, logMessageCallback?.Invoke());
            }
            return DoNotFilterException;
        }

        public bool Debug(LogMessage logMessage)
        {
            if (IsDebugEnabled)
            {
                WriteLogMessage(LogLevel.Debug, logMessage);
            }
            return DoNotFilterException;
        }

        public bool Debug(Func<LogMessage> logMessageCallback)
        {
            if (IsDebugEnabled)
            {
                WriteLogMessage(LogLevel.Debug, logMessageCallback?.Invoke());
            }
            return DoNotFilterException;
        }

        public bool Info(LogMessage logMessage)
        {
            if (IsInfoEnabled)
            {
                WriteLogMessage(LogLevel.Info, logMessage);
            }
            return DoNotFilterException;
        }

        public bool Info(Func<LogMessage> logMessageCallback)
        {
            if (IsInfoEnabled)
            {
                WriteLogMessage(LogLevel.Info, logMessageCallback?.Invoke());
            }
            return DoNotFilterException;
        }

        public bool Warn(LogMessage logMessage)
        {
            if (IsWarnEnabled)
            {
                WriteLogMessage(LogLevel.Warn, logMessage);
            }
            return DoNotFilterException;
        }

        public bool Warn(Func<LogMessage> logMessageCallback)
        {
            if (IsWarnEnabled)
            {
                WriteLogMessage(LogLevel.Warn, logMessageCallback?.Invoke());
            }
            return DoNotFilterException;
        }

        public bool Error(LogMessage logMessage)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessage);
            }
            return DoNotFilterException;
        }

        public bool Error(Func<LogMessage> logMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessageCallback?.Invoke());
            }
            return DoNotFilterException;
        }

        public bool Fatal(LogMessage logMessage)
        {
            if (IsFatalEnabled)
            {
                WriteLogMessage(LogLevel.Fatal, logMessage);
            }
            return DoNotFilterException;
        }

        public bool Fatal(Func<LogMessage> logMessageCallback)
        {
            if (IsFatalEnabled)
            {
                WriteLogMessage(LogLevel.Fatal, logMessageCallback?.Invoke());
            }
            return DoNotFilterException;
        }

        #endregion Standard members

        #region Catching

        public void Catching(LogMessage logMessage)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessage);
            }
        }

        public void Catching(Func<LogMessage> logMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessageCallback?.Invoke());
            }
        }

        public void Catching(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Catching(object message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, message, exception);
            }
        }

        public void Catching(Exception exception)
        {
            if (IsErrorEnabled)
            {
                var msg = $"Catching an exception of type '{exception?.GetType()?.FullName ?? UndefinedExceptionType}'";
                WriteInternal(LogLevel.Error, msg, exception);
            }
        }

        public void Catching(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void CatchingFormat(string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void CatchingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        #endregion Catching

        #region Throwing

        public void Throwing(LogMessage logMessage)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessage);
            }
        }

        public void Throwing(Func<LogMessage> logMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessageCallback?.Invoke());
            }
        }

        public void Throwing(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Throwing(object message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, message, exception);
            }
        }

        public void Throwing(Exception exception)
        {
            if (IsErrorEnabled)
            {
                var msg = $"Throwing an exception of type '{exception?.GetType()?.FullName ?? UndefinedExceptionType}'";
                WriteInternal(LogLevel.Error, msg, exception);
            }
        }

        public void Throwing(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void ThrowingFormat(string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void ThrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        #endregion Throwing

        #region Rethrowing

        public bool Rethrowing(LogMessage logMessage)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessage);
            }
            return DoNotFilterException;
        }

        public bool Rethrowing(Func<LogMessage> logMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteLogMessage(LogLevel.Error, logMessageCallback?.Invoke());
            }
            return DoNotFilterException;
        }

        public bool Rethrowing(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool Rethrowing(object message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, message, exception);
            }
            return DoNotFilterException;
        }

        public bool Rethrowing(Exception exception)
        {
            if (IsErrorEnabled)
            {
                var msg = $"Rethrowing an exception of type '{exception?.GetType()?.FullName ?? UndefinedExceptionType}'";
                WriteInternal(LogLevel.Error, msg, exception);
            }
            return DoNotFilterException;
        }

        public bool Rethrowing(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool RethrowingFormat(string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool RethrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        #endregion Rethrowing

        #endregion ICaravanLog members

        #region Custom LogEventInfo

        sealed class LogEventInfo : NLog.LogEventInfo
        {
            public LogEventInfo(LogLevel logLevel, string loggerName, LogEntry logEntry)
                : base(ToNLogLevel(logLevel), loggerName, logEntry.ShortMessage)
            {
                LogEntry = logEntry;
            }

            public LogEntry LogEntry { get; }

            private static NLog.LogLevel ToNLogLevel(LogLevel logLevel)
            {
                switch (logLevel)
                {
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
                        throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "Unknown log level");
                }
            }
        }

        #endregion Custom LogEventInfo
    }
}
