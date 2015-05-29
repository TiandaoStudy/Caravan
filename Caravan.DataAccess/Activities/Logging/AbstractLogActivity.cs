using Common.Logging;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Models.Logging;
using System;
using System.Activities;
using System.Collections.Generic;
using Finsa.Caravan.Common;
using LL = Common.Logging.LogLevel;

namespace Finsa.Caravan.DataAccess.Activities.Logging
{
    public abstract class AbstractLogActivity : CodeActivity
    {
        protected abstract ICaravanLog GetCaravanLog();

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

        public InArgument<string> UserLogin { get; set; }

        [RequiredArgument]
        public InArgument<string> WorkflowName { get; set; }

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

            var exception = Exception.Get(ctx);
            LogMessage logMessage;
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
                    ShortMessage = ShortMessage.Get(ctx),
                    LongMessage = LongMessage.Get(ctx),
                    Context = Context.Get(ctx),
                    Arguments = Arguments.Get(ctx)
                };

                CaravanDataSource.Logger.LogRaw(
                    ,
                    CommonConfiguration.Instance.AppName,
                    UserLogin.Get(ctx),
                    WorkflowName.Get(ctx),
                    "LogActivity.Execute",
                    exception,
                    Context.Get(ctx),
                    Arguments.Get(ctx)
                );
            }
            switch (logLevel)
            {
                case LL.Trace:
                    log.TraceArgs(() => logMessage);
                    break;
                case LL.Debug:
                    log.DebugArgs(() => logMessage);
                    break;
            }
        }
    }
}