﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FLEX.Web.MVC.Controls.ValueHolders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    
    #line 4 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 5 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
    using FLEX.Web.MVC.Controls.ValueHolders;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
    using PommaLabs.GRAMPA.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public static class BootstrapSwitch_
    {

public static System.Web.WebPages.HelperResult BootstrapSwitch(this HtmlHelper htmlHelper, BootstrapSwitchOptions options) {
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 8 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
                                                                                     


#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "   <input id=\"");



#line 10 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\" type=\"checkbox\" />\r\n");



#line 11 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"


#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "   <script type=\"text/javascript\">\r\n      $(\"#");



#line 13 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\").bootstrapSwitch({\r\n         state: ");



#line 14 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.State.ToJavaScriptBoolean());

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, ",\r\n         size: \"");



#line 15 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.SizeString);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\"\r\n      });\r\n   </script>\r\n");



#line 18 "..\..\Controls\ValueHolders\BootstrapSwitch_.cshtml"

#line default
#line hidden

});

}


    }
}
#pragma warning restore 1591
