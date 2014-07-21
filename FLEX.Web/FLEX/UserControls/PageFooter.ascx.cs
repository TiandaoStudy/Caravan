using System;
using System.Diagnostics;
using System.Web.UI;
using FLEX.Common;
using FLEX.Common.Web;
using Pair = FLEX.Common.Pair;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
   public partial class PageFooter : UserControl
   {
      private static readonly string FlexVersion = "v" + FileVersionInfo.GetVersionInfo(typeof (PageFooter).Assembly.Location).FileVersion;

      protected void Page_Load(object sender, EventArgs e)
      {
         // ...
         if (IsPostBack) return;

         try
         {
            var pageManagerTypeInfo = Configuration.Instance.PageManagerTypeInfo;
            var pageManager = ServiceLocator.Load<IPageManager>(pageManagerTypeInfo);
            var footerInfo = pageManager.GetFooterInfo();
            
            // We add the FLEX version to the footer info.
            footerInfo.Add(Pair.Create("FLEX", FlexVersion));

            rptFooterInfo.DataSource = footerInfo;
            rptFooterInfo.DataBind();
         }
         catch (Exception ex)
         {
            QuickLogger.LogError<PageFooter>(ex);
            throw;
         }
      }
   }
}