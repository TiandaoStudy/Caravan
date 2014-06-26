using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FLEX.Web.WebControls.Templates
{
   [ToolboxData(@"<{0}:DataGridEmpty runat=""server""></{0}:DataGridEmpty>")]
   internal sealed class DataGridEmpty : WebControl
   {
      private void Page_Load(object sender, EventArgs e)
      {
         Controls.Add(new Literal {Text = "<div class=\"h5\">There are currently no items in this table.</div>"});
      }
   }
}