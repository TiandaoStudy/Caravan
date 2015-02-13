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
        [Route("{appName}/entries")]
        public IEnumerable<LogEntry> GetEntries(string appName)
        {
            return Db.Logger.Logs(appName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="l"></param>
        [Route("{appName}/entries")]
        public void PutLog(string appName, [FromBody] LogEntry l)
        {
            Db.Logger.Log<LoggerController>(l.Type, l.ShortMessage, l.LongMessage, l.Context, l.Arguments, appName, l.UserLogin);
        }
    }
}