using System;
using System.Collections.Generic;
using Finsa.Caravan.DataModel;

namespace Finsa.Caravan.DataAccess.Dummy
{
   public sealed class DummySecurityManager : ISecurityManager
   {
      public IEnumerable<SecApp> Apps()
      {
         throw new NotImplementedException();
      }

      public SecApp App(string appName)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<SecGroup> Groups()
      {
         throw new NotImplementedException();
      }

      public IEnumerable<SecGroup> Groups(string appName)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<SecUser> Users()
      {
         throw new NotImplementedException();
      }

      public IEnumerable<SecUser> Users(string appName)
      {
         throw new NotImplementedException();
      }
   }
}
