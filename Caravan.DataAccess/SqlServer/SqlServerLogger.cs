using System.Collections.Generic;
using Finsa.Caravan;
using Finsa.Caravan.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.SqlServer
{
	public sealed class SqlServerLogger : LoggerBase
	{
	   protected override LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
	   {
	      throw new System.NotImplementedException();
	   }

	   protected override IEnumerable<LogEntry> Logs(string applicationName, LogType? logType)
	   {
	      throw new System.NotImplementedException();
	   }

	   protected override IList<LogSettings> LogSettings(string applicationName, LogType? logType)
	   {
	      throw new System.NotImplementedException();
	   }
	}
}
