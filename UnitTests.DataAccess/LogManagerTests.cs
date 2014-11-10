using System;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Security;
using NUnit.Framework;

namespace UnitTests.DataAccess
{
   [TestFixture]
   public class LogManagerTests
   {
      private SecApp _myApp;
      private SecApp _myApp2;

      [SetUp]
      public void Init()
      {
         Db.ClearAllTablesUseOnlyInsideUnitTestsPlease();
           _myApp = new SecApp {Name = "mio_test", Description = "Test Application 1"};
           Db.Security.AddApp(_myApp);
           _myApp2 = new SecApp {Name = "mio_test2", Description = "Test Application 2"};
           Db.Security.AddApp(_myApp2);
      }

      [TearDown]
      public void CleanUp()
      {
      }

      [Test]
      public void LogSettings_EmptyDb()
      {
         var settings = Db.Logger.LogSettings();
         Assert.IsEmpty(settings);
      }

      [Test]
      [ExpectedException(typeof (ArgumentNullException))]
      public void LogSettings_NullAppNameValidLogType_ThrowsArgumentNullException()
      {

      }

      [Test]
      public void LogSettings_NullArg_ReturnListOfAllApps()
      {
      }

      [Test]
      [ExpectedException(typeof (ArgumentNullException))]
      public void LogSettings_NullLogTypeAndValidAppName_ThrowsArgumentNullException()
      {
      }

      [Test]
      public void LogSettings_ValidAppName_ReturnsListForAppName()
      {
      }

      [Test]
      public void LogSettings_ValidArgs_Returns()
      {
      }
   }
}