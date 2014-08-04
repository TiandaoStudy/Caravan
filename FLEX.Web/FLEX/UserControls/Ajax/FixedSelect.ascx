<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FixedSelect.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.FixedSelect" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<ajax:UpdatePanel ID="updPanel" runat="server">
   <ContentTemplate>      
      <asp:DropDownList ID="ddlSelect" class="form-control" runat="server" OnSelectedIndexChanged="ddlSelect_OnSelectedIndexChanged" />
   </ContentTemplate>
</ajax:UpdatePanel>