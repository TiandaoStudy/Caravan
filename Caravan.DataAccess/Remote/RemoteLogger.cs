using System;
using System.Collections.Generic;
using Finsa.Caravan;
using FLEX.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.Remote
{
   public sealed class RemoteLogger : LoggerBase
   {
      public override IEnumerable<LogEntry> GetAllLogs()
      {
         throw new NotImplementedException();
      }

      public override IEnumerable<LogEntry> GetApplicationLogs(string applicationName)
      {
         throw new NotImplementedException();
      }

      public override IList<LogSettings> GetAllSettings(LogType logType)
      {
         throw new NotImplementedException();
      }

      public override LogSettings GetApplicationSettings(LogType logType, string applicationName)
      {
         throw new NotImplementedException();
      }

      protected override LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         throw new NotImplementedException();
      }
   }
}
