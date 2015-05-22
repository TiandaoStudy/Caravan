using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Finsa.Caravan.Common.WebForms;
using Finsa.CodeServices.Common.Diagnostics;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class CollapsibleCheckBoxList : AjaxControlBase, IAjaxControl, ISearchControl
   {
      private const string MaxVisibleItemCountViewStateKey = "CollapsibleCheckBoxList.MaxVisibleItemCount";

      protected override void Page_Load(object sender, EventArgs e)
      {
         base.Page_Load(sender, e);
         altro.Attributes["onclick"] = String.Format("P('{0}', '{1}');", divToggle.ClientID, ic.ClientID);
         altro.Attributes["data-target"] = "#" + divToggle.ClientID;
      }

      #region Public Properties

      public int MaxVisibleItemCount
      {
         get { return (int) ViewState[MaxVisibleItemCountViewStateKey]; }
         set
         {
            ViewState[MaxVisibleItemCountViewStateKey] = value;
            HideExpanderIfNecessary();
         }
      }

      public CheckBoxList VisibleCheckBoxList
      {
         get { return chklVisible; }
      }

      public CheckBoxList HiddenCheckBoxList
      {
         get { return chklHidden; }
      }

      #endregion

      #region Public Methods

      public void SetDataSource(DataTable data, string valueColumn, string labelColumn)
      {
         Raise<ArgumentNullException>.IfIsNull(data);
         Raise<ArgumentException>.IfIsEmpty(valueColumn);
         Raise<ArgumentException>.IfIsEmpty(labelColumn);

         var rowCount = data.Rows.Count;
         var visibleItemCount = Math.Min(MaxVisibleItemCount, rowCount);
         for (var i = 0; i < visibleItemCount; i++)
         {
            var row = data.Rows[i];
            var label = row[labelColumn].ToString();
            var value = row[valueColumn].ToString();
            chklVisible.Items.Add(new ListItem(label, value));
         }
         for (var i = visibleItemCount; i < rowCount; i++)
         {
            var row = data.Rows[i];
            var label = row[labelColumn].ToString();
            var value = row[valueColumn].ToString();
            chklHidden.Items.Add(new ListItem(label, value));
         }
         HideExpanderIfNecessary();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="values">
      ///   A list of values that will be used to fill the checkbox list.
      ///   Items will be used both as label and as value.
      /// </param>
      public void SetDataSource(IList<string> values)
      {
         Raise<ArgumentNullException>.IfIsNull(values);

         var visibleItemCount = Math.Min(MaxVisibleItemCount, values.Count);
         for (var i = 0; i < visibleItemCount; i++)
         {
            var item = values[i];
            chklVisible.Items.Add(new ListItem(item, item));
         }
         for (var i = visibleItemCount; i < values.Count; i++)
         {
            var item = values[i];
            chklHidden.Items.Add(new ListItem(item, item));
         }
         HideExpanderIfNecessary();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="values">
      ///   A list of values that will be used to fill the checkbox list.
      ///   First item will be used as label, while second item will be used as value.
      /// </param>
      public void SetDataSource(IList<KeyValuePair<string, string>> values)
      {
         Raise<ArgumentNullException>.IfIsNull(values);

         var visibleItemCount = Math.Min(MaxVisibleItemCount, values.Count);
         for (var i = 0; i < visibleItemCount; i++)
         {
            var pair = values[i];
            chklVisible.Items.Add(new ListItem(pair.Key, pair.Value));
         }
         for (var i = visibleItemCount; i < values.Count; i++)
         {
            var pair = values[i];
            chklHidden.Items.Add(new ListItem(pair.Key, pair.Value));
         }
         HideExpanderIfNecessary();
      }

      public void SetSelectedValues(params string[] values)
      {
         foreach (var value in values)
         {
            var item = VisibleCheckBoxList.Items.Cast<ListItem>().FirstOrDefault(x => x.Value == value);
            if (item != null)
            {
               item.Selected = true;
               continue;
            }
            item = HiddenCheckBoxList.Items.Cast<ListItem>().FirstOrDefault(x => x.Value == value);
            if (item != null)
            {
               item.Selected = true;
               continue;
            }
            throw new ArgumentException("Value '" + value + "' was not found");
         }
      }

      #endregion

      #region IAjaxControl

      public UpdatePanel UpdatePanel
      {
         get { return updPanel1; }
      }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(VisibleCheckBoxList);
         Master.ScriptManager.RegisterPostBackControl(HiddenCheckBoxList);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         // Primary check box list
         var trigger = new PostBackTrigger
         {
            ControlID = VisibleCheckBoxList.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
         // Secondary check box list
         trigger = new PostBackTrigger
         {
            ControlID = HiddenCheckBoxList.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         // Primary check box list
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = VisibleCheckBoxList.UniqueID,
            EventName = "SelectedIndexChanged"
         };
         updatePanel.Triggers.Add(trigger);
         // Secondary check box list
         trigger = new AsyncPostBackTrigger
         {
            ControlID = HiddenCheckBoxList.UniqueID,
            EventName = "SelectedIndexChanged"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl

      public dynamic DynamicSelectedValues
      {
         get { return HasValues ? GetSelectedValues() : null; }
      }

      public bool HasValues
      {
         get
         {
            var count = 0;
            var items = VisibleCheckBoxList.Items;
            for (var i = 0; i < items.Count; ++i)
            {
               if (items[i].Selected)
               {
                  count++;
               }
            }
            items = HiddenCheckBoxList.Items;
            for (var i = 0; i < items.Count; ++i)
            {
               if (items[i].Selected)
               {
                  count++;
               }
            }
            return count > 0;
         }
      }

      public IList<string> SelectedValues
      {
         get { return GetSelectedValues(); }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         VisibleCheckBoxList.ClearSelection();
         HiddenCheckBoxList.ClearSelection();
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<CollapsibleCheckBoxList>(searchControl);

         var otherCheckBoxList = (CollapsibleCheckBoxList) searchControl;

         var otherVisibleItems = otherCheckBoxList.VisibleCheckBoxList.Items;
         var visibleItems = VisibleCheckBoxList.Items;

         Debug.Assert(visibleItems.Count == otherVisibleItems.Count);
         var visibleCount = visibleItems.Count;
         for (var i = 0; i < visibleCount; ++i)
         {
            visibleItems[i].Selected = otherVisibleItems[i].Selected;
         }

         var otherHiddenItems = otherCheckBoxList.HiddenCheckBoxList.Items;
         var hiddenItems = HiddenCheckBoxList.Items;

         Debug.Assert(hiddenItems.Count == otherHiddenItems.Count);
         var hiddenCount = hiddenItems.Count;
         for (var i = 0; i < hiddenCount; ++i)
         {
            hiddenItems[i].Selected = otherHiddenItems[i].Selected;
         }
      }

      #endregion

      #region AjaxControlBase Members

      protected override void SetDefaultValues()
      {
         base.SetDefaultValues();
         VisibleCheckBoxList.RepeatDirection = RepeatDirection.Vertical;
         HiddenCheckBoxList.RepeatDirection = RepeatDirection.Vertical;
      }

      protected override void OnDoPostBackChanged(bool doPostBack)
      {
         base.OnDoPostBackChanged(doPostBack);
         VisibleCheckBoxList.AutoPostBack = doPostBack;
         HiddenCheckBoxList.AutoPostBack = doPostBack;
      }

      protected override void OnEnabledChanged(bool enabled)
      {
         base.OnEnabledChanged(enabled);
         VisibleCheckBoxList.Enabled = enabled;
         HiddenCheckBoxList.Enabled = enabled;
      }

      #endregion

      #region Private Members

      private void HideExpanderIfNecessary()
      {
         divAltre.Visible = (MaxVisibleItemCount < chklVisible.Items.Count + chklHidden.Items.Count);
      }

      private IList<string> GetSelectedValues()
      {
         var result = (from ListItem item in chklVisible.Items where item.Selected select item.Value);
         result = result.Union(from ListItem item in chklHidden.Items where item.Selected select item.Value);
         return result.ToList();
      }

      protected void CheckBoxListData_OnSelectedIndexChanged(object sender, EventArgs e)
      {
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }

      protected void CheckBoxListDataAltre_OnSelectedIndexChanged(object sender, EventArgs e)
      {
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }

      #endregion
   }
}