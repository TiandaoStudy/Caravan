using NUnit.Framework;

namespace FLEX.Common.Web.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        protected const string EmptyString = "";
        protected const string ShortString = "BOOM BABY";
        protected const string MediumString = "Reality continues to ruin my life.";
        protected const string LongString = @"If a man does not keep pace with his companions, perhaps it is because 
                                              he hears a different drummer. Let him step to the music which he hears, 
                                              however measured or far away.";
    }
}
