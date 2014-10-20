using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;

namespace Finsa.Caravan.DataAccess.Direct
{
   public sealed class DirectLogger : LoggerBase
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

      public override LogResult Log(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context,
         IEnumerable<GKeyValuePair<string, string>> args)
      {
         try
         {
            Raise<ArgumentException>.IfIsEmpty(codeUnit);
            var argsList = (args == null) ? new GKeyValuePair<string, string>[0] : args.ToArray();
            Raise<ArgumentOutOfRangeException>.If(argsList.Length > MaxArgumentCount);

            using (var ctx = Db.CreateContext())
            using (var trx = ctx.BeginTransaction())
            {
               var appId = ctx.SecApps.Where(a => a.Name == appName.ToLower()).Select(a => a.Id).First();
               var typeId = type.ToString().ToLower();
               var settings = ctx.LogSettings.First(s => s.AppId == appId && s.TypeString == typeId);

               // We delete logs older than "settings.Days"
               var oldLogs = from l in ctx.LogEntries
                             where l.AppId == appId && l.TypeId == typeId
                             where l.Date < DateTime.Now.Subtract(TimeSpan.FromDays(settings.Days))
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
               if (settings.Enabled)
               {
                  ctx.LogEntries.Add(new LogEntry
                  {
                     Id = ctx.LogEntries.Max(e => (long?) e.Id) ?? 0,
                     AppId = appId,
                     TypeId = typeId,
                     UserLogin = GetCurrentUserName(userName).Truncate(MaxUserNameLength),
                     CodeUnit = codeUnit.Truncate(MaxCodeUnitLength),
                     Function = function.Truncate(MaxFunctionLength),
                     ShortMessage = shortMessage == null ? LogEntry.NotSpecified : shortMessage.Truncate(MaxShortMessageLength),
                     LongMessage = longMessage == null ? LogEntry.NotSpecified : longMessage.Truncate(MaxLongMessageLength),
                     Context = context == null ? LogEntry.NotSpecified : context.Truncate(MaxContextLength),
                     Arguments = argsList
                  });
               }
               
               ctx.SaveChanges();
               trx.Commit();
               return LogResult.Success;
            }
         }
         catch (Exception ex)
         {
            return LogResult.Failure(ex);
         }
      }

      protected override IEnumerable<LogEntry> GetLogs(string appName, LogType? logType)
      {
         using (var ctx = Db.CreateContext())
         {
            return (from s in ctx.LogEntries.Include(s => s.App)
                    where appName == null || s.App.Name == appName.ToLower()
                    where logType == null || s.TypeId == logType.ToString().ToLower()
                    orderby s.App.Name, s.TypeId, s.Date descending
                    select s).ToList();
         }
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         using (var ctx = Db.CreateContext())
         {
            return (from s in ctx.LogSettings.Include(s => s.App)
                    where appName == null || s.App.Name == appName.ToLower()
                    where logType == null || s.TypeString == logType.ToString().ToLower()
                    orderby s.App.Name, s.TypeString
                    select s).ToList();
         }
      }
   }
}