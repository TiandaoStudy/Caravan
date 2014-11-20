using System;
using System.Globalization;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.Diagnostics;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;
using System.Collections.Generic;
using System.Linq;
using System.Data;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace Finsa.Caravan.WebForms.Pages
// ReSharper restore CheckNamespace
{
   public partial class SecGroupDetails : PopupBase
   {
      private const string NewMode = "new";
      private const string EditMode = "edit";

      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
                 Mode = Request["mode"];
                 Raise<ArgumentException>.IfIsEmpty(Mode, "Page mode cannot be empty");
                 Mode = Mode.ToLower();
                 Raise<ArgumentException>.IfNot(Mode == NewMode || Mode == EditMode, "Invalid mode value");

                 if (Mode == NewMode)
                 {
                     LoadForNew();
                 }
                 else if (Mode == EditMode)
                 {
                     GroupName = Request["groupName"];
                     Raise<ArgumentException>.IfIsEmpty(GroupName);
                     LoadForEdit(GroupName);
                 }
           
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.ModalWindow | ErrorLocation.PageEvent); 
         }
      }

      private string Mode { get; set; }

      private string GroupName { get; set; }

      private void LoadForNew()
      {
         if (IsPostBack)
         {
            return;
         }

         txtGrpId.Text = @"Automatically filled";

         var allowedUsers = Db.Security.Users(Finsa.Caravan.Common.Configuration.Instance.ApplicationName).ToList();

         //Users
         DataTable _tableLeft = new DataTable();
         _tableLeft.Columns.Add("Id", typeof(int));
         _tableLeft.Columns.Add("Login", typeof(string));
         _tableLeft.Columns.Add("FirstName", typeof(string));
         _tableLeft.Columns.Add("LastName", typeof(string));


         DataTable _tableRight = new DataTable();
         _tableRight.Columns.Add("Id", typeof(int));
         _tableRight.Columns.Add("Login", typeof(string));
         _tableRight.Columns.Add("FirstName", typeof(string));
         _tableRight.Columns.Add("LastName", typeof(string));


         foreach (var item in allowedUsers)
         {
            _tableLeft.Rows.Add(item.Id, item.Login, item.FirstName, item.LastName);
         }

         crvnMultiSelectUsersGroups.SetLeftDataSource(_tableLeft);
         crvnMultiSelectUsersGroups.SetRightDataSource(_tableRight);
         crvnMultiSelectUsersGroups.LeftPanelTitle = "Available Users";
         crvnMultiSelectUsersGroups.RightPanelTitle = "Chosen Users";

      }

      private void LoadForEdit(string groupName)
      {
         if (IsPostBack)
         {
            return;
         }

         var group = Db.Security.Group(Common.Configuration.Instance.ApplicationName, groupName);
         Raise<ArgumentException>.IfIsNull(group, "Given group name does not exist");

         txtGrpId.Text = group.Id.ToString(CultureInfo.InvariantCulture);
         txtGrpName.Text = group.Name;
         txtGrpDescr.Text = group.Description;
         txtNotes.Text = group.Notes;
         chkAdmin.Checked = group.IsAdmin == 1;

         var blockedUsersToGroup = group.Users.ToList();
         var allowedUsers = Db.Security.Users(Finsa.Caravan.Common.Configuration.Instance.ApplicationName).Except(blockedUsersToGroup).ToList();

         //Users
         DataTable _tableLeft = new DataTable();
         _tableLeft.Columns.Add("Id", typeof(int));
         _tableLeft.Columns.Add("Login", typeof(string));
         _tableLeft.Columns.Add("FirstName", typeof(string));
         _tableLeft.Columns.Add("LastName", typeof(string));

         DataTable _tableRight = new DataTable();
         _tableRight.Columns.Add("Id", typeof(int));
         _tableRight.Columns.Add("Login", typeof(string));
         _tableRight.Columns.Add("FirstName", typeof(string));
         _tableRight.Columns.Add("LastName", typeof(string));

         foreach (var item in allowedUsers)
         {
             _tableLeft.Rows.Add(item.Id, item.Login, item.FirstName, item.LastName);
         }

         foreach (var item in blockedUsersToGroup)
         {
             _tableRight.Rows.Add(item.Id, item.Login, item.FirstName, item.LastName);
         }

         crvnMultiSelectUsersGroups.SetLeftDataSource(_tableLeft);
         crvnMultiSelectUsersGroups.SetRightDataSource(_tableRight);
         crvnMultiSelectUsersGroups.LeftPanelTitle = "Available Users";
         crvnMultiSelectUsersGroups.RightPanelTitle = "Chosen Users";
      }

      #region Buttons

      protected void hiddenSave_OnTriggered(object sender, EventArgs e)
      {
         try
         {
            if (Mode == NewMode)
            {
               var newGroup = new SecGroup { Name = txtGrpName.Text, Description = txtGrpDescr.Text, Notes = txtNotes.Text, IsAdmin = chkAdmin.Checked ? 1 : 0 };
               Db.Security.AddGroup(Common.Configuration.Instance.ApplicationName, newGroup);

               foreach (DataRow oDrR in crvnMultiSelectUsersGroups.RightDataTable.Rows)
               {
                  if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
                  {
                     Db.Security.AddUserToGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrR["Login"].ToString(), newGroup.Name);
                  }
               }

               foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
               {
                  if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
                  {
                     Db.Security.RemoveUserFromGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrL["Login"].ToString(), newGroup.Name);
                  }
               }
              
            
            }
            else if (Mode == EditMode)
            {
             

               foreach (DataRow oDrR in crvnMultiSelectUsersGroups.RightDataTable.Rows)
               {
                  if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
                  {
                     Db.Security.AddUserToGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrR["Login"].ToString(), GroupName);
                  }
               }

               foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
               {
                  if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
                  {
                     Db.Security.RemoveUserFromGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrL["Login"].ToString(), GroupName);
                  }
               }


               var newGroup = new SecGroup { Name = txtGrpName.Text, Description = txtGrpDescr.Text, Notes= txtNotes.Text ,IsAdmin = chkAdmin.Checked ? 1 : 0 };
               Db.Security.UpdateGroup(Common.Configuration.Instance.ApplicationName, GroupName, newGroup);
            }
            Master.RegisterCloseScript(this);
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      #endregion
   }
}