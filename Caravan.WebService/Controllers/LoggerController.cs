using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.DataAccess;
using System.Collections.Generic;
using System.Web.Http;

namespace Caravan.WebService.Controllers
{
   [RoutePrefix("logger")]
   public class LoggerController : ApiController
   {
      /// <summary>
      ///   Returns all log entries.
      /// </summary>
      /// <returns>All log entries.</returns>
      [Route("entries")]
      public IList<LogEntry> GetLogEntries()
      {
         return Db.Logger.Logs();
      }
   }
}