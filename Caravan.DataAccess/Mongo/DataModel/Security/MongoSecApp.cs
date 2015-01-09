using System.Collections.Generic;
using Finsa.Caravan.DataAccess.Mongo.DataModel.Logging;

namespace Finsa.Caravan.DataAccess.Mongo.DataModel.Security
{
   public class MongoSecApp
   {
      public long Id { get; set; }

      public string Name { get; set; }

      public List<MongoLogSettings> LogSettings { get; set; } 
   }
}