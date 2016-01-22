// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Finsa.Caravan.Common.Security.Models;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Finsa.Caravan.UnitTests.Common.Security
{
    internal sealed class SecurityRepositoryTests : AbstractTests
    {
        private const string TestGroupName1 = "group1";

        private const string TestRoleName1 = "role1";

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
        }

        #region Apps

        [Test]
        public async Task GetApps_ShouldReturnEmptyArrayIfThereAreNoApps()
        {
            var apps = await SecurityRepository.GetAppsAsync();
            Assert.That(apps.Length, Is.EqualTo(0));
        }

        [Test]
        public async Task AddApp_ShouldReadAppAfterItHasBeenAdded()
        {
            var appId = await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName,
                Description = TestAppDescription
            });

            var apps = await SecurityRepository.GetAppsAsync();
            Assert.That(apps.Length, Is.EqualTo(1));

            var app = apps[0];
            Assert.That(app.Id, Is.EqualTo(appId));
            Assert.That(app.Name, Is.EqualTo(TestAppName));
            Assert.That(app.Description, Is.EqualTo(TestAppDescription));
        }

        #endregion Apps

        #region Groups

        [Test]
        public async Task QueryUsersInGroup_ShouldWorkIfThereAreNoUsers()
        {
            await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName
            });
            await SecurityRepository.AddGroupAsync(TestAppName, new SecGroup
            {
                Name = TestGroupName1
            });

            var users = await SecurityRepository.QueryUsersInGroupAsync(TestAppName, TestGroupName1);
            Assert.That(users.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task QueryUsersInGroup_ShouldReturnUsersInRole()
        {
            await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName
            });
            await SecurityRepository.AddGroupAsync(TestAppName, new SecGroup
            {
                Name = TestGroupName1
            });
            await SecurityRepository.AddRoleAsync(TestAppName, TestGroupName1, new SecRole
            {
                Name = TestRoleName1
            });
            await SecurityRepository.AddUserAsync(TestAppName, new SecUser
            {
                Login = TestUserLogin1
            });
            await SecurityRepository.AddUserToRoleAsync(TestAppName, TestUserLogin1, TestGroupName1, TestRoleName1);

            var users = await SecurityRepository.QueryUsersInGroupAsync(TestAppName, TestGroupName1);
            Assert.That(users.Count(), Is.EqualTo(1));

            var user = users.First();
            Assert.That(user.Login, Is.EqualTo(TestUserLogin1));
        }

        #endregion

        #region Roles

        [Test]
        public async Task QueryUsersInRole_ShouldWorkIfThereAreNoUsers()
        {
            await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName
            });
            await SecurityRepository.AddGroupAsync(TestAppName, new SecGroup
            {
                Name = TestGroupName1
            });
            await SecurityRepository.AddRoleAsync(TestAppName, TestGroupName1, new SecRole
            {
                Name = TestRoleName1
            });

            var users = await SecurityRepository.QueryUsersInRoleAsync(TestAppName, TestGroupName1, TestRoleName1);
            Assert.That(users.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task QueryUsersInRole_ShouldReturnUsersInRole()
        {
            await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName
            });
            await SecurityRepository.AddGroupAsync(TestAppName, new SecGroup
            {
                Name = TestGroupName1
            });
            await SecurityRepository.AddRoleAsync(TestAppName, TestGroupName1, new SecRole
            {
                Name = TestRoleName1
            });
            await SecurityRepository.AddUserAsync(TestAppName, new SecUser
            {
                Login = TestUserLogin1
            });
            await SecurityRepository.AddUserToRoleAsync(TestAppName, TestUserLogin1, TestGroupName1, TestRoleName1);

            var users = await SecurityRepository.QueryUsersInRoleAsync(TestAppName, TestGroupName1, TestRoleName1);
            Assert.That(users.Count(), Is.EqualTo(1));

            var user = users.First();
            Assert.That(user.Login, Is.EqualTo(TestUserLogin1));
        }

        #endregion

        #region Users

        [Test]
        public async Task QueryUsers_ShouldWorkIfThereAreNoUsers()
        {
            var appId = await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName,
                Description = TestAppDescription
            });

            var users = await SecurityRepository.QueryUsersAsync(TestAppName);
            Assert.That(users.Count(), Is.EqualTo(0));
        }

        #endregion
    }
}