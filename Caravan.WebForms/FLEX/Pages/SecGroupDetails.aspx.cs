using System;
using Finsa.Caravan.Common.Security.Models;
using Finsa.Caravan.DataAccess;
using Finsa.CodeServices.Common;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;
using System.Linq;
using System.Data;
using Finsa.Caravan.Common;
using PommaLabs.Thrower;

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
      }

      private async void MostraMultiSelectForNew() 
      {
         var allowedUsers = await CaravanDataSource.Security.GetUsersAsync(CaravanCommonConfiguration.Instance.AppName);

          //Users
          DataTable tableLeft = new DataTable();
          tableLeft.Columns.Add("Login", typeof(string));
          tableLeft.Columns.Add("FirstName", typeof(string));
          tableLeft.Columns.Add("LastName", typeof(string));


          DataTable tableRight = new DataTable();
          tableRight.Columns.Add("Login", typeof(string));
          tableRight.Columns.Add("FirstName", typeof(string));
          tableRight.Columns.Add("LastName", typeof(string));


          foreach (var item in allowedUsers)
          {
              tableLeft.Rows.Add(item.Login, item.FirstName, item.LastName);
          }

          crvnMultiSelectUsersGroups.SetLeftDataSource(tableLeft);
          crvnMultiSelectUsersGroups.SetRightDataSource(tableRight);
          crvnMultiSelectUsersGroups.LeftPanelTitle = "Available Users";
          crvnMultiSelectUsersGroups.RightPanelTitle = "Chosen Users";
      }

      private async void LoadForEdit(string groupName)
      {
         if (IsPostBack)
         {
            return;
         }
      
         var group = await CaravanDataSource.Security.GetGroupByNameAsync(CaravanCommonConfiguration.Instance.AppName, groupName);
         Raise<ArgumentException>.IfIsNull(group, "Given group name does not exist");

         ViewState["group"] =group; 

         txtGrpName.Text = group.Name;
         txtGrpDescr.Text = group.Description;
         txtNotes.Text = group.Notes;
      }

      private async void MostraMultiSelectForEdit()
      {
          if (ViewState["group"] != null)
          {

              SecUser[] blockedUsersToGroup = null;// ((SecGroup)ViewState["group"]).Users;
              var allowedUsers = (await CaravanDataSource.Security.GetUsersAsync(CaravanCommonConfiguration.Instance.AppName)).Except(blockedUsersToGroup);

              //Users
              DataTable _tableLeft = new DataTable();
              _tableLeft.Columns.Add("Login", typeof(string));
              _tableLeft.Columns.Add("FirstName", typeof(string));
              _tableLeft.Columns.Add("LastName", typeof(string));

              DataTable _tableRight = new DataTable();
              _tableRight.Columns.Add("Login", typeof(string));
              _tableRight.Columns.Add("FirstName", typeof(string));
              _tableRight.Columns.Add("LastName", typeof(string));

              foreach (var item in allowedUsers)
              {
                  _tableLeft.Rows.Add(item.Login, item.FirstName, item.LastName);
              }

              foreach (var item in blockedUsersToGroup)
              {
                  _tableRight.Rows.Add(item.Login, item.FirstName, item.LastName);
              }

              crvnMultiSelectUsersGroups.SetLeftDataSource(_tableLeft);
              crvnMultiSelectUsersGroups.SetRightDataSource(_tableRight);
              crvnMultiSelectUsersGroups.LeftPanelTitle = "Available Users";
              crvnMultiSelectUsersGroups.RightPanelTitle = "Chosen Users";
          }

      }

      #region Buttons

      protected async void hiddenSave_OnTriggered(object sender, EventArgs e)
      {
         try
         {
        

            if (Mode == NewMode)
            {
               var newGroup = new SecGroup { Name = txtGrpName.Text, Description = txtGrpDescr.Text, Notes = txtNotes.Text };
               await CaravanDataSource.Security.AddGroupAsync(CaravanCommonConfiguration.Instance.AppName, newGroup);

               if (crvnMultiSelectUsersGroups.RightDataTable != null) 
               {
                   foreach (DataRow oDrR in crvnMultiSelectUsersGroups.RightDataTable.Rows)
                   {
                       if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
                       {
                           await CaravanDataSource.Security.AddUserToRoleAsync(CaravanCommonConfiguration.Instance.AppName, oDrR["Login"].ToString(), newGroup.Name, "role");
                       }
                   }

                   foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
                   {
                       if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
                       {
                           await CaravanDataSource.Security.RemoveUserFromRoleAsync(CaravanCommonConfiguration.Instance.AppName, oDrL["Login"].ToString(), newGroup.Name, "role");
                       }
                   }
               }
            
            }
            else if (Mode == EditMode)
            {

                if (crvnMultiSelectUsersGroups.RightDataTable != null) 
                {
                    foreach (DataRow oDrR in crvnMultiSelectUsersGroups.RightDataTable.Rows)
                    {
                        if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
                        {
                            await CaravanDataSource.Security.AddUserToRoleAsync(CaravanCommonConfiguration.Instance.AppName, oDrR["Login"].ToString(), GroupName, "role");
                        }
                    }

                    foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
                    {
                        if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
                        {
                            await CaravanDataSource.Security.RemoveUserFromRoleAsync(CaravanCommonConfiguration.Instance.AppName, oDrL["Login"].ToString(), GroupName, "role");
                        }
                    }
                }

               var newGroup = new SecGroupUpdates
               {
                   Name = Option.Some(txtGrpName.Text), 
                   Description = Option.Some(txtGrpDescr.Text), 
                   Notes = Option.Some(txtNotes.Text)
               };
               await CaravanDataSource.Security.UpdateGroupAsync(CaravanCommonConfiguration.Instance.AppName, GroupName, newGroup);
            }
            Master.RegisterCloseScript(this);
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex);
         }
      }

      protected void hiddenAddUserToGroup_OnTriggered(object sender, EventArgs e)
      {
          try
          {
              if (Mode == NewMode)
              {

                  MostraMultiSelectForNew();

              }
              else if (Mode == EditMode)
              {

                  MostraMultiSelectForEdit();
                
              }
            
          }
          catch (Exception ex)
          {
              ErrorHandler.CatchException(ex);
          }
      }

      #endregion
   }
}