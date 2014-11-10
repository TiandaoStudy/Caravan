//
// ConcurrentWorkQueueTests.cs
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnitTests.Helpers.Threading
{
    internal sealed class ConcurrentWorkQueueTests : TestBase
    {
        private static readonly List<int> IntItems = new List<int>(Enumerable.Range(0, 10000));

        #region ConcurrentList

        [Test]
        public void ConcurrentList_AddOneItem()
        {
            const int item = 1;
            var list = new ConcurrentList<int>();
            AddToListAndCheck(list, item);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void ConcurrentList_AddManyItems_Parallel()
        {
            var list = new ConcurrentList<int>();
            Parallel.ForEach(IntItems, x => AddToListAndCheck(list, x));
            Assert.AreEqual(IntItems.Count, list.Count);
        }

        [Test]
        public void ConcurrentList_AddManyItems_Serial()
        {
            var list = new ConcurrentList<int>();
            foreach (var x in IntItems) AddToListAndCheck(list, x);
            Assert.AreEqual(IntItems.Count, list.Count);
        }

        private static void AddToListAndCheck<T>(ConcurrentList<T> list, T item)
        {
            list.Add(item);
            Assert.GreaterOrEqual(list.Count, 1);
            Assert.IsTrue(list.Contains(item));
        }

        [Test]
        public void ConcurrentList_AddRemoveOneItem()
        {
            const int item = 1;
            var list = new ConcurrentList<int>();
            AddToListAndRemove(list, item);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void ConcurrentList_AddRemoveManyItems_Parallel()
        {
            var list = new ConcurrentList<int>();
            Parallel.ForEach(IntItems, x => AddToListAndRemove(list, x));
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void ConcurrentList_AddRemoveManyItems_Serial()
        {
            var list = new ConcurrentList<int>();
            foreach (var x in IntItems) AddToListAndRemove(list, x);
            Assert.AreEqual(0, list.Count);
        }

        private static void AddToListAndRemove<T>(ConcurrentList<T> list, T item)
        {
            list.Add(item);
            Assert.GreaterOrEqual(list.Count, 1);
            Assert.IsTrue(list.Contains(item));
            list.Remove(item);
            Assert.IsFalse(list.Contains(item));
        }

        #endregion

        #region ConcurrentLinkedList

        [Test]
        public void ConcurrentLinkedList_AddOneItem()
        {
            const int item = 1;
            var list = new ConcurrentLinkedList<int>();
            AddToListAndCheck(list, item);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void ConcurrentLinkedList_AddManyItems_Parallel()
        {
            var list = new ConcurrentLinkedList<int>();
            Parallel.ForEach(IntItems, x => AddToListAndCheck(list, x));
            Assert.AreEqual(IntItems.Count, list.Count);
        }

        [Test]
        public void ConcurrentLinkedList_AddManyItems_Serial()
        {
            var list = new ConcurrentLinkedList<int>();
            foreach (var x in IntItems) AddToListAndCheck(list, x);
            Assert.AreEqual(IntItems.Count, list.Count);
        }

        private static void AddToListAndCheck<T>(ConcurrentLinkedList<T> list, T item)
        {
            list.Add(item);
            Assert.GreaterOrEqual(list.Count, 1);
            Assert.IsTrue(list.Contains(item));
        }

        [Test]
        public void ConcurrentLinkedList_AddRemoveOneItem()
        {
            const int item = 1;
            var list = new ConcurrentLinkedList<int>();
            AddToListAndRemove(list, item);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void ConcurrentLinkedList_AddRemoveManyItems_Parallel()
        {
            var list = new ConcurrentLinkedList<int>();
            Parallel.ForEach(IntItems, x => AddToListAndRemove(list, x));
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void ConcurrentLinkedList_AddRemoveManyItems_Serial()
        {
            var list = new ConcurrentLinkedList<int>();
            foreach (var x in IntItems) AddToListAndRemove(list, x);
            Assert.AreEqual(0, list.Count);
        }

        private static void AddToListAndRemove<T>(ConcurrentLinkedList<T> list, T item)
        {
            list.Add(item);
            Assert.GreaterOrEqual(list.Count, 1);
            Assert.IsTrue(list.Contains(item));
            list.Remove(item);
            Assert.IsFalse(list.Contains(item));
        }

        #endregion
    }
}
