using System;
using Finsa.Caravan.DataAccess;
using NUnit.Framework;

namespace UnitTests.DataAccess
{
   [TestFixture]
   public class LogManagerTests
   {
      [SetUp]
      public void Init()
      {
         //connessione al db? 
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