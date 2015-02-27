using System;
using System.Activities;
using System.Collections.Generic;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.DataAccess.Activities
{
    public abstract class LogActivityBase : CodeActivity<LogResult>
    {
        public InArgument<IEnumerable<KeyValuePair<string, string>>> Arguments { get; set; }

        public InArgument<string> Context { get; set; }

        /// <summary>
        ///   The exception that should be logged. It may not be specified.
        /// </summary>
        public InArgument<Exception> Exception { get; set; }

        public InArgument<LogType> LogType { get; set; }

        public InArgument<string> LongMessage { get; set; }

        public InArgument<string> ShortMessage { get; set; }

        public InArgument<string> UserLogin { get; set; }

        public InArgument<string> WorkflowName { get; set; }

        protected override LogResult Execute(CodeActivityContext ctx)
        {
            LogResult result;
            var exception = Exception.Get(ctx);
            if (exception == null)
            {
                result = Db.Logger.LogRaw(
                    LogType.Get(ctx),
                    Finsa.Caravan.Common.Properties.Settings.Default.ApplicationName,
                    UserLogin.Get(ctx),
                    WorkflowName.Get(ctx),
                    "LogActivity.Execute",
                    ShortMessage.Get(ctx),
                    LongMessage.Get(ctx),
                    Context.Get(ctx),
                    Arguments.Get(ctx)
                );
            }
            else
            {
                result = Db.Logger.LogRaw(
                    LogType.Get(ctx),
                    Finsa.Caravan.Common.Properties.Settings.Default.ApplicationName,
                    UserLogin.Get(ctx),
                    WorkflowName.Get(ctx),
                    "LogActivity.Execute",
                    exception,
                    Context.Get(ctx),
                    Arguments.Get(ctx)
                );
            }
            return result;
        }
    }
}