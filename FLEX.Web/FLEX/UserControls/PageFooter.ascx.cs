using System;
using System.Web;
using System.Web.UI;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.UserControls
// ReSharper restore CheckNamespace
{
  public partial class PageFooter : UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      
       lblHost.Text = "Host : "+HttpContext.Current.Server.MachineName;
    }
  }
}