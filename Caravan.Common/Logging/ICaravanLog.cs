using System;
using System.Collections.Generic;
using Common.Logging;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.Common.Logging
{
    public interface ICaravanLog : ILog
    {
        void TraceArgs(object shortMessage, object longMessage = null, object context = null, IEnumerable<KeyValuePair<string, object>> args = null);

        void TraceArgs(Func<LogMessage> logMessageCallback);
    }
}