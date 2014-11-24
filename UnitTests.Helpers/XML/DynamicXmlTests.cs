//
// DynamicXmlTests.cs
// 
// Author(s):
//     Alessio Parma <alessio.parma@gmail.com>
//
// The MIT License (MIT)
// 
// Copyright (c) 2014-2016 Alessio Parma <alessio.parma@gmail.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Finsa.Caravan.Xml;
using NUnit.Framework;

namespace UnitTests.Helpers.Xml
{
    internal sealed class DynamicXmlTests : TestBase
    {
        [Test]
        public void Parse_SimpleXml_OneAttribute()
        {
            const string xml = @"
                <UnitTest>
                   <Elems Value=""1"">
                      <Elem>1</Elem>
                      <Elem>2</Elem>
                   </Elems> 
                </UnitTest>
            ";
            dynamic obj = DynamicXml.Parse(xml);
            Assert.AreEqual("1", obj.Elems.Value);
        }

        [Test]
        public void Parse_SimpleXml_OneElement()
        {
            const string xml = @"
                <UnitTest>
                   <Elem>1</Elem>
                </UnitTest>
            ";
            dynamic obj = DynamicXml.Parse(xml);
            Assert.AreEqual("1", obj.Elem);
        }

        [Test]
        public void Parse_SimpleXml_TwoAttributes()
        {
            const string xml = @"
                <UnitTest>
                   <Elems Value=""1"" AnotherValue=""2"">
                      <Elem>1</Elem>
                      <Elem>2</Elem>
                   </Elems> 
                </UnitTest>
            ";
            dynamic obj = DynamicXml.Parse(xml);
            Assert.AreEqual("1", obj.Elems.Value);
            Assert.AreEqual("2", obj.Elems.AnotherValue);
        }

        [Test]
        public void Parse_SimpleXml_TwoElements()
        {
            const string xml = @"
                <UnitTest>
                   <Elems>
                      <Elem>1</Elem>
                      <Elem>2</Elem>
                   </Elems> 
                </UnitTest>
            ";
            dynamic obj = DynamicXml.Parse(xml);
            Assert.AreEqual("1", obj.Elems.Elem[0].Value);
            Assert.AreEqual("2", obj.Elems.Elem[1].Value);
        }
    }
}