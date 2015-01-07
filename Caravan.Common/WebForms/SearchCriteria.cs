using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PommaLabs.Collections.ReadOnly;
using PommaLabs.Diagnostics;

namespace FLEX.Common.Web
{
   /// <summary>
   ///   TODO
   /// </summary>
   [Serializable]
   public sealed class SearchCriteria : IEnumerable<SearchCriteriaItem>
   {
      private readonly Dictionary<string, ISearchControl> _observedControls = new Dictionary<string, ISearchControl>();

      public event Action<SearchCriteria, SearchCriteriaChangedArgs> CriteriaChanged;

      public event Action ClearingCriteria;

      public void ClearCriteria(object sender, EventArgs args)
      {
         foreach (var ctrl in _observedControls.Values.Where(c => c.Enabled.HasValue && c.Enabled.Value))
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

      /// <summary>
      /// 
      /// </summary>
      /// <param name="control"></param>
      /// <param name="key"></param>
      public void RegisterControl(ISearchControl control, string key)
      {
         Raise<ArgumentNullException>.IfIsNull(control);
         Raise<ArgumentException>.IfIsEmpty(key);

         key = key.ToLower();
         if (_observedControls.ContainsKey(key))
         {
            // Control is already observed, we need to update the references.
            var ctrl = _observedControls[key];
            control.CopySelectedValuesFrom(ctrl);
            _observedControls[key] = control;
         }
         else
         {
            _observedControls.Add(key, control);
         }
         control.ValueSelected += UpdateCriteria;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="value"></param>
      /// <param name="key"></param>
      public void RegisterValue(string value, string key)
      {
         Raise<ArgumentException>.IfIsEmpty(key);

         var searchControl = new FakeSearchControl(value);
         key = key.ToLower();
         if (_observedControls.ContainsKey(key))
         {
            // Control is already observed, we need to update the references.
            _observedControls[key] = searchControl;
         }
         else
         {
            _observedControls.Add(key, searchControl);
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

      #region IEnumerable Members

      public IEnumerator<SearchCriteriaItem> GetEnumerator()
      {
         return _observedControls.Select(x => new SearchCriteriaItem {Key = x.Key, Values = x.Value.SelectedValues}).GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      #endregion

      #region Public Properties

      /// <summary>
      ///   TODO
      /// </summary>
      public IEnumerable<string> Keys
      {
         get { return _observedControls.Keys; }
      }

      /// <summary>
      ///   TODO
      /// </summary>
      public IEnumerable<IList<string>> Values
      {
         get { return _observedControls.Values.Select(v => v.SelectedValues); }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      public IList<string> this[string key]
      {
         get
         {
            key = key.ToLower();
            Raise<ArgumentException>.IfIsNotContainedIn(key, _observedControls);
            return _observedControls[key].SelectedValues;
         }
      }

      #endregion

      private sealed class FakeSearchControl : ISearchControl
      {
         private readonly string _value;

         public FakeSearchControl(string value)
         {
            _value = value;
         }

         public dynamic DynamicSelectedValues
         {
            get { return _value; }
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
            get { return ReadOnlyList.Create(_value); }
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

   [Serializable]
   public struct SearchCriteriaItem
   {
      public string Key { get; set; }
      public IList<string> Values { get; set; }
   }

   [Serializable]
   public struct SearchCriteriaChangedArgs
   {
      // Empty, for now...
   }

   [Serializable]
   public struct SearchCriteriaSelectedArgs
   {
      // Empty, for now...
   }
}