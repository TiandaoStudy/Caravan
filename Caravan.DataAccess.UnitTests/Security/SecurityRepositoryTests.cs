using NUnit.Framework;
using System.Threading.Tasks;

namespace Finsa.Caravan.DataAccess.UnitTests.Security
{
    internal sealed class SecurityRepositoryTests : AbstractTests
    {
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

        #endregion Apps
    }
}