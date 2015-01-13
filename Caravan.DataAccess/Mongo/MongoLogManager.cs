using System.Diagnostics;
using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Mongo.DataModel.Logging;
using Finsa.Caravan.DataAccess.Mongo.DataModel.Security;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Mongo
{
   internal sealed class MongoLogManager : LogManagerBase
   {
      public override LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
      {
         return LogResult.Success;
      }

      protected override IList<LogEntry> GetLogEntries(string appName, LogType? logType)
      {
         var apps = MongoUtilities.GetSecAppCollection().AsQueryable();
         Dictionary<ObjectId, MongoSecApp> appMap;
         if (appName != null)
         {
            var app = apps.First(a => a.Name == appName);
            appMap = new Dictionary<ObjectId, MongoSecApp> {{app.Id, app}};
         }
         else
         {
            appMap = apps.ToDictionary(a => a.Id, a => a);
         }

         var logEntries = MongoUtilities.GetLogEntryCollection().AsQueryable();
         if (appName != null)
         {
            Debug.Assert(appMap.Count == 1);
            var appId = appMap.First().Key;
            logEntries = logEntries.Where(l => l.AppId == appId);
         }
         if (logType != null)
         {
            var logTypeStr = logType.ToString().ToLower();
            logEntries = logEntries.Where(l => l.Type == logTypeStr);
         }

         return logEntries.AsEnumerable().Select(l => new LogEntry
         {
            Id = l.LogId,
            AppId = appMap[l.AppId].AppId,
            TypeId = l.Type
         }).ToList();
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         var apps = MongoUtilities.GetSecAppCollection();
         var query = apps.AsQueryable();

         if (appName != null)
         {
            query = query.Where(a => a.Name == appName);
         }

         var logTypeStr = (logType == null) ? null : logType.ToString().ToLower();

         return (from a in query.AsEnumerable()
                 from s in a.LogSettings.Where(s => logTypeStr == null || s.Type == logTypeStr)
                 select new LogSettings
                 {
                    App = new SecApp
                    {
                       Id = a.AppId,
                       Name = a.Name
                    }
                 }).ToList();
      }

      protected override bool DoAddSettings(string appName, LogType logType, LogSettings settings)
      {
         // Update preparation
         var logTypeStr = logType.ToString().ToLower();
         var query = Query<MongoSecApp>.EQ(a => a.Name, appName);
         var update = Update<MongoSecApp>.AddToSet(a => a.LogSettings, new MongoLogSettings
         {
            Id = MongoUtilities.CreateObjectId(logTypeStr),
            Type = logTypeStr
         });

         // Real update
         var db = MongoUtilities.GetDatabase();
         var apps = db.GetCollection<MongoSecApp>(MongoUtilities.SecAppCollection);
         return apps.Update(query, update).Ok;
      }

      protected override bool DoUpdateSettings(string appName, LogType logType, LogSettings settings)
      {
         throw new System.NotImplementedException();
      }
   }
}