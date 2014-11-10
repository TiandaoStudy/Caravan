<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HiddenTrigger.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.HiddenTrigger" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<ajax:UpdatePanel ID="updPanel" runat="server">
   <ContentTemplate>      
      <asp:TextBox ID="txtHidden" AutoPostBack="True" runat="server" CssClass="hidden" />
   </ContentTemplate>
</ajax:UpdatePanel>