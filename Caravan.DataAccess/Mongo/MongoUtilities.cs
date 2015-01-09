using Finsa.Caravan.DataAccess.Properties;
using MongoDB.Driver;

namespace Finsa.Caravan.DataAccess.Mongo
{
   internal static class MongoUtilities
   {
      #region Constants

      public const string LogEntriesCollection = "caravan_log";
      public const string SecAppCollection = "caravan_sec_app";

      #endregion Constants

      public static MongoDatabase GetDatabase()
      {
         var server = new MongoClient(Db.ConnectionString).GetServer();
         return server.GetDatabase(Settings.Default.MongoDbName);
      }
   }
}
