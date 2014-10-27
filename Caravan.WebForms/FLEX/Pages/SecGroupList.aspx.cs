using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.Diagnostics;
using Finsa.Caravan.Extensions;
using FLEX.Common.Web;
using FLEX.Web.Pages;

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
      }

      #endregion

      #region Grid Events

      protected void fdtgGroups_DataSourceUpdating(object sender, EventArgs args)
      {
         // This should not catch any exception, others will do.
         var groups = (from g in DataAccess.Db.Security.Groups(Common.Configuration.Instance.ApplicationName)
                       select new SecGroup {Id = g.Id, Name = g.Name, Description = g.Description, IsAdmin = g.IsAdmin})
                       .ToDataTable();
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
            DataAccess.Db.Security.RemoveGroup(Common.Configuration.Instance.ApplicationName, groupName);
            fdtgGroups.UpdateDataSource();
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      #endregion
   }
}