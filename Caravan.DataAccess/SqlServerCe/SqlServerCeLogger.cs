using System.Collections.Generic;
using Finsa.Caravan;
using FLEX.Common.DataModel;
using FLEX.DataAccess.Core;

namespace FLEX.DataAccess.SqlServerCe
{
   public sealed class SqlServerCeLogger : LoggerBase
	{
	   public override IEnumerable<LogEntry> GetAllLogs()
	   {
	      throw new System.NotImplementedException();
	   }

	   public override IEnumerable<LogEntry> GetApplicationLogs(string applicationName)
	   {
	      throw new System.NotImplementedException();
	   }

	   public override IList<LogSettings> GetAllSettings(LogType logType)
	   {
	      throw new System.NotImplementedException();
	   }

	   public override LogSettings GetApplicationSettings(LogType logType, string applicationName)
	   {
	      throw new System.NotImplementedException();
	   }

	   protected override LogResult Log<TCodeUnit>(LogType type, string applicationName, string userName, string function, string shortMessage, string longMessage, string context, IEnumerable<GKeyValuePair<string, string>> args)
	   {
	      throw new System.NotImplementedException();
	   }
	}
}
