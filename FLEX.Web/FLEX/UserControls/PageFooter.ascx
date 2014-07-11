<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageFooter.ascx.cs" Inherits="FLEX.Web.UserControls.PageFooter" %>

<footer class="navbar navbar-default navbar-fixed-bottom">
   <div class="container-fluid">
      <div class="navbar-header">
         <a href="http://www.finsa.it" class="navbar-brand"><img src='<%= FLEX.Web.MasterPages.Head.FlexPath %>/Images/logo-finsa.png' class='logo-finsa' alt='Logo' />FINSA S.p.A.</a>
         <button class="navbar-toggle" type="button" data-toggle="collapse" data-target="#navbar-main">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
         </button>
      </div>

      <div class="navbar-collapse collapse navbar-inverse-collapse">
         <ul class="nav navbar-nav navbar-right">
            <li>
             <a><asp:Label ID="lblHost" runat="server"></asp:Label></a>
            </li>
         </ul>
      </div>
   </div>
</footer>