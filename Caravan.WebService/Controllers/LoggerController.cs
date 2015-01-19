using System.Collections.Generic;
using System.Web.Http;
using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;

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
       public SecUser GetUsers()
       {
          return new SecUser{FirstName = "sarav",Login = "sarav"};
       }
       //public IList<LogEntry> GetLogEntries()
       //{
       //   return Db.Logger.Logs();
       //}
    }
}
