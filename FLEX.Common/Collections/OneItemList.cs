using System;
using System.Collections;
using System.Collections.Generic;

namespace FLEX.Common.Collections
{
   public sealed class OneItemList<T> : IList<T>
   {
      private readonly T _item;

      public OneItemList(T item)
      {
         _item = item;
      }

      public int Count
      {
         get { return 1; }
      }

      public bool IsReadOnly
      {
         get { return true; }
      }

      public T this[int x]
      {
         get
         {
            if (x > 0) ThrowExc();
            return _item;
         }
         set { ThrowExc(); }
      }

      public void Add(T item)
      {
         ThrowExc();
      }

      public void Clear()
      {
         ThrowExc();
      }

      public bool Contains(T item)
      {
         return Equals(_item, item);
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
         ThrowExc();
      }

      public IEnumerator<T> GetEnumerator()
      {
         yield return _item;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public int IndexOf(T item)
      {
         return Equals(_item, item) ? 0 : -1;
      }

      public void Insert(int index, T item)
      {
         ThrowExc();
      }

      public bool Remove(T item)
      {
         ThrowExc();
         return default(bool);
      }

      public void RemoveAt(int index)
      {
         ThrowExc();
      }

      private static void ThrowExc()
      {
         throw new InvalidOperationException();
      }
   }
}