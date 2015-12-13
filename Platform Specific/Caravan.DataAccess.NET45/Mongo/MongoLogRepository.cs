﻿using Common.Logging;
using Finsa.Caravan.Common.Logging.Models;
using Finsa.Caravan.DataAccess.Core;
using Finsa.Caravan.DataAccess.Mongo.Models.Logging;
using Finsa.Caravan.DataAccess.Mongo.Models.Security;
using Finsa.CodeServices.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Finsa.Caravan.Common.Logging;

namespace Finsa.Caravan.DataAccess.Mongo
{
    internal sealed class MongoLogRepository : AbstractLogRepository<MongoLogRepository>
    {
        public MongoLogRepository(ICaravanLog log)
            : base(log)
        {
        }

        public override void Dispose()
        {
            // Nulla da fare, per ora...
        }

        //protected override LogResult DoLogRaw(LogLevel logLevel, string appName, string userLogin, string codeUnit, string function, string shortMessage, string longMessage, string context, IList<KeyValuePair<string, string>> args)
        //{
        //    var app = MongoUtilities.GetSecAppCollection().AsQueryable().First(a => a.Name == appName);

        // var newLogId = MongoUtilities.GetSequenceCollection().FindAndModify(new FindAndModifyArgs
        // { Query = Query<MongoSequence>.Where(s => s.AppId == app.Id && s.CollectionName ==
        // MongoUtilities.LogEntryCollection), Update = Update<MongoSequence>.Inc(s => s.LastNumber,
        // 1), VersionReturned = FindAndModifyDocumentVersion.Modified, Upsert = true, // Creates a
        // new document if it does not exist. }).GetModifiedDocumentAs<MongoSequence>().LastNumber;

        // var logLevelStr = logLevel.ToString().ToLower();
        // MongoUtilities.GetLogEntryCollection().Insert(new MongoLogEntry { Id =
        // MongoUtilities.CreateObjectId(newLogId), AppId = app.Id, LogId = newLogId, Type =
        // logLevelStr, ShortMessage = shortMessage, CodeUnit = codeUnit });

        //    return LogResult.Success;
        //}

        protected override IList<LogEntry> GetEntriesInternal(string appName, LogLevel? logLevel)
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
            if (logLevel != null)
            {
                var logLevelStr = logLevel.ToString().ToLower();
                logEntries = logEntries.Where(l => l.Type == logLevelStr);
            }

            return logEntries.AsEnumerable().Select(l => new LogEntry
            {
                Id = l.LogId,
                AppName = appMap[l.AppId].Name,
                LogLevel = (LogLevel) Enum.Parse(typeof(LogLevel), l.Type, true),
                ShortMessage = l.ShortMessage,
                CodeUnit = l.CodeUnit
            }).ToList();
        }

        protected override IList<LogEntry> QueryEntriesInternal(LogEntryQuery logEntryQuery)
        {
            throw new NotImplementedException();
        }

        protected override Option<LogEntry> GetEntryInternal(string appName, long logId)
        {
            throw new NotImplementedException();
        }

        protected override bool DoRemoveEntry(string appName, int logId)
        {
            throw new NotImplementedException();
        }

        protected override IList<LogSetting> GetSettingsInternal(string appName, LogLevel? logLevel)
        {
            var apps = MongoUtilities.GetSecAppCollection();
            var query = apps.AsQueryable();

            if (appName != null)
            {
                query = query.Where(a => a.Name == appName);
            }

            var logLevelStr = (logLevel == null) ? null : logLevel.ToString().ToLower();

            return (from a in query.AsEnumerable()
                    from s in a.LogSettings.Where(s => logLevelStr == null || s.Type == logLevelStr)
                    select new LogSetting
                    {
                        AppName = a.Name
                    }).ToList();
        }

        protected override bool DoAddSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            // Update preparation
            var logLevelStr = logLevel.ToString().ToLower();
            var query = Builders<MongoSecApp>.Filter.Eq(a => a.Name, appName);
            var update = Builders<MongoSecApp>.Update.AddToSet(a => a.LogSettings, new MongoLogSettings
            {
                Id = MongoUtilities.CreateObjectId(logLevelStr),
                Type = logLevelStr
            });

            // Real update
            var apps = MongoUtilities.GetSecAppCollection();
            return apps.UpdateOneAsync(query, update).Result.IsAcknowledged;
        }

        protected override bool DoUpdateSetting(string appName, LogLevel logLevel, LogSetting setting)
        {
            // Update preparation
            var logLevelStr = logLevel.ToString().ToLower();
            var docId = MongoUtilities.CreateObjectId(logLevelStr);
            var query = Builders<MongoSecApp>.Filter.Eq(a => a.Name, appName);
            var update = Builders<MongoSecApp>.Update.Pull(a => a.LogSettings, new MongoLogSettings
            {
                Id = docId
            }).AddToSet(a => a.LogSettings, new MongoLogSettings
            {
                Id = docId,
                Type = logLevelStr
            });

            // Real update
            var apps = MongoUtilities.GetSecAppCollection();
            return apps.UpdateOneAsync(query, update).Result.IsAcknowledged;
        }

        protected override bool DoRemoveSetting(string appName, LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        protected override Task AddEntryAsyncInternal(string appName, LogEntry logEntry)
        {
            throw new NotImplementedException();
        }

        protected override Task AddEntriesAsyncInternal(string appName, IEnumerable<LogEntry> logEntries)
        {
            throw new NotImplementedException();
        }

        protected override Task CleanUpEntriesAsyncInternal(string appName)
        {
            throw new NotImplementedException();
        }
    }
}