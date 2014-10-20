//
// PairAndTriple.cs
// 
// Author(s):
//     Alessio Parma <alessio.parma@finsa.it>
//
// The MIT License (MIT)
// 
// Copyright (c) 2014-2024 Finsa S.p.A. <finsa@finsa.it>
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
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan
{
   [Serializable, JsonObject(MemberSerialization.OptIn)]
   public sealed class CPair<T1, T2> : IEquatable<CPair<T1, T2>>, IList<object>
   {
      [JsonProperty]
      public T1 First { get; set; }

      [JsonProperty]
      public T2 Second { get; set; }

      #region IEquatable<CPair<T1,T2>> Members

      public bool Equals(CPair<T1, T2> other)
      {
         return EqualityComparer<T1>.Default.Equals(First, other.First) && EqualityComparer<T2>.Default.Equals(Second, other.Second);
      }

      #endregion

      #region Object Members

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
         {
            return false;
         }
         return obj is CPair<T1, T2> && Equals((CPair<T1, T2>) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (EqualityComparer<T1>.Default.GetHashCode(First)*397) ^ EqualityComparer<T2>.Default.GetHashCode(Second);
         }
      }

      public static bool operator ==(CPair<T1, T2> left, CPair<T1, T2> right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(CPair<T1, T2> left, CPair<T1, T2> right)
      {
         return !left.Equals(right);
      }

      public override string ToString()
      {
         return string.Format("First: [{0}], Second: [{1}]", First, Second);
      }

      #endregion

      #region IList<object> Members

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public IEnumerator<object> GetEnumerator()
      {
         yield return First;
         yield return Second;
      }

      public void Add(object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public void Clear()
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public bool Contains(object item)
      {
         return Equals(First, item) || Equals(Second, item);
      }

      public void CopyTo(object[] array, int arrayIndex)
      {
         array[arrayIndex] = First;
         array[arrayIndex + 1] = Second;
      }

      public bool Remove(object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public int Count
      {
         get { return 2; }
      }

      public bool IsReadOnly
      {
         get { return false; }
      }

      public int IndexOf(object item)
      {
         return Equals(First, item) ? 0 : Equals(Second, item) ? 1 : -1;
      }

      public void Insert(int index, object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public void RemoveAt(int index)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public object this[int index]
      {
         get { return (index == 0) ? First : (index == 1) ? Second : (object) null; }
         set
         {
            switch (index)
            {
               case 0:
                  First = (T1) value;
                  break;
               case 1:
                  Second = (T2) value;
                  break;
            }
         }
      }

      #endregion
   }

   public static class CPair
   {
      public static CPair<T1, T2> Create<T1, T2>(T1 first, T2 second)
      {
         return new CPair<T1, T2> {First = first, Second = second};
      }
   }

   [Serializable, JsonObject(MemberSerialization.OptIn)]
   public sealed class CKeyValuePair<T1, T2> : IEquatable<CKeyValuePair<T1, T2>>, IList<object>
   {
      [JsonProperty]
      public T1 Key { get; set; }

      [JsonProperty]
      public T2 Value { get; set; }

      #region IEquatable<CKeyValuePair<T1,T2>> Members

      public bool Equals(CKeyValuePair<T1, T2> other)
      {
         return EqualityComparer<T1>.Default.Equals(Key, other.Key);
      }

      #endregion

      #region Object Members

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
         {
            return false;
         }
         return obj is CKeyValuePair<T1, T2> && Equals((CKeyValuePair<T1, T2>) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            return (EqualityComparer<T1>.Default.GetHashCode(Key)*397);
         }
      }

      public static bool operator ==(CKeyValuePair<T1, T2> left, CKeyValuePair<T1, T2> right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(CKeyValuePair<T1, T2> left, CKeyValuePair<T1, T2> right)
      {
         return !left.Equals(right);
      }

      public override string ToString()
      {
         return string.Format("Key: [{0}], Value: [{1}]", Key, Value);
      }

      #endregion

      #region IList<object> Members

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public IEnumerator<object> GetEnumerator()
      {
         yield return Key;
         yield return Value;
      }

      public void Add(object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public void Clear()
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public bool Contains(object item)
      {
         return Equals(Key, item) || Equals(Value, item);
      }

      public void CopyTo(object[] array, int arrayIndex)
      {
         array[arrayIndex] = Key;
         array[arrayIndex + 1] = Value;
      }

      public bool Remove(object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public int Count
      {
         get { return 2; }
      }

      public bool IsReadOnly
      {
         get { return false; }
      }

      public int IndexOf(object item)
      {
         return Equals(Key, item) ? 0 : Equals(Value, item) ? 1 : -1;
      }

      public void Insert(int index, object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public void RemoveAt(int index)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Pair_NotFullCollection);
      }

      public object this[int index]
      {
         get { return (index == 0) ? Key : (index == 1) ? Value : (object) null; }
         set
         {
            switch (index)
            {
               case 0:
                  Key = (T1) value;
                  break;
               case 1:
                  Value = (T2) value;
                  break;
            }
         }
      }

      #endregion
   }

   public static class CKeyValuePair
   {
      public static CKeyValuePair<T1, T2> Create<T1, T2>(T1 key, T2 value)
      {
         return new CKeyValuePair<T1, T2> {Key = key, Value = value};
      }
   }

   [Serializable, JsonObject(MemberSerialization.OptIn)]
   public sealed class CTriple<T1, T2, T3> : IEquatable<CTriple<T1, T2, T3>>, IList<object>
   {
      [JsonProperty]
      public T1 First { get; set; }

      [JsonProperty]
      public T2 Second { get; set; }

      [JsonProperty]
      public T3 Third { get; set; }

      #region IEquatable<CTriple<T1,T2,T3>> Members

      public bool Equals(CTriple<T1, T2, T3> other)
      {
         return EqualityComparer<T1>.Default.Equals(First, other.First) && EqualityComparer<T2>.Default.Equals(Second, other.Second) &&
                EqualityComparer<T3>.Default.Equals(Third, other.Third);
      }

      #endregion

      #region Object Members

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj))
         {
            return false;
         }
         return obj is CTriple<T1, T2, T3> && Equals((CTriple<T1, T2, T3>) obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = EqualityComparer<T1>.Default.GetHashCode(First);
            hashCode = (hashCode*397) ^ EqualityComparer<T2>.Default.GetHashCode(Second);
            hashCode = (hashCode*397) ^ EqualityComparer<T3>.Default.GetHashCode(Third);
            return hashCode;
         }
      }

      public static bool operator ==(CTriple<T1, T2, T3> left, CTriple<T1, T2, T3> right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(CTriple<T1, T2, T3> left, CTriple<T1, T2, T3> right)
      {
         return !left.Equals(right);
      }

      public override string ToString()
      {
         return string.Format("First: [{0}], Second: [{1}], Third: [{2}]", First, Second, Third);
      }

      #endregion

      #region IList<object> Members

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public IEnumerator<object> GetEnumerator()
      {
         yield return First;
         yield return Second;
         yield return Third;
      }

      public void Add(object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Triple_NotFullCollection);
      }

      public void Clear()
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Triple_NotFullCollection);
      }

      public bool Contains(object item)
      {
         return Equals(First, item) || Equals(Second, item) || Equals(Third, item);
      }

      public void CopyTo(object[] array, int arrayIndex)
      {
         array[arrayIndex] = First;
         array[arrayIndex + 1] = Second;
         array[arrayIndex + 2] = Third;
      }

      public bool Remove(object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Triple_NotFullCollection);
      }

      public int Count
      {
         get { return 3; }
      }

      public bool IsReadOnly
      {
         get { return false; }
      }

      public int IndexOf(object item)
      {
         return Equals(First, item) ? 0 : Equals(Second, item) ? 1 : Equals(Third, item) ? 2 : -1;
      }

      public void Insert(int index, object item)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Triple_NotFullCollection);
      }

      public void RemoveAt(int index)
      {
         throw new NotImplementedException(ErrorMessages.TopLevel_Triple_NotFullCollection);
      }

      public object this[int index]
      {
         get { return (index == 0) ? First : (index == 1) ? Second : (index == 2) ? Third : (object) null; }
         set
         {
            switch (index)
            {
               case 0:
                  First = (T1) value;
                  break;
               case 1:
                  Second = (T2) value;
                  break;
               case 2:
                  Third = (T3) value;
                  break;
            }
         }
      }

      #endregion
   }

   public static class CTriple
   {
      public static CTriple<T1, T2, T3> Create<T1, T2, T3>(T1 first, T2 second, T3 third)
      {
         return new CTriple<T1, T2, T3> {First = first, Second = second, Third = third};
      }
   }
}