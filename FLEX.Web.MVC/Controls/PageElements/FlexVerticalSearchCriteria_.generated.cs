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

namespace FLEX.Web.MVC.Controls.PageElements
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    
    #line 4 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 5 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
    using FLEX.Web.MVC.Controls.PageElements;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public static class FlexVerticalSearchCriteria_
    {

public static System.Web.WebPages.HelperResult FlexVerticalSearchCriteria(this HtmlHelper htmlHelper, IEnumerable<FlexVerticalSearchCriteriumOptions> spec)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 8 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
 
   var i = 0;
   foreach (var s in spec)
   {

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "      <li class=\"vertical-search-criterium form-group\" id=\"search-criterium-1\">\r\n" +
"         <label class=\"control-label\" data-toggle=\"collapse\" data-target=\"#searc" +
"h-criterium-collapse-");



#line 13 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
                                                          WebViewPage.WriteTo(@__razor_helper_writer, i);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\" onclick=\" onSearchCriteriumCollapsing(");



#line 13 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
                                                                                                    WebViewPage.WriteTo(@__razor_helper_writer, i);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "); \">\r\n            <i id=\"icr");



#line 14 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, i);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\" class=\"glyphicon glyphicon-chevron-up padded-icon\"></i>");



#line 14 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
                                        WebViewPage.WriteTo(@__razor_helper_writer, s.Label);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\r\n         </label>\r\n         <div id=\"search-criterium-collapse-");



#line 16 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
 WebViewPage.WriteTo(@__razor_helper_writer, i);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\" class=\"collapse in\">\r\n            ");



#line 17 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, s.Control());

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\r\n         </div>\r\n      </li>\r\n");



#line 20 "..\..\Controls\PageElements\FlexVerticalSearchCriteria_.cshtml"
      ++i;
   }

#line default
#line hidden

});

}


    }
}
#pragma warning restore 1591
