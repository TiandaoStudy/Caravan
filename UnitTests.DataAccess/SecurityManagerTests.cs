using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.DataAccess;

namespace UnitTests.DataAccess
{

    [TestFixture]
    class SecurityManagerTests
    {
        [SetUp]
        public void Init()
        {
           Db.ClearAllTablesUseOnlyInsideUnitTestsPlease();
           Db.Security.AddApp(new SecApp {Name = "mio_test", Description = "Test Application 1"});
        }

        [TearDown]
        public void CleanUp()
        {

        }

        #region Users_Tests

        [Test]
        public void Users_Insert2Users_ReturnsListOfUsers()
        {
            
            string appName = "mio_test";//da commentare
           
            var user1 = new SecUser();
            var user2 = new SecUser();
      
            Db.Security.AddUser(appName,user1);
            Db.Security.AddUser(appName,user2);
            
            IEnumerable<SecUser> retValue = Db.Security.Users(appName);
            Assert.That(retValue.Count(),Is.EqualTo(2));            
            
        }

        [Test]
        public void Users_NoUsers_ReturnsNull()
        {
            string appName = "mio_test"; //da commentare

            IEnumerable<SecUser> retValue = Db.Security.Users(appName);
            Assert.That(retValue, Is.EqualTo(null));

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Users_NullAppName_ThrowsArgumentNullException()
        {

            Db.Security.Users(null);
        }

        #endregion

        #region User_Tests
        [Test]
        public void User_ValidArgs_ReturnsUser()
        {
            string appName = "mio_test"; //da commentare
            SecUser user1 = new SecUser();
            user1.FirstName = "pippo";
            user1.Login = "blabla";
            
            Db.Security.AddUser(appName,user1);

           var u= Db.Security.User(appName,user1.Login);
           Assert.That(u,Is.Not.Null);
           Assert.That(u.FirstName,Is.EqualTo("pippo"));
           Assert.That(u.Login,Is.EqualTo("blabla"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_NullAppName_ThrowsArgumentNullException()
        {
            SecUser user1 = new SecUser();
           
            ;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void User_NullUserLogin_ThrowsArgumentNullException()
        {
            string appName = "mio_test"; //da commentare
            SecUser user1 = new SecUser();

            Db.Security.User(appName,null);

        }

        #endregion

        #region AddUser_Tests

        [Test]
        public void AddUser_ValidArgs_InsertOk()
        {
            string name_user1= "pippo";
            string login_user1 = "blabla1";
            string name_user2 = "pluto";
            string login_user2 = "blabla2";
            string appName = "mio_test";//da commentare
            
            var user1 = new SecUser();
            var user2 = new SecUser();

            user1.FirstName = name_user1;
            user1.Login = login_user1;

            user2.FirstName = name_user2;
            user2.Login = login_user2;

            Db.Security.AddUser(appName,user1);
            Db.Security.AddUser(appName,user2);

            //verifico che sia stato inserito user1
            var q = (from c in Db.Security.Users(appName) 
                     where ((c.FirstName==user1.FirstName) && (c.Login == user1.Login)) select c);

            Assert.That(q.First().FirstName,Is.EqualTo("pippo"));
            Assert.That(q.First().Login, Is.EqualTo("blabla1"));

            //verifico che sia stato inserito correttamente user2
            var q2 = (from c in Db.Security.Users(appName) 
                      where ((c.FirstName==user2.FirstName) && (c.Login == user2.Login)) select c);

            Assert.That(q2.First().FirstName,Is.EqualTo("pluto"));
            Assert.That(q2.First().Login, Is.EqualTo("blabla2"));

        }

        [Test]
        [ExpectedException]//inserire il tipo di eccezione aspettata: la duplicazione è sulla login?
        public void AddUser_UserAlreadyPresent_ThrowsException()
        {
            string name_user1= "pippo";
            string login_user1 = "blabla1";
            string name_user2 = "pippo";
            string login_user2 = "blabla1";
            string appName = "mio_test";//da commentare
            
            var user1 = new SecUser();
            var user2 = new SecUser();

            user1.FirstName = name_user1;
            user1.Login = login_user1;

            user2.FirstName = name_user2;
            user2.Login = login_user2;

            Db.Security.AddUser(appName,user1);
            Db.Security.AddUser(appName,user2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullAppName_ThrowsArgumentNullException()
        {
            var user1 = new SecUser();
            Db.Security.AddUser(null,user1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullNewUser_ThrowsArgumentNullException()
        {
             string appName = "mio_test";//da commentare
             Db.Security.AddUser(appName,null);
        }

        #endregion

        #region RemoveUser_Tests

        [Test]
        public void RemoveUser_ValidArgs_RemoveUser()
        {
            string name_user1= "pippo";
            string login_user1 = "blabla1";
            string appName = "mio_test";//da commentare
            var user1 = new SecUser();
            Db.Security.AddUser(appName,user1);
            Db.Security.RemoveUser(appName,user1.Login);
            var q = from u in Db.Security.Users(appName) where u.Login==user1.Login select u;
            Assert.That(q.Count(),Is.EqualTo(0));
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

        #region Groups_Tests

        [Test]
        public void Groups_ValidArgs_ReturnsListOfGroups()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Groups_NullAppName_ThrowsArgumentNullException()
        {

        }

        #endregion

        #region Group_Tests

        [Test]
        public void Group_ValidArgs_ReturnsASecGroupObject()
        {

        }
        

        #endregion

    }
}
