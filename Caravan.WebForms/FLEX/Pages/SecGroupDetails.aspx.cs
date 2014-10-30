using System;
using System.Globalization;
using Finsa.Caravan.DataAccess;
using Finsa.Caravan.DataModel;
using Finsa.Caravan.DataModel.Security;
using Finsa.Caravan.Diagnostics;
using FLEX.Web.Pages;
using FLEX.Web.UserControls.Ajax;

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

      private void LoadForEdit(string groupName)
      {
         if (IsPostBack)
         {
            return;
         }

         var group = DataAccess.Db.Security.Group(Common.Configuration.Instance.ApplicationName, groupName);
         Raise<ArgumentException>.IfIsNull(group, "Given group name does not exist");

         txtGrpId.Text = group.Id.ToString(CultureInfo.InvariantCulture);
         txtGrpName.Text = group.Name;
         txtGrpDescr.Text = group.Description;
         chkAdmin.Checked = group.IsAdmin;
      }

      #region Buttons

      protected void hiddenSave_OnTriggered(object sender, EventArgs e)
      {
         try
         {
            if (Mode == NewMode)
            {
            
            }
            else if (Mode == EditMode)
            {
               var newGroup = new SecGroup {Name = txtGrpName.Text, Description = txtGrpDescr.Text, IsAdmin = chkAdmin.Checked};
               DataAccess.Db.Security.UpdateGroup(Common.Configuration.Instance.ApplicationName, GroupName, newGroup);
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