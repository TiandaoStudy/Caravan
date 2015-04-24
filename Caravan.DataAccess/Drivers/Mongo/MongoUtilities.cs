using System.Diagnostics;
using System.Linq;
using Finsa.Caravan.Common.Utilities.Extensions;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Logging;
using Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel.Security;
using Finsa.Caravan.DataAccess.Properties;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo
{
    internal static class MongoUtilities
    {
        #region Constants

        public const string LogEntryCollection = "caravan_log";
        public const string SecAppCollection = "caravan_sec_app";
        public const string SequenceCollection = "caravan_sequence";

        #endregion Constants

        public static ObjectId CreateObjectId<T>(T obj)
        {
            var md5 = obj.ToMD5Bytes();
            var hash12 = md5.Take(12).ToArray();
            Debug.Assert(hash12.Length == 12);
            return new ObjectId(hash12);
        }

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