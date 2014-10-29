using System;

namespace Finsa.Caravan.DataModel.Security
{
   [Serializable]
   public class SecEntry
   {
      public long AppId { get; set; }

      public SecApp App { get; set; }


   }
}
