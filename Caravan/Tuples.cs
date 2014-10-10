//
// Tuples.cs
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

namespace Finsa.Caravan.Helpers
{
	[Serializable]
    public abstract class GTuple : IList<object>
    {
        public static GTuple<T1> Create<T1>(T1 item1)
        {
            var tuple = new GTuple<T1>();
            tuple.Item1 = item1;
            return tuple;
        }

        public static GTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            var tuple = new GTuple<T1, T2>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            return tuple;
        }

        public static GTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            var tuple = new GTuple<T1, T2, T3>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            var tuple = new GTuple<T1, T2, T3, T4>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            tuple.Item15 = item15;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            tuple.Item15 = item15;
            tuple.Item16 = item16;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            tuple.Item15 = item15;
            tuple.Item16 = item16;
            tuple.Item17 = item17;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            tuple.Item15 = item15;
            tuple.Item16 = item16;
            tuple.Item17 = item17;
            tuple.Item18 = item18;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            tuple.Item15 = item15;
            tuple.Item16 = item16;
            tuple.Item17 = item17;
            tuple.Item18 = item18;
            tuple.Item19 = item19;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            tuple.Item15 = item15;
            tuple.Item16 = item16;
            tuple.Item17 = item17;
            tuple.Item18 = item18;
            tuple.Item19 = item19;
            tuple.Item20 = item20;
            return tuple;
        }

        public static GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10, T11 item11, T12 item12, T13 item13, T14 item14, T15 item15, T16 item16, T17 item17, T18 item18, T19 item19, T20 item20, T21 item21)
        {
            var tuple = new GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>();
            tuple.Item1 = item1;
            tuple.Item2 = item2;
            tuple.Item3 = item3;
            tuple.Item4 = item4;
            tuple.Item5 = item5;
            tuple.Item6 = item6;
            tuple.Item7 = item7;
            tuple.Item8 = item8;
            tuple.Item9 = item9;
            tuple.Item10 = item10;
            tuple.Item11 = item11;
            tuple.Item12 = item12;
            tuple.Item13 = item13;
            tuple.Item14 = item14;
            tuple.Item15 = item15;
            tuple.Item16 = item16;
            tuple.Item17 = item17;
            tuple.Item18 = item18;
            tuple.Item19 = item19;
            tuple.Item20 = item20;
            tuple.Item21 = item21;
            return tuple;
        }


        #region IList<object> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<object> GetEnumerator();

        public void Add(object item)
        {
            throw new NotImplementedException(ErrorMessages.TopLevel_Tuple_NotFullCollection);
        }

        public void Clear()
        {
            throw new NotImplementedException(ErrorMessages.TopLevel_Tuple_NotFullCollection);
        }

        public abstract bool Contains(object item);

        public abstract void CopyTo(object[] array, int arrayIndex);

        public bool Remove(object item)
        {
            throw new NotImplementedException(ErrorMessages.TopLevel_Tuple_NotFullCollection);
        }

        public abstract int Count { get; }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public abstract int IndexOf(object item);

        public void Insert(int index, object item)
        {
            throw new NotImplementedException(ErrorMessages.TopLevel_Tuple_NotFullCollection);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException(ErrorMessages.TopLevel_Tuple_NotFullCollection);
        }

        public abstract object this[int index] { get; set; }

        #endregion

    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1> : GTuple, IEquatable<GTuple<T1>>
    {
        public T1 Item1 { get; set; }
        
        #region IEquatable<GTuple<T1>> Members

        public bool Equals(GTuple<T1> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1> && Equals((GTuple<T1>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ;
            }
        }

        public static bool operator ==(GTuple<T1> left, GTuple<T1> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1> left, GTuple<T1> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}]", Item1);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
        }

        public override int Count
        {
            get { return 1; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2> : GTuple, IEquatable<GTuple<T1, T2>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        
        #region IEquatable<GTuple<T1, T2>> Members

        public bool Equals(GTuple<T1, T2> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2> && Equals((GTuple<T1, T2>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2> left, GTuple<T1, T2> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2> left, GTuple<T1, T2> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}]", Item1, Item2);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
        }

        public override int Count
        {
            get { return 2; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3> : GTuple, IEquatable<GTuple<T1, T2, T3>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3>> Members

        public bool Equals(GTuple<T1, T2, T3> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3> && Equals((GTuple<T1, T2, T3>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3> left, GTuple<T1, T2, T3> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3> left, GTuple<T1, T2, T3> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}]", Item1, Item2, Item3);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
        }

        public override int Count
        {
            get { return 3; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4> : GTuple, IEquatable<GTuple<T1, T2, T3, T4>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4>> Members

        public bool Equals(GTuple<T1, T2, T3, T4> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4> && Equals((GTuple<T1, T2, T3, T4>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4> left, GTuple<T1, T2, T3, T4> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4> left, GTuple<T1, T2, T3, T4> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}]", Item1, Item2, Item3, Item4);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
        }

        public override int Count
        {
            get { return 4; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5> && Equals((GTuple<T1, T2, T3, T4, T5>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5> left, GTuple<T1, T2, T3, T4, T5> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5> left, GTuple<T1, T2, T3, T4, T5> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}]", Item1, Item2, Item3, Item4, Item5);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
        }

        public override int Count
        {
            get { return 5; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6> && Equals((GTuple<T1, T2, T3, T4, T5, T6>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6> left, GTuple<T1, T2, T3, T4, T5, T6> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6> left, GTuple<T1, T2, T3, T4, T5, T6> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}]", Item1, Item2, Item3, Item4, Item5, Item6);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
        }

        public override int Count
        {
            get { return 6; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7> left, GTuple<T1, T2, T3, T4, T5, T6, T7> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7> left, GTuple<T1, T2, T3, T4, T5, T6, T7> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
        }

        public override int Count
        {
            get { return 7; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
        }

        public override int Count
        {
            get { return 8; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
        }

        public override int Count
        {
            get { return 9; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
        }

        public override int Count
        {
            get { return 10; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
        }

        public override int Count
        {
            get { return 11; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
        }

        public override int Count
        {
            get { return 12; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
        }

        public override int Count
        {
            get { return 13; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
        }

        public override int Count
        {
            get { return 14; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        public T15 Item15 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
                && EqualityComparer<T15>.Default.Equals(Item15, other.Item15)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ^ EqualityComparer<T15>.Default.GetHashCode(Item15)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}], Item15: [{14}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
            yield return Item15;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
                || Equals(Item15, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
            array[arrayIndex + 14] = Item15;
        }

        public override int Count
        {
            get { return 15; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
                   Equals(Item15, item) ? 14 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                       (index == 14) ? Item15 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                    case 14:
                        Item15 = (T15) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        public T15 Item15 { get; set; }
        public T16 Item16 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
                && EqualityComparer<T15>.Default.Equals(Item15, other.Item15)
                && EqualityComparer<T16>.Default.Equals(Item16, other.Item16)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ^ EqualityComparer<T15>.Default.GetHashCode(Item15)
                ^ EqualityComparer<T16>.Default.GetHashCode(Item16)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}], Item15: [{14}], Item16: [{15}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15, Item16);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
            yield return Item15;
            yield return Item16;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
                || Equals(Item15, item)
                || Equals(Item16, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
            array[arrayIndex + 14] = Item15;
            array[arrayIndex + 15] = Item16;
        }

        public override int Count
        {
            get { return 16; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
                   Equals(Item15, item) ? 14 :
                   Equals(Item16, item) ? 15 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                       (index == 14) ? Item15 :
                       (index == 15) ? Item16 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                    case 14:
                        Item15 = (T15) value;
                        break;
                    case 15:
                        Item16 = (T16) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        public T15 Item15 { get; set; }
        public T16 Item16 { get; set; }
        public T17 Item17 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
                && EqualityComparer<T15>.Default.Equals(Item15, other.Item15)
                && EqualityComparer<T16>.Default.Equals(Item16, other.Item16)
                && EqualityComparer<T17>.Default.Equals(Item17, other.Item17)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ^ EqualityComparer<T15>.Default.GetHashCode(Item15)
                ^ EqualityComparer<T16>.Default.GetHashCode(Item16)
                ^ EqualityComparer<T17>.Default.GetHashCode(Item17)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}], Item15: [{14}], Item16: [{15}], Item17: [{16}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15, Item16, Item17);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
            yield return Item15;
            yield return Item16;
            yield return Item17;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
                || Equals(Item15, item)
                || Equals(Item16, item)
                || Equals(Item17, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
            array[arrayIndex + 14] = Item15;
            array[arrayIndex + 15] = Item16;
            array[arrayIndex + 16] = Item17;
        }

        public override int Count
        {
            get { return 17; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
                   Equals(Item15, item) ? 14 :
                   Equals(Item16, item) ? 15 :
                   Equals(Item17, item) ? 16 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                       (index == 14) ? Item15 :
                       (index == 15) ? Item16 :
                       (index == 16) ? Item17 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                    case 14:
                        Item15 = (T15) value;
                        break;
                    case 15:
                        Item16 = (T16) value;
                        break;
                    case 16:
                        Item17 = (T17) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        public T15 Item15 { get; set; }
        public T16 Item16 { get; set; }
        public T17 Item17 { get; set; }
        public T18 Item18 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
                && EqualityComparer<T15>.Default.Equals(Item15, other.Item15)
                && EqualityComparer<T16>.Default.Equals(Item16, other.Item16)
                && EqualityComparer<T17>.Default.Equals(Item17, other.Item17)
                && EqualityComparer<T18>.Default.Equals(Item18, other.Item18)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ^ EqualityComparer<T15>.Default.GetHashCode(Item15)
                ^ EqualityComparer<T16>.Default.GetHashCode(Item16)
                ^ EqualityComparer<T17>.Default.GetHashCode(Item17)
                ^ EqualityComparer<T18>.Default.GetHashCode(Item18)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}], Item15: [{14}], Item16: [{15}], Item17: [{16}], Item18: [{17}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15, Item16, Item17, Item18);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
            yield return Item15;
            yield return Item16;
            yield return Item17;
            yield return Item18;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
                || Equals(Item15, item)
                || Equals(Item16, item)
                || Equals(Item17, item)
                || Equals(Item18, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
            array[arrayIndex + 14] = Item15;
            array[arrayIndex + 15] = Item16;
            array[arrayIndex + 16] = Item17;
            array[arrayIndex + 17] = Item18;
        }

        public override int Count
        {
            get { return 18; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
                   Equals(Item15, item) ? 14 :
                   Equals(Item16, item) ? 15 :
                   Equals(Item17, item) ? 16 :
                   Equals(Item18, item) ? 17 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                       (index == 14) ? Item15 :
                       (index == 15) ? Item16 :
                       (index == 16) ? Item17 :
                       (index == 17) ? Item18 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                    case 14:
                        Item15 = (T15) value;
                        break;
                    case 15:
                        Item16 = (T16) value;
                        break;
                    case 16:
                        Item17 = (T17) value;
                        break;
                    case 17:
                        Item18 = (T18) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        public T15 Item15 { get; set; }
        public T16 Item16 { get; set; }
        public T17 Item17 { get; set; }
        public T18 Item18 { get; set; }
        public T19 Item19 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
                && EqualityComparer<T15>.Default.Equals(Item15, other.Item15)
                && EqualityComparer<T16>.Default.Equals(Item16, other.Item16)
                && EqualityComparer<T17>.Default.Equals(Item17, other.Item17)
                && EqualityComparer<T18>.Default.Equals(Item18, other.Item18)
                && EqualityComparer<T19>.Default.Equals(Item19, other.Item19)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ^ EqualityComparer<T15>.Default.GetHashCode(Item15)
                ^ EqualityComparer<T16>.Default.GetHashCode(Item16)
                ^ EqualityComparer<T17>.Default.GetHashCode(Item17)
                ^ EqualityComparer<T18>.Default.GetHashCode(Item18)
                ^ EqualityComparer<T19>.Default.GetHashCode(Item19)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}], Item15: [{14}], Item16: [{15}], Item17: [{16}], Item18: [{17}], Item19: [{18}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15, Item16, Item17, Item18, Item19);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
            yield return Item15;
            yield return Item16;
            yield return Item17;
            yield return Item18;
            yield return Item19;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
                || Equals(Item15, item)
                || Equals(Item16, item)
                || Equals(Item17, item)
                || Equals(Item18, item)
                || Equals(Item19, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
            array[arrayIndex + 14] = Item15;
            array[arrayIndex + 15] = Item16;
            array[arrayIndex + 16] = Item17;
            array[arrayIndex + 17] = Item18;
            array[arrayIndex + 18] = Item19;
        }

        public override int Count
        {
            get { return 19; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
                   Equals(Item15, item) ? 14 :
                   Equals(Item16, item) ? 15 :
                   Equals(Item17, item) ? 16 :
                   Equals(Item18, item) ? 17 :
                   Equals(Item19, item) ? 18 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                       (index == 14) ? Item15 :
                       (index == 15) ? Item16 :
                       (index == 16) ? Item17 :
                       (index == 17) ? Item18 :
                       (index == 18) ? Item19 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                    case 14:
                        Item15 = (T15) value;
                        break;
                    case 15:
                        Item16 = (T16) value;
                        break;
                    case 16:
                        Item17 = (T17) value;
                        break;
                    case 17:
                        Item18 = (T18) value;
                        break;
                    case 18:
                        Item19 = (T19) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        public T15 Item15 { get; set; }
        public T16 Item16 { get; set; }
        public T17 Item17 { get; set; }
        public T18 Item18 { get; set; }
        public T19 Item19 { get; set; }
        public T20 Item20 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
                && EqualityComparer<T15>.Default.Equals(Item15, other.Item15)
                && EqualityComparer<T16>.Default.Equals(Item16, other.Item16)
                && EqualityComparer<T17>.Default.Equals(Item17, other.Item17)
                && EqualityComparer<T18>.Default.Equals(Item18, other.Item18)
                && EqualityComparer<T19>.Default.Equals(Item19, other.Item19)
                && EqualityComparer<T20>.Default.Equals(Item20, other.Item20)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ^ EqualityComparer<T15>.Default.GetHashCode(Item15)
                ^ EqualityComparer<T16>.Default.GetHashCode(Item16)
                ^ EqualityComparer<T17>.Default.GetHashCode(Item17)
                ^ EqualityComparer<T18>.Default.GetHashCode(Item18)
                ^ EqualityComparer<T19>.Default.GetHashCode(Item19)
                ^ EqualityComparer<T20>.Default.GetHashCode(Item20)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}], Item15: [{14}], Item16: [{15}], Item17: [{16}], Item18: [{17}], Item19: [{18}], Item20: [{19}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15, Item16, Item17, Item18, Item19, Item20);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
            yield return Item15;
            yield return Item16;
            yield return Item17;
            yield return Item18;
            yield return Item19;
            yield return Item20;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
                || Equals(Item15, item)
                || Equals(Item16, item)
                || Equals(Item17, item)
                || Equals(Item18, item)
                || Equals(Item19, item)
                || Equals(Item20, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
            array[arrayIndex + 14] = Item15;
            array[arrayIndex + 15] = Item16;
            array[arrayIndex + 16] = Item17;
            array[arrayIndex + 17] = Item18;
            array[arrayIndex + 18] = Item19;
            array[arrayIndex + 19] = Item20;
        }

        public override int Count
        {
            get { return 20; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
                   Equals(Item15, item) ? 14 :
                   Equals(Item16, item) ? 15 :
                   Equals(Item17, item) ? 16 :
                   Equals(Item18, item) ? 17 :
                   Equals(Item19, item) ? 18 :
                   Equals(Item20, item) ? 19 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                       (index == 14) ? Item15 :
                       (index == 15) ? Item16 :
                       (index == 16) ? Item17 :
                       (index == 17) ? Item18 :
                       (index == 18) ? Item19 :
                       (index == 19) ? Item20 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                    case 14:
                        Item15 = (T15) value;
                        break;
                    case 15:
                        Item16 = (T16) value;
                        break;
                    case 16:
                        Item17 = (T17) value;
                        break;
                    case 17:
                        Item18 = (T18) value;
                        break;
                    case 18:
                        Item19 = (T19) value;
                        break;
                    case 19:
                        Item20 = (T20) value;
                        break;
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///   TODO
    /// </summary>
	[Serializable]
    public sealed class GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> : GTuple, IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }
        public T5 Item5 { get; set; }
        public T6 Item6 { get; set; }
        public T7 Item7 { get; set; }
        public T8 Item8 { get; set; }
        public T9 Item9 { get; set; }
        public T10 Item10 { get; set; }
        public T11 Item11 { get; set; }
        public T12 Item12 { get; set; }
        public T13 Item13 { get; set; }
        public T14 Item14 { get; set; }
        public T15 Item15 { get; set; }
        public T16 Item16 { get; set; }
        public T17 Item17 { get; set; }
        public T18 Item18 { get; set; }
        public T19 Item19 { get; set; }
        public T20 Item20 { get; set; }
        public T21 Item21 { get; set; }
        
        #region IEquatable<GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>> Members

        public bool Equals(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> other)
        {
            return true
                && EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                && EqualityComparer<T9>.Default.Equals(Item9, other.Item9)
                && EqualityComparer<T10>.Default.Equals(Item10, other.Item10)
                && EqualityComparer<T11>.Default.Equals(Item11, other.Item11)
                && EqualityComparer<T12>.Default.Equals(Item12, other.Item12)
                && EqualityComparer<T13>.Default.Equals(Item13, other.Item13)
                && EqualityComparer<T14>.Default.Equals(Item14, other.Item14)
                && EqualityComparer<T15>.Default.Equals(Item15, other.Item15)
                && EqualityComparer<T16>.Default.Equals(Item16, other.Item16)
                && EqualityComparer<T17>.Default.Equals(Item17, other.Item17)
                && EqualityComparer<T18>.Default.Equals(Item18, other.Item18)
                && EqualityComparer<T19>.Default.Equals(Item19, other.Item19)
                && EqualityComparer<T20>.Default.Equals(Item20, other.Item20)
                && EqualityComparer<T21>.Default.Equals(Item21, other.Item21)
            ;
        }

        #endregion

        #region Object Members

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj is GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> && Equals((GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                return 0
                ^ EqualityComparer<T1>.Default.GetHashCode(Item1)
                ^ EqualityComparer<T2>.Default.GetHashCode(Item2)
                ^ EqualityComparer<T3>.Default.GetHashCode(Item3)
                ^ EqualityComparer<T4>.Default.GetHashCode(Item4)
                ^ EqualityComparer<T5>.Default.GetHashCode(Item5)
                ^ EqualityComparer<T6>.Default.GetHashCode(Item6)
                ^ EqualityComparer<T7>.Default.GetHashCode(Item7)
                ^ EqualityComparer<T8>.Default.GetHashCode(Item8)
                ^ EqualityComparer<T9>.Default.GetHashCode(Item9)
                ^ EqualityComparer<T10>.Default.GetHashCode(Item10)
                ^ EqualityComparer<T11>.Default.GetHashCode(Item11)
                ^ EqualityComparer<T12>.Default.GetHashCode(Item12)
                ^ EqualityComparer<T13>.Default.GetHashCode(Item13)
                ^ EqualityComparer<T14>.Default.GetHashCode(Item14)
                ^ EqualityComparer<T15>.Default.GetHashCode(Item15)
                ^ EqualityComparer<T16>.Default.GetHashCode(Item16)
                ^ EqualityComparer<T17>.Default.GetHashCode(Item17)
                ^ EqualityComparer<T18>.Default.GetHashCode(Item18)
                ^ EqualityComparer<T19>.Default.GetHashCode(Item19)
                ^ EqualityComparer<T20>.Default.GetHashCode(Item20)
                ^ EqualityComparer<T21>.Default.GetHashCode(Item21)
                ;
            }
        }

        public static bool operator ==(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> left, GTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Item1: [{0}], Item2: [{1}], Item3: [{2}], Item4: [{3}], Item5: [{4}], Item6: [{5}], Item7: [{6}], Item8: [{7}], Item9: [{8}], Item10: [{9}], Item11: [{10}], Item12: [{11}], Item13: [{12}], Item14: [{13}], Item15: [{14}], Item16: [{15}], Item17: [{16}], Item18: [{17}], Item19: [{18}], Item20: [{19}], Item21: [{20}]", Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15, Item16, Item17, Item18, Item19, Item20, Item21);
        }

        #endregion

        #region IList<object> Members

        public override IEnumerator<object> GetEnumerator()
        {
            yield return Item1;
            yield return Item2;
            yield return Item3;
            yield return Item4;
            yield return Item5;
            yield return Item6;
            yield return Item7;
            yield return Item8;
            yield return Item9;
            yield return Item10;
            yield return Item11;
            yield return Item12;
            yield return Item13;
            yield return Item14;
            yield return Item15;
            yield return Item16;
            yield return Item17;
            yield return Item18;
            yield return Item19;
            yield return Item20;
            yield return Item21;
 
        }

        public override bool Contains(object item)
        {
            return false
                || Equals(Item1, item)
                || Equals(Item2, item)
                || Equals(Item3, item)
                || Equals(Item4, item)
                || Equals(Item5, item)
                || Equals(Item6, item)
                || Equals(Item7, item)
                || Equals(Item8, item)
                || Equals(Item9, item)
                || Equals(Item10, item)
                || Equals(Item11, item)
                || Equals(Item12, item)
                || Equals(Item13, item)
                || Equals(Item14, item)
                || Equals(Item15, item)
                || Equals(Item16, item)
                || Equals(Item17, item)
                || Equals(Item18, item)
                || Equals(Item19, item)
                || Equals(Item20, item)
                || Equals(Item21, item)
            ;
        }

        public override void CopyTo(object[] array, int arrayIndex)
        {
            array[arrayIndex + 0] = Item1;
            array[arrayIndex + 1] = Item2;
            array[arrayIndex + 2] = Item3;
            array[arrayIndex + 3] = Item4;
            array[arrayIndex + 4] = Item5;
            array[arrayIndex + 5] = Item6;
            array[arrayIndex + 6] = Item7;
            array[arrayIndex + 7] = Item8;
            array[arrayIndex + 8] = Item9;
            array[arrayIndex + 9] = Item10;
            array[arrayIndex + 10] = Item11;
            array[arrayIndex + 11] = Item12;
            array[arrayIndex + 12] = Item13;
            array[arrayIndex + 13] = Item14;
            array[arrayIndex + 14] = Item15;
            array[arrayIndex + 15] = Item16;
            array[arrayIndex + 16] = Item17;
            array[arrayIndex + 17] = Item18;
            array[arrayIndex + 18] = Item19;
            array[arrayIndex + 19] = Item20;
            array[arrayIndex + 20] = Item21;
        }

        public override int Count
        {
            get { return 21; }
        }

        public override int IndexOf(object item)
        {
            return
                   Equals(Item1, item) ? 0 :
                   Equals(Item2, item) ? 1 :
                   Equals(Item3, item) ? 2 :
                   Equals(Item4, item) ? 3 :
                   Equals(Item5, item) ? 4 :
                   Equals(Item6, item) ? 5 :
                   Equals(Item7, item) ? 6 :
                   Equals(Item8, item) ? 7 :
                   Equals(Item9, item) ? 8 :
                   Equals(Item10, item) ? 9 :
                   Equals(Item11, item) ? 10 :
                   Equals(Item12, item) ? 11 :
                   Equals(Item13, item) ? 12 :
                   Equals(Item14, item) ? 13 :
                   Equals(Item15, item) ? 14 :
                   Equals(Item16, item) ? 15 :
                   Equals(Item17, item) ? 16 :
                   Equals(Item18, item) ? 17 :
                   Equals(Item19, item) ? 18 :
                   Equals(Item20, item) ? 19 :
                   Equals(Item21, item) ? 20 :
            -1;
        }

        public override object this[int index]
        {
            get
            {
                return 
                       (index == 0) ? Item1 :
                       (index == 1) ? Item2 :
                       (index == 2) ? Item3 :
                       (index == 3) ? Item4 :
                       (index == 4) ? Item5 :
                       (index == 5) ? Item6 :
                       (index == 6) ? Item7 :
                       (index == 7) ? Item8 :
                       (index == 8) ? Item9 :
                       (index == 9) ? Item10 :
                       (index == 10) ? Item11 :
                       (index == 11) ? Item12 :
                       (index == 12) ? Item13 :
                       (index == 13) ? Item14 :
                       (index == 14) ? Item15 :
                       (index == 15) ? Item16 :
                       (index == 16) ? Item17 :
                       (index == 17) ? Item18 :
                       (index == 18) ? Item19 :
                       (index == 19) ? Item20 :
                       (index == 20) ? Item21 :
                (object) null;
            }
            set
            {
                switch (index) {
                    case 0:
                        Item1 = (T1) value;
                        break;
                    case 1:
                        Item2 = (T2) value;
                        break;
                    case 2:
                        Item3 = (T3) value;
                        break;
                    case 3:
                        Item4 = (T4) value;
                        break;
                    case 4:
                        Item5 = (T5) value;
                        break;
                    case 5:
                        Item6 = (T6) value;
                        break;
                    case 6:
                        Item7 = (T7) value;
                        break;
                    case 7:
                        Item8 = (T8) value;
                        break;
                    case 8:
                        Item9 = (T9) value;
                        break;
                    case 9:
                        Item10 = (T10) value;
                        break;
                    case 10:
                        Item11 = (T11) value;
                        break;
                    case 11:
                        Item12 = (T12) value;
                        break;
                    case 12:
                        Item13 = (T13) value;
                        break;
                    case 13:
                        Item14 = (T14) value;
                        break;
                    case 14:
                        Item15 = (T15) value;
                        break;
                    case 15:
                        Item16 = (T16) value;
                        break;
                    case 16:
                        Item17 = (T17) value;
                        break;
                    case 17:
                        Item18 = (T18) value;
                        break;
                    case 18:
                        Item19 = (T19) value;
                        break;
                    case 19:
                        Item20 = (T20) value;
                        break;
                    case 20:
                        Item21 = (T21) value;
                        break;
                }
            }
        }

        #endregion
    }

}

