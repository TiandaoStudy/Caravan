using System;
using System.Activities;
using System.Collections.Generic;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.DataAccess.Activities
{
    public abstract class LogActivityBase : CodeActivity<LogResult>
    {
        private InArgument<IEnumerable<KeyValuePair<string, string>>> Arguments { get; set; }

        private InArgument<string> Context { get; set; }

        /// <summary>
        ///   The exception that should be logged. It may not be specified.
        /// </summary>
        private InArgument<Exception> Exception { get; set; }

        private InArgument<LogType> LogType { get; set; }

        private InArgument<string> LongMessage { get; set; }

        private InArgument<string> ShortMessage { get; set; }

        private InArgument<string> WorkflowName { get; set; }

        protected override LogResult Execute(CodeActivityContext ctx)
        {
            var exception = Exception.Get(ctx);
            if (exception == null)
            {
                return Db.Logger.LogRaw(
                    LogType.Get(ctx),
                    Common.Properties.Settings.Default.ApplicationName,
                    String.Empty,
                    WorkflowName.Get(ctx),
                    "Execute",
                    ShortMessage.Get(ctx),
                    LongMessage.Get(ctx),
                    Context.Get(ctx),
                    Arguments.Get(ctx)
                );
            }
            return Db.Logger.LogRaw(
                LogType.Get(ctx),
                Common.Properties.Settings.Default.ApplicationName,
                String.Empty,
                WorkflowName.Get(ctx),
                "Execute",
                exception,
                Context.Get(ctx),
                Arguments.Get(ctx)
            );
        }
    }
}