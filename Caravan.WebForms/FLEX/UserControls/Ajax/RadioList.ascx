<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RadioList.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.RadioList" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<style>
   .rbl input[type="radio"] {
      margin-left: 10px;
      margin-right: 1px;
   }
</style>

<div id="divData" runat="server">
   <ajax:UpdatePanel ID="updPanel" runat="server">
      <ContentTemplate>
         <asp:RadioButtonList AutoPostBack="true" ID="rblData"  CssClass="rbl" runat="server" OnSelectedIndexChanged="RadioButtonListData_OnSelectedIndexChanged" />               
      </ContentTemplate>
   </ajax:UpdatePanel>
</div>