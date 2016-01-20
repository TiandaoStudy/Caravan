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

using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Exceptions;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Clock;
using Finsa.CodeServices.Common;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Finsa.Caravan.WebApi.Filters;
using System.Web.Hosting;

namespace Finsa.Caravan.WebService.Controllers
{
    /// <summary>
    ///   Controller che si occupa della parte di logging.
    /// </summary>
    [RoutePrefix("logger"), AuthorizeForCaravan]
    public sealed class LoggerController : ApiController
    {
        static readonly IList<LogLevel> NoLogLevels = new LogLevel[0];

        readonly ILog _log;
        readonly IClock _clock;
        readonly ICaravanLogRepository _logRepository;

        /// <summary>
        ///   Inizializza il controller con l'istanza del log di Caravan.
        /// </summary>
        public LoggerController(ILog log, IClock clock, ICaravanLogRepository logRepository)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            RaiseArgumentNullException.IfIsNull(clock, nameof(clock));
            RaiseArgumentNullException.IfIsNull(logRepository, nameof(logRepository));
            _log = log;
            _clock = clock;
            _logRepository = logRepository;
        }

        /// <summary>
        ///   Writes a silly message into the log.
        /// </summary>
        [Route("ping")]
        public HttpResponseMessage GetPing()
        {
            try
            {
                _log.Info("Ping pong :)");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        ///   Completely cleans up the log repository.
        /// </summary>
        [Route("cleanup")]
        public void PostCleanUp()
        {
            HostingEnvironment.QueueBackgroundWorkItem(ct => _logRepository.CleanUpEntriesAsync());
        }

        /// <summary>
        ///   Writes a silly message into the log.
        /// </summary>
        [Route("{appName}/ping")]
        public async Task<HttpResponseMessage> GetPing(string appName)
        {
            appName = appName ?? CaravanCommonConfiguration.Instance.AppName;
            var result = await _logRepository.AddEntryAsync(appName, new LogEntry
            {
                Date = _clock.UtcNow,
                ShortMessage = "Ping pong :)"
            });
            if (result.Succeeded)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, result.Exception);
        }

        /// <summary>
        ///   Completely cleans up the log repository for given application.
        /// </summary>
        [Route("{appName}/cleanup")]
        public void PostCleanUp(string appName)
        {
            HostingEnvironment.QueueBackgroundWorkItem(ct => _logRepository.CleanUpEntriesAsync(appName));
        }

        /// <summary>
        ///   Returns all log entries.
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns>All log entries.</returns>
        [Route("{appName}/entries")]
        public IEnumerable<LogEntry> GetEntries(string appName, string logLevels = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _logRepository.QueryEntries(new LogEntryQuery
            {
                AppNames = new[] { appName },
                LogLevels = logLevels?.Split(',').Select(ll => (LogLevel)Enum.Parse(typeof(LogLevel), ll, true)).ToArray() ?? NoLogLevels,
                TruncateLongMessage = true,
                MaxTruncatedLongMessageLength = 30,
                FromDate = fromDate?.ToOption() ?? Option.None<DateTime>(),
                ToDate = toDate?.ToOption() ?? Option.None<DateTime>()
            });
        }

        /// <summary>
        ///   returns all logs of a specified logLevel
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logLevel">Type of log which can be "warn", "info" or "error"</param>
        /// <returns></returns>
        [Route("{appName}/entries/{logLevel:alpha}")]
        public IEnumerable<LogEntry> GetEntries(string appName, LogLevel logLevel, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _logRepository.QueryEntries(new LogEntryQuery
            {
                AppNames = new[] { appName },
                LogLevels = new[] { logLevel },
                TruncateLongMessage = true,
                MaxTruncatedLongMessageLength = 30,
                FromDate = fromDate?.ToOption() ?? Option.None<DateTime>(),
                ToDate = toDate?.ToOption() ?? Option.None<DateTime>()
            });
        }

        /// <summary>
        ///   returns a logs of with specified ID
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logId">ID of the entry</param>
        /// <returns></returns>
        [Route("{appName}/entries/{logId:long}")]
        public NegotiatedContentResult<LogEntry> GetEntry(string appName, long logId)
        {
            var result = _logRepository.GetEntry(appName, logId);
            return result.HasValue ? Content(HttpStatusCode.OK, result.Value) : Content<LogEntry>(HttpStatusCode.NotFound, null);
        }

        /// <summary>
        ///   Add a new log with the features specified in the body of the request
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logEntry">The log to add</param>
        [Route("{appName}/entries")]
        public async Task PostLog(string appName, [FromBody] LogEntry logEntry)
        {
            await _logRepository.AddEntryAsync(appName, logEntry);
        }

        /// <summary>
        ///   Add
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logLevel">Type of log which can be "warn", "info" or "error"</param>
        /// <param name="logEntry">The log to add</param>
        [Route("{appName}/entries/{logLevel}")]
        public async Task PostLog(string appName, LogLevel logLevel, [FromBody] LogEntry logEntry)
        {
            logEntry.LogLevel = logLevel;
            await _logRepository.AddEntryAsync(appName, logEntry);
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
                _logRepository.RemoveEntry(appName, logId);
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
        [Route("settings")]
        public IQueryable<LogSetting> GetSettings()
        {
            return GetSettings(null);
        }

        /// <summary>
        ///   Returns all settings of the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <returns></returns>
        [Route("{appName}/settings")]
        public IQueryable<LogSetting> GetSettings(string appName)
        {
            appName = appName ?? CaravanCommonConfiguration.Instance.AppName;
            return _logRepository.GetSettings(appName).AsQueryable();
        }

        /// <summary>
        ///   Returns all settings of a specified logLevel
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="logLevel">Type of log which can be "warn", "info" or "error"</param>
        /// <returns></returns>
        [Route("{appName}/settings/{logLevel}")]
        public LogSetting GetSettings(string appName, LogLevel logLevel)
        {
            var settings = _logRepository.GetSettings(appName, logLevel);
            if (settings == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return settings;
        }

        /// <summary>
        ///   Add a new setting of type = logLevel
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logLevel">Type of log which can be "warn", "info" or "error"</param>
        /// <param name="settings">The setting to add</param>
        [Route("{appName}/settings/{logLevel}")]
        public void PostSetting(string appName, LogLevel logLevel, [FromBody] LogSetting settings)
        {
            _logRepository.AddSetting(appName, logLevel, settings);
        }

        /// <summary>
        ///   Update the setting of a particular logLevel
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logLevel">Type of log which can be "warn", "info" or "error"</param>
        /// <param name="settings">The new data setting</param>
        [Route("{appName}/settings/{logLevel}")]
        public void PutSetting(string appName, LogLevel logLevel, [FromBody] LogSetting settings)
        {
            _logRepository.UpdateSetting(appName, logLevel, settings);
        }

        /// <summary>
        ///   Deletes setting of the logtype specified in the specified application
        /// </summary>
        /// <param name="appName">Application name</param>
        /// <param name="logLevel">Type of log which can be "warn", "info" or "error"</param>
        [Route("{appName}/settings/{logLevel}")]
        public HttpResponseMessage DeleteSetting(string appName, LogLevel logLevel)
        {
            try
            {
                _logRepository.RemoveSetting(appName, logLevel);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (LogSettingNotFoundException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, LogEntryNotFoundException.TheMessage);
            }
        }
    }
}
