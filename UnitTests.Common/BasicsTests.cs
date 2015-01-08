using System;
using NUnit.Framework;
using PommaLabs.Collections.ReadOnly;

namespace UnitTests.Common
{
    public sealed class BasicsTests : TestBase
    {
        [TestCase(1)]
        [TestCase("PROVA")]
        public void OneItemList_Create(object obj)
        {
            var l = ReadOnlyList.Create(obj);
            Assert.AreEqual(1, l.Count);
            Assert.True(l.Contains(obj));
            Assert.AreEqual(obj, l[0]);
            Assert.AreEqual(0, l.IndexOf(obj));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OneItemList_Add()
        {
            var l = ReadOnlyList.Create(1);
            l.Add(2);
        }
    }
}
