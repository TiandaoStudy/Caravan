using System.Collections.Generic;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess
{
   public interface ISecurityManager
   {
      IEnumerable<SecApp> Applications();

      SecApp Application(string applicationName);
   }
}
