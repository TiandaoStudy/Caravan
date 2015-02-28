using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Finsa.Caravan.Common.Models.Logging;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Logging;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Security;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo
{
    internal sealed class MongoLogManager : LogManagerBase<MongoLogManager>
    {
        protected override LogResult DoLogRaw(LogType logType, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage, string context, IEnumerable<KeyValuePair<string, string>> args)
        {
            var app = MongoUtilities.GetSecAppCollection().AsQueryable().First(a => a.Name == appName);

            var newLogId = MongoUtilities.GetSequenceCollection().FindAndModify(new FindAndModifyArgs
            {
                Query = Query<MongoSequence>.Where(s => s.AppId == app.Id && s.CollectionName == MongoUtilities.LogEntryCollection),
                Update = Update<MongoSequence>.Inc(s => s.LastNumber, 1),
                VersionReturned = FindAndModifyDocumentVersion.Modified,
                Upsert = true, // Creates a new document if it does not exist.
            }).GetModifiedDocumentAs<MongoSequence>().LastNumber;

            var logTypeStr = logType.ToString().ToLower();
            MongoUtilities.GetLogEntryCollection().Insert(new MongoLogEntry
            {
                Id = MongoUtilities.CreateObjectId(newLogId),
                AppId = app.Id,
                LogId = newLogId,
                Type = logTypeStr,
                ShortMessage = shortMessage,
                CodeUnit = codeUnit
            });

            return LogResult.Success;
        }

        protected override IList<LogEntry> GetEntries(string appName, LogType? logType)
        {
            var apps = MongoUtilities.GetSecAppCollection().AsQueryable();
            Dictionary<ObjectId, MongoSecApp> appMap;
            if (appName != null)
            {
                var app = apps.First(a => a.Name == appName);
                appMap = new Dictionary<ObjectId, MongoSecApp> { { app.Id, app } };
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
                AppName = appMap[l.AppId].Name,
                LogType = (LogType) Enum.Parse(typeof(LogType), l.Type, true),
                ShortMessage = l.ShortMessage,
                CodeUnit = l.CodeUnit
            }).ToList();
        }

        protected override bool DoRemoveEntry(string appName, int logId)
        {
            throw new System.NotImplementedException();
        }

        protected override IList<LogSetting> GetSettings(string appName, LogType? logType)
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
                    select new LogSetting
                    {
                        AppName = a.Name
                    }).ToList();
        }

        protected override bool DoAddSetting(string appName, LogType logType, LogSetting setting)
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
            var apps = MongoUtilities.GetSecAppCollection();
            return apps.Update(query, update).Ok;
        }

        protected override bool DoUpdateSetting(string appName, LogType logType, LogSetting setting)
        {
            // Update preparation
            var logTypeStr = logType.ToString().ToLower();
            var docId = MongoUtilities.CreateObjectId(logTypeStr);
            var query = Query<MongoSecApp>.EQ(a => a.Name, appName);
            var update = Update<MongoSecApp>.Pull(a => a.LogSettings, new MongoLogSettings
            {
                Id = docId
            }).AddToSet(a => a.LogSettings, new MongoLogSettings
            {
                Id = docId,
                Type = logTypeStr
            });

            // Real update
            var apps = MongoUtilities.GetSecAppCollection();
            return apps.Update(query, update).Ok;
        }

        protected override bool DoRemoveSetting(string appName, LogType logType)
        {
            throw new System.NotImplementedException();
        }
    }
}