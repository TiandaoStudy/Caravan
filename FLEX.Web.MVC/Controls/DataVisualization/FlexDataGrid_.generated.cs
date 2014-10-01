﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FLEX.Web.MVC.Controls.DataVisualization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 4 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
    using FLEX.Web.MVC.Controls.DataVisualization;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
    using PagedList.Mvc;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Controls/DataVisualization/FlexDataGrid_.cshtml")]
    public partial class FlexDataGrid_ : System.Web.Mvc.WebViewPage<FlexDataGridOptions>
    {
        public FlexDataGrid_()
        {
        }
        public override void Execute()
        {


WriteLiteral("\r\n");



WriteLiteral("\r\n\r\n");



WriteLiteral("\r\n");


WriteLiteral("\r\n<div id=\"");


            
            #line 9 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
    Write(Model.ID);

            
            #line default
            #line hidden
WriteLiteral("-container\">\r\n   <table id=\"");


            
            #line 10 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
         Write(Model.ID);

            
            #line default
            #line hidden
WriteLiteral("\" class=\"table table-striped table-bordered table-condensed table-hover\">\r\n      " +
"<thead>\r\n");


            
            #line 12 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
          foreach (var column in Model.Columns)
         {

            
            #line default
            #line hidden
WriteLiteral("            <th>");


            
            #line 14 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
           Write(column.Header);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");


            
            #line 15 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
         }

            
            #line default
            #line hidden
WriteLiteral("      </thead>\r\n      \r\n      <tbody>\r\n\r\n");


            
            #line 20 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
        for (int indexItem = 0; indexItem < Model.PagedItems.Count; indexItem++ )
       {

            
            #line default
            #line hidden
WriteLiteral("           <tr>\r\n                \r\n");


            
            #line 24 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
             foreach (var column in Model.Columns)
            {

            
            #line default
            #line hidden
WriteLiteral("               <td>\r\n                  ");


            
            #line 27 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
             Write(column.Control(Model.PagedItems[indexItem]));

            
            #line default
            #line hidden
WriteLiteral("\r\n               </td>\r\n");


            
            #line 29 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            </tr>\r\n");


            
            #line 31 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
       }

            
            #line default
            #line hidden
WriteLiteral(@"                 
      </tbody>
   </table>
   
   <div class=""row"">
      <div class=""col-xs-4 text-left"">
         Count
      </div>
      <div class=""col-xs-4 text-center"">
         ???
      </div>
      <div class=""col-xs-4 text-right"">
         <!-- Outputs a paging control that lets the user navigation to the previous page, next page, etc -->
         ");


            
            #line 45 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
    Write(Html.PagedListPager(Model.PagedItems, Model.PagerAction));

            
            #line default
            #line hidden
WriteLiteral("\r\n         <script type=\"text/javascript\">\r\n            var pageLinks = $(\"#");


            
            #line 47 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
                           Write(Model.ID);

            
            #line default
            #line hidden
WriteLiteral("-container .pagination a\");\r\n            pageLinks.attr(\"data-ajax\", \"true\");\r\n  " +
"          pageLinks.attr(\"data-ajax-method\", \"GET\");\r\n            pageLinks.attr" +
"(\"data-ajax-mode\", \"replace\");\r\n            pageLinks.attr(\"data-ajax-update\", \"" +
"#");


            
            #line 51 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
                                            Write(Model.ID);

            
            #line default
            #line hidden
WriteLiteral("-container\");\r\n         </script>\r\n      </div>\r\n   </div>\r\n</div>\r\n");


        }
    }
}
#pragma warning restore 1591
