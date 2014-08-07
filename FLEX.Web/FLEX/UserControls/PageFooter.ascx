<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageFooter.ascx.cs" Inherits="FLEX.Web.UserControls.PageFooter" %>
<%@ Import Namespace="FLEX.Web.MasterPages" %>

<footer>
   <div id="pageFooter" class="navbar navbar-default navbar-fixed-bottom">
      <div class="container-fluid">
         
         <div class="navbar-header">
            <a href="http://www.finsa.it" class="navbar-brand"><img src='<%= Head.FlexPath %>/Images/logo-finsa.png' class='logo-finsa' alt='Logo' />FINSA S.p.A.</a>
            <button class="navbar-toggle" type="button" data-toggle="collapse" data-target="#pageFooter-collapse">
               <span class="icon-bar"></span>
               <span class="icon-bar"></span>
               <span class="icon-bar"></span>
            </button>
         </div>
         
         <div id="pageFooter-collapse" class="navbar-collapse collapse"> 
            <ul class="nav navbar-nav navbar-right">   
               <asp:Repeater id="rptFooterInfo" runat="server">
                  <ItemTemplate>
                     <li>
                        <a>
                           <strong><asp:Label ID="lblKey" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "First") %>' /></strong>:&nbsp;
                           <asp:Label ID="lblValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Second") %>' />
                        </a>
                     </li>
                  </ItemTemplate>
               </asp:Repeater>
               <asp:PlaceHolder runat="server" ID="footerExtender" />
            </ul>
         </div>

      </div>
   </div>   
</footer>