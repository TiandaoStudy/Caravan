using System.Collections.Generic;
using Finsa.Caravan.Collections;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Logging;

namespace Finsa.Caravan.DataAccess.Dummy
{
   public sealed class DummyLogManager : LogManagerBase
   {
      public override LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<CKeyValuePair<string, string>> args)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<LogEntry> GetLogs(string appName, LogType? logType)
      {
         return ReadOnlyList.Empty<LogEntry>();
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         return ReadOnlyList.Empty<LogSettings>();
      }

      protected override bool DoAddSettings(string appName, LogType logType, LogSettings settings)
      {
         throw new System.NotImplementedException();
      }

      protected override bool DoUpdateSettings(string appName, LogType logType, LogSettings settings)
      {
         throw new System.NotImplementedException();
      }
   }
}
