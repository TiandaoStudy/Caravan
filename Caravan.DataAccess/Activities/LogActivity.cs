using Common.Logging;
using Finsa.Caravan.Common.DataModel.Logging;
using System;
using System.Activities;

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