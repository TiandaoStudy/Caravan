using MongoDB.Bson;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo.Models
{
    internal class MongoSequence : MongoDocument
    {
        public string CollectionName { get; set; }

        public ObjectId AppId { get; set; }

        public long LastNumber { get; set; }
    }
}