using System;
using FLEX.Common.Data;

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
            var ext = LoadControl(Configuration.Instance.ControlExtendersFolder + "/PageFooter.ascx");
            footerExtender.Controls.Add(ext);

            // ...
            if (IsPostBack) return;
            
            rptFooterInfo.DataSource = PageManager.Instance.GetFooterInfo();
            rptFooterInfo.DataBind();
         }
         catch (Exception ex)
         {
            DbLogger.Instance.LogError<PageFooter>("Page_Load", ex);
            throw;
         }
      }
   }
}