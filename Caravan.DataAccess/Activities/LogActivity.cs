using Common.Logging;
using System;
using System.Activities;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.DataAccess.Activities
{
    public sealed class LogActivity : CodeActivity<LogResult>
    {
        protected override LogResult Execute(CodeActivityContext context)
        {
            LogManager.GetLogger<LogActivity>();

            throw new NotImplementedException();
        }
    }
}