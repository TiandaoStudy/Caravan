using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTests.DataAccess
{

    [TestFixture]
    class SecurityManagerTests
    {
        public SecurityManagerTests()
        {

        }

        [SetUp]
        public void Init()
        {

        }

        [TearDown]
        public void CleanUp()
        {

        }

        #region Users_Tests

        [Test]
        public void Users_Insert2Users_ReturnsListOfUsers()
        {
            string name_user1= "pippo";
            string login_user1 = "blabla1";
            string name_user2 = "pluto";
            string login_user2 = "blabla2";
            string appName = "mio_test";

            //creare 2 oggetti SecUser
            
            //creare App con "appName"
            
            //chiamare AddUser passando i parametri creati
            //AddUser(appName,)
            
            //richiamare il metodo da testare (Users)
            //IEnumerable<SecUser> retValue = Users(appName);
            //Assert.That(retValue.Count(),IsEqualTo(2));
            
        }

        [Test]
        public void Users_NoUsers_ReturnsNull()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Users_NullAppName_ThrowsArgumentNullException()
        {

        }

        //[Test]

        //public void Users_NotExistingAppName_ThrowsArgumentException()
        //{

        //}

        #endregion

        #region User_Tests
        [Test]
        public void User_ValidArgs_ReturnsUser()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_NullAppName_ThrowsArgumentNullException()
        {

        }

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void User_NotExistingAppName_ThrowsArgumentException()
        //{

        //}

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_NullUserLogin_ThrowsArgumentNullException()
        {

        }

        #endregion

        #region AddUser_Tests

        [Test]
        public void AddUser_ValidArgs_InsertOk()
        {

        }

        [Test]
        [ExpectedException]//inserire il tipo di eccezione aspettata
        public void AddUser_UserAlreadyPresent_ThrowsException()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullAppName_ThrowsArgymentNullException()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullNewUser_ThrowsArgymentNullException()
        {

        }

        #endregion

        #region RemoveUser_Tests

        [Test]
        public void RemoveUser_ValidArgs_RemoveUser()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveUser_NullAppName_ThrowsArgumentNullException()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveUser_NullUserLogin_ThrowsArgumentNullException(){

        }

        #endregion

        #region UpdateUser_Tests

        [Test]
        public void UpdateUser_ValidArgs_UserUpdated()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateUser_NullAppName_ThrowsArgumentNullException()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateUser_NullUserLogin_ThrowsArgumentNullException()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateUser_NullNewUserArg_ThrowsArgumentNullException()
        {

        }

        #endregion
    }
}
