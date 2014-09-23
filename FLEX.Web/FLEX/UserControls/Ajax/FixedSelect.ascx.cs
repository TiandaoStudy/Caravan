using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FLEX.Common.Collections;
using FLEX.Common.Web;
using PommaLabs.GRAMPA;
using PommaLabs.GRAMPA.Diagnostics;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls.Ajax
// ReSharper restore CheckNamespace
{
   public partial class FixedSelect : AjaxControlBase, IAjaxControl, ISearchControl
   {
      #region Public Properties

      public DropDownList Select
      {
         get { return ddlSelect; }
      }

      #endregion

      #region Public Methods

      public void SetDataSource(DataTable data, string valueColumn, string labelColumn)
      {
         Raise<ArgumentNullException>.IfIsNull(data);
         Raise<ArgumentException>.IfIsEmpty(valueColumn);
         Raise<ArgumentException>.IfIsEmpty(labelColumn);

         var rowCount = data.Rows.Count;
         for (var i = 0; i < rowCount; i++)
         {
            var row = data.Rows[i];
            var label = row[labelColumn].ToString();
            var value = row[valueColumn].ToString();
            ddlSelect.Items.Add(new ListItem(label, value));
         }
         ddlSelect.Items.Insert(0, new ListItem("", ""));
      }

      public void SetDataSource(DataView data, string valueColumn, string labelColumn)
      {
         Raise<ArgumentNullException>.IfIsNull(data);
         Raise<ArgumentException>.IfIsEmpty(valueColumn);
         Raise<ArgumentException>.IfIsEmpty(labelColumn);

         ddlSelect.DataSource = data;
         ddlSelect.DataValueField = valueColumn;
         ddlSelect.DataTextField = labelColumn;
         ddlSelect.DataBind();
         ddlSelect.Items.Insert(0, new ListItem("", ""));
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
            ddlSelect.Items.Add(new ListItem(item, item));
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="values">
      ///   A list of values that will be used to fill the checkbox list.
      ///   First item will be used as label, while second item will be used as value.
      /// </param>
      public void SetDataSource(IList<GKeyValuePair<string, string>> values)
      {
         Raise<ArgumentNullException>.IfIsNull(values);

         foreach (var pair in values)
         {
            ddlSelect.Items.Add(new ListItem(pair.Key, pair.Value));
         }
      }

      #endregion

      #region IAjaxControl Members

      public UpdatePanel UpdatePanel
      {
         get { return updPanel; }
      }

      public void ActivateFullPostBack()
      {
         Master.ScriptManager.RegisterPostBackControl(ddlSelect);
      }

      public void RegisterAsFullPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new PostBackTrigger
         {
            ControlID = ddlSelect.UniqueID
         };
         updatePanel.Triggers.Add(trigger);
      }

      public void RegisterAsAsyncPostBackTrigger(UpdatePanel updatePanel)
      {
         var trigger = new AsyncPostBackTrigger
         {
            ControlID = ddlSelect.UniqueID,
            EventName = "SelectedIndexChanged"
         };
         updatePanel.Triggers.Add(trigger);
      }

      #endregion

      #region ISearchControl Members

      public dynamic DynamicSelectedValues
      {
         get { return HasValues ? ddlSelect.SelectedValue : null; }
      }

      public bool HasValues
      {
         get { return ddlSelect.SelectedIndex >= 0; }
      }

      public IList<string> SelectedValues // Valore attualmente selezionato
      {
         get { return new OneItemList<string>(ddlSelect.SelectedValue); }
      }

      public event Action<ISearchControl, SearchCriteriaSelectedArgs> ValueSelected;

      public void ClearContents()
      {
         ddlSelect.ClearSelection();
      }

      public void CopySelectedValuesFrom(ISearchControl searchControl)
      {
         Raise<ArgumentException>.IfIsNotInstanceOf<FixedSelect>(searchControl);

         var otherFixedSelect = (FixedSelect) searchControl;
         ddlSelect.SelectedIndex = otherFixedSelect.ddlSelect.SelectedIndex;
      }

      #endregion

      #region AjaxControlBase Members

       protected override void OnDoPostBackChanged(bool doPostBack)
       {
           base.OnDoPostBackChanged(doPostBack);
           ddlSelect.AutoPostBack = doPostBack;
       }

       protected override void OnEnabledChanged(bool enabled)
       {
           base.OnEnabledChanged(enabled);
           ddlSelect.Enabled = enabled;
       }

       #endregion

      protected void ddlSelect_OnSelectedIndexChanged(object sender, EventArgs e)
      {
         if (ValueSelected != null)
         {
            ValueSelected(this, new SearchCriteriaSelectedArgs());
         }
      }
   }
}