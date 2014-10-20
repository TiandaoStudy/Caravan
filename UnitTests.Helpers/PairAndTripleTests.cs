using Finsa.Caravan;
using NUnit.Framework;

namespace UnitTests.Helpers
{
   class PairAndTripleTests : TestBase
   {
      [Test]
      public void GPair_Equals()
      {
         var p1 = CPair.Create(1, "2");
         var p2 = CPair.Create(1, "2");
         Assert.True(p1 == p2);
         p2 = CPair.Create(1, "3");
         Assert.False(p1 == p2);
         p2 = CPair.Create(2, "2");
         Assert.False(p1 == p2);
      }

      [Test]
      public void GKeyValuePair_Equals()
      {
         var p1 = CKeyValuePair.Create(1, "2");
         var p2 = CKeyValuePair.Create(1, "2");
         Assert.True(p1 == p2);
         p2 = CKeyValuePair.Create(1, "3");
         Assert.True(p1 == p2); // Same key!
         p2 = CKeyValuePair.Create(2, "2");
         Assert.False(p1 == p2);
      }
   }
}
