using MongoDB.Bson;

namespace Finsa.Caravan.DataAccess.Mongo.DataModel.Logging
{
   internal class MongoLogEntry : MongoDocument
   {
      public ObjectId AppId { get; set; }

      public long LogId { get; set; }

      public string Type { get; set; }

      public string CodeUnit { get; set; }

      public string ShortMessage { get; set; }
   }
}
