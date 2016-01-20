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

using AutoMapper;
using Common.Logging;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Sql.Logging.Entities;
using Finsa.Caravan.DataAccess.Sql.Security.Entities;
using Finsa.CodeServices.Clock;
using Finsa.CodeServices.Common;
using PommaLabs.Thrower;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.Sql
{
    internal sealed class SqlLogRepository : AbstractLogRepository<SqlLogRepository>
    {
        #region Constants

        private const int MaxArgumentCount = 10;

        #endregion Constants

        private readonly IDbContextFactory<SqlDbContext> _dbContextFactory;
        private readonly SqlDbContext _queryableDbContext;
        private readonly IClock _clock;
        private bool _disposed;

        public SqlLogRepository(ICaravanLog log, IDbContextFactory<SqlDbContext> dbContextFactory, IClock clock)
            : base(log)
        {
            RaiseArgumentNullException.IfIsNull(dbContextFactory, nameof(dbContextFactory));
            RaiseArgumentNullException.IfIsNull(clock, nameof(clock));
            _dbContextFactory = dbContextFactory;
            _queryableDbContext = dbContextFactory.Create();
            _clock = clock;
        }

        public override void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            // NOTA BENE: Mi devo occupare del contesto usato per spedire in giro i vari IQueryable.
            _queryableDbContext?.Dispose();

            _disposed = true;
        }

        protected override async Task AddEntryAsyncInternal(string appName, LogEntry logEntry)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var settings = await GetAndSetCachedSettingsAsync(appName, ctx);

                var sqlLogSettingId = ToSqlLogSettingId(logEntry.LogLevel);
                var sqlLogSetting = settings[sqlLogSettingId];

                if (sqlLogSetting.Enabled)
                {
                    ctx.LogEntries.Add(ToSqlLogEntry(logEntry, sqlLogSetting));
                }

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task AddEntriesAsyncInternal(string appName, IEnumerable<LogEntry> logEntries)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var settings = await GetAndSetCachedSettingsAsync(appName, ctx);

                foreach (var logEntry in logEntries)
                {
                    var sqlLogSettingId = ToSqlLogSettingId(logEntry.LogLevel);
                    var sqlLogSetting = settings[sqlLogSettingId];

                    if (sqlLogSetting.Enabled)
                    {
                        ctx.LogEntries.Add(ToSqlLogEntry(logEntry, sqlLogSetting));
                    }
                }

                await ctx.SaveChangesAsync();
            }
        }

        protected override async Task CleanUpEntriesAsyncInternal(string appName)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                // Process variables and constants.
                const int pageSize = 1000;
                bool shouldRemove;

                // Log deletion might take a long time...
                ctx.Database.CommandTimeout = 300;

                var utcNow = _clock.UtcNow;

                var appIds = ((appName == null) ? ctx.SecApps : ctx.SecApps.Where(a => a.Name == appName)).Select(a => a.Id).ToArray();

                // We delete logs older than "settings.Days".
                shouldRemove = true;
                while (shouldRemove)
                {
                    using (var tmpCtx = _dbContextFactory.Create())
                    {
                        var oldLogs = (from e in tmpCtx.LogEntries
                                       where appIds.Contains(e.AppId)
                                       from s in tmpCtx.LogSettings
                                       where s.AppId == e.AppId && s.LogLevel == e.LogLevel
                                       where DbFunctions.DiffDays(utcNow, e.Date) > s.Days
                                       select e).Take(pageSize).ToArray();

                        tmpCtx.LogEntries.RemoveRange(oldLogs);
                        await tmpCtx.SaveChangesAsync();

                        if (oldLogs.Length < pageSize)
                        {
                            shouldRemove = false;
                            continue;
                        }
                    }
                }                

                // We delete enough entries to preserve the upper limit.

                var logSettings = from e in ctx.LogEntries
                                  where appIds.Contains(e.AppId)
                                  from s in ctx.LogSettings
                                  where s.AppId == e.AppId && s.LogLevel == e.LogLevel
                                  select new { Entry = e, Setting = s };

                var logCounts = from es in logSettings
                                group es by new { es.Setting.AppId, es.Setting.LogLevel } into g
                                where (g.Count() - g.FirstOrDefault().Setting.MaxEntries) > 0
                                select new { g.Key.AppId, g.Key.LogLevel, Keep = g.FirstOrDefault().Setting.MaxEntries };

                var logCountsArray = await logCounts.ToArrayAsync();
                foreach (var lc in logCountsArray)
                {
                    shouldRemove = true;
                    while (shouldRemove)
                    {
                        using (var tmpCtx = _dbContextFactory.Create())
                        {
                            var tooManyLogEntries = (from e in tmpCtx.LogEntries
                                                     where e.AppId == lc.AppId && e.LogLevel == lc.LogLevel
                                                     orderby e.Date descending
                                                     select e).Skip(lc.Keep).Take(pageSize).ToArray();

                            tmpCtx.LogEntries.RemoveRange(tooManyLogEntries);
                            await tmpCtx.SaveChangesAsync();

                            if (tooManyLogEntries.Length < pageSize)
                            {
                                shouldRemove = false;
                                continue;
                            }
                        }
                    }
                }
            }
        }

        protected override IList<LogEntry> GetEntriesInternal(string appName, LogLevel? logLevel)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var q = ctx.LogEntries.Include(s => s.App);
                if (appName != null)
                {
                    q = q.Where(s => s.App.Name == appName);
                }
                if (logLevel != null)
                {
                    var logLevelString = logLevel.ToString().ToLower();
                    q = q.Where(s => s.LogLevel == logLevelString);
                }
                return q.OrderByDescending(s => s.Id)
                    .ThenByDescending(s => s.Date)
                    .AsEnumerable()
                    .Select(Mapper.Map<LogEntry>)
                    .ToArray();
            }
        }

        protected override IList<LogEntry> QueryEntriesInternal(LogEntryQuery logEntryQuery)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var q = ctx.LogEntries.Include(s => s.App);

                if (logEntryQuery.AppNames != null && logEntryQuery.AppNames.Count > 0)
                {
                    q = q.Where(e => logEntryQuery.AppNames.Contains(e.App.Name));
                }

                if (logEntryQuery.LogLevels != null && logEntryQuery.LogLevels.Count > 0)
                {
                    var logLevelStrings = logEntryQuery.LogLevels.Select(ll => ll.ToString().ToLower()).ToArray();
                    q = q.Where(e => logLevelStrings.Contains(e.LogLevel));
                }

                logEntryQuery.FromDate.Do(x => q = q.Where(e => DbFunctions.DiffDays(e.Date, x) <= 0));
                logEntryQuery.ToDate.Do(x => q = q.Where(e => DbFunctions.DiffDays(e.Date, x) >= 0));
                logEntryQuery.UserLoginLike.Do(x => q = q.Where(e => e.UserLogin.Contains(x)));
                logEntryQuery.CodeUnitLike.Do(x => q = q.Where(e => e.CodeUnit.Contains(x)));
                logEntryQuery.FunctionLike.Do(x => q = q.Where(e => e.Function.Contains(x)));
                logEntryQuery.ShortMessageLike.Do(x => q = q.Where(e => e.ShortMessage.Contains(x)));
                logEntryQuery.LongMessageLike.Do(x => q = q.Where(e => e.LongMessage.Contains(x)));
                logEntryQuery.ContextLike.Do(x => q = q.Where(e => e.Context.Contains(x)));

                // Ordinamento delle voci del log.
                q = q.OrderByDescending(e => e.Date).ThenByDescending(e => e.Id);

                // Reale esecuzione della query.
                var result = !logEntryQuery.TruncateLongMessage ? q.AsEnumerable() : q.Select(e => new
                {
                    AppName = e.App.Name,
                    e.Id,
                    e.LogLevel,
                    e.Date,
                    e.UserLogin,
                    e.CodeUnit,
                    e.Function,
                    e.ShortMessage,
                    LongMessage = e.LongMessage.Substring(0, logEntryQuery.MaxTruncatedLongMessageLength),
                    e.Context,
                    e.Key0,
                    e.Value0,
                    e.Key1,
                    e.Value1,
                    e.Key2,
                    e.Value2,
                    e.Key3,
                    e.Value3,
                    e.Key4,
                    e.Value4,
                    e.Key5,
                    e.Value5,
                    e.Key6,
                    e.Value6,
                    e.Key7,
                    e.Value7,
                    e.Key8,
                    e.Value8,
                    e.Key9,
                    e.Value9,
                }).AsEnumerable().Select(e => new SqlLogEntry
                {
                    App = new SqlSecApp { Name = e.AppName },
                    Id = e.Id,
                    LogLevel = e.LogLevel,
                    Date = e.Date,
                    UserLogin = e.UserLogin,
                    CodeUnit = e.CodeUnit,
                    Function = e.Function,
                    ShortMessage = e.ShortMessage,
                    LongMessage = e.LongMessage,
                    Context = e.Context,
                    Key0 = e.Key0,
                    Value0 = e.Value0,
                    Key1 = e.Key1,
                    Value1 = e.Value1,
                    Key2 = e.Key2,
                    Value2 = e.Value2,
                    Key3 = e.Key3,
                    Value3 = e.Value3,
                    Key4 = e.Key4,
                    Value4 = e.Value4,
                    Key5 = e.Key5,
                    Value5 = e.Value5,
                    Key6 = e.Key6,
                    Value6 = e.Value6,
                    Key7 = e.Key7,
                    Value7 = e.Value7,
                    Key8 = e.Key8,
                    Value8 = e.Value8,
                    Key9 = e.Key9,
                    Value9 = e.Value9,
                });

                return result.Select(Mapper.Map<LogEntry>).ToArray();
            }
        }

        protected override Option<LogEntry> GetEntryInternal(string appName, long logId)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                return ctx.LogEntries
                    .Include(s => s.App)
                    .Where(e => e.App.Name == appName)
                    .Where(e => e.Id == logId)
                    .Select(Mapper.Map<LogEntry>)
                    .FirstAsOption();
            }
        }

        protected override bool DoRemoveEntry(string appName, int logId)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var deleted = false;
                var log = ctx.LogEntries.FirstOrDefault(l => l.App.Name == appName && l.Id == logId);
                if (log != null)
                {
                    ctx.LogEntries.Remove(log);
                    deleted = true;
                    ctx.SaveChanges();
                }
                return deleted;
            }
        }

        protected override IList<LogSetting> GetSettingsInternal(string appName, LogLevel? logLevel)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var q = ctx.LogSettings.Include(s => s.App);
                if (appName != null)
                {
                    q = q.Where(s => s.App.Name == appName);
                }
                if (logLevel != null)
                {
                    var logLevelString = logLevel.ToString().ToLower();
                    q = q.Where(s => s.LogLevel == logLevelString);
                }
                return q.OrderBy(s => s.App.Name)
                    .ThenBy(s => s.LogLevel)
                    .AsEnumerable()
                    .Select(Mapper.Map<LogSetting>)
                    .ToArray();
            }
        }

        protected override bool DoAddSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var added = false;
                var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                var typeId = logLevel.ToString().ToLower();

                if (!ctx.LogSettings.Any(s => s.AppId == appId && s.LogLevel == typeId))
                {
                    var newSetting = new SqlLogSetting
                    {
                        AppId = appId,
                        Days = setting.Days,
                        Enabled = setting.Enabled,
                        MaxEntries = setting.MaxEntries,
                        LogLevel = typeId
                    };

                    ctx.LogSettings.Add(newSetting);
                    added = true;
                }

                ctx.SaveChanges();
                return added;
            }
        }

        protected override bool DoRemoveSetting(string appName, LogLevel logLevel)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var deleted = false;
                var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                var typeId = logLevel.ToString().ToLower();
                var settings = ctx.LogSettings.FirstOrDefault(a => a.AppId == appId && a.LogLevel == typeId);

                if (settings != null)
                {
                    ctx.LogSettings.Remove(settings);
                    deleted = true;
                    ctx.SaveChanges();
                }
                return deleted;
            }
        }

        protected override bool DoUpdateSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            using (var ctx = _dbContextFactory.Create())
            {
                var update = false;
                var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                var typeId = logLevel.ToString().ToLower();

                var settingToUpdate = ctx.LogSettings.First(s => s.AppId == appId && s.LogLevel == typeId);

                if (settingToUpdate != null)
                {
                    settingToUpdate.Days = setting.Days;
                    settingToUpdate.Enabled = setting.Enabled;
                    settingToUpdate.MaxEntries = setting.MaxEntries;
                    update = true;
                }

                ctx.SaveChanges();
                return update;
            }
        }

        #region Private members

        private const string CachePartition = "Caravan.SqlLogSettings";

        private async Task<Dictionary<string, SqlLogSetting>> GetAndSetCachedSettingsAsync(string appName, SqlDbContext ctx)
        {
            // Provo a recuperare le impostazioni di log dalla cache in memoria.
            var maybe = CaravanServiceProvider.MemoryCache.Get<Dictionary<string, SqlLogSetting>>(CachePartition, appName);

            // Se le impostazioni in cache sono ancora valide, le restituisco.
            if (maybe.HasValue)
            {
                return maybe.Value;
            }

            // Altrimenti, le devo leggere da DB.
            var appId = await ctx.SecApps.Where(a => a.Name == appName).Select(a => a.Id).FirstAsync();
            var settings = await ctx.LogSettings.Where(s => s.AppId == appId).ToDictionaryAsync(s => s.LogLevel);

            // Prima di restituirle, le metto in cache.
            var interval = CaravanDataAccessConfiguration.Instance.Logging_SettingsCache_Interval;
            var utcExpiry = _clock.UtcNow.Add(interval);
            CaravanServiceProvider.MemoryCache.AddTimed(CachePartition, appName, settings, utcExpiry);

            // Restituisco le impostazioni appena lette da DB.
            return settings;
        }

        private static SqlLogEntry ToSqlLogEntry(LogEntry logEntry, SqlLogSetting sqlLogSetting)
        {
            var sqlLogEntry = new SqlLogEntry
            {
                Date = logEntry.Date,
                AppId = sqlLogSetting.AppId,
                LogLevel = sqlLogSetting.LogLevel,
                UserLogin = logEntry.UserLogin.Truncate(SqlDbContext.SmallLength).ToLowerInvariant(),
                CodeUnit = logEntry.CodeUnit.Truncate(SqlDbContext.MediumLength).ToLowerInvariant(),
                Function = logEntry.Function.Truncate(SqlDbContext.MediumLength).ToLowerInvariant(),
                ShortMessage = logEntry.ShortMessage.Truncate(SqlDbContext.LargeLength),
                LongMessage = logEntry.LongMessage, // Not truncated, because it should be a CLOB/TEXT/whatever...
                Context = logEntry.Context.Truncate(SqlDbContext.MediumLength)
            };

            var args = logEntry.Arguments;
            for (var i = 0; args != null && i < args.Count && i < MaxArgumentCount; ++i)
            {
                var tmp = args[i];
                var tmpKey = tmp.Key.Truncate(SqlDbContext.SmallLength);
                var tmpValue = tmp.Value.Truncate(SqlDbContext.LargeLength);
                switch (i)
                {
                    case 0:
                        sqlLogEntry.Key0 = tmpKey;
                        sqlLogEntry.Value0 = tmpValue;
                        break;

                    case 1:
                        sqlLogEntry.Key1 = tmpKey;
                        sqlLogEntry.Value1 = tmpValue;
                        break;

                    case 2:
                        sqlLogEntry.Key2 = tmpKey;
                        sqlLogEntry.Value2 = tmpValue;
                        break;

                    case 3:
                        sqlLogEntry.Key3 = tmpKey;
                        sqlLogEntry.Value3 = tmpValue;
                        break;

                    case 4:
                        sqlLogEntry.Key4 = tmpKey;
                        sqlLogEntry.Value4 = tmpValue;
                        break;

                    case 5:
                        sqlLogEntry.Key5 = tmpKey;
                        sqlLogEntry.Value5 = tmpValue;
                        break;

                    case 6:
                        sqlLogEntry.Key6 = tmpKey;
                        sqlLogEntry.Value6 = tmpValue;
                        break;

                    case 7:
                        sqlLogEntry.Key7 = tmpKey;
                        sqlLogEntry.Value7 = tmpValue;
                        break;

                    case 8:
                        sqlLogEntry.Key8 = tmpKey;
                        sqlLogEntry.Value8 = tmpValue;
                        break;

                    case 9:
                        sqlLogEntry.Key9 = tmpKey;
                        sqlLogEntry.Value9 = tmpValue;
                        break;
                }
            }

            return sqlLogEntry;
        }

        private static string ToSqlLogSettingId(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return "debug";

                case LogLevel.Trace:
                    return "trace";

                case LogLevel.Info:
                    return "info";

                case LogLevel.Warn:
                    return "warn";

                case LogLevel.Error:
                    return "error";

                case LogLevel.Fatal:
                    return "fatal";

                default:
                    return "debug";
            }
        }

        #endregion Private members
    }
}
