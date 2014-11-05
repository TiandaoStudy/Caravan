using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void LogSettings_NullArg_ReturnListOfAllApps()
        {

        }
        [Test]
        public void LogSettings_ValidAppName_ReturnsListForAppName(){

        }

        [Test]
        public void LogSettings_ValidArgs_Returns()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LogSettings_NullLogTypeAndValidAppName_ThrowsArgumentNullException()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LogSettings_NullAppNameValidLogType_ThrowsArgumentNullException()
        {

        }
        
         [Test]
         public void LogSettings_EmptyDb()
      {
         var settings = Db.Logger.LogSettings();
         Assert.IsEmpty(settings);
      }
    }
}