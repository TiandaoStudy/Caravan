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
    
    #line 4 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 5 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
    using FLEX.Web.MVC.Controls.ValueHolders;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
    using PommaLabs.GRAMPA.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public static class JQuerySelect2_
    {

public static System.Web.WebPages.HelperResult JQuerySelect2(this HtmlHelper htmlHelper, JQuerySelect2Options options) {
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 8 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
                                                                                 


#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "   <input id=\"");



#line 10 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\" type=\"hidden\" class=\"bigdrop\" style=\"width: 100%\" />\r\n");



#line 11 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
   

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "   <script type=\"text/javascript\">\r\n      $(document).ready(function () {\r\n      " +
"   $(\"#");



#line 14 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\").select2({\r\n            allowClear: ");



#line 15 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.AllowClear.ToJavaScriptBoolean());

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, ",\r\n            minimumInputLength: ");



#line 16 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.MinimumInputLength);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, ",\r\n            placeholder: \"");



#line 17 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.PlaceHolder);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\",\r\n            query: function (query) {\r\n               $.ajax(\"");



#line 19 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.DataSourceUrl);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\", {\r\n                  dataType: \"json\",\r\n                  type: \"POST\",\r\n     " +
"             data: {q: ");



#line 22 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.QueryBuilder);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "(query.term) },\r\n                  success: function(data, textStatus, jqXhr) {\r\n" +
"                     data = _.map(data, ");



#line 24 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.DataMapper);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, ");\r\n                     query.callback({ more: false, results: data });\r\n       " +
"           }\r\n               });\r\n            }\r\n         });\r\n\r\n         $(\"#");



#line 31 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\").on(\"change\", function (e) {\r\n            // Custom handlers BUG FIXME\r\n       " +
"     // TODO\r\n            \r\n            // Search criteria handling\r\n           " +
" var searchCriteria = getSearchCriteria(\"");



#line 36 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
         WebViewPage.WriteTo(@__razor_helper_writer, options.SearchCriteriaID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\");\r\n            if (searchCriteria !== undefined) {\r\n               var criteria" +
" = searchCriteria.get(\"criteria\");\r\n               if (e.val !== \"undefined\" && " +
"e.val != null && e.val != \"\") {\r\n                  criteria[\"");



#line 40 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.SafeID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\"] = e.val;\r\n               } else {\r\n                  delete criteria[\"");



#line 42 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.SafeID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\"];\r\n               }\r\n               searchCriteria.set({ criteria: criteria });" +
"\r\n            }\r\n         });\r\n      });\r\n   </script>\r\n");



#line 49 "..\..\Controls\ValueHolders\JQuerySelect2_.cshtml"

#line default
#line hidden

});

}


    }
}
#pragma warning restore 1591
