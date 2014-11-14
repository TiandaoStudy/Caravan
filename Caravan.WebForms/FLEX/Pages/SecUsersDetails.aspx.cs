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
   public partial class SecUsersDetails : PopupBase
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
               Login = Request["login"];
               Raise<ArgumentException>.IfIsEmpty(Login);
               LoadForEdit(Login);
            }
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.ModalWindow | ErrorLocation.PageEvent); 
         }
      }

      private string Mode { get; set; }

      private string Login { get; set; }

      private void LoadForNew()
      {
         if (IsPostBack)
         {
            return;
         }

         txtUserId.Text = @"Automatically filled";

      }

      private void LoadForEdit(string login)
      {
         if (IsPostBack)
         {
            return;
         }

         var user = Db.Security.User(Common.Configuration.Instance.ApplicationName, login);
         Raise<ArgumentException>.IfIsNull(user, "Given user name does not exist");

         txtUserId.Text = user.Id.ToString(CultureInfo.InvariantCulture);
         txtFirstName.Text = user.FirstName;
         txtLastName.Text = user.LastName;
         txtEmail.Text = user.Email;
         chkIsActive.Checked = user.Active == 1;

         
     
      }

      #region Buttons

      //protected void hiddenSave_OnTriggered(object sender, EventArgs e)
      //{
      //   try
      //   {
      //      if (Mode == NewMode)
      //      {
      //         var newGroup = new SecGroup { Name = txtGrpName.Text, Description = txtGrpDescr.Text, Notes = txtNotes.Text, IsAdmin = chkAdmin.Checked ? 1 : 0 };
      //         Db.Security.AddGroup(Common.Configuration.Instance.ApplicationName, newGroup);

      //         foreach (DataRow oDrR in crvnMultiSelectUsersGroups.RightDataTable.Rows)
      //         {
      //            if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
      //            {
      //               Db.Security.AddUserToGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrR["Login"].ToString(), newGroup.Name);
      //            }
      //         }

      //         foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
      //         {
      //            if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
      //            {
      //               Db.Security.RemoveUserFromGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrL["Login"].ToString(), newGroup.Name);
      //            }
      //         }
              
            
      //      }
      //      else if (Mode == EditMode)
      //      {
             

      //         foreach (DataRow oDrR in crvnMultiSelectUsersGroups.RightDataTable.Rows)
      //         {
      //            if (oDrR[MultiSelect.FlagCrud].ToString() == "L")
      //            {
      //               Db.Security.AddUserToGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrR["Login"].ToString(), GroupName);
      //            }
      //         }

      //         foreach (DataRow oDrL in crvnMultiSelectUsersGroups.LeftDataTable.Rows)
      //         {
      //            if (oDrL[MultiSelect.FlagCrud].ToString() == "R")
      //            {
      //               Db.Security.RemoveUserFromGroup(Finsa.Caravan.Common.Configuration.Instance.ApplicationName, oDrL["Login"].ToString(), GroupName);
      //            }
      //         }


      //         var newGroup = new SecGroup { Name = txtGrpName.Text, Description = txtGrpDescr.Text, Notes= txtNotes.Text ,IsAdmin = chkAdmin.Checked ? 1 : 0 };
      //         Db.Security.UpdateGroup(Common.Configuration.Instance.ApplicationName, GroupName, newGroup);
      //      }
      //      Master.RegisterCloseScript(this);
      //   }
      //   catch (Exception ex)
      //   {
      //      ErrorHandler.CatchException(ex);
      //   }
      //}

      #endregion
   }
}