using System;
using FLEX.DataAccess;
using FLEX.WebForms;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class PageFooter : ControlBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         try
         {
            // Footer extender
            var ext = LoadControl(WebForms.Configuration.Instance.ControlExtendersFolder + "/PageFooter.ascx");
            footerExtender.Controls.Add(ext);

            // ...
            if (IsPostBack) return;
            
            rptFooterInfo.DataSource = PageManager.Instance.GetFooterInfo();
            rptFooterInfo.DataBind();
         }
         catch (Exception ex)
         {
            Logger.Instance.LogError<PageFooter>(ex);
            throw;
         }
      }
   }
}