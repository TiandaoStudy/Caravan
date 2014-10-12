using System;

// ReSharper disable CheckNamespace
// This is the correct namespace, despite the file physical position.

namespace FLEX.Web.Pages
// ReSharper restore CheckNamespace
{
    public partial class AjaxLookup : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var xmlLookup = Request["xmlLookup"];
            var lookupBy = Request["lookupBy"];
            var userQuery = Request["userQuery"];
            var queryFilter = Request["queryFilter"];
            //var dt = Services.AjaxLookup.RetrieveData(xmlLookup, lookupBy, userQuery, queryFilter);
            //Response.Write(dt.ToJson());
        }
    }
}