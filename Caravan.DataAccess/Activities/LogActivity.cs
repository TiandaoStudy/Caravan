using System;
using System.Activities;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.DataAccess.Activities
{
    public sealed class LogActivity : CodeActivity<LogResult>
    {
        /// <summary>
        ///   The exception that should be logged. It may not be specified.
        /// </summary>
        InArgument<Exception> Exception { get; set; } 

        protected override LogResult Execute(CodeActivityContext context)
        {
            var exception = Exception.Get(context);
            if (exception == null)
            {
                
            }
            else
            {
                Db.Logger.Log<LogActivity>(exception);
            }
        }
    }
}