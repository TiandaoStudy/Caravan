﻿using System;
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
using System.Data;
using System.Collections.Generic;


// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace Finsa.Caravan.WebForms.Pages
// ReSharper restore CheckNamespace
{
   public partial class SecUsersList : PageBaseListAndSearch
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         if (!IsPostBack)
         {
             fdtgUsers.UpdateDataSource();
         }
      }

      #region Search Criteria

      protected override void FillSearchCriteria()
      {
          var active = new List<CKeyValuePair<String, String>>();
          active.Add(CKeyValuePair.Create("Active", "Y"));
          active.Add(CKeyValuePair.Create("Inactive", "N"));
          crvnActive.SetDataSource(active);

          //Valore di default per "Active": "Y"
          crvnActive.VisibleCheckBoxList.Items[0].Selected = true;
      }

      protected override void RegisterSearchCriteria(SearchCriteria criteria)
      {
          SearchCriteria.RegisterControl(crvnUsersLkp, "CUSR_LOGIN");
          SearchCriteria.RegisterControl(crvnActive, "Active");
          SearchCriteria.CriteriaChanged += SearchCriteria_CriteriaChanged;
      }

      private void SearchCriteria_CriteriaChanged(SearchCriteria searchCriteria, SearchCriteriaChangedArgs args)
      {
          fdtgUsers.UpdateDataSource();
      }

      #endregion

      #region Grid Events

      protected void fdtgUsers_DataSourceUpdating(object sender, EventArgs args)
      {
          var users = new DataTable();
          var active = 0;
          var userLogin = "";
          if (SearchCriteria["CUSR_LOGIN"].Count > 0 && SearchCriteria["Active"].Count == 1)
          {
              userLogin = SearchCriteria["CUSR_LOGIN"][0];
              active = crvnActive.SelectedValues[0] == "Y" ? 1 : 0;
              // This should not catch any exception, others will do.
              users = (from us in DataAccess.Db.Security.Users(Common.Configuration.Instance.ApplicationName)
                       select new SecUser { Id = us.Id, FirstName = us.FirstName, LastName = us.LastName, Login = us.Login, Email= us.Email, Active= us.Active }).Where(x => x.Login == userLogin.ToString() && x.Active == active)
                            .ToDataTable();

              fdtgUsers.DataSource = users;
              return;
          }
          else if (SearchCriteria["CUSR_LOGIN"].Count > 0) 
          {
              userLogin = SearchCriteria["CUSR_LOGIN"][0];
              users = (from us in DataAccess.Db.Security.Users(Common.Configuration.Instance.ApplicationName)
                       select new SecUser { Id = us.Id, FirstName = us.FirstName, LastName = us.LastName, Login = us.Login, Email = us.Email, Active = us.Active }).Where(x => x.Login == userLogin.ToString())
              .ToDataTable();

              fdtgUsers.DataSource = users;
              return;
          }

          else if (SearchCriteria["Active"].Count == 1)
          {
              active = crvnActive.SelectedValues[0] == "Y" ? 1 : 0;
              users = (from us in DataAccess.Db.Security.Users(Common.Configuration.Instance.ApplicationName)
                       select new SecUser { Id = us.Id, FirstName = us.FirstName, LastName = us.LastName, Login = us.Login, Email = us.Email, Active = us.Active }).Where(x => x.Active == active)
                           .ToDataTable();

              fdtgUsers.DataSource = users;
              return;
          }
          else
          {
              users = (from us in DataAccess.Db.Security.Users(Common.Configuration.Instance.ApplicationName)
                       select new SecUser { Id = us.Id, FirstName = us.FirstName, LastName = us.LastName, Login = us.Login, Email = us.Email, Active = us.Active })
                          .ToDataTable();

              fdtgUsers.DataSource = users;
              return;
          }
      }

      protected void fdtgUsers_OnRowDataBound(object sender, GridViewRowEventArgs e)
      {
         if (e.Row.RowType != DataControlRowType.DataRow)
         {
            return;
         }

         var login = DataBinder.Eval(e.Row.DataItem, "Login").ToJavaScriptString();

         var btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
         btnEdit.OnClientClick = String.Format("return editUser({0});", login);

         var btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
         btnDelete.OnClientClick = String.Format("return deleteUser({0});", login);
      }

      #endregion

      #region Buttons

      protected void hiddenRefresh_OnTriggered(object sender, EventArgs e)
      {
         try
         {
             fdtgUsers.UpdateDataSource();
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
            var login = loginToBeDeleted.Value;
            Raise<ArgumentException>.IfIsEmpty(login);
            DataAccess.Db.Security.RemoveUser(Common.Configuration.Instance.ApplicationName, login);
            fdtgUsers.UpdateDataSource();
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
            crvnExportList.SetDataSource(fdtgUsers, "Users", new FLEX.Web.UserControls.ExportList.ColumnData[]{
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=1,Name="Id", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=2,Name="FirstName", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=3,Name="LastName", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=4,Name="Login", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=5,Name="Email", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column},
                                            new FLEX.Web.UserControls.ExportList.ColumnData{Index=7,Name="Active", Type=FLEX.Web.UserControls.ExportList.ColumnType.Column}});
          }
          catch (Exception ex)
          {
              ErrorHandler.CatchException(ex);
          }
      }

      #endregion
   }
}