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

namespace Finsa.Caravan.Mvc.Controls.DataVisualization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    
    #line 4 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
    using System.Web.Mvc.Ajax;
    
    #line default
    #line hidden
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 6 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
    using Finsa.Caravan.Mvc.Controls.DataVisualization;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public static class CaravanDataGrid_
    {

public static System.Web.WebPages.HelperResult CaravanDataGrid(this AjaxHelper ajaxHelper, CaravanDataGridOptions options)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 9 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
 

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "   <script type=\"text/javascript\" defer=\"defer\">\r\n      $(document).ready(functio" +
"n() {\r\n         $(\'#");



#line 12 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\').bootstrapTable();\r\n      });\r\n   </script>\r\n");



#line 15 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"


#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "   <div id=\"");



#line 16 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "-container\">\r\n      ");



#line 17 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, ajaxHelper.ActionLink("update-grid", "InitializeSearchGrid", new {searchCriteriaJson = "", pageIndex = 1}, new AjaxOptions
      {
         HttpMethod = "GET",
         InsertionMode = InsertionMode.Replace,
         UpdateTargetId = @options.ID + "-container"
      }, new {id = "update-grid", @class = "hidden"}));

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\r\n\r\n      <table id=\"");



#line 24 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, options.ID);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\" class=\"table table-striped table-condensed table-hover\">\r\n         <thead>\r\n   " +
"         <tr>\r\n");



#line 27 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
                foreach (var column in options.Columns)
               {

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "                  <th>");



#line 29 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, column.Header);

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "</th>\r\n");



#line 30 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
               }

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "            </tr>\r\n         </thead>\r\n      \r\n         <tbody>\r\n");



#line 35 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
             foreach (var item in options.Items)
            {

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "               <tr>\r\n");



#line 38 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
                   foreach (var column in options.Columns)
                  {

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "                     <td>\r\n                        ");



#line 41 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
WebViewPage.WriteTo(@__razor_helper_writer, column.Control(item));

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "\r\n                     </td>\r\n");



#line 43 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
                  }

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "               </tr>\r\n");



#line 45 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"
            }

#line default
#line hidden

WebViewPage.WriteLiteralTo(@__razor_helper_writer, "         </tbody>\r\n      </table>\r\n   </div>\r\n");



#line 49 "..\..\Controls\DataVisualization\CaravanDataGrid_.cshtml"

#line default
#line hidden

});

}


    }
}
#pragma warning restore 1591
