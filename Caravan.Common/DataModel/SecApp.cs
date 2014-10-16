using System;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecApp
   {
      public long Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
   }
}
