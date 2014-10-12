<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageButton.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.ImageButton" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<div class="btn-group">
   <ajax:UpdatePanel ID="updPanel" runat="server">
      <ContentTemplate>      
         <asp:LinkButton id="btn" runat="server" OnClick="btn_Click">
            <span id="btnIcon" runat="server"></span><span id="btnText" runat="server"></span>
         </asp:LinkButton>
      </ContentTemplate>
   </ajax:UpdatePanel>
</div>