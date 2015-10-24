using MongoDB.Bson;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo.Models
{
    internal abstract class MongoDocument
    {
        public ObjectId Id { get; set; }
    }
}