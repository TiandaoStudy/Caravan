using MongoDB.Bson;

namespace Finsa.Caravan.DataAccess.Mongo.Models
{
    internal abstract class MongoDocument
    {
        public ObjectId Id { get; set; }
    }
}