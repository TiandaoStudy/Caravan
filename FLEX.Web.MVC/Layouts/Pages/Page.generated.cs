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
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 4 "..\..\Layouts\Pages\Page.cshtml"
    using FLEX.Web.MVC.Controls.PageElements;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Layouts/Pages/Page.cshtml")]
    public partial class Page : System.Web.Mvc.WebViewPage<dynamic>
    {
        public Page()
        {
        }
        public override void Execute()
        {


WriteLiteral("\r\n");



WriteLiteral("\r\n\r\n");


WriteLiteral("\r\n");


            
            #line 6 "..\..\Layouts\Pages\Page.cshtml"
  
   Layout = "~/Layouts/Common_.cshtml";


            
            #line default
            #line hidden
WriteLiteral("\r\n<div class=\"container-fluid\">\r\n   PAGE\r\n   \r\n   ");


            
            #line 13 "..\..\Layouts\Pages\Page.cshtml"
Write(Html.MenuBar());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n   ");


            
            #line 15 "..\..\Layouts\Pages\Page.cshtml"
Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n   \r\n   ");


            
            #line 17 "..\..\Layouts\Pages\Page.cshtml"
Write(Html.Footer());

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n");


        }
    }
}
#pragma warning restore 1591