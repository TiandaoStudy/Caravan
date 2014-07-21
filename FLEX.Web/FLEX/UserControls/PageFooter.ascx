<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageFooter.ascx.cs" Inherits="FLEX.Web.UserControls.PageFooter" %>
<%@ Import Namespace="FLEX.Web.MasterPages" %>

<footer class="navbar navbar-default navbar-fixed-bottom">
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
</footer>