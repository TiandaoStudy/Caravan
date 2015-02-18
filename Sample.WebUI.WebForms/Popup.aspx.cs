using System;
using System.Text;

namespace FLEX.Sample.WebUI
{
    public partial class Popup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            StringBuilder script = new StringBuilder();

            //script.Append("<script type=\"text/javascript\">\n")
            //    .Append("var retVal = new Object;\n")
            //    .Append("retVal.name = '").Append(TextBoxName.Text).Append("';\n")
            //    .Append("retVal.surname = '").Append(TextBoxSurname.Text).Append("';\n")
            //    .Append("top.returnValue = retVal;\n")
            //    .Append("closeWindow();\n")
            //    .Append("</script>");

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "retVal", script.ToString().Replace("\n",System.Environment.NewLine));
        }

    }
}