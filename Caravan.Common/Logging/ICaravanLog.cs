using System;
using System.Collections.Generic;
using Common.Logging;
using Finsa.Caravan.Common.Models.Logging;

namespace Finsa.Caravan.Common.Logging
{
    public interface ICaravanLog : ILog
    {
        void TraceArgs(string shortMessage, string longMessage = null, string context = null, IEnumerable<KeyValuePair<string, string>> args = null);

        void TraceArgs(Func<LogMessage> logMessageCallback);
    }
}