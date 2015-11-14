// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Common.Logging;
using Common.Logging.Factory;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Serialization;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Finsa.Caravan.Common.Logging
{
    /// <summary>
    ///   Logger personalizzato per gestire messaggi più complessi e per sfruttare al meglio le
    ///   funzionalità introdotte in C#6.
    /// </summary>
    public sealed class CaravanLogger : AbstractLogger, ICaravanLog
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
        ///   Regex usata per togliere le righe vuote dai singoli componenti del messaggio di log.
        /// </summary>
        private static readonly Regex RemoveBlankRows = new Regex(@"^[ \t]*((\r|\n)\n?)", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        ///   Regex usata per togliere gli spazi in fondo alle righe dei singoli componenti del
        ///   messaggio di log.
        /// </summary>
        private static readonly Regex RemoveLastBlanks = new Regex(@"[ \t]+((\r|\n)\n?)", RegexOptions.Compiled | RegexOptions.Singleline);

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

        #region Logging

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
        {
            var logMessage = new LogMessage
            {
                ShortMessage = message.SafeToString(),
                Exception = exception
            };
            var logEventInfo = new LogEventInfo(logLevel, _logger.Name, ToLogEntry(logLevel, logMessage));
            _logger.Log(DeclaringType, logEventInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteLogMessage(LogLevel logLevel, LogMessage logMessage)
        {
            var logEventInfo = new LogEventInfo(logLevel, _logger.Name, ToLogEntry(logLevel, logMessage));
            _logger.Log(DeclaringType, logEventInfo);
        }

        /// <summary>
        ///   Trasforma il messaggio di log in una vera voce di log, pronta per essere gestita da <see cref="ICaravanLogRepository"/>.
        /// 
        ///   Per farlo, formatta ogni singolo pezzo del messaggio, in modo che il risultato sia
        ///   altamente leggibile.
        /// </summary>
        /// <param name="logLevel">Il livello della voce di log.</param>
        /// <param name="logMessage">Il messaggio di log da trasformare.</param>
        internal static LogEntry ToLogEntry(LogLevel logLevel, LogMessage logMessage)
        {
            // Eseguo una pulizia dell'oggetto .NET, in modo da avere un risultato molto pulito. Per
            // ShortMessage e Context mi limito a una TRIM (è difficile che siano su più righe),
            // mentre per LongMessage e per gli Arguments devo applicare pulizie più efficaci.

            var tmpLongMessage = logMessage.LongMessage;
            if (tmpLongMessage != null)
            {
                tmpLongMessage = RemoveBlankRows.Replace(tmpLongMessage, string.Empty);
                tmpLongMessage = RemoveLastBlanks.Replace(tmpLongMessage, Environment.NewLine);
            }

            var argumentsHelper = CaravanVariablesContext.GlobalVariables
                .Union(CaravanVariablesContext.ThreadVariables)
                .Union(logMessage.Arguments ?? GTuple0<KeyValuePair<string, object>>.Instance)
                .ToGTuple();

            var tmpArguments = new KeyValuePair<string, string>[argumentsHelper.Count];

            for (var i = 0; i < argumentsHelper.Count; ++i)
            {
                var kv = argumentsHelper[i];
                if (kv.Value == null)
                {
                    tmpArguments[i] = KeyValuePair.Create(kv.Key, (string)null);
                    continue;
                }
                var tmpValue = RemoveBlankRows.Replace(kv.Value.SafeToString(), string.Empty);
                tmpValue = RemoveLastBlanks.Replace(tmpValue, Environment.NewLine);
                tmpArguments[i] = KeyValuePair.Create(kv.Key, tmpValue);
            }

            return new LogEntry
            {
                LogLevel = logLevel,
                AppName = CaravanCommonConfiguration.Instance.AppName,
                Date = CaravanServiceProvider.Clock.UtcNow,
                ShortMessage = logMessage.ShortMessage?.Trim(),
                LongMessage = tmpLongMessage,
                Context = logMessage.Context?.Trim(),
                Arguments = tmpArguments
            };
        }

        #endregion Logging

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

        #region Trace

        public override void Trace(object message)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, message, null);
            }
        }

        public override void Trace(object message, Exception exception)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, message, exception);
            }
        }

        public override void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
            }
        }

        public override void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
        }

        public override void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
            }
        }

        public override void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public override void TraceFormat(string format, params object[] args)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), null);
            }
        }

        public override void TraceFormat(string format, Exception exception, params object[] args)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public override void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), null);
            }
        }

        public override void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsTraceEnabled)
            {
                WriteInternal(LogLevel.Trace, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
        }

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

        #endregion Trace

        #region Debug

        public override void Debug(object message)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, message, null);
            }
        }

        public override void Debug(object message, Exception exception)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, message, exception);
            }
        }

        public override void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
            }
        }

        public override void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
        }

        public override void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
            }
        }

        public override void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public override void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), null);
            }
        }

        public override void DebugFormat(string format, Exception exception, params object[] args)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public override void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), null);
            }
        }

        public override void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsDebugEnabled)
            {
                WriteInternal(LogLevel.Debug, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
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

        #endregion Debug

        #region Info

        public override void Info(object message)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, message, null);
            }
        }

        public override void Info(object message, Exception exception)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, message, exception);
            }
        }

        public override void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
            }
        }

        public override void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
        }

        public override void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
            }
        }

        public override void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public override void InfoFormat(string format, params object[] args)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(null, format, args), null);
            }
        }

        public override void InfoFormat(string format, Exception exception, params object[] args)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public override void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), null);
            }
        }

        public override void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsInfoEnabled)
            {
                WriteInternal(LogLevel.Info, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
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

        #endregion Info

        #region Warn

        public override void Warn(object message)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, message, null);
            }
        }

        public override void Warn(object message, Exception exception)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, message, exception);
            }
        }

        public override void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
            }
        }

        public override void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
        }

        public override void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
            }
        }

        public override void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public override void WarnFormat(string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), null);
            }
        }

        public override void WarnFormat(string format, Exception exception, params object[] args)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public override void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), null);
            }
        }

        public override void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsWarnEnabled)
            {
                WriteInternal(LogLevel.Warn, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
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

        #endregion Warn

        #region Error

        public override void Error(object message)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, message, null);
            }
        }

        public override void Error(object message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, message, exception);
            }
        }

        public override void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
            }
        }

        public override void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
        }

        public override void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
            }
        }

        public override void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public override void ErrorFormat(string format, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(null, format, args), null);
            }
        }

        public override void ErrorFormat(string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public override void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), null);
            }
        }

        public override void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
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

        #endregion Error

        #region Fatal

        public override void Fatal(object message)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, message, null);
            }
        }

        public override void Fatal(object message, Exception exception)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, message, exception);
            }
        }

        public override void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatMessageCallback), null);
            }
        }

        public override void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
        }

        public override void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), null);
            }
        }

        public override void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public override void FatalFormat(string format, params object[] args)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), null);
            }
        }

        public override void FatalFormat(string format, Exception exception, params object[] args)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public override void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), null);
            }
        }

        public override void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsFatalEnabled)
            {
                WriteInternal(LogLevel.Fatal, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
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

        #endregion Fatal

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
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
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
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public void CatchingFormat(string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public void CatchingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
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
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
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
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
        }

        public void ThrowingFormat(string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(null, format, args), exception);
            }
        }

        public void ThrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
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
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatMessageCallback), exception);
            }
            return DoNotFilterException;
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
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new FormatMessageCallbackFormattedMessage(formatProvider, formatMessageCallback), exception);
            }
            return DoNotFilterException;
        }

        public bool RethrowingFormat(string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(null, format, args), exception);
            }
            return DoNotFilterException;
        }

        public bool RethrowingFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            if (IsErrorEnabled)
            {
                WriteInternal(LogLevel.Error, new StringFormatFormattedMessage(formatProvider, format, args), exception);
            }
            return DoNotFilterException;
        }

        #endregion Rethrowing

        #endregion ICaravanLog members

        #region Custom LogEventInfo

        internal sealed class LogEventInfo : NLog.LogEventInfo
        {
            public LogEventInfo(LogLevel logLevel, string loggerName, LogEntry logEntry)
                : base(ToNLogLevel(logLevel), loggerName, ToFormattedMessage(logEntry))
            {
                LogEntry = logEntry;
            }

            public LogEntry LogEntry { get; }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static string ToFormattedMessage(LogEntry logEntry)
            {
                // Generazione della serializzazione YAML.
                var yaml = logEntry.ToYamlString(LogMessage.ReadableYamlSettings);
                yaml = RemoveBlankRows.Replace(yaml, string.Empty);

                // Aggiungo NewLine così che nei file di testo parta da una riga sotto, dato che il
                // messaggio YAML è sicuramente molto lungo.
                return Environment.NewLine + yaml;
            }
        }

        #endregion Custom LogEventInfo
    }
}
