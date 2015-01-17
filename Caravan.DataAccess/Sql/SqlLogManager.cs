using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.DataAccess.Core;
using PommaLabs.Diagnostics;
using PommaLabs.Extensions;

namespace Finsa.Caravan.DataAccess.Sql
{
   public sealed class SqlLogManager : LogManagerBase
   {
      #region Constants

      private const int MaxArgumentCount = 10;
      private const int MaxStringLength = 2000;
      private const int NoId = 0;

      #endregion

      public override LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function,
         string shortMessage, string longMessage, string context,
         IEnumerable<KeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentException>.IfIsEmpty(codeUnit);
            var argsList = (args == null) ? new KeyValuePair<string, string>[0] : args.ToArray();
            Raise<ArgumentOutOfRangeException>.If(argsList.Length > MaxArgumentCount);
            
            using (var trx = new TransactionScope(TransactionScopeOption.Suppress))
            using (var ctx = Db.CreateWriteContext())
            {
               var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
               var typeId = type.ToString().ToLower();
               var settings = ctx.LogSettings.First(s => s.AppId == appId && s.TypeId == typeId);

               // We delete logs older than "settings.Days"
               var minDate = DateTime.Now.Subtract(TimeSpan.FromDays(settings.Days));
               var oldLogs = from l in ctx.LogEntries
                  where l.AppId == appId && l.TypeId == typeId
                  where l.Date < minDate
                  select l;
               ctx.LogEntries.RemoveRange(oldLogs);

               // We delete enough entries to preserve the upper limit
               var logCount = ctx.LogEntries.Count(e => e.AppId == appId && e.TypeId == typeId);
               if (logCount >= settings.MaxEntries)
               {
                  var olderLogs = (from l in ctx.LogEntries
                     where l.AppId == appId && l.TypeId == typeId
                     orderby l.Date ascending
                     select l).Take(logCount - settings.MaxEntries + 1);
                  ctx.LogEntries.RemoveRange(olderLogs);
               }

               // If log is enabled, then we can insert a new entry
               if (settings.Enabled == 1)
               {
                  ctx.LogEntries.Add(new LogEntry
                  {
                     Id = NoId,
                     Date = DateTime.Now,
                     AppId = appId,
                     TypeId = typeId,
                     UserLogin = userName.Truncate(MaxStringLength).ToLower(),
                     CodeUnit = codeUnit.Truncate(MaxStringLength).ToLower(),
                     Function = function.Truncate(MaxStringLength).ToLower(),
                     ShortMessage = shortMessage.Truncate(MaxStringLength),
                     LongMessage = longMessage, // Not truncated, because it should be a CLOB.
                     Context = context.Truncate(MaxStringLength),
                     Arguments = argsList
                  });
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

      protected override IList<LogEntry> GetLogEntries(string appName, LogType? logType)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var q = ctx.LogEntries.Include(s => s.App);
            if (appName != null)
            {
               q = q.Where(s => s.App.Name == appName);
            }
            if (logType != null)
            {
               var logTypeString = logType.ToString().ToLower();
               q = q.Where(s => s.TypeId == logTypeString);
            }
            return q.OrderByDescending(s => s.Id).ThenByDescending(s => s.Date).ToList();
         }
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         using (var ctx = Db.CreateReadContext())
         {
            var q = ctx.LogSettings.Include(s => s.App);
            if (appName != null)
            {
               q = q.Where(s => s.App.Name == appName);
            }
            if (logType != null)
            {
               var logTypeString = logType.ToString().ToLower();
               q = q.Where(s => s.TypeId == logTypeString);
            }
            return q.OrderBy(s => s.App.Name).ThenBy(s => s.TypeId).ToLogAndList();
         }
      }

      protected override bool DoAddSettings(string appName, LogType logType, LogSettings settings)
      {
         using (var trx = new TransactionScope(TransactionScopeOption.Suppress))
         using (var ctx = Db.CreateWriteContext())
         {
            var added = false;
            var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
            var typeId = logType.ToString().ToLower();

            if (!ctx.LogSettings.Any(s => s.AppId == appId && s.TypeId == typeId))
            {
               var newSetting = new LogSettings
               {
                  AppId = appId,
                  Days = settings.Days,
                  Enabled = settings.Enabled,
                  MaxEntries = settings.MaxEntries,
                  TypeId = typeId
               };

               ctx.LogSettings.Add(newSetting);
               added = true;
            }

            ctx.SaveChanges();
            trx.Complete();
            return added;
         }
      }

      protected override bool DoUpdateSettings(string appName, LogType logType, LogSettings settings)
      {
         using (var trx = new TransactionScope(TransactionScopeOption.Suppress))
         using (var ctx = Db.CreateWriteContext())
         {
            var update = false;
            var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
            var typeId = logType.ToString().ToLower();

            var settingToUpdate = ctx.LogSettings.First(s => s.AppId == appId && s.TypeId == typeId);

            if (settingToUpdate != null)
            {
               settingToUpdate.App = settings.App;
               settingToUpdate.AppId = appId;
               settingToUpdate.Days = settings.Days;
               settingToUpdate.Enabled = settings.Enabled;
               settingToUpdate.LogEntries = settings.LogEntries;
               settingToUpdate.MaxEntries = settings.MaxEntries;
               settingToUpdate.Type = logType;
               settingToUpdate.TypeId = typeId;

               update = true;
            }

            ctx.SaveChanges();
            trx.Complete();
            return update;
         }
      }
   }
}