﻿using System;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Finsa.Caravan.DataAccess;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;
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
      }

      private void MostraMultiSelectForNew() 
      {
         var allowedUsers = Db.Security.Users(Common.Properties.Settings.Default.ApplicationName);

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

      private void LoadForEdit(string groupName)
      {
         if (IsPostBack)
         {
            return;
         }
      
         var group = Db.Security.Group(Common.Properties.Settings.Default.ApplicationName, groupName);
         Raise<ArgumentException>.IfIsNull(group, "Given group name does not exist");

         ViewState["group"] =group; 

         txtGrpName.Text = group.Name;
         txtGrpDescr.Text = group.Description;
         txtNotes.Text = group.Notes;
      }

      private void MostraMultiSelectForEdit()
      {
          if (ViewState["group"] != null)
          {

              var blockedUsersToGroup = ((SecGroup)ViewState["group"]).Users;
              var allowedUsers = Db.Security.Users(Common.Properties.Settings.Default.ApplicationName).Except(blockedUsersToGroup);

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

      protected void hiddenSave_OnTriggered(object sender, EventArgs e)
      {
         try
         {
        

            if (Mode == NewMode)
            {
               var newGroup = new SecGroup { Name = txtGrpName.Text, Description = txtGrpDescr.Text, Notes = txtNotes.Text };
               Db.Security.AddGroup(Common.Properties.Settings.Default.ApplicationName, newGroup);

               if (crvnMultiSelectUsersGroups.RightDataTable != null) 
               {
                   foreach (DataRow oDrR in crvnMultiSelectUsersGroups.RightDataTable.Rows)
                   {
                       if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
                       {
                           Db.Security.AddUserToGroup(Finsa.Caravan.Common.Properties.Settings.Default.ApplicationName, oDrR["Login"].ToString(), newGroup.Name);
                       }
                   }

                   foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
                   {
                       if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
                       {
                           Db.Security.RemoveUserFromGroup(Finsa.Caravan.Common.Properties.Settings.Default.ApplicationName, oDrL["Login"].ToString(), newGroup.Name);
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
                            Db.Security.AddUserToGroup(Finsa.Caravan.Common.Properties.Settings.Default.ApplicationName, oDrR["Login"].ToString(), GroupName);
                        }
                    }

                    foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
                    {
                        if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
                        {
                            Db.Security.RemoveUserFromGroup(Finsa.Caravan.Common.Properties.Settings.Default.ApplicationName, oDrL["Login"].ToString(), GroupName);
                        }
                    }
                }

               var newGroup = new SecGroup { Name = txtGrpName.Text, Description = txtGrpDescr.Text, Notes= txtNotes.Text };
               Db.Security.UpdateGroup(Common.Properties.Settings.Default.ApplicationName, GroupName, newGroup);
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