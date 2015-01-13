namespace Finsa.Caravan.DataAccess.Mongo.DataModel
{
   internal class MongoSequence : MongoDocument
   {
      public string CollectionName { get; set; }

      public string AppName { get; set; }

      public long LastNumber { get; set; }
   }
}
