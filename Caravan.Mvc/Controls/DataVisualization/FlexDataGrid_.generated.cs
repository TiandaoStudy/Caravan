﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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
    using System.Web.Mvc;
    
    #line 4 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
    using System.Web.Mvc.Ajax;
    
    #line default
    #line hidden
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 5 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
    using FLEX.Web.MVC.Controls.DataVisualization;
    
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
WriteLiteral("-container\">\r\n   ");


            
            #line 10 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
Write(Ajax.ActionLink("update-grid", "InitializeSearchGrid", new {searchCriteriaJson = "", pageIndex = 1}, new AjaxOptions
   {
      HttpMethod = "GET",
      InsertionMode = InsertionMode.Replace,
      UpdateTargetId = @Model.ID + "-container"
   }, new {id = "update-grid", @class="hidden"}));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n   <table id=\"");


            
            #line 17 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
         Write(Model.ID);

            
            #line default
            #line hidden
WriteLiteral("\" class=\"table table-striped table-bordered table-condensed table-hover\">\r\n      " +
"<thead>\r\n         <tr>\r\n");


            
            #line 20 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
             foreach (var column in Model.Columns)
            {

            
            #line default
            #line hidden
WriteLiteral("               <th>");


            
            #line 22 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
              Write(column.Header);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");


            
            #line 23 "..\..\Controls\DataVisualization\FlexDataGrid_.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("         </tr>\r\n      </thead>\r\n      \r\n      <tbody>\r\n         ");



WriteLiteral("\r\n      </tbody>\r\n   </table>\r\n</div>\r\n");


        }
    }
}
#pragma warning restore 1591