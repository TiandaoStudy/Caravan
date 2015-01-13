using MongoDB.Bson;

namespace Finsa.Caravan.DataAccess.Mongo.DataModel.Logging
{
   internal class MongoLogEntry : MongoDocument
   {
      public ObjectId AppId { get; set; }

      public int LogId { get; set; }

      public string Type { get; set; }
   }
}
