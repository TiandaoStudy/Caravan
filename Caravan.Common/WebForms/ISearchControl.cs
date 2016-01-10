using System;
using System.Collections.Generic;

namespace Finsa.Caravan.Common.WebForms
{
    /// <summary>
    ///   TODO
    /// </summary>
    public interface ISearchControl
    {
        /// <summary>
        ///   TODO
        /// </summary>
        dynamic DynamicSelectedValues { get; }

        /// <summary>
        ///   TODO
        /// </summary>
        bool? Enabled { get; }

        /// <summary>
        ///   TODO
        /// </summary>
        bool HasValues { get; }

        /// <summary>
        ///   TODO
        /// </summary>
        IList<string> SelectedValues { get; }

        /// <summary>
        ///   TODO
        /// </summary>
        event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

        /// <summary>
        ///   TODO
        /// </summary>
        void ClearContents();

        /// <summary>
        ///   TODO
        /// </summary>
        /// <param name="searchControl"></param>
        void CopySelectedValuesFrom(ISearchControl searchControl);
    }
}