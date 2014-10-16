using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecGroup
   {
      public int Id { get; set; }

      public IEnumerable<SecUser> Users { get; set; } 
   }
}
