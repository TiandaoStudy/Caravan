using Finsa.Caravan.RestService.Core;
using FLEX.DataAccess;

namespace Finsa.Caravan.RestService
{
   public sealed class LogsModule : CaravanModule
   {
      public LogsModule() : base("logs")
      {
         Get["/{applicationName}"] = p => DbLogger.Instance.RetrieveApplicationLogs(p.applicationName);
      }
   }
}