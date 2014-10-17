using System;
using System.Collections.Generic;

namespace Finsa.Caravan.DataModel
{
   [Serializable]
   public class SecGroup
   {
      public long Id { get; set; }

      public SecApp App { get; set; }

      public string Name { get; set; }

      public string Description { get; set; }

      public bool IsAdmin { get; set; }

      public virtual ICollection<SecUser> Users { get; set; } 
   }
}
