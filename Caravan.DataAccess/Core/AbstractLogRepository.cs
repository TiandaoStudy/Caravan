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
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Exceptions;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.CodeServices.Common;
using PommaLabs.Thrower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Core
{
    internal abstract class AbstractLogRepository<TLog> : ICaravanLogRepository where TLog : AbstractLogRepository<TLog>
    {
        protected AbstractLogRepository(ICaravanLog log)
        {
            RaiseArgumentNullException.IfIsNull(log, nameof(log));
            Log = log;
        }

        protected ICaravanLog Log { get; }

        public abstract void Dispose();

        protected abstract Task AddEntryAsyncInternal(string appName, LogEntry logEntry);

        protected abstract Task AddEntriesAsyncInternal(string appName, IEnumerable<LogEntry> logEntries);

        protected abstract Task CleanUpEntriesAsyncInternal(string appName);

        protected abstract IList<LogEntry> GetEntriesInternal(string appName, LogLevel? logLevel);

        protected abstract IList<LogEntry> QueryEntriesInternal(LogEntryQuery logEntryQuery);

        protected abstract Option<LogEntry> GetEntryInternal(string appName, long logId);

        protected abstract IList<LogSetting> GetSettingsInternal(string appName, LogLevel? logLevel);

        protected abstract bool DoRemoveEntry(string appName, int logId);

        protected abstract bool DoAddSetting(string appName, LogLevel logLevel, LogSetting setting);

        protected abstract bool DoUpdateSetting(string appName, LogLevel logLevel, LogSetting setting);

        protected abstract bool DoRemoveSetting(string appName, LogLevel logLevel);

        #region ICaravanLogRepository members

        public async Task<LogResult> AddEntryAsync(string appName, LogEntry logEntry)
        {
            try
            {
                RaiseArgumentNullException.IfIsNull(logEntry, nameof(logEntry));
                await AddEntryAsyncInternal(appName.ToLowerInvariant(), logEntry);
                return LogResult.Success;
            }
            catch (Exception ex)
            {
                // Non loggo le eccezioni, visto che ho un problema nel logger...
                return LogResult.Failure(ex);
            }
        }

        public async Task<LogResult> AddEntriesAsync(string appName, IEnumerable<LogEntry> logEntries)
        {
            try
            {
                RaiseArgumentNullException.IfIsNull(logEntries, nameof(logEntries));
                RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), nameof(appName));
                await AddEntriesAsyncInternal(appName.ToLowerInvariant(), logEntries);
                return LogResult.Success;
            }
            catch (Exception ex)
            {
                // Non loggo le eccezioni, visto che ho un problema nel logger...
                return LogResult.Failure(ex);
            }
        }

        public async Task CleanUpEntriesAsync()
        {
            const string logCtx = "Cleaning up log entries";
            try
            {
                await CleanUpEntriesAsyncInternal(null);
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public async Task CleanUpEntriesAsync(string appName)
        {
            const string logCtx = "Cleaning up log entries";
            try
            {
                RaiseArgumentException.If(string.IsNullOrWhiteSpace(appName), nameof(appName));
                await CleanUpEntriesAsyncInternal(appName.ToLowerInvariant());
            }
            catch (Exception ex) when (Log.Rethrowing(new LogMessage { Context = logCtx, Exception = ex }))
            {
                // Lascio emergere l'eccezione...
            }
        }

        public IList<LogEntry> GetEntries()
        {
            return GetEntriesInternal(null, null);
        }

        public IList<LogEntry> GetEntries(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetEntriesInternal(appName.ToLower(), null);
        }

        public IList<LogEntry> GetEntries(LogLevel logLevel)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetEntriesInternal(null, logLevel);
        }

        public IList<LogEntry> GetEntries(string appName, LogLevel logLevel)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetEntriesInternal(appName.ToLowerInvariant(), logLevel);
        }

        public IList<LogEntry> QueryEntries(LogEntryQuery logEntryQuery)
        {
            Raise<ArgumentNullException>.If(logEntryQuery == null);

            // Metto in lower i nomi delle applicazioni, se ce ne sono.
            var appNames = logEntryQuery.AppNames;
            if (appNames != null && appNames.Count > 0)
            {
                logEntryQuery.AppNames = appNames.Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => n.ToLowerInvariant()).ToArray();
            }

            return QueryEntriesInternal(logEntryQuery);
        }

        public Option<LogEntry> GetEntry(string appName, long logId)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.If(logId < 0);
            return GetEntryInternal(appName.ToLowerInvariant(), logId);
        }

        public void RemoveEntry(string appName, int id)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentNullException>.IfIsNull(id);
            if (!DoRemoveEntry(appName, id))
            {
                throw new LogEntryNotFoundException();
            }
        }

        public IList<LogSetting> GetSettings()
        {
            return GetSettingsInternal(null, null);
        }

        public IList<LogSetting> GetSettings(string appName)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            return GetSettingsInternal(appName.ToLower(), null);
        }

        public IList<LogSetting> GetSettings(LogLevel logLevel)
        {
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetSettingsInternal(null, logLevel);
        }

        public LogSetting GetSettings(string appName, LogLevel logLevel)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            return GetSettingsInternal(appName.ToLower(), logLevel).FirstOrDefault();
        }

        public void AddSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            Raise<ArgumentNullException>.IfIsNull(setting);
            Raise<ArgumentOutOfRangeException>.If(setting.Days < 1 || setting.MaxEntries < 1);
            if (!DoAddSetting(appName.ToLower(), logLevel, setting))
            {
                throw new LogSettingExistingException();
            }
        }

        public void UpdateSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            Raise<ArgumentNullException>.IfIsNull(setting);
            Raise<ArgumentOutOfRangeException>.If(setting.Days < 1 || setting.MaxEntries < 1);
            if (!DoUpdateSetting(appName.ToLower(), logLevel, setting))
            {
                throw new LogSettingNotFoundException();
            }
        }

        public void RemoveSetting(string appName, LogLevel logLevel)
        {
            Raise<ArgumentException>.IfIsEmpty(appName);
            Raise<ArgumentException>.IfNot(Enum.IsDefined(typeof(LogLevel), logLevel));
            if (!DoRemoveSetting(appName, logLevel))
            {
                throw new LogSettingNotFoundException();
            }
        }

        #endregion ICaravanLogRepository members
    }
}
