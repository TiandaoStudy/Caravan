using System;
using System.Diagnostics;
using System.Web.UI;
using FLEX.Common;
using FLEX.Common.Web;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class PageFooter : UserControl
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         // ...
         if (IsPostBack) return;

         try
         {
            var pageManagerTypeInfo = Configuration.Instance.PageManagerTypeInfo;
            var pageManager = ServiceLocator.Load<IPageManager>(pageManagerTypeInfo);
            rptFooterInfo.DataSource = pageManager.GetFooterInfo();
            rptFooterInfo.DataBind();
         }
         catch (Exception ex)
         {
            DbLogger.Instance.LogError<PageFooter>("Page_Load(sender, e)", ex);
            throw;
         }
      }
   }
}