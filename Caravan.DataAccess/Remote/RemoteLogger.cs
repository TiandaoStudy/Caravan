using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Remote
{
   public sealed class RemoteLogger : LoggerBase
   {
      public override LogResult Log(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<LogEntry> GetLogs(string appName, LogType? logType)
      {
         throw new NotImplementedException();
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         throw new NotImplementedException();
      }
   }
}
