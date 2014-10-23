using System.Collections.Generic;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.Mvc.ViewModels.Security
{
   public sealed class SecGroupList
   {
      public IEnumerable<SecGroup> Groups { get; set; }
   }
}