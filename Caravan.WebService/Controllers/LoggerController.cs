// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Logging.Exceptions;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.WebApi.ActionFilters.Logging;
using LinqToQuerystring.WebApi;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller che si occupa della parte di logging.
    /// </summary>
    [RoutePrefix("logger")]
    [LogRequest, LogResponse]
    public sealed class LoggerController : ApiController
    {
        /// <summary>
        ///   Writes a silly message into the log.
        /// </summary>
        [Route("ping")]
        public HttpResponseMessage GetPing()
        {
            return GetPing(null);
        }

        /// <summary>
        ///   Writes a silly message into the log.
        /// </summary>
        [Route("{appName}/ping")]
        public HttpResponseMessage GetPing(string appName)
        {
            appName = appName ?? Common.Properties.Settings.Default.ApplicationName;
            var result = Db.Logger.LogInfo<LoggerController>(new LogEntry
            {
                AppName = appName,
                ShortMessage = "Ping pong :)"
            });
            if (result.Succeeded)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, result.Exception);
        }

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
        [Route("{appName}/entries/{logType:alpha}"), LinqToQueryable]
        public IQueryable<LogEntry> GetEntries(string appName, LogType logType)
        {
            return Db.Logger.Entries(appName, logType).AsQueryable();
        }

        /// <summary>
        ///   returns a logs of with specified ID
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logId">ID of the entry</param>
        /// <returns></returns>
        [Route("{appName}/entries/{logId:long}")]
        public LogEntry GetEntry(string appName, long logId)
        {
            return Db.Logger.Entries(appName).FirstOrDefault(e => e.Id == logId);
        }

        /// <summary>
        ///   Add a new log with the features specified in the body of the request
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="log">The log to add</param>
        [Route("{appName}/entries")]
        public void PostLog(string appName, [FromBody] LogEntry log)
        {
            Db.Logger.LogRaw(log.LogType, appName, log.UserLogin, log.CodeUnit, log.Function, log.ShortMessage, log.LongMessage, log.Context, log.Arguments);
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
        /// <param name="logId">The id of the log to delete which can be "warn", "info" or "error"</param>
        [Route("{appName}/entries/{id}")]
        public HttpResponseMessage DeleteLog(string appName, int logId)
        {
            try
            {
                Db.Logger.RemoveEntry(appName, logId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (LogEntryNotFoundException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, LogEntryNotFoundException.TheMessage);
            }
        }

        /// <summary>
        ///   Returns all settings of the current application
        /// </summary>
        /// <returns></returns>
        [Route("settings"), LinqToQueryable]
        public IQueryable<LogSetting> GetSettings()
        {
            return GetSettings(null);
        }

        /// <summary>
        ///   Returns all settings of the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns></returns>
        [Route("{appName}/settings"), LinqToQueryable]
        public IQueryable<LogSetting> GetSettings(string appName)
        {
            appName = appName ?? Common.Properties.Settings.Default.ApplicationName;
            return Db.Logger.Settings(appName).AsQueryable();
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
            var settings = Db.Logger.Settings(appName, logType);
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
            Db.Logger.AddSetting(appName, logType, settings);
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
            Db.Logger.UpdateSetting(appName, logType, settings);
        }

        /// <summary>
        ///   Deletes setting of the logtype specified in the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logType">Type of log which can be "warn", "info" or "error"</param>
        [Route("{appName}/settings/{logType}")]
        public HttpResponseMessage DeleteSetting(string appName, LogType logType)
        {
            try
            {
                Db.Logger.RemoveSetting(appName, logType);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (LogSettingNotFoundException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, LogEntryNotFoundException.TheMessage);
            }
        }
    }
}