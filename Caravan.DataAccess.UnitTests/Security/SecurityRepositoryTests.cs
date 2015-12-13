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

namespace Finsa.Caravan.DataAccess.UnitTests.Security
{
    internal sealed class SecurityRepositoryTests : AbstractTests
    {
        private const string TestAppName = "mytest";
        private const string TestAppDescription = "My TEST";

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