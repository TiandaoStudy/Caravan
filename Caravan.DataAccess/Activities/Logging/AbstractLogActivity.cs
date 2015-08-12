using System;
using System.Activities;
using System.Collections.Generic;
using Common.Logging;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.Common.Models.Logging;
using LL = Common.Logging.LogLevel;

namespace Finsa.Caravan.DataAccess.Activities.Logging
{
    public abstract class AbstractLogActivity<TLogActivity> : CodeActivity
        where TLogActivity : AbstractLogActivity<TLogActivity>
    {
        #region Arguments

        public InArgument<IEnumerable<KeyValuePair<string, string>>> Arguments { get; set; }

        public InArgument<string> Context { get; set; }

        /// <summary>
        ///   The exception that should be logged. It may not be specified.
        /// </summary>
        public InArgument<Exception> Exception { get; set; }

        [RequiredArgument]
        public InArgument<LogLevel> LogLevel { get; set; }

        public InArgument<string> LongMessage { get; set; }

        public InArgument<string> ShortMessage { get; set; }

        #endregion

        protected override void Execute(CodeActivityContext ctx)
        {
            var log = GetCaravanLog();
            var logLevel = LogLevel.Get(ctx);

            var isEnabled = false;
            switch (logLevel)
            {
                case LL.Trace:
                    isEnabled = log.IsTraceEnabled;
                    break;

                case LL.Debug:
                    isEnabled = log.IsDebugEnabled;
                    break;

                case LL.Info:
                    isEnabled = log.IsInfoEnabled;
                    break;

                case LL.Warn:
                    isEnabled = log.IsWarnEnabled;
                    break;

                case LL.Error:
                    isEnabled = log.IsErrorEnabled;
                    break;

                case LL.Fatal:
                    isEnabled = log.IsFatalEnabled;
                    break;
            }
            if (!isEnabled)
            {
                return;
            }

            // Preparazione del messaggio di log, condizionato dalla presenza di un'eccezione.
            LogMessage logMessage;
            var exception = Exception.Get(ctx);
            if (exception == null)
            {
                logMessage = new LogMessage
                {
                    ShortMessage = ShortMessage.Get(ctx),
                    LongMessage = LongMessage.Get(ctx),
                    Context = Context.Get(ctx),
                    Arguments = Arguments.Get(ctx)
                };
            }
            else
            {
                logMessage = new LogMessage
                {
                    Exception = exception,
                    Context = Context.Get(ctx),
                    Arguments = Arguments.Get(ctx)
                };
            }

            switch (logLevel)
            {
                case LL.Trace:
                    log.TraceArgs(() => logMessage);
                    break;

                case LL.Debug:
                    log.DebugArgs(() => logMessage);
                    break;

                case LL.Info:
                    log.InfoArgs(() => logMessage);
                    break;

                case LL.Warn:
                    log.WarnArgs(() => logMessage);
                    break;

                case LL.Error:
                    log.ErrorArgs(() => logMessage);
                    break;

                case LL.Fatal:
                    log.FatalArgs(() => logMessage);
                    break;
            }
        }

        protected virtual ICaravanLog GetCaravanLog()
        {
            return LogManager.GetLogger<TLogActivity>() as ICaravanLog;
        }
    }
}
