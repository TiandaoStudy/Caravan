using System;
using System.Web.UI;
using FLEX.Web.MasterPages;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
   public partial class MenuNavigator : PageBase
   {
      protected void Page_Load(object sender, EventArgs e)
      {
         var page = Request.RawUrl.Replace(Request.Path, "").Substring(1);
         Response.Redirect(Head.RootPath + page);
      }
   }
}