using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel.Logging;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;

namespace Finsa.Caravan.DataAccess.Sql
{
   public sealed class SqlLogManager : LogManagerBase
   {
      #region Constants

      private const int MaxUserNameLength = 30;
      private const int MaxCodeUnitLength = 100;
      private const int MaxFunctionLength = 100;
      private const int MaxShortMessageLength = 400;
      private const int MaxLongMessageLength = 2000;
      private const int MaxContextLength = 400;
      private const int MaxArgumentCount = 10;

      #endregion

      public override LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context,
         IEnumerable<CKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentException>.IfIsEmpty(codeUnit);
            var argsList = (args == null) ? new CKeyValuePair<string, string>[0] : args.ToArray();
            Raise<ArgumentOutOfRangeException>.If(argsList.Length > MaxArgumentCount);

            using (var ctx = Db.CreateWriteContext())
            {
               ctx.BeginTransaction();

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
                     Id = (ctx.LogEntries.Max(e => (long?) e.Id) ?? -1) + 1,
                     Date = DateTime.Now,
                     AppId = appId,
                     TypeId = typeId,
                     UserLogin = userName.Truncate(MaxUserNameLength).ToLower(),
                     CodeUnit = codeUnit.Truncate(MaxCodeUnitLength).ToLower(),
                     Function = function.Truncate(MaxFunctionLength).ToLower(),
                     ShortMessage = shortMessage.Truncate(MaxShortMessageLength),
                     LongMessage = longMessage.Truncate(MaxLongMessageLength),
                     Context = context.Truncate(MaxContextLength),
                     Arguments = argsList
                  });
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

      protected override IList<LogEntry> GetLogs(string appName, LogType? logType)
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
            return q.OrderBy(s => s.App.Name).ThenBy(s => s.TypeId).ThenByDescending(s => s.Date).ToList();
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

      protected override void DoAddSettings(string appName, LogType logType, LogSettings settings)
      {
         throw new NotImplementedException();
      }
   }
}