using System;
using System.Web.UI;
using System.Web.UI.WebControls;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.WebForms.UserControls.Templates
// ReSharper restore CheckNamespace
{
   [ToolboxData(@"<{0}:DataGridEmpty runat=""server""></{0}:DataGridEmpty>")]
   internal sealed class DataGridEmpty : UserControl
   {
      protected override void OnLoad(EventArgs args)
      {
         base.OnLoad(args);
         Controls.Add(new Literal {Text = @"<div class=""text-center h5"">There are currently no items in this table.</div>"});
      }
   }
}