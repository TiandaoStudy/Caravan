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

   $(document).ready(function () {
      var searchCriteria = addSearchCriteria(""default"");
      searchCriteria.on('change:criteria', function (model, criteria) {
         $(""#update-grid"").attr(""href"", $(""#update-grid"").attr(""href"") + ""&searchCriteriaJson="" + Base64.encode64(JSON.stringify(criteria)));
         $(""#update-grid"").click();
      });
      initSearchCriteria(searchCriteria.get(""id""));

      // Movable panels
      var panelList = $(""#search-criteria-list"");
      panelList.sortable({
         handle: '.control-label',
         update: function () {
            var pageName = """);


            
            #line 28 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                       Write(VirtualPath);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            var userName = \"");


            
            #line 29 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                       Write(HttpContext.Current.User.Identity.Name);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            var key = userName + \"_VerticalSearch_\" + pageName;\r\n            " +
"var controlStatus = JSON.parse(localStorage.getItem(key));\r\n\r\n            // If " +
"no exists _statusControl, create the array _statusControl\r\n            if (contr" +
"olStatus == null) {\r\n               controlStatus = [];\r\n            }\r\n\r\n      " +
"      $(\'.vertical-search-criterium\', panelList).each(function (index, elem) {\r\n" +
"               var $listItem = $(elem), newIndex = $listItem.index();\r\n         " +
"      var id = elem.id.replace(\"li\", \"\");\r\n               var elemt = _.find(con" +
"trolStatus, function (i) { return i.id == id; });\r\n               if (elemt == n" +
"ull) {\r\n                  // Persist the new index.\r\n                  controlSt" +
"atus.push({ id: id, closed: false, index: newIndex });\r\n               } else {\r" +
"\n                  // Persist the new index.\r\n                  elemt.index = ne" +
"wIndex;\r\n               }\r\n            });\r\n\r\n            //Save in the localSto" +
"rage array _statusControl\r\n            localStorage.setItem(key, JSON.stringify(" +
"controlStatus));\r\n         }\r\n      });\r\n\r\n      $(\"#btn-hide-search-criteria\")." +
"click(function () {\r\n         var searchCriteriaPanel = $(\"#search-criteria-pane" +
"l\");\r\n         var btnCriteriaPlaceholder = $(\"#btn-criteria-placeholder\");\r\n   " +
"      var dataGridPanel = $(\"#data-grid-panel\");\r\n\r\n         if (!searchCriteria" +
"Panel.hasClass(\'hidden\')) {\r\n            // Collapse Search criteria and expand " +
"Data grid\r\n            searchCriteriaPanel.addClass(\"hidden\");\r\n            btnC" +
"riteriaPlaceholder.removeClass(\"hidden\");\r\n            var plHeight = dataGridPa" +
"nel.children().outerHeight();\r\n            btnCriteriaPlaceholder.css({ height: " +
"plHeight + \"px\", paddingTop: plHeight / 2 + \"px\" });\r\n            dataGridPanel." +
"switchClass(\"");


            
            #line 67 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                  Write(ViewBag.DataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\", \"");


            
            #line 67 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
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


            
            #line 78 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                  Write(ViewBag.ExpandedDataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\", \"");


            
            #line 78 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                                                            Write(ViewBag.DataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\", 0);\r\n            btnCriteriaPlaceholder.addClass(\"hidden\");\r\n            searc" +
"hCriteriaPanel.removeClass(\"hidden\");\r\n         }\r\n      });\r\n   });\r\n\r\n   funct" +
"ion onSearchCriteriumCollapsing(id) {\r\n      var pageName = \"");


            
            #line 86 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                 Write(VirtualPath);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n      var userName = \"");


            
            #line 87 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                 Write(HttpContext.Current.User.Identity.Name);

            
            #line default
            #line hidden
WriteLiteral(@""";
      var key = userName + ""_VerticalSearch_"" + pageName;
      var controlStatus = JSON.parse(localStorage.getItem(key));

      // If no exists _statusControl, create the array _statusControl
      if (controlStatus == null) {
         controlStatus = [];
      }

      if ($(""#icr"" + id).hasClass('glyphicon-chevron-down')) {
         $(""#icr"" + id).switchClass('glyphicon-chevron-down', 'glyphicon-chevron-up');
         var elemt = _.find(controlStatus, function (i) { return i.id == id; });
         if (elemt == null) {
            controlStatus.push({ id: id, closed: false, index: -1 });
         } else {
            elemt.closed = false;
         }

      } else if ($(""#icr"" + id).hasClass('glyphicon-chevron-up')) {
         $(""#icr"" + id).switchClass('glyphicon-chevron-up', 'glyphicon-chevron-down');
         var elemt = _.find(controlStatus, function (i) { return i.id == id; });
         if (elemt == null) {
            controlStatus.push({ id: id, closed: true, index: -1 });
         } else {
            elemt.closed = true;
         }
      }

      // Save in the localStorage the controlStatus array.
      localStorage.setItem(key, JSON.stringify(controlStatus));
   }

   ");


            
            #line 119 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
Write(RenderSection("ScriptSection", true));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n<!-- Search Criteria (START) -->\r\n<div id=\"search-criteria-panel\" " +
"class=\"");


            
            #line 123 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                                  Write(ViewBag.SearchCriteriaPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral(@""">
   <div class=""panel panel-primary"">
      <div class=""panel-heading"">
         <h3 class=""panel-title"">Search Criteria</h3>
      </div>
         
      <div class=""panel-body page-working-area"">
         <ul id=""search-criteria-list"" class=""panel-group list-unstyled"">
            ");


            
            #line 131 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
       Write(RenderSection("SearchCriteriaSection", true));

            
            #line default
            #line hidden
WriteLiteral(@"
         </ul>
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


            
            #line 163 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
                            Write(ViewBag.DataGridPanelCssClass);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n   <div class=\"panel panel-primary\">\r\n      <div class=\"panel-heading\">\r\n    " +
"     <h3 class=\"panel-title\">TITLE</h3>\r\n      </div>\r\n         \r\n      <div cla" +
"ss=\"page-working-area\">\r\n         ");


            
            #line 170 "..\..\Layouts\Pages\FlexVerticalSearchLayout_.cshtml"
    Write(RenderSection("DataGridSection", true));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n         \r\n      <div class=\"panel-footer\">\r\n            \r\n      " +
"</div>\r\n   </div>\r\n</div>\r\n<!-- Data Grid (END) -->");


        }
    }
}
#pragma warning restore 1591