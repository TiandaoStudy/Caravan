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

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void LogSettings_NotExistingAppName_ThrowsArgumenException()
        //{

        //}
                
    }
}
