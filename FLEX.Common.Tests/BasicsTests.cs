using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using NUnit.Framework;

namespace FLEX.Common.Web.Tests
{
    public sealed class BasicsTests : TestBase
    {
        [TestCase(1)]
        [TestCase("PROVA")]
        public void OneItemList_Create(object obj)
        {
            var l = new OneItemList<object>(obj);
            Assert.AreEqual(1, l.Count);
            Assert.True(l.Contains(obj));
            Assert.AreEqual(obj, l[0]);
            Assert.AreEqual(0, l.IndexOf(obj));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OneItemList_Add()
        {
            var l = new OneItemList<int>(1);
            l.Add(2);
        }
    }
}
