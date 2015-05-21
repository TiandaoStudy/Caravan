using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging;
using Common.Logging;
using Finsa.CodeServices.Common;
using Finsa.CodeServices.Common.Diagnostics;
using Finsa.CodeServices.Common.Extensions;

namespace Finsa.Caravan.DataAccess.Drivers.Sql
{
    internal sealed class SqlLogRepository : AbstractLogRepository<SqlLogRepository>
    {
        #region Constants

        private const int MaxArgumentCount = 10;
        private const int MaxStringLength = 2000;

        #endregion Constants

        protected override LogResult DoLogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function,
           string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentException>.IfIsEmpty(codeUnit);
                var argsList = (args == null) ? new KeyValuePair<string, string>[0] : args.ToArray();
                Raise<ArgumentOutOfRangeException>.If(argsList.Length > MaxArgumentCount);

                using (var ctx = SqlDbContext.CreateWriteContext())
                {
                    var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                    var typeId = logLevel.ToString().ToLower();
                    var settings = ctx.LogSettings.First(s => s.AppId == appId && s.LogLevel == typeId);

                    // We delete logs older than "settings.Days"
                    var minDate = DateTime.Now.Subtract(TimeSpan.FromDays(settings.Days));
                    var oldLogs = from l in ctx.LogEntries
                                  where l.AppId == appId && l.LogLevel == typeId
                                  where l.Date < minDate
                                  select l;
                    ctx.LogEntries.RemoveRange(oldLogs);

                    // We delete enough entries to preserve the upper limit
                    var logCount = ctx.LogEntries.Count(e => e.AppId == appId && e.LogLevel == typeId);
                    if (logCount >= settings.MaxEntries)
                    {
                        var olderLogs = (from l in ctx.LogEntries
                                         where l.AppId == appId && l.LogLevel == typeId
                                         orderby l.Date ascending
                                         select l).Take(logCount - settings.MaxEntries + 1);
                        ctx.LogEntries.RemoveRange(olderLogs);
                    }

                    // If log is enabled, then we can insert a new entry
                    if (settings.Enabled)
                    {
                        var entry = new SqlLogEntry
                        {
                            Date = DateTime.Now,
                            AppId = appId,
                            LogLevel = typeId,
                            UserLogin = userLogin.Truncate(MaxStringLength).ToLower(),
                            CodeUnit = codeUnit.Truncate(MaxStringLength).ToLower(),
                            Function = function.Truncate(MaxStringLength).ToLower(),
                            ShortMessage = shortMessage.Truncate(MaxStringLength),
                            LongMessage = longMessage, // Not truncated, because it should be a CLOB/TEXT/whatever...
                            Context = context.Truncate(MaxStringLength)
                        };

                        for (var i = 0; i < argsList.Length; ++i)
                        {
                            var key = argsList[i].Key;
                            var val = argsList[i].Value;
                            switch (i)
                            {
                                case 0:
                                    entry.Key0 = key;
                                    entry.Value0 = val;
                                    break;

                                case 1:
                                    entry.Key1 = key;
                                    entry.Value1 = val;
                                    break;

                                case 2:
                                    entry.Key2 = key;
                                    entry.Value2 = val;
                                    break;

                                case 3:
                                    entry.Key3 = key;
                                    entry.Value3 = val;
                                    break;

                                case 4:
                                    entry.Key4 = key;
                                    entry.Value4 = val;
                                    break;

                                case 5:
                                    entry.Key5 = key;
                                    entry.Value5 = val;
                                    break;

                                case 6:
                                    entry.Key6 = key;
                                    entry.Value6 = val;
                                    break;

                                case 7:
                                    entry.Key7 = key;
                                    entry.Value7 = val;
                                    break;

                                case 8:
                                    entry.Key8 = key;
                                    entry.Value8 = val;
                                    break;

                                case 9:
                                    entry.Key9 = key;
                                    entry.Value9 = val;
                                    break;
                            }
                        }

                        ctx.LogEntries.Add(entry);
                    }

                    ctx.SaveChanges();
                    return LogResult.Success;
                }
            }
            catch (Exception ex)
            {
                return LogResult.Failure(ex);
            }
        }

        protected override IList<LogEntry> GetEntriesInternal(string appName, LogLevel? logLevel)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
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
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.LogEntries.Include(s => s.App);
                
                if (logEntryQuery.AppNames != null && logEntryQuery.AppNames.Count > 0)
                {
                    q = q.Where(e => logEntryQuery.AppNames.Contains(e.App.Name));
                }
                
                if (logEntryQuery.LogLevels != null && logEntryQuery.LogLevels.Count > 0)
                {
                    var logLevelStrings = logEntryQuery.LogLevels.Select(ll => ll.ToString().ToLowerInvariant()).ToArray();
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
                
                // Execute the query.
                var r = q.OrderByDescending(s => s.Id).ThenByDescending(s => s.Date).AsEnumerable();
                
                if (logEntryQuery.TruncateLongMessage)
                {
                    r = r.Select(e =>
                    {
                        e.LongMessage = e.LongMessage.Truncate(logEntryQuery.MaxTruncatedLongMessageLength);
                        return e;
                    });
                }

                return r.Select(Mapper.Map<LogEntry>).ToArray();
            }
        }

        protected override Option<LogEntry> GetEntryInternal(string appName, long logId)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
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
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var deleted = false;
                var log = ctx.LogEntries.FirstOrDefault(l => l.App.Name == appName && l.Id == logId);
                if (log != null)
                {
                    ctx.LogEntries.Remove(log);
                    deleted = true;
                    ctx.SaveChanges();
                    trx.Complete();
                }
                return deleted;
            }
        }

        protected override IList<LogSetting> GetSettingsInternal(string appName, LogLevel? logLevel)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
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
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
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
                trx.Complete();
                return added;
            }
        }

        protected override bool DoRemoveSetting(string appName, LogLevel logLevel)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
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
                    trx.Complete();
                }
                return deleted;
            }
        }

        protected override bool DoUpdateSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            using (var trx = SqlDbContext.BeginTrasaction())
            using (var ctx = SqlDbContext.CreateWriteContext())
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
                trx.Complete();
                return update;
            }
        }
    }
}