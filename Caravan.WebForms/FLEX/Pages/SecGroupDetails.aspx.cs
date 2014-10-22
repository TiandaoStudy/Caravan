using System;
using System.Globalization;
using Finsa.Caravan.DataAccess;
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
            var mode = Request["mode"];
            Raise<ArgumentException>.IfIsEmpty(mode, "Page mode cannot be empty");
            mode = mode.ToLower();
            Raise<ArgumentException>.IfNot(mode == NewMode || mode == EditMode, "Invalid mode value");

            if (mode == NewMode)
            {
               LoadForNew();
            }
            else if (mode == EditMode)
            {
               var grpName = Request["groupName"];
               Raise<ArgumentException>.IfIsEmpty(grpName);
               LoadForEdit(grpName);
            }
         }
         catch (Exception ex)
         {
            ErrorHandler.CatchException(ex, ErrorLocation.ModalWindow | ErrorLocation.PageEvent); 
         }
      }

      private void LoadForNew()
      {
         txtGrpId.Text = @"Automatically filled";
      }

      private void LoadForEdit(string groupName)
      {
         var group = Db.Security.Group(Common.Configuration.Instance.ApplicationName, groupName);
         Raise<ArgumentException>.IfIsNull(group, "Given group name does not exist");

         txtGrpId.Text = group.Id.ToString(CultureInfo.InvariantCulture);
         txtGrpName.Text = group.Name;
         txtGrpDescr.Text = group.Description;
         chkAdmin.Checked = group.IsAdmin;
      }
   }
}