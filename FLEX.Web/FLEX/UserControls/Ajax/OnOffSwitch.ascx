<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnOffSwitch.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.OnOffSwitch" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<div class="btn-group">
   <ajax:UpdatePanel ID="updPanel" runat="server">
      <ContentTemplate>
         <asp:CheckBox ID="chkSwitch" runat="server" CssClass="hidden" AutoPostBack="False" />
         <div class="btn-group">
            <asp:Button ID="btnON" Text="ON" runat="server" OnClick="btnON_Click" UseSubmitBehavior="False" CausesValidation="False" />
            <asp:Button ID="btnOFF" Text="OFF" runat="server" OnClick="btnOFF_Click" UseSubmitBehavior="False" CausesValidation="False" />
         </div>
      </ContentTemplate>
   </ajax:UpdatePanel> 
</div>