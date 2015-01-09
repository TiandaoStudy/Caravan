using Finsa.Caravan.Common.DataModel.Logging;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Mongo.DataModel.Logging;
using Finsa.Caravan.DataAccess.Mongo.DataModel.Security;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Mongo
{
   internal sealed class MongoLogManager : LogManagerBase
   {
      public override LogResult LogRaw(LogType type, string appName, string userName, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<LogEntry> GetLogEntries(string appName, LogType? logType)
      {
         throw new System.NotImplementedException();
      }

      protected override IList<LogSettings> GetLogSettings(string appName, LogType? logType)
      {
         var db = MongoUtilities.GetDatabase();
         var apps = db.GetCollection<MongoSecApp>(MongoUtilities.SecAppCollection);
         var query = apps.AsQueryable();

         if (appName != null)
         {
            query = query.Where(a => a.Name == appName);
         }

         var logTypeStr = (logType == null) ? null : logType.ToString().ToLower();

         return (from a in query
                 from s in a.LogSettings.Where(s => logTypeStr == null || s.Type == logTypeStr)
                 select new LogSettings
                 {
                    App = new SecApp
                    {
                       Id = a.Id,
                       Name = a.Name
                    }
                 }).ToList();
      }

      protected override bool DoAddSettings(string appName, LogType logType, LogSettings settings)
      {
         var db = MongoUtilities.GetDatabase();
         var apps = db.GetCollection<MongoSecApp>(MongoUtilities.SecAppCollection);
         var app = apps.AsQueryable().First(a => a.Name == appName);
         app.LogSettings.Add(new MongoLogSettings
         {
            Type = logType.ToString().ToLower()
         });
         return true;
      }

      protected override bool DoUpdateSettings(string appName, LogType logType, LogSettings settings)
      {
         throw new System.NotImplementedException();
      }
   }
}