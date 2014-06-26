<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClearButton.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.ClearButton" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<ajax:UpdatePanel ID="updPanel" runat="server">
   <ContentTemplate>      
      <asp:LinkButton id="btnClear" class="btn btn-primary btn-sm" runat="server" OnClick="btnClear_Click">
         <span id="btnClearIcon" class="glyphicon glyphicon-trash"></span>&nbsp;Clear
      </asp:LinkButton>
   </ContentTemplate>
</ajax:UpdatePanel>