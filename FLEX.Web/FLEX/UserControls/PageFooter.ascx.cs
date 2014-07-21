using System;
using System.Linq;
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
         try
         {
            if (!IsPostBack)
            {
               var pageManagerTypeInfo = Configuration.Instance.PageManagerTypeInfo;
               var pageManager = ServiceLocator.Load<IPageManager>(pageManagerTypeInfo);
               rptFooterInfo.DataSource = pageManager.GetFooterInfo().ToList();
               rptFooterInfo.DataBind();
            }
         }
         catch (Exception ex)
         {
            QuickLogger.LogError<PageFooter>(ex);
            throw;
         }
         // lblHost.Text = "Host: " + HttpContext.Current.Server.MachineName;
      }
   }
}