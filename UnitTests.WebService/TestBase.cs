using Effort.Provider;
using NUnit.Framework;

namespace UnitTests.WebService
{
    [TestFixture]
    internal abstract class TestBase
    {
        protected const int Small = 1;
        protected const int Medium = 10;
        protected const int Large = 20;

        static TestBase()
        {
            EffortProviderConfiguration.RegisterProvider();
        }
    }
}