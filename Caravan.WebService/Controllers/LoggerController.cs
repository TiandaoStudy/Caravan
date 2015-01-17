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
        public int[] GetLogEntries()
        {
            return new []{1, 2};
        }
    }
}
