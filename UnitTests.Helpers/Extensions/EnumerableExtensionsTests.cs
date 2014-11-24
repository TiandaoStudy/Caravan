//
// EnumerableExtensionsTests.cs
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
using System.Collections.Generic;
using System.Dynamic;
using Finsa.Caravan.Extensions;
using NUnit.Framework;
using UnitTests.Helpers;

namespace UnitTests.Extensions
{
   class EnumerableExtensionsTests : TestBase
   {
      [Test]
      public void ToDataTable_Dynamic_EmptyEnumerable_NoColumns()
      {
         var list = new List<dynamic>();
         var table = list.ToDataTable();
         Assert.AreEqual(0, table.Columns.Count);
         Assert.AreEqual(0, table.Rows.Count);
      }

      [Test]
      public void ToDataTable_Dynamic_OneItemEnumerable_NoColumns()
      {
         var list = new List<dynamic> {new ExpandoObject()};
         var table = list.ToDataTable();
         Assert.AreEqual(0, table.Columns.Count);
         Assert.AreEqual(1, table.Rows.Count);
      }

      [Test]
      public void ToDataTable_Dynamic_OneItemEnumerable_NullValues()
      {
         dynamic item = new ExpandoObject();
         item.A = null;
         item.B = null;
         var list = new List<dynamic> {item};
         var table = list.ToDataTable();
         Assert.AreEqual(2, table.Columns.Count);
         Assert.AreEqual(1, table.Rows.Count);
         Assert.IsInstanceOf<DBNull>(table.Rows[0][0]);
         Assert.IsInstanceOf<DBNull>(table.Rows[0][1]);
      }

      [Test]
      public void ToDataTable_Dynamic_OneItemEnumerable()
      {
         dynamic item = new ExpandoObject();
         item.A = 1;
         item.B = "TEST";
         var list = new List<dynamic> {item};
         var table = list.ToDataTable();
         Assert.AreEqual(2, table.Columns.Count);
         Assert.AreEqual(1, table.Rows.Count);
         Assert.AreEqual(1, table.Rows[0][0]);
         Assert.AreEqual("TEST", table.Rows[0][1]);
      }

      [Test]
      public void ToDataTable_Dynamic_TwoItemsEnumerable_NullableTypes()
      {
         dynamic item = new ExpandoObject();
         item.A = null;
         item.B = null;
         var list = new List<dynamic> {item};
         item = new ExpandoObject();
         item.A = 19;
         item.B = 3;
         list.Add(item);

         var table = list.ToDataTable();
         Assert.AreEqual(2, table.Columns.Count);
         Assert.AreEqual(2, table.Rows.Count);

         Assert.IsInstanceOf<DBNull>(table.Rows[0][0]);
         Assert.IsInstanceOf<DBNull>(table.Rows[0][1]);

         Assert.IsInstanceOf<int>(table.Rows[1][0]);
         Assert.IsInstanceOf<int>(table.Rows[1][1]);
         Assert.AreEqual(19, table.Rows[1][0]);
         Assert.AreEqual(3, table.Rows[1][1]);
      }
   }
}
