<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageFooter.ascx.cs" Inherits="FLEX.Web.UserControls.PageFooter" %>
<%@ Import Namespace="FLEX.Web.MasterPages" %>

<style>
   footer .navbar-default {
      min-height: 20px !important;
   }

   .navbar-inverse .navbar-nav > .active > a:hover, .navbar-inverse .navbar-nav > li > a:hover, .navbar-inverse .navbar-nav > li > a:focus { }

   .navbar-inverse .navbar-nav > .active > a, .navbar-inverse .navbar-nav > .open > a, .navbar-inverse .navbar-nav > .open > a, .navbar-inverse .navbar-nav > .open > a:hover, .navbar-inverse .navbar-nav > .open > a, .navbar-inverse .navbar-nav > .open > a:hover, .navbar-inverse .navbar-nav > .open > a:focus { }

   .dropdown-menu { }

   .dropdown-menu > li > a:hover, .dropdown-menu > li > a:focus { }

   .navbar-inverse { }

   .dropdown-menu > li > a:hover, .dropdown-menu > li > a:focus { }

   .navbar-inverse { }

   footer .navbar-default .navbar-brand {
      padding-top: 4px !important;
      padding-bottom: 4px !important;
      height: 10px !important;
   }

   .navbar-inverse .navbar-brand:hover { }

   footer .navbar-default .navbar-nav > li > a {
      padding-top: 4px !important;
      padding-bottom: 4px !important;
   }

   .navbar-inverse .navbar-nav > li > a:hover, .navbar-inverse .navbar-nav > li > a:focus { }

   .navbar-inverse .navbar-nav > .active > a, .navbar-inverse .navbar-nav > .open > a, .navbar-inverse .navbar-nav > .open > a:hover, .navbar-inverse .navbar-nav > .open > a:focus { }

   .navbar-inverse .navbar-nav > .active > a:hover, .navbar-inverse .navbar-nav > .active > a:focus { }

   .dropdown-menu > li > a { }

   .dropdown-menu > li > a:hover, .dropdown-menu > li > a:focus { }

   .navbar-inverse .navbar-nav > .dropdown > a .caret { }

   .navbar-inverse .navbar-nav > .dropdown > a:hover .caret { }

   .navbar-inverse .navbar-nav > .dropdown > a .caret { }

   .navbar-inverse .navbar-nav > .dropdown > a:hover .caret { }
</style>

<footer>
   <div id="pageFooter" class="navbar navbar-default navbar-fixed-bottom">
      <div class="container-fluid">
         <div class="navbar-header">
            <a href="http://www.finsa.it" class="navbar-brand"><img src='<%= Head.FlexPath %>/Images/logo-finsa.png' class='logo-finsa' alt='Logo' />FINSA S.p.A.</a>
            <button class="navbar-toggle" type="button" data-toggle="collapse" data-target="#navbar-main">
               <span class="icon-bar"></span>
               <span class="icon-bar"></span>
               <span class="icon-bar"></span>
            </button>
         </div>
       
         <asp:Repeater id="rptFooterInfo" runat="server">
            <HeaderTemplate>
               <div class="navbar-collapse collapse navbar-inverse-collapse"> 
               <ul class="nav navbar-nav navbar-right">
               <li>
            </HeaderTemplate>
            <ItemTemplate>
               <a>
                  <strong><asp:Label ID="lblKey" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Key") %>' />:</strong>&nbsp;
                  <asp:Label ID="lblValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>' />
               </a>
            </ItemTemplate>
            <FooterTemplate>
            </li>
            </ul>
            </div>
            </FooterTemplate>
         </asp:Repeater>
      </div>
   </div>   
</footer>