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

        [Route("{appName}/entries/{logType}")]
        public IEnumerable<LogEntry> GetEntries(string appName, LogType logType)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="logType"></param>
        /// <param name="l"></param>
        [Route("{appName}/entries/{logType}")]
        public void PutLog(string appName, LogType logType, [FromBody] LogEntry l)
        {
            Db.Logger.Log<LoggerController>(l.Type, l.ShortMessage, l.LongMessage, l.Context, l.Arguments, appName, l.UserLogin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        [Route("{appName}/settings")]
        public IEnumerable<LogSettings> GetSettings(string appName)
        {
            return Db.Logger.LogSettings(appName);
        }
        
        /// <summary>
        /// Returns all settings of a specified logType
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        [Route("{appName}/settings/{logType}")]
        public IEnumerable<LogSettings> GetSettings(LogType logType)
        {
            return Db.Logger.LogSettings(logType);
        }

        /// <summary>
        /// Add a new setting 
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="logType"></param>
        /// <param name="settings"></param>
        [Route("{appName}/settings/{logType}")]
        public void PutSetting(string appName, LogType logType, [FromBody] LogSettings settings)
        {
            Db.Logger.AddSettings(appName,logType,settings);
        }

        /// <summary>
        /// Update the setting of a particular logType
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="logType"></param>
        /// <param name="settings"></param>
        [Route("{appName}/settings/{logType}")]
        public void PostSetting(string appName, LogType logType, [FromBody] LogSettings settings)
        {
            Db.Logger.UpdateSettings(appName,logType,settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="logType"></param>
        /// <param name="settings"></param>

        [Route("{appName}/settings/{logType}")]
        public void DeleteSetting(string appName, LogType logType, [FromBody] LogSettings settings)
        {
            //Db.Logger.DeleteSettings(appName, logType, settings);
        }




    }
}