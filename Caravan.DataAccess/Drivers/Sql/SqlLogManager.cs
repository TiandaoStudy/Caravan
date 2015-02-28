using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using AutoMapper;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Drivers.Sql.Models.Logging;
using PommaLabs.Diagnostics;
using PommaLabs.Extensions;

namespace Finsa.Caravan.DataAccess.Drivers.Sql
{
    internal sealed class SqlLogManager : LogManagerBase<SqlLogManager>
    {
        #region Constants

        private const int MaxArgumentCount = 10;
        private const int MaxStringLength = 2000;

        private const int TrueInt = 1;
        private const int FalseInt = 0;

        #endregion Constants

        protected override LogResult DoLogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function,
           string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            try
            {
                Raise<ArgumentException>.IfIsEmpty(codeUnit);
                var argsList = (args == null) ? new KeyValuePair<string, string>[0] : args.ToArray();
                Raise<ArgumentOutOfRangeException>.If(argsList.Length > MaxArgumentCount);

                using (var trx = new TransactionScope())
                using (var ctx = SqlDbContext.CreateWriteContext())
                {
                    var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                    var typeId = logType.ToString().ToLower();
                    var settings = ctx.LogSettings.First(s => s.AppId == appId && s.LogType == typeId);

                    // We delete logs older than "settings.Days"
                    var minDate = DateTime.Now.Subtract(TimeSpan.FromDays(settings.Days));
                    var oldLogs = from l in ctx.LogEntries
                                  where l.AppId == appId && l.LogType == typeId
                                  where l.Date < minDate
                                  select l;
                    ctx.LogEntries.RemoveRange(oldLogs);

                    // We delete enough entries to preserve the upper limit
                    var logCount = ctx.LogEntries.Count(e => e.AppId == appId && e.LogType == typeId);
                    if (logCount >= settings.MaxEntries)
                    {
                        var olderLogs = (from l in ctx.LogEntries
                                         where l.AppId == appId && l.LogType == typeId
                                         orderby l.Date ascending
                                         select l).Take(logCount - settings.MaxEntries + 1);
                        ctx.LogEntries.RemoveRange(olderLogs);
                    }

                    // If log is enabled, then we can insert a new entry
                    if (settings.Enabled == 1)
                    {
                        var entry = new SqlLogEntry
                        {
                            Date = DateTime.Now,
                            AppId = appId,
                            LogType = typeId,
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
                    trx.Complete();
                    return LogResult.Success;
                }
            }
            catch (Exception ex)
            {
                return LogResult.Failure(ex);
            }
        }

        protected override IList<LogEntry> GetEntries(string appName, LogType? logType)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.LogEntries.Include(s => s.App);
                if (appName != null)
                {
                    q = q.Where(s => s.App.Name == appName);
                }
                if (logType != null)
                {
                    var logTypeString = logType.ToString().ToLower();
                    q = q.Where(s => s.LogType == logTypeString);
                }
                return q.OrderByDescending(s => s.Id)
                    .ThenByDescending(s => s.Date)
                    .AsEnumerable()
                    .Select(Mapper.Map<LogEntry>)
                    .ToList();
            }
        }

        protected override bool DoRemoveEntry(string appName, int logId)
        {
            using (var trx = new TransactionScope())
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

        protected override IList<LogSetting> GetSettings(string appName, LogType? logType)
        {
            using (var ctx = SqlDbContext.CreateReadContext())
            {
                var q = ctx.LogSettings.Include(s => s.App);
                if (appName != null)
                {
                    q = q.Where(s => s.App.Name == appName);
                }
                if (logType != null)
                {
                    var logTypeString = logType.ToString().ToLower();
                    q = q.Where(s => s.LogType == logTypeString);
                }
                return q.OrderBy(s => s.App.Name)
                    .ThenBy(s => s.LogType)
                    .AsEnumerable()
                    .Select(Mapper.Map<LogSetting>)
                    .ToList();
            }
        }

        protected override bool DoAddSetting(string appName, LogType logType, LogSetting setting)
        {
            using (var trx = new TransactionScope())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var added = false;
                var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                var typeId = logType.ToString().ToLower();

                if (!ctx.LogSettings.Any(s => s.AppId == appId && s.LogType == typeId))
                {
                    var newSetting = new SqlLogSetting
                    {
                        AppId = appId,
                        Days = setting.Days,
                        Enabled = setting.Enabled ? TrueInt : FalseInt,
                        MaxEntries = setting.MaxEntries,
                        LogType = typeId
                    };

                    ctx.LogSettings.Add(newSetting);
                    added = true;
                }

                ctx.SaveChanges();
                trx.Complete();
                return added;
            }
        }

        protected override bool DoRemoveSetting(string appName, LogType logType)
        {
            using (var trx = new TransactionScope())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var deleted = false;
                var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                var typeId = logType.ToString().ToLower();
                var settings = ctx.LogSettings.FirstOrDefault(a => a.AppId == appId && a.LogType == typeId);

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

        protected override bool DoUpdateSetting(string appName, LogType logType, LogSetting setting)
        {
            using (var trx = new TransactionScope())
            using (var ctx = SqlDbContext.CreateWriteContext())
            {
                var update = false;
                var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
                var typeId = logType.ToString().ToLower();

                var settingToUpdate = ctx.LogSettings.First(s => s.AppId == appId && s.LogType == typeId);

                if (settingToUpdate != null)
                {
                    settingToUpdate.Days = setting.Days;
                    settingToUpdate.Enabled = setting.Enabled ? TrueInt : FalseInt;
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