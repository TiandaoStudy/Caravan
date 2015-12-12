using Finsa.Caravan.Common.Security.Models;
using NUnit.Framework;
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
        public async Task GetApps_ShouldReturnCollectionIfThereAreNoApps()
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
    }
}