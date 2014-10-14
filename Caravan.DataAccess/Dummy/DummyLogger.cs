using System.Collections.Generic;
using Finsa.Caravan;
using Finsa.Caravan.Collections;
using FLEX.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.Dummy
{
   public sealed class DummyLogger : LoggerBase
   {
      public override IEnumerable<LogEntry> GetAllLogs()
      {
         return ReadOnlyList.Empty<LogEntry>();
      }

      public override IEnumerable<LogEntry> GetApplicationLogs(string applicationName)
      {
         return ReadOnlyList.Empty<LogEntry>();
      }

      public override IList<LogSettings> GetAllSettings(LogType logType)
      {
         return ReadOnlyList.Empty<LogSettings>();
      }

      public override LogSettings GetApplicationSettings(string applicationName, LogType logType)
      {
         return new LogSettings {TypeString = logType.ToString(), ApplicationName = applicationName, Enabled = false, Days = 1, MaxEntries = 1};
      }

      protected override LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         return LogResult.Successful;
      }
   }
}
