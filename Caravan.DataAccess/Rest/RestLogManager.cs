using System;
using System.Collections.Generic;
using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.DataAccess.Core;

namespace Finsa.Caravan.DataAccess.Rest
{
   public sealed class RestLogManager : LogManagerBase
   {
      public override LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
      {
         throw new NotImplementedException();
      }

      protected override IList<LogEntry> GetLogEntries(string appName, LogType? logType)
      {
         throw new NotImplementedException();
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         throw new NotImplementedException();
      }

      protected override bool DoAddSettings(string appName, LogType logType, LogSettings settings)
      {
         throw new NotImplementedException();
      }

      protected override bool DoUpdateSettings(string appName, LogType logType, LogSettings settings)
      {
         throw new NotImplementedException();
      }
   }
}
