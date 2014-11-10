using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
        public void User_ValidArgs_ReturnsCorrectUser()
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

           Db.Security.AddUser(_myApp.Name, user1);
           Db.Security.User(null, user1.Login);
            
        }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void User_EmptyAppName_ThrowsArgumentException()
       {
          var user1 = new SecUser {FirstName = "pippo", Login = "blabla"};

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.User("", user1.Login);
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
       public void AddUser_LogIncremented_ReturnOK()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla1" };

          var l = Db.Logger.Logs(_myApp.Name);

          Db.Security.AddUser(_myApp.Name, user1);

          var l1 = Db.Logger.Logs(_myApp.Name);

          Assert.That(l1.Count(), Is.EqualTo(l.Count()+1));

          

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
        public void RemoveUser_LogIncremented_ReturnOK()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla1" };

           Db.Security.AddUser(_myApp.Name, user1);

           var l = Db.Logger.Logs(_myApp.Name);

           Db.Security.RemoveUser(_myApp.Name,user1.Login);

           var l1 = Db.Logger.Logs(_myApp.Name);

           Assert.That(l1.Count(), Is.EqualTo(l.Count() + 1));

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
           Assert.That(q.First().Login, Is.EqualTo("updatedLogin".ToLower()));

           var q2 = (from u in Db.Security.Users(_myApp.Name) 
                     where u.Id==user1.Id && u.Login == "blabla" select u).ToList();
           Assert.That(q2.Count,Is.EqualTo(0));
           
        }

        [Test]
        public void UpdateUser_LogIncremented_ReturnOK()
        {
           var user1 = new SecUser { FirstName = "pippo", Login = "blabla1" };

           Db.Security.AddUser(_myApp.Name, user1);

           var l = Db.Logger.Logs(_myApp.Name);

           user1.Login = "updatedLogin";

           Db.Security.UpdateUser(_myApp.Name, "blabla", user1);

           var l1 = Db.Logger.Logs(_myApp.Name);

           Assert.That(l1.Count(), Is.EqualTo(l.Count() + 1));

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

          Db.Security.UpdateUser(_myApp.Name,"blabla",user1);

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

        #region AddUserToGroup_Tests

        [Test]
       public void AddUserToGroup_ValidArgs_UserAddedInCorrectGroup()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var group1 = new SecGroup {Name = "mygroup"};
          var group2 = new SecGroup { Name = "mygroup2" };
          Db.Security.AddUser(_myApp.Name,user1);
          Db.Security.AddGroup(_myApp.Name,group1);
          Db.Security.AddGroup(_myApp.Name, group2);
          Db.Security.AddUserToGroup(_myApp.Name,user1.Login,group1.Name);
           
           user1 = Db.Security.User(_myApp.Name, user1.Login);
           group1 = Db.Security.Group(_myApp.Name, group1.Name);
           group2 = Db.Security.Group(_myApp.Name, group2.Name);

           Assert.True(user1.Groups.Contains(group1));
           Assert.False(user1.Groups.Contains(group2));
           Assert.AreEqual(1, user1.Groups.Count);

           Assert.True(group1.Users.Contains(user1));
           Assert.False(group2.Users.Contains(user1));
           Assert.AreEqual(1, group1.Users.Count);
           Assert.AreEqual(0, group2.Users.Count);

       }
       
       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void AddUserToGroup_NullAppName_ThrowsArgumentException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var group1 = new SecGroup { Name = "mygroup" };
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddUserToGroup(null, user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void AddUserToGroup_EmptyAppName_ThrowsArgumentException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var group1 = new SecGroup { Name = "mygroup" };
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddUserToGroup("", user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void AddUserToGroup_NullUserLogin_ThrowsArgumentException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var group1 = new SecGroup { Name = "mygroup" };
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddUserToGroup(_myApp.Name, null, group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void AddUserToGroup_EmptyUserLogin_ThrowsArgumentException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var group1 = new SecGroup { Name = "mygroup" };
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddUserToGroup(_myApp.Name, "", group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void AddUserToGroup_NullGroupName_ThrowsArgumentException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var group1 = new SecGroup { Name = "mygroup" };
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddUserToGroup(_myApp.Name, user1.Login, null);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void AddUserToGroup_EmptyGroupName_ThrowsArgumentException()
       {
          var user1 = new SecUser { FirstName = "pippo", Login = "blabla", Email = "test@email.it" };
          var group1 = new SecGroup { Name = "mygroup" };
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddUserToGroup(_myApp.Name, user1.Login, "");
       }

       #endregion

        #region Groups_Tests

        [Test]
        public void Groups_ValidArgs_ReturnsListOfGroups()
        {
           var group1 = new SecGroup() {Name = "g1"};
           var group2 = new SecGroup() {Name = "g2"};

           Db.Security.AddGroup(_myApp.Name,group1);
           Db.Security.AddGroup(_myApp.Name,group2);

           var retValue = Db.Security.Groups(_myApp.Name);
           Assert.That(retValue.Count(),Is.EqualTo(2));

           var retValue2 = Db.Security.Groups(_myApp2.Name);
           Assert.That(retValue2.Count(),Is.EqualTo(0));

        }

       [Test]
       public void Groups_NoGroups_ReturnsNoGroups()
       {
          var retvalue = Db.Security.Groups(_myApp.Name);
          Assert.That(retvalue.Count(),Is.EqualTo(0));
       }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Groups_NullAppName_ThrowsArgumentException()
        {
           Db.Security.Groups(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Groups_EmptyAppName_ThrowsArgumentException()
        {
           Db.Security.Groups("");
        }



        #endregion

        #region Group_Tests

        [Test]
        public void Group_ValidArgs_ReturnsOK()
        {
           var group1 = new SecGroup{Name = "my_group"};

           Db.Security.AddGroup(_myApp.Name,group1);

           var ret = Db.Security.Group(_myApp.Name, group1.Name);

           Assert.That(ret.Name, Is.EqualTo("my_group"));
           Assert.That(ret,Is.Not.Null);

        }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void Group_NullAppName_ThrowsArgumentException()
       {
            var group1 = new SecGroup { Name = "my_group" };

            Db.Security.AddGroup(_myApp.Name, group1);

            Db.Security.Group(null, group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Group_EmptyAppName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };

          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.Group("", group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Group_NullGroupName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };

          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.Group(_myApp.Name, null);

       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void Group_EmptyGroupName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };

          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.Group(_myApp.Name, "");
       }

        #endregion

       #region AddGroup_Tests

       [Test]
       public void AddGroup_validArgs_InsertCorrectGroup()
       {
          var group1 = new SecGroup { Name = "my_group" };
          var group2 = new SecGroup {Name = "other_group"};

          Db.Security.AddGroup(_myApp.Name,group1);
          Db.Security.AddGroup(_myApp.Name,group2);

          var q1 = (from g in Db.Security.Groups(_myApp.Name) 
                   where g.Name == group1.Name select g).ToList();

          Assert.That(q1.First().Name, Is.EqualTo("my_group"));

          var q2 = (from g in Db.Security.Groups(_myApp.Name)
                   where g.Name == group2.Name select g).ToList();

          Assert.That(q2.First().Name, Is.EqualTo("other_group"));

          var q3 = (from g in Db.Security.Groups(_myApp2.Name) 
                    where g.Name == group1.Name || g.Name == group2.Name select g).ToList();

          Assert.That(q3.Count(),Is.EqualTo(0));
       }

       [Test]
       public void AddGroup_InsertSameGroupInDifferentApps_ReturnsOK()
       {
          var group1 = new SecGroup { Name = "my_group" };
          
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddGroup(_myApp2.Name, group1);

          var q = (from g in Db.Security.Groups(_myApp.Name) where g.Name == group1.Name select g).ToList();
          Assert.That(q.First().Name, Is.EqualTo("my_group"));

          var q2 = (from g in Db.Security.Groups(_myApp2.Name) where g.Name == group1.Name select g).ToList();
          Assert.That(q2.First().Name, Is.EqualTo("my_group"));
       }

       [Test]
       [ExpectedException(typeof (GroupExistingException))]
       public void AddGroup_GroupAlreadyExisting_ThrowsGroupExistingException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          var group2 = new SecGroup { Name = "my_group" };

          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.AddGroup(_myApp.Name, group2);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void AddGroup_NullAppName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(null, group1);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void AddGroup_EmptyAppName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup("", group1);
       }

       [Test]
       [ExpectedException(typeof(ArgumentNullException))]
       public void AddGroup_NullNewGroup_ThrowsArgumentNullException()
       {
          Db.Security.AddGroup(_myApp.Name, null);
       }
       
      #endregion 

       #region RemoveGroup_Tests

       [Test]
       public void RemoveGroup_validArgs_RemovedGroup()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.RemoveGroup(_myApp.Name,group1.Name);

          var q = (from g in Db.Security.Groups(_myApp.Name) where g.Name == group1.Name select g).ToList();

          Assert.That(q.Count(),Is.EqualTo(0));
       }

       [Test]
       [ExpectedException(typeof (GroupNotFoundException))]
       public void RemoveGroup_GroupNotExisting_ThrowsGroupNotFoundException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.RemoveGroup(_myApp.Name, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveGroup_NullAppName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.RemoveGroup(null, group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void RemoveGroup_EmptyAppName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.RemoveGroup("", group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void RemoveGroup_EmptyGroupName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.RemoveGroup(_myApp.Name, "");
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void RemoveGroup_NullGroupName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.RemoveGroup(_myApp.Name, null);
       }


       #endregion

       #region UpdateGroup_Tests

       [Test]
       public void UpdateGroup_ValidArgs_GroupUpdated()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);

          group1.Name = "updated_group";
          Db.Security.UpdateGroup(_myApp.Name,"my_group",group1);

          var q = (from g in Db.Security.Groups(_myApp.Name)
              where g.Id == group1.Id && g.Name == group1.Name select g).ToList();

          Assert.That(q.Count(),Is.EqualTo(1));
          Assert.That(q.First().Name,Is.EqualTo("updated_group"));

          var q2 = (from g in Db.Security.Groups(_myApp.Name)
                   where g.Id == group1.Id && g.Name == "my_group"
                   select g).ToList();

          Assert.That(q2.Count(), Is.EqualTo(0));
       }

       [Test]
       [ExpectedException(typeof (GroupNotFoundException))]
       public void UpdateGroup_GroupNotExisting_ThrowsGroupNotFoundException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          
          group1.Name = "updated_group";
          Db.Security.UpdateGroup(_myApp.Name, "my_group", group1);
       }

       [Test]
       [ExpectedException(typeof (GroupExistingException))]
       public void UpdateGroup_AlreadyexistingGroup_ThrowsGroupExistingException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          var group2 = new SecGroup {Name = "updated_group"};

          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddGroup(_myApp.Name, group2);

          group1.Name = "updated_group";
          Db.Security.UpdateGroup(_myApp.Name, "my_group", group1);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void UpdateGroup_NullAppName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          group1.Name = "updated_group";
          Db.Security.UpdateGroup(null, "my_group", group1);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void UpdateGroup_EmptyAppName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          group1.Name = "updated_group";
          Db.Security.UpdateGroup("", "my_group", group1);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void UpdateGroup_NullGroupName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          group1.Name = "updated_group";
          Db.Security.UpdateGroup(_myApp.Name, null, group1);
      }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void UpdateGroup_EmptyGroupName_ThrowsArgumentException()
       {
          var group1 = new SecGroup { Name = "my_group" };
          Db.Security.AddGroup(_myApp.Name, group1);
          group1.Name = "";
          Db.Security.UpdateGroup(_myApp.Name, "my_group", group1);
       }

       #endregion

         #region Apps_Tests

         [Test]
         public void Apps_ValidArgs_ReturnlistOfApps()
         {
            var a = Db.Security.Apps();

            Assert.That(a.Count(),Is.EqualTo(2));
            Assert.That(a.Contains(_myApp));
            Assert.That(a.Contains(_myApp2));
         }

         #endregion

         #region App_Tests

       [Test]
       public void App_ValidArgs_ReturnsApp()
       {
          var a = Db.Security.App(_myApp.Name);
          Assert.That(a,Is.Not.Null);
          Assert.That(a.Name, Is.EqualTo("mio_test"));
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void App_NullAppName_ThrowsArgumentException()
       {
           Db.Security.App(null);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void App_EmptyAppName_ThrowsArgumentException()
       {
          Db.Security.App("");
       }

         #endregion

         #region AddApp_Tests

       [Test]
       public void AddApp_ValidArgs_AppAdded()
       {
          var newApp = new SecApp() {Name = "AddedApp", Description = "my new application"};
          Db.Security.AddApp(newApp);
          var a = Db.Security.Apps();
          Assert.That(a.Contains(newApp));
       }

       [Test]
       [ExpectedException(typeof (AppExistingException))]
       public void AddApp_AlreadyExistingAppName_ThrowsAppExistingException()
       {
          var newApp = new SecApp() { Name = "mio_test", Description = "my new application" };
          Db.Security.AddApp(newApp);
       }

       [Test]
       [ExpectedException(typeof (ArgumentNullException))]
       public void AddApp_NullApp_ThrowsArgumentNullException()
       {
          Db.Security.AddApp(null);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void AddApp_EmptyAppName_ThrowsArgumentException()
       {
          var newApp = new SecApp() { Name = "", Description = "my new application" };
          Db.Security.AddApp(newApp);
       }

         #endregion

       #region Contexts_Tests

       [Test]
       
       public void Contexts_ValidArgs_ReturnslistOfContexts() //null groupName
       {
          var c1 = new SecContext {Name = "c1", Description = "context1"};
          var c2 = new SecContext {Name = "c2", Description = "context2"};

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var user1 = new SecUser {FirstName = "user1", Login = "myLogin"};
          var group1 = new SecGroup {Name = "my_group"};

          Db.Security.AddUser(_myApp.Name,user1);
          Db.Security.AddGroup(_myApp.Name,group1);

          Db.Security.AddEntry(_myApp.Name,c1,obj1,user1.Login,null);

          //obj1 = new SecObject
          //{
          //   Name = "obj1",
          //   Description = "oggetto1",
          //   Type = "button"
          //};

          Db.Security.AddEntry(_myApp.Name,c2,obj1,user1.Login,null);

          var l = Db.Security.Contexts(_myApp.Name);

          Assert.That(l.Count(),Is.EqualTo(2));
          Assert.That(l.Contains(c1));
          Assert.That(l.Contains(c2));
       }

       [Test]
       public void Contexts_ValidArgs1_ReturnslistOfContexts() //NullGroupName
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var c2 = new SecContext { Name = "c2", Description = "context2" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          var group1 = new SecGroup { Name = "my_group" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);
          Db.Security.AddEntry(_myApp.Name, c2, obj1, user1.Login, null);

          var l = Db.Security.Contexts(_myApp.Name);

          Assert.That(l.Count(), Is.EqualTo(2));
          Assert.That(l.Contains(c1));
          Assert.That(l.Contains(c2));
       }

       [Test]
       public void Contexts_ValidArgs2_ReturnslistOfContexts() //Null Userlogin
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var c2 = new SecContext { Name = "c2", Description = "context2" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          var group1 = new SecGroup { Name = "my_group" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);
          Db.Security.AddEntry(_myApp.Name, c2, obj1, null, group1.Name);

          var l = Db.Security.Contexts(_myApp.Name);

          Assert.That(l.Count(), Is.EqualTo(2));
          Assert.That(l.Contains(c1));
          Assert.That(l.Contains(c2));
       }

       [Test]
       public void Contexts_ValidArgs3_ReturnslistOfContexts() //Null Userlogin
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var c2 = new SecContext { Name = "c2", Description = "context2" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          var group1 = new SecGroup { Name = "my_group" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);
          Db.Security.AddEntry(_myApp.Name, c2, obj1, null, group1.Name);

          var l = Db.Security.Contexts(_myApp.Name);

          Assert.That(l.Count(), Is.EqualTo(2));
          Assert.That(l.Contains(c1));
          Assert.That(l.Contains(c2));
       }


       [Test]
       public void Contexts_NotExistingContext_ReturnsZero()
       {
         
          var l = Db.Security.Contexts(_myApp.Name);

          Assert.That(l.Count(), Is.EqualTo(0));
          
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void Contexts_NullAppName_ThrowsArgumentException()
       {
          Db.Security.Contexts(null);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Contexts_EmptyAppName_ThrowsArgumentException()
       {
          Db.Security.Contexts("");
       }
       
       #endregion

       #region Objects_Tests

       [Test]
       public void Objects_ValidArgs_ReturnsListOfObjects()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          var group1 = new SecGroup { Name = "my_group" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          var l = Db.Security.Objects(_myApp.Name);

          Assert.That(l.Count(),Is.EqualTo(1));

          var l1 = Db.Security.Objects(_myApp2.Name);

          Assert.That(l1.Count(),Is.EqualTo(0));

       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void Objects_NullAppName_ThrowsArgumentException()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { App = _myApp, Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin"};

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name,group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.Objects(null);
       }


       

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Objects_EmptyAppName_ThrowsArgumentException()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup {Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.Objects("");
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Objects_NotExistingAppName_ThrowsArgumentException()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.Objects("_app");
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Objects_NotExistingContextName_ThrowsArgumentException()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.Objects(_myApp.Name,"cra");
       }

       #endregion

       #region Entries_tests

       [Test]
       public void Entries_validArgs_ReturnsListOfEntries()//associo un gruppo e un utente allo stesso oggetto
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          var l = Db.Security.Entries(_myApp.Name, c1.Name);
          var l1 = Db.Security.EntriesForObject(_myApp.Name,c1.Name,obj1.Name);
          var l3 = Db.Security.Entries(_myApp.Name, c1.Name, user1.Login);
          
          Assert.That(l.Count(),Is.EqualTo(2));
          Assert.That(l3.Count(), Is.EqualTo(1));
          Assert.That(l1.Count(), Is.EqualTo(2));

       }

       [Test]
       public void Entries_NoEntries_ReturnsZero()
       {
            var c1 = new SecContext { Name = "c1", Description = "context1" };
            var group1 = new SecGroup { Name = "my_group" };
            var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

            Db.Security.AddUser(_myApp.Name, user1);
            Db.Security.AddGroup(_myApp.Name, group1);
          
            var l = Db.Security.Entries(_myApp.Name, c1.Name);

            Assert.That(l.Count(), Is.EqualTo(0));
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void Entries_NullAppName_throwsArgumentException()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          var l = Db.Security.Entries(null, c1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_EmptyAppName_throwsArgumentException()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          var l = Db.Security.Entries("", c1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_NullContextName_throwsArgumentException()
       {
          //var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.Entries(_myApp.Name, null);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_EmptyContextName_throwsArgumentException()
       {
          //var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);

          Db.Security.Entries(_myApp.Name, "");
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_EmptyObjectname_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

         Db.Security.EntriesForObject(_myApp.Name, c1.Name,"");
          
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_NullObjectname_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.EntriesForObject(_myApp.Name, c1.Name, null);

       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_EmptyUserLogin_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.Entries(_myApp.Name, c1.Name, "");

       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_NullUserLogin_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.Entries(_myApp.Name, c1.Name, null);

       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_EmptyGroupsName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.Entries(_myApp.Name, c1.Name, user1.Login);

       }
       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_NullGroupsName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

         Db.Security.Entries(_myApp.Name, c1.Name, user1.Login);

       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_EmptyUserLoginAsFourthParam_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.EntriesForObject(_myApp.Name, c1.Name,obj1.Name, "");

       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_NullUserLoginAsFourthParam_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.EntriesForObject(_myApp.Name, c1.Name,obj1.Name, null);

       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_EmptyGroupsNameAsFifthParam_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.EntriesForObject(_myApp.Name, c1.Name,obj1.Name, user1.Login);

       }
       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void Entries_NullGroupsNameAsFifthParam_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          Db.Security.EntriesForObject(_myApp.Name, c1.Name,obj1.Name, user1.Login);

       }

       #endregion

       #region AddEntry_Tests

       [Test]
       public void Addentry_ValidArgsNullUserLogin_EntryAdded()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);

          var l = Db.Security.Entries(_myApp.Name, c1.Name);

          Assert.That(l.Count(),Is.EqualTo(1));
          Assert.That(l.First().Context.Name,Is.EqualTo("c1"));
          Assert.That(l.First().Object.Name,Is.EqualTo("obj1"));
          Assert.That(l.First().Group.Name, Is.EqualTo("my_group"));

       }

       [Test]
       public void Addentry_ValidArgsNullGroupName_EntryAdded()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          var l = Db.Security.Entries(_myApp.Name, c1.Name);

          Assert.That(l.Count(), Is.EqualTo(1));
          Assert.That(l.First().Context.Name, Is.EqualTo("c1"));
          Assert.That(l.First().Object.Name, Is.EqualTo("obj1"));
          Assert.That(l.First().User.Login, Is.EqualTo("mylogin"));

       }

       [Test]
       public void AddEntry_Insert2DifferentUsersOnTheSameObject_EntryAdded()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          var user2 = new SecUser { FirstName = "user2", Login = "myLogin2" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

         
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddUser(_myApp.Name, user2);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user2.Login, null);

          var l = Db.Security.EntriesForObject(_myApp.Name, c1.Name, obj1.Name);

          Assert.That(l.Count(),Is.EqualTo(2));

          var l1 = Db.Security.EntriesForObject(_myApp.Name, c1.Name, obj1.Name, user1.Login);

          Assert.That(l1.Count(),Is.EqualTo(1));

          var l2 = Db.Security.EntriesForObject(_myApp.Name, c1.Name, obj1.Name, user2.Login);

          Assert.That(l2.Count(),Is.EqualTo(1));
          
       }

       [Test]
       public void AddEntry_InsertTheSameUserOnDifferentObjects_EntryAdded()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          //var user2 = new SecUser { FirstName = "user2", Login = "myLogin2" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var obj2 = new SecObject
          {
             Name = "obj2",
             Description = "oggetto2",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
         // Db.Security.AddUser(_myApp.Name, user2);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);
          Db.Security.AddEntry(_myApp.Name, c1, obj2, user1.Login, null);

          var l1 = Db.Security.Entries(_myApp.Name, c1.Name, user1.Login);

          Assert.That(l1.Count(), Is.EqualTo(2));
         
       }

       [Test]
       public void AddEntry_InsertDifferentGroupsOnTheSameObject_EntryAdded()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var group2 = new SecGroup {Name = "group2"};
          

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          
         Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddGroup(_myApp.Name, group2);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group2.Name);

         var l1 = Db.Security.EntriesForObject(_myApp.Name, c1.Name, obj1.Name);

          Assert.That(l1.Count(), Is.EqualTo(2));
       }

       [Test]
       public void AddEntry_InsertTheSameGroupOnDifferentObjects_EntryAdded()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var obj2 = new SecObject
          {
             Name = "obj2",
             Description = "oggetto2",
             Type = "button"
          };

         
         Db.Security.AddGroup(_myApp.Name, group1);
         Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);
         Db.Security.AddEntry(_myApp.Name, c1, obj2, null, group1.Name);
          
          var l1 = Db.Security.Entries(_myApp.Name, c1.Name);

          Assert.That(l1.Count(), Is.EqualTo(2));

          var l2 = Db.Security.EntriesForObject(_myApp.Name, c1.Name, obj1.Name);

          Assert.That(l2.Count(), Is.EqualTo(1));

          Assert.That(l2.First().Group.Name, Is.EqualTo("my_group"));

          var l3 = Db.Security.EntriesForObject(_myApp.Name, c1.Name, obj2.Name);

          Assert.That(l3.Count(), Is.EqualTo(1));

          Assert.That(l3.First().Group.Name, Is.EqualTo("my_group"));

       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void AddEntry_EmptyUserLogin_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, "", group1.Name);
       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void AddEntry_EmptyGroupName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1,user1.Login, "");
       }

       [Test]
       [ExpectedException(typeof (GroupExistingException))]
       public void AddEntry_GroupAlreadyExisting_Throws()//same context,App,obj,group;
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var group2 = new SecGroup { Name = "my_group2" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          
          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

        
          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group2);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group1.Name);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, null, group2.Name);

       }

       [Test]
       [ExpectedException(typeof(UserExistingException))]
       public void AddEntry_UserAlreadyExisting_Throws()//same app,context,obj, user
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          
          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };
          
          Db.Security.AddUser(_myApp.Name, user1);
          
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

       }

       [Test]
       public void AddEntry_InsertTwiceTheSameUserDifferentObject_EntriesAdded()//same context and App; different object
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          var user2 = new SecUser { FirstName = "user2", Login = "myLogin2" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var obj2 = new SecObject
          {
             Name = "obj2",
             Description = "oggetto2",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddUser(_myApp.Name, user2);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);
          Db.Security.AddEntry(_myApp.Name, c1, obj2, user1.Login, null);

          var l = Db.Security.Entries(_myApp.Name, c1.Name, user1.Login);
          
          Assert.That(l.Count(),Is.EqualTo(2));
          


       }

       #endregion

       #region RemoveEntry_Tests

       [Test]
       public void RemoveEntry_ValidArgs_EntryRemoved()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
          var user2 = new SecUser { FirstName = "user2", Login = "myLogin2" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          var obj2 = new SecObject
          {
             Name = "obj2",
             Description = "oggetto2",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddUser(_myApp.Name, user2);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);
          Db.Security.AddEntry(_myApp.Name, c1, obj2, user1.Login, null);
        
          Db.Security.RemoveEntry(_myApp.Name,c1.Name,obj1.Name,user1.Login,group1.Name);
          Db.Security.RemoveEntry(_myApp.Name, c1.Name, obj2.Name, user1.Login, group1.Name);

          var l = Db.Security.Entries(_myApp.Name, c1.Name, obj1.Name);
          Assert.That(l.Count(),Is.EqualTo(0));

          var l1 = Db.Security.Entries(_myApp.Name, c1.Name, obj2.Name);
          Assert.That(l1.Count(), Is.EqualTo(0));

       }

       [Test]
       [ExpectedException(typeof(ArgumentException))]
       public void RemoveEntry_NullAppName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };
         
          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(null, c1.Name, obj1.Name, user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_EmptyAppName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry("", c1.Name, obj1.Name, user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_NullContextName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, null, obj1.Name, user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntryEmptyContextName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, "", obj1.Name, user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_EmptyObjectName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, c1.Name, "", user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_NullObjectName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, c1.Name, null, user1.Login, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_NullUserLogin_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, c1.Name, obj1.Name, null, group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_EmptyUserLogin_Throws()
       {

          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, c1.Name, obj1.Name, "", group1.Name);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_NullGroupName_Throws()
       {

          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, c1.Name, obj1.Name, user1.Login, null);
       }

       [Test]
       [ExpectedException(typeof (ArgumentException))]
       public void RemoveEntry_EmptyGroupName_Throws()
       {
          var c1 = new SecContext { Name = "c1", Description = "context1" };
          var group1 = new SecGroup { Name = "my_group" };
          var user1 = new SecUser { FirstName = "user1", Login = "myLogin" };

          var obj1 = new SecObject
          {
             Name = "obj1",
             Description = "oggetto1",
             Type = "button"
          };

          Db.Security.AddUser(_myApp.Name, user1);
          Db.Security.AddGroup(_myApp.Name, group1);
          Db.Security.AddEntry(_myApp.Name, c1, obj1, user1.Login, null);

          Db.Security.RemoveEntry(_myApp.Name, c1.Name, obj1.Name, user1.Login, "");
       }

       #endregion


    }
}
