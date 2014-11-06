using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.DataModel.Exceptions;
using Finsa.Caravan.DataAccess;

namespace UnitTests.DataAccess
{

    [TestFixture]
    class SecurityManagerTests
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

        #region Users_Tests

        [Test]
        public void Users_Insert2Users_ReturnsListOfUsers()
        {
           
            var user1 = new SecUser {FirstName = "pippo", Login = "blabla"};
            var user2 = new SecUser {FirstName = "pluto", Login = "blabla2"};

            Db.Security.AddUser(_myApp.Name, user1);
            Db.Security.AddUser(_myApp.Name, user2);

            IEnumerable<SecUser> retValue = Db.Security.Users(_myApp.Name);
            Assert.That(retValue.Count(),Is.EqualTo(2));

            var q = (from user in Db.Security.Users(_myApp2.Name) 
                     where (user.Login==user1.Login || user.Login==user2.Login) select user).ToList();
           
            Assert.That(q.Count(),Is.EqualTo(0));
            
        }

       
        [Test]
        public void Users_NoUsers_ReturnsNull()
        {
            
            IEnumerable<SecUser> retValue = Db.Security.Users(_myApp.Name);
            Assert.That(retValue.Count(), Is.EqualTo(0));

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Users_NullAppName_ThrowsArgumentException()
        {
            Db.Security.Users(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Users_EmptyAppName_ThrowsArgumentException()
        {
           Db.Security.Users("");
        }

        #endregion

        #region User_Tests
        [Test]
        public void User_ValidArgs_ReturnsUser()
        {
           var user1 = new SecUser {FirstName = "pippo", Login = "blabla"};

           Db.Security.AddUser(_myApp.Name, user1);

           var u = Db.Security.User(_myApp.Name, user1.Login);
           Assert.That(u,Is.Not.Null);
           Assert.That(u.FirstName,Is.EqualTo("pippo"));
           Assert.That(u.Login,Is.EqualTo("blabla"));
        }

       
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void User_NullAppName_ThrowsArgumentException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla" };

           Db.Security.AddUser(null, user1);
            
        }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void User_EmptyAppName_ThrowsArgumentException()
       {
          var user1 = new SecUser {FirstName = "pippo", Login = "blabla"};

          Db.Security.AddUser("", user1);
       }

       [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void User_NullUserLogin_ThrowsArgumentException()
        {
            Db.Security.User(_myApp.Name, null);

        }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void User_EmptyUserLogin_ThrowsArgumentException()
       {
          Db.Security.User(_myApp.Name, "");

       }

        #endregion

        #region AddUser_Tests

        [Test]
        public void AddUser_ValidArgs_InsertOk()
        {
           var user1 = new SecUser {FirstName = "pippo", Login = "blabla1"};
           var user2 = new SecUser {FirstName = "pluto", Login = "blabla2"};

           Db.Security.AddUser(_myApp.Name, user1);
           Db.Security.AddUser(_myApp.Name, user2);

            //verifico che sia stato inserito user1
           var q = (from c in Db.Security.Users(_myApp.Name) 
                     where ((c.FirstName==user1.FirstName) && (c.Login == user1.Login)) select c).ToList();

            Assert.That(q.First().FirstName,Is.EqualTo("pippo"));
            Assert.That(q.First().Login, Is.EqualTo("blabla1"));

            //verifico che sia stato inserito correttamente user2
            var q2 = (from c in Db.Security.Users(_myApp.Name) 
                      where ((c.FirstName==user2.FirstName) && (c.Login == user2.Login)) select c).ToList();

            Assert.That(q2.First().FirstName,Is.EqualTo("pluto"));
            Assert.That(q2.First().Login, Is.EqualTo("blabla2"));

        }

        [Test]
        public void AddUser_InsertSameUserInDifferentApps_InsertOk()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla1" };
           
           Db.Security.AddUser(_myApp.Name, user1);
           Db.Security.AddUser(_myApp2.Name, user1);

           var q = (from c in Db.Security.Users(_myApp.Name)
                    where ((c.FirstName == user1.FirstName) && (c.Login == user1.Login))
                    select c).ToList();

           Assert.That(q.First().FirstName, Is.EqualTo("pippo"));
           Assert.That(q.First().Login, Is.EqualTo("blabla1"));

           //verifico che sia stato inserito correttamente user2
           var q2 = (from c in Db.Security.Users(_myApp2.Name)
                     where ((c.FirstName == user1.FirstName) && (c.Login == user1.Login))
                     select c).ToList();

           Assert.That(q2.First().FirstName, Is.EqualTo("pippo"));
           Assert.That(q2.First().Login, Is.EqualTo("blabla1"));

        }

        [Test]
        [ExpectedException(typeof(UserExistingException))]
        public void AddUser_UserLoginAlreadyPresent_ThrowsException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla1" };
           var user2 = new SecUser { FirstName = "pluto", Login = "blabla1" };

            Db.Security.AddUser(_myApp.Name, user1);
            Db.Security.AddUser(_myApp.Name, user2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AddUser_NullAppName_ThrowsArgumentException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla1" };
            Db.Security.AddUser(null,user1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AddUser_EmptyAppName_ThrowsArgumentException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla1" }; 
           Db.Security.AddUser("", user1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUser_NullNewUser_ThrowsArgumentNullException()
        {
            Db.Security.AddUser(_myApp.Name, null);
        }

        #endregion

        #region RemoveUser_Tests

        [Test]
        public void RemoveUser_ValidArgs_RemoveUser()
        {
           var user1 = new SecUser {FirstName = "pippo",Login = "blabla"};

           Db.Security.AddUser(_myApp.Name, user1);

           Db.Security.RemoveUser(_myApp.Name, user1.Login);

           var q = (from u in Db.Security.Users(_myApp.Name) where u.Login == user1.Login select u).ToList();

           Assert.That(q.Count(),Is.EqualTo(0));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveUser_NullAppName_ThrowsArgumentException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla" };

           Db.Security.RemoveUser(null, user1.Login);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveUser_EmptyAppName_ThrowsArgumentException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla" };

           Db.Security.RemoveUser("", user1.Login);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveUser_NullUserLogin_ThrowsArgumentException()
        {
           Db.Security.RemoveUser(_myApp.Name, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveUser_EmptyUserLogin_ThrowsArgumentException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "" };

           Db.Security.RemoveUser(_myApp.Name, user1.Login);
        }

       [Test]
       [ExpectedException(typeof (UserNotFoundException))]
       public void RemoveUser_UserNotFound_ThrowsUserNotFoundException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "sarav" };

          Db.Security.RemoveUser(_myApp.Name, user1.Login);
       }

        #endregion

        #region UpdateUser_Tests

        [Test]
        public void UpdateUser_ValidArgs_UserUpdated()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it"};

           Db.Security.AddUser(_myApp.Name,user1);

           user1.Login = "updatedLogin"; 

           Db.Security.UpdateUser(_myApp.Name,"blabla",user1);

           var q = (from u in Db.Security.Users(_myApp.Name)
                    where u.Id == user1.Id && u.Login == user1.Login select u).ToList();

           Assert.That(q.Count(),Is.EqualTo(1));
           Assert.That(q.First().Login, Is.EqualTo("updatedLogin"));

           var q2 = (from u in Db.Security.Users(_myApp.Name) 
                     where u.Id==user1.Id && u.Login == "blabla" select u).ToList();
           Assert.That(q2.Count,Is.EqualTo(0));
           
        }

       [Test]
       [ExpectedException(typeof(UserNotFoundException))]
        public void UpdateUser_UserNotExisting_ThrowsUserNotFoundException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };

          user1.Login = "updatedLogin";

          Db.Security.UpdateUser(_myApp.Name, user1.Login, user1);

       }

       [Test]
       [ExpectedException(typeof (UserExistingException))]
       public void UpdateUser_UserUpdatedAlreadyExisting_ThrowsUserExistingException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var user2 = new SecUser { FirstName = "pluto", Login = "bobobo", Email = "test2@email.it" };

          Db.Security.AddUser(_myApp.Name,user1);
          Db.Security.AddUser(_myApp.Name,user2);

          user1.Login = "bobobo";

          Db.Security.UpdateUser(_myApp.Name,user1.Login,user1);

       }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUser_NullAppName_ThrowsArgumentNullException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };

           user1.Login = "updatedLogin";

           Db.Security.UpdateUser(null, user1.Login, user1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUser_EmptyAppName_ThrowsArgumentNullException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };

           user1.Login = "updatedLogin";

           Db.Security.UpdateUser("", user1.Login, user1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUser_NullUserLogin_ThrowsArgumentNullException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };

           user1.Login = null;

           Db.Security.UpdateUser(_myApp.Name, user1.Login, user1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateUser_EmptyUserLogin_ThrowsArgumentNullException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };

           user1.Login = "";

           Db.Security.UpdateUser(_myApp.Name, user1.Login, user1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateUser_NullNewUserArg_ThrowsArgumentNullException()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };

           user1.Login = "blobloblo";

           Db.Security.UpdateUser(_myApp.Name, user1.Login, null);
        }

        #endregion

        #region Groups_Tests

        [Test]
        public void Groups_ValidArgs_ReturnsListOfGroups()
        {

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Groups_NullAppName_ThrowsArgumentException()
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
