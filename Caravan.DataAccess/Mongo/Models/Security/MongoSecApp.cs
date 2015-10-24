using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Drivers.Mongo.Models.Logging;

namespace Finsa.Caravan.DataAccess.Drivers.Mongo.Models.Security
{
    internal class MongoSecApp : MongoDocument
    {
        public long AppId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<MongoLogSettings> LogSettings { get; set; }
    }
}