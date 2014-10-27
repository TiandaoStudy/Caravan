//
// StringExtensionsTests.cs
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

using System;
using System.IO;
using Finsa.Caravan.Extensions;
using NUnit.Framework;

namespace UnitTests.Helpers.Extensions
{
    internal class StringExtensionsTests : TestBase
    {
        [Test]
        public void ToBooleanOrDefault_NullString()
        {
            Assert.AreEqual((null as string).ToBooleanOrDefault(), default(bool));
        }

       [Test]
       [ExpectedException(typeof(ArgumentNullException))]
       public void MapPath_NullString()
       {
          (null as string).MapPath();
       }

       [Test]
       public void MapPath_EmptyOrBlankIsBasePath()
       {
          var emptyMap = String.Empty.MapPath();
          var blankMap = "   ".MapPath();
          var baseMap = "~".MapPath();
          Assert.AreEqual(baseMap, emptyMap);
          Assert.AreEqual(baseMap, blankMap);
       }

       [Test]
       public void MapPath_BasePathIsAppDomainDirectory()
       {
          var basePath = "~".MapPath();
          Assert.AreEqual(AppDomain.CurrentDomain.BaseDirectory, basePath);
       }

       [Test]
       public void MapPath_RootedPathIsEqualToRelative()
       {
          var rootedPath = "~/my/test".MapPath();
          var relativePath = "my/test".MapPath();
          Assert.AreEqual(Path.GetFullPath(rootedPath), Path.GetFullPath(relativePath));
       }

       [Test]
       public void MapPath_MappedPathIsAlwaysRooted()
       {
          Assert.IsTrue(Path.IsPathRooted("~/my/test".MapPath()));
          Assert.IsTrue(Path.IsPathRooted("my/test".MapPath()));
          Assert.IsTrue(Path.IsPathRooted("C:/my/test".MapPath()));
       }
    }
}