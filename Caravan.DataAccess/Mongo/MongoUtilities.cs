﻿using Finsa.Caravan.DataAccess.Mongo.DataModel;
using Finsa.Caravan.DataAccess.Mongo.DataModel.Logging;
using Finsa.Caravan.DataAccess.Mongo.DataModel.Security;
using Finsa.Caravan.DataAccess.Properties;
using MongoDB.Driver;

namespace Finsa.Caravan.DataAccess.Mongo
{
   internal static class MongoUtilities
   {
      #region Constants

      public const string LogEntryCollection = "caravan_log";
      public const string SecAppCollection = "caravan_sec_app";
      public const string SequenceCollection = "caravan_sequence";

      #endregion Constants

      public static MongoDatabase GetDatabase()
      {
         var server = new MongoClient(Db.ConnectionString).GetServer();
         return server.GetDatabase(Settings.Default.MongoDbName);
      }

      public static MongoCollection<MongoLogEntry> GetLogEntryCollection()
      {
         return GetDatabase().GetCollection<MongoLogEntry>(LogEntryCollection);
      }

      public static MongoCollection<MongoSecApp> GetSecAppCollection()
      {
         return GetDatabase().GetCollection<MongoSecApp>(SecAppCollection);
      }

      public static MongoCollection<MongoSequence> GetSequenceCollection()
      {
         return GetDatabase().GetCollection<MongoSequence>(SequenceCollection);
      }
   }
}
