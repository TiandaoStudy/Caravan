using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecUser
   {
      public long Id { get; set; }

      public string Login { get; set; }

      public string HashedPassword { get; set; }

      public IEnumerable<SecGroup> Groups { get; set; }
   }
}
