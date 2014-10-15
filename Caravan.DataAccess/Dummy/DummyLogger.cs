using System.Collections.Generic;
using Finsa.Caravan;
using Finsa.Caravan.Collections;
using Finsa.Caravan.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.Dummy
{
   public sealed class DummyLogger : LoggerBase
   {
      protected override LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         return LogResult.Success;
      }

      protected override IEnumerable<LogEntry> Logs(string applicationName, LogType? logType)
      {
         return ReadOnlyList.Empty<LogEntry>();
      }

      protected override IList<LogSettings> LogSettings(string applicationName, LogType? logType)
      {
         return ReadOnlyList.Empty<LogSettings>();
      }
   }
}
