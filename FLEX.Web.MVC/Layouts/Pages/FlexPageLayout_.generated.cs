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
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 4 "..\..\Layouts\Pages\FlexPageLayout_.cshtml"
    using FLEX.Web.MVC.Controls.PageElements;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Layouts/Pages/FlexPageLayout_.cshtml")]
    public partial class FlexPageLayout_ : System.Web.Mvc.WebViewPage<dynamic>
    {
        public FlexPageLayout_()
        {
        }
        public override void Execute()
        {


WriteLiteral("\r\n");



WriteLiteral("\r\n\r\n");


WriteLiteral("\r\n");


            
            #line 6 "..\..\Layouts\Pages\FlexPageLayout_.cshtml"
  
   Layout = "~/Layouts/FlexSharedLayout_.cshtml";


            
            #line default
            #line hidden
WriteLiteral("\r\n");


DefineSection("HeadSection", () => {

WriteLiteral("\r\n   \r\n   ");


            
            #line 12 "..\..\Layouts\Pages\FlexPageLayout_.cshtml"
Write(RenderSection("HeadSection"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


});

WriteLiteral("\r\n\r\n");


DefineSection("BodySection", () => {

WriteLiteral("\r\n\r\n   <div  class=\"container-fluid\">\r\n      ");


            
            #line 18 "..\..\Layouts\Pages\FlexPageLayout_.cshtml"
 Write(Html.FlexPageHeader());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n      ");


            
            #line 20 "..\..\Layouts\Pages\FlexPageLayout_.cshtml"
 Write(Html.FlexMenuBar(Ajax));

            
            #line default
            #line hidden
WriteLiteral("\r\n      \r\n     <div class=\"row\" id=\"main-page-container\" >  \r\n         ");


            
            #line 23 "..\..\Layouts\Pages\FlexPageLayout_.cshtml"
    Write(RenderSection("BodySection"));

            
            #line default
            #line hidden
WriteLiteral("\r\n      </div>\r\n   \r\n      ");


            
            #line 26 "..\..\Layouts\Pages\FlexPageLayout_.cshtml"
 Write(Html.FlexPageFooter());

            
            #line default
            #line hidden
WriteLiteral("\r\n   </div>\r\n\r\n");


});


        }
    }
}
#pragma warning restore 1591
