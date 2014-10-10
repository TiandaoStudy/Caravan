using Finsa.Caravan;
using Finsa.Caravan.Helpers;
using NUnit.Framework;

namespace UnitTests.Helpers
{
   class PairAndTripleTests : TestBase
   {
      [Test]
      public void GPair_Equals()
      {
         var p1 = GPair.Create(1, "2");
         var p2 = GPair.Create(1, "2");
         Assert.True(p1 == p2);
         p2 = GPair.Create(1, "3");
         Assert.False(p1 == p2);
         p2 = GPair.Create(2, "2");
         Assert.False(p1 == p2);
      }

      [Test]
      public void GKeyValuePair_Equals()
      {
         var p1 = GKeyValuePair.Create(1, "2");
         var p2 = GKeyValuePair.Create(1, "2");
         Assert.True(p1 == p2);
         p2 = GKeyValuePair.Create(1, "3");
         Assert.True(p1 == p2); // Same key!
         p2 = GKeyValuePair.Create(2, "2");
         Assert.False(p1 == p2);
      }
   }
}
