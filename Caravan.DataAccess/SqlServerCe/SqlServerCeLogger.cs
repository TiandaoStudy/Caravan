using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.SqlServerCe
{
   public sealed class SqlServerCeLogger : LoggerBase
	{
      public override LogResult Log(LogType type, string applicationName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
      {
         throw new System.NotImplementedException();
      }

      protected override IEnumerable<LogEntry> GetLogs(string applicationName, LogType? logType)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<LogSettings> GetLogSettings(string applicationName, LogType? logType)
      {
         throw new System.NotImplementedException();
      }
	}
}
