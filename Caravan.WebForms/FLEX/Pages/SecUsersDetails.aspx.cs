﻿using System;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.Common.Utilities.Diagnostics;
using Finsa.Caravan.DataAccess;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;

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

         var user = Db.Security.GetUserByLogin(Common.Properties.Settings.Default.ApplicationName, login);
         Raise<ArgumentException>.IfIsNull(user, "Given user name does not exist");

         txtFirstName.Text = user.FirstName;
         txtLastName.Text = user.LastName;
         txtEmail.Text = user.Email;
         chkIsActive.Checked = user.Active;

         
     
      }

      #region Buttons

      protected void hiddenSave_OnTriggered(object sender, EventArgs e)
      {
          try
          {
              if (Mode == NewMode)
              {
                  var newUser = new SecUser { FirstName = txtFirstName.Text, LastName = txtLastName.Text, Email = txtEmail.Text, Active = chkIsActive.Checked };
                  Db.Security.AddUser(Common.Properties.Settings.Default.ApplicationName, newUser);

              }
              else if (Mode == EditMode)
              {
                  var newUser= new SecUser { FirstName = txtFirstName.Text, LastName = txtLastName.Text, Email = txtEmail.Text, Active = chkIsActive.Checked, Login = Login };
                  Db.Security.UpdateUser(Common.Properties.Settings.Default.ApplicationName, Login, newUser);
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