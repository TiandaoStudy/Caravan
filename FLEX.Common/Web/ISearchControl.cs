using System;
using System.Collections.Generic;

namespace FLEX.Common.Web
{
   public interface ISearchControl
   {
      bool Enabled { get; }

      bool HasValues { get; }

      IList<string> SelectedValues { get; }

      event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      void ClearContents();

      void CopySelectedValuesFrom(ISearchControl searchControl);
   }
}