using Finsa.Caravan.Common.Logging.Models;
using System;
using System.Activities;
using System.Collections.Generic;
using LL = Common.Logging.LogLevel;

namespace Finsa.Caravan.Common.Logging.Activities
{
    public abstract class AbstractLogActivity<TLogActivity> : CodeActivity
        where TLogActivity : AbstractLogActivity<TLogActivity>
    {
        protected ICaravanLog Log { get; } = CaravanServiceProvider.FetchLog<TLogActivity>();

        #region Arguments

        public InArgument<IList<KeyValuePair<string, object>>> Arguments { get; set; }

        public InArgument<string> Context { get; set; }

        /// <summary>
        ///   The exception that should be logged. It may not be specified.
        /// </summary>
        public InArgument<Exception> Exception { get; set; }

        [RequiredArgument]
        public InArgument<LL> LogLevel { get; set; }

        public InArgument<string> LongMessage { get; set; }

        public InArgument<string> ShortMessage { get; set; }

        #endregion Arguments

        protected override void Execute(CodeActivityContext ctx)
        {
            var logLevel = LogLevel.Get(ctx);

            var isEnabled = false;
            switch (logLevel)
            {
                case LL.Trace:
                    isEnabled = Log.IsTraceEnabled;
                    break;

                case LL.Debug:
                    isEnabled = Log.IsDebugEnabled;
                    break;

                case LL.Info:
                    isEnabled = Log.IsInfoEnabled;
                    break;

                case LL.Warn:
                    isEnabled = Log.IsWarnEnabled;
                    break;

                case LL.Error:
                    isEnabled = Log.IsErrorEnabled;
                    break;

                case LL.Fatal:
                    isEnabled = Log.IsFatalEnabled;
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
                    Log.Trace(logMessage);
                    break;

                case LL.Debug:
                    Log.Debug(logMessage);
                    break;

                case LL.Info:
                    Log.Info(logMessage);
                    break;

                case LL.Warn:
                    Log.Warn(logMessage);
                    break;

                case LL.Error:
                    Log.Error(logMessage);
                    break;

                case LL.Fatal:
                    Log.Fatal(logMessage);
                    break;
            }
        }
    }
}
