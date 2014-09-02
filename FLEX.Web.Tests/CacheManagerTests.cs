using FLEX.Web;
using NUnit.Framework;

namespace FLEX.Extensions.Web.Tests
{
    public sealed class CacheManagerTests : TestBase
    {
        private const int AggregateKeyLength = 47;

        [TestCase(EmptyString)]
        [TestCase(ShortString)]
        [TestCase(MediumString)]
        [TestCase(LongString)]
        public void CreateAggregateKey_String_CorrectResultLength(string str)
        {
            var key = CacheManager.CreateAggregateKey(str);
            Assert.AreEqual(AggregateKeyLength, key.Length);
        }

        [TestCase(EmptyString, ShortString)]
        [TestCase(ShortString, MediumString)]
        [TestCase(MediumString, LongString)]
        [TestCase(LongString, EmptyString)]
        public void CreateAggregateKey_TwoStrings_CorrectResultLength(string str1, string str2)
        {
            var key = CacheManager.CreateAggregateKey(str1, str2);
            Assert.AreEqual(AggregateKeyLength, key.Length);
        }

        [TestCase(EmptyString, ShortString)]
        [TestCase(ShortString, MediumString)]
        [TestCase(MediumString, LongString)]
        [TestCase(LongString, EmptyString)]
        public void CreateAggregateKey_String_DifferentResults(string str1, string str2)
        {
            var key1 = CacheManager.CreateAggregateKey(str1);
            var key2 = CacheManager.CreateAggregateKey(str2);
            Assert.AreNotEqual(key1, key2);
        }

        [TestCase(EmptyString, ShortString)]
        [TestCase(ShortString, MediumString)]
        [TestCase(MediumString, LongString)]
        [TestCase(LongString, EmptyString)]
        public void CreateAggregateKey_TwoStrings_DifferentResults(string str1, string str2)
        {
            var key1 = CacheManager.CreateAggregateKey(str1, str1);
            var key2 = CacheManager.CreateAggregateKey(str2, str2);
            Assert.AreNotEqual(key1, key2);
        }

        [TestCase(EmptyString)]
        [TestCase(ShortString)]
        [TestCase(MediumString)]
        [TestCase(LongString)]
        public void CreateAggregateKey_String_TwoTimes(string str)
        {
            var key1 = CacheManager.CreateAggregateKey(str);
            var key2 = CacheManager.CreateAggregateKey(str);
            Assert.AreEqual(key1, key2);
        }

        [TestCase(EmptyString, ShortString)]
        [TestCase(ShortString, MediumString)]
        [TestCase(MediumString, LongString)]
        [TestCase(LongString, EmptyString)]
        public void CreateAggregateKey_TwoStrings_TwoTimes(string str1, string str2)
        {
            var key1 = CacheManager.CreateAggregateKey(str1, str2);
            var key2 = CacheManager.CreateAggregateKey(str1, str2);
            Assert.AreEqual(key1, key2);
        }
    }
}
