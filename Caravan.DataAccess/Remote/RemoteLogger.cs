using System;
using System.Collections.Generic;
using Finsa.Caravan;
using Finsa.Caravan.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.Remote
{
   public sealed class RemoteLogger : LoggerBase
   {
      protected override LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         throw new NotImplementedException();
      }

      protected override IEnumerable<LogEntry> Logs(string applicationName, LogType? logType)
      {
         throw new NotImplementedException();
      }

      protected override IList<LogSettings> LogSettings(string applicationName, LogType? logType)
      {
         throw new NotImplementedException();
      }
   }
}
