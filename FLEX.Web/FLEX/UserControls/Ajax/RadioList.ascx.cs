using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FLEX.Common;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using Thrower;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class RadioList : AjaxControlBase, IAjaxControl, ISearchControl
   {
      #region Public Properties

      public int RepeatColumn
      {
         set { rblData.RepeatColumns = value; }
      }

      public RadioButtonList VisibleRadioButtonList
      {
         get { return rblData; }
      }

      public string Orientation
      {
         set { rblData.RepeatDirection = value == "horizontal" ? RepeatDirection.Horizontal : RepeatDirection.Vertical; }
      }

      #endregion

      #region Public Methods

      public void SetDataSource(DataTable data, string valueColumn, string labelColumn)
      {
         Raise<ArgumentNullException>.IfIsNull(data);
         Raise<ArgumentException>.IfIsEmpty(valueColumn);
         Raise<ArgumentException>.IfIsEmpty(labelColumn);

         for (var i = 0; i < data.Rows.Count; i++)
         {
            var row = data.Rows[i];
            var label = row[labelColumn].ToString();
            var value = row[valueColumn].ToString();
            rblData.Items.Add(new ListItem(label, value));
         }
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

         foreach (var item in values)
         {
            rblData.Items.Add(new ListItem(item, item));
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="values">
      ///   A list of values that will be used to fill the checkbox list.
      ///   First item will be used as label, while second item will be used as value.
      /// </param>
      public void SetDataSource(IList<Pair<string, string>> values)
      {
         Raise<ArgumentNullException>.IfIsNull(values);

         foreach (var pair in values)
         {
            rblData.Items.Add(new ListItem(pair.First, pair.Second));
         }
      }

      #endregion

      #region IAjaxControl

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void ActivatePostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(VisibleRadioButtonList);
      }

      public void RegisterAsPostBackTrigger(UpdatePanel updatePanel)
      {
         // Primary check box list
         var trigger = new PostBackTrigger
         {
            ControlID = VisibleRadioButtonList.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         // Primary check box list
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = VisibleRadioButtonList.UniqueID,
            EventName = "SelectedIndexChanged"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl

      public bool HasValues
      {
         get { return VisibleRadioButtonList.SelectedIndex >= 0; }
      }

      public IList<string> SelectedValues
      {
         get { return GetSelectedValues(); }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         VisibleRadioButtonList.ClearSelection();
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<RadioList>(searchControl);

         var otherSwitch = (RadioList) searchControl;
         rblData.SelectedIndex = otherSwitch.rblData.SelectedIndex;
      }

      #endregion

      #region AjaxControlBase Members

       protected override void SetDefaultValues()
       {
          rblData.RepeatDirection = RepeatDirection.Vertical;
       }

       protected override void OnDoPostBackChanged(bool doPostBack)
       {
           base.OnDoPostBackChanged(doPostBack);
           rblData.AutoPostBack = doPostBack;
       }

       protected override void OnEnabledChanged(bool enabled)
       {
           base.OnEnabledChanged(enabled);
           rblData.Enabled = enabled;
       }

       #endregion

      #region Private Members

      private IList<string> GetSelectedValues()
      {
         if (rblData.SelectedValue != null)
         {
            return new OneItemList<string>(rblData.SelectedValue);
         }
         return CommonSettings.EmptyStringList;
      }

      protected void RadioButtonListData_OnSelectedIndexChanged(object sender, EventArgs e)
      {
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }

      #endregion
   }
}