using System.Collections.Generic;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Security;

namespace Finsa.Caravan.Mvc.ViewModels.Security
{
   public sealed class SecGroupList
   {
      public IEnumerable<SecGroup> Groups { get; set; }
   }
}