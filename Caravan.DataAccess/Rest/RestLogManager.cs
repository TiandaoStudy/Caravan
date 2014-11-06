using System;
using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Logging;

namespace Finsa.Caravan.DataAccess.Rest
{
   public sealed class RestLogManager : LogManagerBase
   {
      public override LogResult Log(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<CKeyValuePair<string, string>> args)
      {
         throw new NotImplementedException();
      }

      protected override IList<LogEntry> GetLogs(string appName, LogType? logType)
      {
         throw new NotImplementedException();
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         throw new NotImplementedException();
      }
   }
}
