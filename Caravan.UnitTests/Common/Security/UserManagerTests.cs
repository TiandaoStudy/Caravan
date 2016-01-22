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
    internal sealed class UserManagerTests : AbstractTests
    {
        [Test]
        public async Task CreateAsync_InvalidEmail()
        {
            var appId = await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName,
                Description = TestAppDescription
            });

            using (var userManager = await UserManagerFactory.CreateAsync(TestAppName))
            {
                var result = await userManager.CreateAsync(new SecUser
                {
                    Login = TestUserLogin1,
                    Email = "not an email!!!"
                });

                Assert.That(result.Errors.Count(), Is.EqualTo(1));
            }
        }

        [Test]
        public async Task CreateAsync_ValidEmail()
        {
            var appId = await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName,
                Description = TestAppDescription
            });

            using (var userManager = await UserManagerFactory.CreateAsync(TestAppName))
            {
                var result = await userManager.CreateAsync(new SecUser
                {
                    Login = TestUserLogin1,
                    Email = "a@b.c"
                });

                Assert.That(result.Errors.Count(), Is.EqualTo(0));
            }
        }

        [Test]
        public async Task CreateAsync_NullEmail()
        {
            var appId = await SecurityRepository.AddAppAsync(new SecApp
            {
                Name = TestAppName,
                Description = TestAppDescription
            });

            using (var userManager = await UserManagerFactory.CreateAsync(TestAppName))
            {
                var result = await userManager.CreateAsync(new SecUser
                {
                    Login = TestUserLogin1,
                    Email = null
                });

                Assert.That(result.Errors.Count(), Is.EqualTo(0));
            }
        }
    }
}
