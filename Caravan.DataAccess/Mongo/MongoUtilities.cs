using Finsa.Caravan.DataAccess.Mongo.Models;
using Finsa.Caravan.DataAccess.Mongo.Models.Logging;
using Finsa.Caravan.DataAccess.Mongo.Models.Security;
using Finsa.CodeServices.Security.Hashing;
using Finsa.CodeServices.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Linq;

namespace Finsa.Caravan.DataAccess.Mongo
{
    internal static class MongoUtilities
    {
        #region Constants

        public const string LogEntryCollection = "caravan_log";
        public const string SecAppCollection = "caravan_sec_app";
        public const string SequenceCollection = "caravan_sequence";

        #endregion Constants

        private static readonly JsonSerializer JsonSerializer = new JsonSerializer();

        public static ObjectId CreateObjectId<T>(T obj)
        {
            var md5 = obj.ToMD5Bytes(JsonSerializer);
            var hash12 = md5.Take(12).ToArray();
            Debug.Assert(hash12.Length == 12);
            return new ObjectId(hash12);
        }

        public static IMongoDatabase GetDatabase()
        {
            return new MongoClient(CaravanDataSource.Manager.ConnectionString).GetDatabase(CaravanDataAccessConfiguration.Instance.MongoDbName);
        }

        public static IMongoCollection<MongoLogEntry> GetLogEntryCollection()
        {
            return GetDatabase().GetCollection<MongoLogEntry>(LogEntryCollection);
        }

        public static IMongoCollection<MongoSecApp> GetSecAppCollection()
        {
            return GetDatabase().GetCollection<MongoSecApp>(SecAppCollection);
        }

        public static IMongoCollection<MongoSequence> GetSequenceCollection()
        {
            return GetDatabase().GetCollection<MongoSequence>(SequenceCollection);
        }
    }
}