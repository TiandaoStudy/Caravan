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

namespace FLEX.Web.MVC.Layouts.Pages
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
    
    #line 4 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
    using System.Web.Mvc.Ajax;
    
    #line default
    #line hidden
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 5 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
    using FLEX.Web.MVC.Controls.PageElements;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Layouts/Pages/FlexVerticalSearchLayout_.cshtml")]
    public partial class FlexVerticalSearchLayout_ : System.Web.Mvc.WebViewPage<dynamic>
    {
        public FlexVerticalSearchLayout_()
        {
        }
        public override void Execute()
        {


WriteLiteral("\r\n");



WriteLiteral("\r\n\r\n");



WriteLiteral("\r\n");


            
            #line 7 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
  
   ViewBag.SearchCriteriaPanelCssClass = "col-xs-12 col-sm-4-5 col-md-3-5 col-lg-2-5";
   ViewBag.DataGridPanelCssClass = "col-xs-12 col-sm-7-5 col-md-8-5 col-lg-9-5";
   ViewBag.ExpandedDataGridPanelCssClass = "col-xs-11-5";


            
            #line default
            #line hidden
WriteLiteral(@"

<script type=""text/javascript"">

   $(document).ready(function() {

      $(""#btn-hide-search-criteria"").click(function () {
         var searchCriteriaPanel = $(""#search-criteria-panel"");
         var btnCriteriaPlaceholder = $(""#btn-criteria-placeholder"");
         var dataGridPanel = $(""#data-grid-panel"");

         if (!searchCriteriaPanel.hasClass('hidden')) {
            // Collapse Search criteria and expand Data grid
            searchCriteriaPanel.addClass(""hidden"");
            btnCriteriaPlaceholder.removeClass(""hidden"");
            var plHeight = dataGridPanel.children().outerHeight();
            btnCriteriaPlaceholder.css({ height: plHeight + ""px"", paddingTop: plHeight / 2 + ""px"" });
            dataGridPanel.switchClass(""");


            
            #line 29 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                  Write(ViewBag.DataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\", \"");


            
            #line 29 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                                                    Write(ViewBag.ExpandedDataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral(@""", 0);
         }
      });

      $(""#btn-criteria-placeholder"").click(function () {
         var searchCriteriaPanel = $(""#search-criteria-panel"");
         var btnCriteriaPlaceholder = $(""#btn-criteria-placeholder"");
         var dataGridPanel = $(""#data-grid-panel"");

         if (searchCriteriaPanel.hasClass('hidden')) {
            // Collapse Data Grid and expand Search Criteria
            dataGridPanel.switchClass(""");


            
            #line 40 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                  Write(ViewBag.ExpandedDataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\", \"");


            
            #line 40 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                                                            Write(ViewBag.DataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\", 0);\r\n            btnCriteriaPlaceholder.addClass(\"hidden\");\r\n            searc" +
"hCriteriaPanel.removeClass(\"hidden\");\r\n         }\r\n      });\r\n   });\r\n\r\n   funct" +
"ion onSearchCriteriumCollapsing(id) {\r\n      var pageName = \"");


            
            #line 48 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                 Write(VirtualPath);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n      var userName = \"");


            
            #line 49 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                 Write(HttpContext.Current.User.Identity.Name);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n      var key = userName + \"_VerticalSearch_\" + pageName;\r\n      var controlS" +
"tatus = JSON.parse(localStorage.getItem(key));\r\n\r\n      // If no exists _statusC" +
"ontrol, create the array _statusControl\r\n      if (controlStatus == null) {\r\n   " +
"      controlStatus = [];\r\n      }\r\n\r\n      if ($(\"#icr\" + id).hasClass(\'glyphic" +
"on-chevron-down\')) {\r\n         $(\"#icr\" + id).switchClass(\'glyphicon-chevron-dow" +
"n\', \'glyphicon-chevron-up\');\r\n         var elemt = _.find(controlStatus, functio" +
"n (i) { return i.id == id; });\r\n         if (elemt == null) {\r\n            contr" +
"olStatus.push({ id: id, closed: false, index: -1 });\r\n         } else {\r\n       " +
"     elemt.closed = false;\r\n         }\r\n\r\n      } else if ($(\"#icr\" + id).hasCla" +
"ss(\'glyphicon-chevron-up\')) {\r\n         $(\"#icr\" + id).switchClass(\'glyphicon-ch" +
"evron-up\', \'glyphicon-chevron-down\');\r\n         var elemt = _.find(controlStatus" +
", function (i) { return i.id == id; });\r\n         if (elemt == null) {\r\n        " +
"    controlStatus.push({ id: id, closed: true, index: -1 });\r\n         } else {\r" +
"\n            elemt.closed = true;\r\n         }\r\n      }\r\n\r\n      // Save in the l" +
"ocalStorage the controlStatus array.\r\n      localStorage.setItem(key, JSON.strin" +
"gify(controlStatus));\r\n   }\r\n\r\n   var SearchCriteria = Backbone.Model.extend({\r\n" +
"      // Empty?\r\n   });\r\n\r\n   window.searchCriteria = new SearchCriteria();\r\n\r\n " +
"  searchCriteria.on(\'change:criteria\', function (model, criteria) {\r\n      alert" +
"(JSON.stringify(criteria));\r\n      $(\"#update-grid\").attr(\"href\", $(\"#update-gri" +
"d\").attr(\"href\") + \"&searchCriteriaJson=\" + Base64.encode64(JSON.stringify(crite" +
"ria)));\r\n      $(\"#update-grid\").click();\r\n   });\r\n\r\n   $(document).ready(functi" +
"on() {\r\n      searchCriteria.set({ criteria: [] });\r\n   });\r\n</script>\r\n\r\n<!-- S" +
"earch Criteria (START) -->\r\n<div id=\"search-criteria-panel\" class=\"");


            
            #line 99 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                  Write(ViewBag.SearchCriteriaPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n   <div class=\"panel panel-primary\">\r\n      <div class=\"panel-heading\">\r\n    " +
"     <h3 class=\"panel-title\">Search Criteria</h3>\r\n      </div>\r\n         \r\n    " +
"  <div class=\"panel-body page-working-area\">\r\n         ");


            
            #line 106 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
    Write(RenderSection("SearchCriteriaSection"));

            
            #line default
            #line hidden
WriteLiteral(@"
      </div>
         
      <div class=""panel-footer"">
         <div class=""row"">
            <div class=""col-xs-4 text-left"">
               <button type=""button"" id=""btn-hide-search-criteria"" class=""btn btn-info"">
                  <span id=""icon-hide-search-criteria"" class=""glyphicon glyphicon-arrow-left padded-icon""></span>Hide
               </button>
            </div>
            <div class=""col-xs-4 text-center"">
                  
            </div>
            <div class=""col-xs-4 text-right"">
               <button type=""button"" id=""btn-clear-search-criteria"" class=""btn btn-primary"">
                  <span id=""icon-clear-search-criteria"" class=""glyphicon glyphicon-trash padded-icon""></span>Clear
               </button>
            </div>
         </div>
      </div>
   </div>
</div>
<!-- Search Criteria (END) -->
   
<!-- Search Criteria Placeholder (START) -->
<button type=""button"" id=""btn-criteria-placeholder"" class=""hidden col-xs-0-5 btn btn-primary"">
   <span class=""glyphicon glyphicon-arrow-right""></span>
</button>
<!-- Search Criteria Placeholder (END) -->

<!-- Data Grid (START) -->
<div id=""data-grid-panel"" class=""");


            
            #line 137 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                            Write(ViewBag.DataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n   <div class=\"panel panel-primary\">\r\n      <div class=\"panel-heading\">\r\n    " +
"     <h3 class=\"panel-title\">TITLE</h3>\r\n      </div>\r\n         \r\n      <div cla" +
"ss=\"page-working-area\">\r\n         ");


            
            #line 144 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
    Write(RenderSection("DataGridSection"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n         \r\n      <div class=\"panel-footer\">\r\n            \r\n      " +
"</div>\r\n   </div>\r\n</div>\r\n<!-- Data Grid (END) -->");


        }
    }
}
#pragma warning restore 1591
