using Finsa.Caravan.Common.DataModel.Exceptions;
using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.DataAccess;
using LinqToQuerystring.WebApi;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Caravan.WebService.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [RoutePrefix("logger")]
    public sealed class LoggerController : ApiController
    {
        /// <summary>
        ///   Returns all log entries.
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns>All log entries.</returns>
        [Route("{appName}/entries"), LinqToQueryable]
        public IQueryable<LogEntry> GetEntries(string appName)
        {
            return Db.Logger.Entries(appName).AsQueryable();
        }

        /// <summary>
        ///   returns all logs of a specified logType
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logType">Type of log which can be "warn", "info" or "error"</param>
        /// <returns></returns>
        [Route("{appName}/entries/{logType}"), LinqToQueryable]
        public IQueryable<LogEntry> GetEntries(string appName, LogType logType)
        {
            return Db.Logger.Entries(appName, logType).AsQueryable();
        }

        /// <summary>
        ///   Add a new log with the features specified in the body of the request
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="log">The log to add</param>
        [Route("{appName}/entries")]
        public void PostLog(string appName, [FromBody] LogEntry log)
        {
            Db.Logger.LogRaw(log.Type, appName, log.UserLogin, log.CodeUnit, log.Function, log.ShortMessage, log.LongMessage, log.Context, log.Arguments);
        }

        /// <summary>
        ///   Add
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logType">Type of log which can be "warn", "info" or "error"</param>
        /// <param name="log">The log to add</param>
        [Route("{appName}/entries/{logType}"), LinqToQueryable]
        public void PostLog(string appName, LogType logType, [FromBody] LogEntry log)
        {
            Db.Logger.LogRaw(logType, appName, log.UserLogin, log.CodeUnit, log.Function, log.ShortMessage, log.LongMessage, log.Context, log.Arguments);
        }

        /// <summary>
        ///   Delete log with the specified id in the specified application
        /// </summary>
        /// <param name="appName">The application name</param>
        /// <param name="id">The id of the log to delete which can be "warn", "info" or "error"</param>
        [Route("{appName}/entries/{id}")]
        public HttpResponseMessage DeleteLog(string appName, int id)
        {
            var log = Db.Logger.DeleteLog(appName, id);
            if (log != "OK")
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, LogNotFoundException.TheMessage);
            return Request.CreateResponse(HttpStatusCode.OK, log);
        }

        /// <summary>
        ///   Returns all settings of the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns></returns>
        [Route("{appName}/settings"), LinqToQueryable]
        public IQueryable<LogSetting> GetSettings(string appName)
        {
            return Db.Logger.LogSettings(appName).AsQueryable();
        }

        /// <summary>
        ///   Returns all settings of a specified logType
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="logType">Type of log which can be "warn", "info" or "error"</param>
        /// <returns></returns>
        [Route("{appName}/settings/{logType}")]
        public LogSetting GetSettings(string appName, LogType logType)
        {
            var settings = Db.Logger.LogSettings(appName, logType);
            if (settings == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return settings;
        }

        /// <summary>
        ///   Add a new setting of type = logType
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logType">Type of log which can be "warn", "info" or "error"</param>
        /// <param name="settings">The setting to add</param>
        [Route("{appName}/settings/{logType}")]
        public void PostSetting(string appName, LogType logType, [FromBody] LogSetting settings)
        {
            Db.Logger.AddSettings(appName, logType, settings);
        }

        /// <summary>
        ///   Update the setting of a particular logType
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logType">Type of log which can be "warn", "info" or "error"</param>
        /// <param name="settings">The new data setting</param>
        [Route("{appName}/settings/{logType}")]
        public void PutSetting(string appName, LogType logType, [FromBody] LogSetting settings)
        {
            Db.Logger.UpdateSettings(appName, logType, settings);
        }

        /// <summary>
        ///   Deletes setting of the logtype specified in the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logType">Type of log which can be "warn", "info" or "error"</param>

        [Route("{appName}/settings/{logType}")]
        public HttpResponseMessage DeleteSetting(string appName, LogType logType)
        {
            var setting = Db.Logger.DeleteSettings(appName, logType);
            if (setting != "OK")
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, LogNotFoundException.TheMessage);
            return Request.CreateResponse(HttpStatusCode.OK, setting);
        }
    }
}