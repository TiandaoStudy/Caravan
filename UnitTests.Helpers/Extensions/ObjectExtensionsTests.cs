using System;
using Finsa.Caravan.Extensions;
using NUnit.Framework;

namespace UnitTests.Helpers.Extensions
{
    internal class ObjectExtensionsTests : TestBase
    {
        private static void AssertIsMD5(byte[] bytes)
        {
            Assert.IsNotEmpty(bytes);
            Assert.AreEqual(16, bytes.Length);
        }

        private static void AssertIsMD5(string str)
        {
            Assert.IsNotEmpty(str);
            Assert.AreEqual(32, str.Length);
        }

        [Test]
        public void ToMD5Bytes_ComplexObject()
        {
            AssertIsMD5(new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5Bytes());
        }

        [Test]
        public void ToMD5Bytes_SameComplexObjects()
        {
            var firstMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5Bytes();
            var secondMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5Bytes();
            Assert.AreEqual(firstMD5, secondMD5);
        }

        [Test]
        public void ToMD5Bytes_DifferentComplexObjects()
        {
            var firstMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5Bytes();
            var secondMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2}}, e = "WOW"}.ToMD5Bytes();
            Assert.AreNotEqual(firstMD5, secondMD5);
        }

        [Test]
        public void ToMD5Bytes_EmptyString()
        {
            AssertIsMD5(String.Empty.ToMD5Bytes());
        }

        [Test]
        public void ToMD5Bytes_NullObject()
        {
            AssertIsMD5((null as object).ToMD5Bytes());
        }

        [Test]
        public void ToMD5String_ComplexObject()
        {
            AssertIsMD5(new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5String());
        }

        [Test]
        public void ToMD5String_SameComplexObjects()
        {
            var firstMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5String();
            var secondMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5String();
            Assert.AreEqual(firstMD5, secondMD5);
        }

        [Test]
        public void ToMD5String_DifferentComplexObjects()
        {
            var firstMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2, 3}}}.ToMD5String();
            var secondMD5 = new {a = 1, b = new {c = "test", d = new[] {1, 2}}, e = "WOW"}.ToMD5String();
            Assert.AreNotEqual(firstMD5, secondMD5);
        }

        [Test]
        public void ToMD5String_EmptyString()
        {
            AssertIsMD5(String.Empty.ToMD5String());
        }

        [Test]
        public void ToMD5String_NullObject()
        {
            AssertIsMD5((null as object).ToMD5String());
        }
    }
}