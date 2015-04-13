// File name: FormattableObject.cs
// 
// Author(s): Alessio Parma <alessio.parma@gmail.com>
// 
// The MIT License (MIT)
// 
// Copyright (c) 2014-2016 Alessio Parma <alessio.parma@gmail.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Enumerable = System.Linq.Enumerable;

namespace Finsa.Caravan.Common.Utilities
{
    /// <summary>
    ///   Implements a simple <see cref="ToString"/>, so that you don't have to copy and paste the
    ///   same boilerplate code over and over again.
    /// </summary>
    [System.Serializable]
    public abstract class FormattableObject
    {
        #region Abstract Methods

        /// <summary>
        ///   Returns all property (or field) values, along with their names, so that they can be
        ///   used to produce a meaningful <see cref="ToString"/>.
        /// </summary>
        /// <returns></returns>
        protected abstract System.Collections.Generic.IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers();

        #endregion Abstract Methods

        #region Object Methods

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return System.String.Join(", ", Enumerable.Select(GetFormattingMembers(), ComputeToString));
        }

        #endregion Object Methods

        #region Private Methods

        internal static string ComputeToString(GKeyValuePair<string, string> pair)
        {
            Diagnostics.Raise<System.ArgumentNullException>.IfIsNull(pair);
            return System.String.Format("{0}: [{1}]", pair.Key, pair.Value);
        }

        #endregion Private Methods
    }

    /// <summary>
    ///   Implements a simple <see cref="ToString"/>, so that you don't have to copy and paste the
    ///   same boilerplate code over and over again.
    /// </summary>
    /// <remarks>
    ///   Exposes the proper attributes (like
    ///   <see cref="System.Runtime.Serialization.DataContractAttribute"/>) so that child objects
    ///   may be properly serialized as references by XML serializers.
    /// </remarks>
    [System.Serializable, System.Runtime.Serialization.DataContract(IsReference = true)]
    public abstract class FormattableReferenceObject
    {
        #region Abstract Methods

        /// <summary>
        ///   Returns all property (or field) values, along with their names, so that they can be
        ///   used to produce a meaningful <see cref="ToString"/>.
        /// </summary>
        /// <returns></returns>
        protected abstract System.Collections.Generic.IEnumerable<GKeyValuePair<string, string>> GetFormattingMembers();

        #endregion Abstract Methods

        #region Object Methods

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return System.String.Join(", ", Enumerable.Select(GetFormattingMembers(), FormattableObject.ComputeToString));
        }

        #endregion Object Methods
    }
}