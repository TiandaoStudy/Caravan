using System;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Security;
using NUnit.Framework;

namespace UnitTests.DataAccess
{
   [TestFixture]
   public class LogManagerTests
   {
      [SetUp]
      public void Init()
      {
         Db.ClearAllTablesUseOnlyInsideUnitTestsPlease();
         Db.Security.AddApp(new SecApp {Name = "test-app-1", Description = "Test Application 1"});
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