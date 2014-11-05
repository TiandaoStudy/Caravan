using Finsa.Caravan.DataAccess;
using NUnit.Framework;

namespace UnitTests.DataAccess
{
   [TestFixture]
   public class LogManagerTests
   {
      [Test]
      public void LogSettings_EmptyDb()
      {
         var settings = Db.Logger.LogSettings();
         Assert.IsEmpty(settings);
      }
   }
}