using Common.Logging;
using Common.Logging.Factory;
using Finsa.Caravan.Common.Logging.Models;
using PommaLabs.Thrower;
using System;

namespace Finsa.Caravan.Common.Logging
{
    sealed class CaravanLogger : AbstractLogger, ICaravanLog
    {
        private const bool DefaultReturnValue = false;
        private static readonly Type DeclaringType = typeof(CaravanLogger);
        private readonly NLog.Logger _logger;

        public CaravanLogger(NLog.Logger logger)
        {
            RaiseArgumentNullException.IfIsNull(logger, nameof(logger));
            _logger = logger;
        }

        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            var logEventInfo = new NLog.LogEventInfo(GetNLogLevel(level), _logger.Name, null, "{0}", new[] { message }, exception);
            _logger.Log(DeclaringType, logEventInfo);
        }

        private static NLog.LogLevel GetNLogLevel(LogLevel logLevel)
        {
            int logLevel1 = (int) logLevel;
            if (logLevel1 <= 16)
            {
                switch (logLevel1)
                {
                    case 0:
                        return NLog.LogLevel.Trace;
                    case 1:
                        return NLog.LogLevel.Trace;
                    case 2:
                        return NLog.LogLevel.Debug;
                    case 3:
                    case 5:
                    case 6:
                    case 7:
                        break;
                    case 4:
                        return NLog.LogLevel.Info;
                    case 8:
                        return NLog.LogLevel.Warn;
                    default:
                        if (logLevel1 == 16)
                            return NLog.LogLevel.Error;
                        break;
                }
            }
            else
            {
                if (logLevel1 == 32)
                    return (LogLevel) LogLevel.Fatal;
                if (logLevel1 == 64)
                    return (LogLevel) LogLevel.Off;
            }
            throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "Unknown log level");
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

        public bool Trace(LogMessage logMessage)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, logMessage, null);
            }
            return DefaultReturnValue;
        }

        public bool Trace(Func<LogMessage> logMessageCallback)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, logMessageCallback?.Invoke(), null);
            }
            return DefaultReturnValue;
        }

        public bool Debug(LogMessage logMessage)
        {
            throw new NotImplementedException();
        }

        public bool Debug(Func<LogMessage> logMessageCallback)
        {
            throw new NotImplementedException();
        }

        public bool Info(LogMessage logMessage)
        {
            throw new NotImplementedException();
        }

        public bool Info(Func<LogMessage> logMessageCallback)
        {
            throw new NotImplementedException();
        }

        public bool Warn(LogMessage logMessage)
        {
            throw new NotImplementedException();
        }

        public bool Warn(Func<LogMessage> logMessageCallback)
        {
            throw new NotImplementedException();
        }

        public bool Error(LogMessage logMessage)
        {
            throw new NotImplementedException();
        }

        public bool Error(Func<LogMessage> logMessageCallback)
        {
            throw new NotImplementedException();
        }

        public bool Fatal(LogMessage logMessage)
        {
            throw new NotImplementedException();
        }

        public bool Fatal(Func<LogMessage> logMessageCallback)
        {
            throw new NotImplementedException();
        }

        public bool Exception(LogMessage logMessage)
        {
            throw new NotImplementedException();
        }

        public bool Exception(Func<LogMessage> logMessageCallback)
        {
            throw new NotImplementedException();
        }

        public bool Exception(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool Exception(object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool Exception(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool ExceptionFormat(string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool ExceptionFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        #endregion ICaravanLog members
    }
}
