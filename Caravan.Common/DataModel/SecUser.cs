using System;
using System.Collections.Generic;

namespace Finsa.Caravan.Common.DataModel
{
   [Serializable]
   public class SecUser
   {
      public IEnumerable<SecGroup> Groups { get; set; }
   }
}
