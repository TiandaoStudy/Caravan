using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FLEX.Common.Collections;
using Thrower;

namespace FLEX.Common.Web
{
   public sealed class SearchCriteria
   {
      private readonly Dictionary<string, ISearchControl> _observedControls;

      public event Action<SearchCriteria, SearchCriteriaChangedArgs> CriteriaChanged;

      public event Action ClearingCriteria; 

      public SearchCriteria()
      {
         _observedControls = new Dictionary<string, ISearchControl>();
      }

      public IList<string> this[string searchTag]
      {
         get
         {
            searchTag = searchTag.ToLower();
            Raise<ArgumentException>.IfIsNotContainedIn(searchTag, _observedControls);
            return _observedControls[searchTag].SelectedValues;
         }
      }

      public void ClearCriteria(object sender, EventArgs args)
      {
         foreach (var ctrl in _observedControls.Values.Where(c => !c.Enabled.HasValue || c.Enabled.Value))
         {
            ctrl.ClearContents();
         }
         if (ClearingCriteria != null)
         {
            ClearingCriteria();
         }
         if (CriteriaChanged != null)
         {
            CriteriaChanged(this, new SearchCriteriaChangedArgs());
         }
      }

      public void RegisterControl(ISearchControl searchControl, string searchTag)
      {
         Raise<ArgumentNullException>.IfIsNull(searchControl);
         Raise<ArgumentException>.IfIsEmpty(searchTag);
         
         searchTag = searchTag.ToLower();
         if (_observedControls.ContainsKey(searchTag))
         {
            // Control is already observed, we need to update the references.
            var ctrl = _observedControls[searchTag];
            searchControl.CopySelectedValuesFrom(ctrl);
            _observedControls[searchTag] = searchControl;
         }
         else
         {
            _observedControls.Add(searchTag, searchControl);
         }
         searchControl.ValueSelected += UpdateCriteria;
      }

      public void RegisterValue(string value, string searchTag)
      {
         Raise<ArgumentException>.IfIsEmpty(searchTag);
         
         var searchControl = new FakeSearchControl(value);
         searchTag = searchTag.ToLower();
         if (_observedControls.ContainsKey(searchTag))
         {
            // Control is already observed, we need to update the references.
            _observedControls[searchTag] = searchControl;
         }
         else
         {
            _observedControls.Add(searchTag, searchControl);
         }
      }

      private void UpdateCriteria(ISearchControl searchControl, SearchCriteriaSelectedArgs args)
      {
         Raise<ArgumentNullException>.IfIsNull(searchControl);

         if (CriteriaChanged != null)
         {
            CriteriaChanged(this, new SearchCriteriaChangedArgs());
         }
      }

      private sealed class FakeSearchControl : ISearchControl
      {
         private string _value;

         public FakeSearchControl(string value)
         {
            _value = value;
         }

         public bool? Enabled
         {
            get { return false; /* User does not access this control */ }
         }

         public bool HasValues
         {
            get { return true; /* This kind of control always has a value */ }
         }

         public IList<string> SelectedValues
         {
            get { return new OneItemList<string>(_value); }
         }
         
         public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;
         
         public void ClearContents()
         {
            throw new InvalidOperationException("FakeSearchControl is readonly");
         }

         public void CopySelectedValuesFrom(ISearchControl searchControl)
         {
            throw new InvalidOperationException("FakeSearchControl is readonly");
         }
      }
   }

   public struct SearchCriteriaChangedArgs
   {
      // Empty, for now...
   }

   public struct SearchCriteriaSelectedArgs
   {
      // Empty, for now...
   }
}