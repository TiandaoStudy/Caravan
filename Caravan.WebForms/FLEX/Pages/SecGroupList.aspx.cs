using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Finsa.Caravan.Common.WebForms;
using FLEX.Web.Pages;
using System.Data;
using Finsa.Caravan.Common.Utilities.Extensions;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace Finsa.Caravan.WebForms.Pages
// ReSharper restore CheckNamespace
{
   public partial class SecGroupList : PageBaseListAndSearch
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
            fdtgGroups.UpdateDataSource();
         }
      }

      #region Search Criteria

      protected override void FillSearchCriteria()
      {
      }

      protected override void RegisterSearchCriteria(SearchCriteria criteria)
      {
          SearchCriteria.RegisterControl(crvnGroupsLkp, "CGRP_NAME");
          SearchCriteria.CriteriaChanged += SearchCriteria_CriteriaChanged;
      }

      private void SearchCriteria_CriteriaChanged(SearchCriteria searchCriteria, SearchCriteriaChangedArgs args)
      {
          fdtgGroups.UpdateDataSource();
      }

      #endregion

      #region Grid Events

      protected void fdtgGroups_DataSourceUpdating(object sender, EventArgs args)
      {
          var groups = new DataTable();
          if(SearchCriteria["CGRP_NAME"].Count > 0)
          {
              var groupName = SearchCriteria["CGRP_NAME"][0];
              // This should not catch any exception, others will do.
              groups = (from g in DataAccess.Db.Security.GetGroups(Common.Properties.Settings.Default.ApplicationName)
                            select new SecGroup { Name = g.Name, Description = g.Description, Notes = g.Notes }).Where(x => x.Name == groupName.ToString())
                            .ToDataTable();
          }

          else
          {
             groups = (from g in DataAccess.Db.Security.GetGroups(Common.Properties.Settings.Default.ApplicationName)
                    select new SecGroup { Name = g.Name, Description = g.Description, Notes = g.Notes })
                           .ToDataTable();
          }

         fdtgGroups.DataSource = groups;
      }

      protected void fdtgGroups_OnRowDataBound(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType != DataControlRowType.DataRow)
         {
            return;
         }

         var groupName = DataBinder.Eval(e.Row.DataItem, "Name").ToJavaScriptString();

         var btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
         btnEdit.OnClientClick = String.Format("return editGroup({0});", groupName);

         var btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
         btnDelete.OnClientClick = String.Format("return deleteGroup({0});", groupName);
      }

      #endregion

      #region Buttons

      protected void hiddenRefresh_OnTriggered(object sender, EventArgs e)
      {
         try
         {
            fdtgGroups.UpdateDataSource();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      protected void hiddenDelete_OnTriggered(object sender, EventArgs e)
      {
         try
         {
            var groupName = groupNameToBeDeleted.Value;
            Raise<ArgumentException>.IfIsEmpty(groupName);
            DataAccess.Db.Security.RemoveGroup(Common.Properties.Settings.Default.ApplicationName, groupName);
            fdtgGroups.UpdateDataSource();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      protected void crvnExportList_DataSourceNeeded(object sender, EventArgs e)
      {
          try
          {
              crvnExportList.SetDataSource(fdtgGroups, "Groups", new FLEX.Web.UserControls.ExportList.ColumnData[]{
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=1,Name="Id", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=2,Name="Name", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=3,Name="Description", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=4,Name="Notes", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=6,Name="IsAdmin", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column}});
          }
          catch (Exception ex)
          {
              ErrorHandler.CatchException(ex);
          }
      }

      #endregion
   }
}