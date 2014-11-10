using Finsa.Caravan;
using NUnit.Framework;

namespace UnitTests.Helpers
{
   class EnvironmentTests : TestBase
   {
      [Test]
      public void AppIsRunningOnAspNet_FalseForUnitTests()
      {
         Assert.IsFalse(CEnvironment.AppIsRunningOnAspNet);
      }
   }
}
