using MongoDB.Bson;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo.DataModel
{
   internal abstract class MongoDocument
   {
      public ObjectId Id { get; set; }
   }
}
