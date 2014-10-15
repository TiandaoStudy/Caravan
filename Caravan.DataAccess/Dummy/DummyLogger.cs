using System.Collections.Generic;
using Finsa.Caravan;
using Finsa.Caravan.Collections;
using Finsa.Caravan.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.Dummy
{
   public sealed class DummyLogger : LoggerBase
   {
      public override LogResult Log(LogType type, string applicationName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         throw new System.NotImplementedException();
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
