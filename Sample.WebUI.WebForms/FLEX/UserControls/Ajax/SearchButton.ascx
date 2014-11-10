<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchButton.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.SearchButton" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<ajax:UpdatePanel ID="updPanel" runat="server">
   <ContentTemplate>      
      <!-- IMPORTANT: Type must be "button", in order to avoid a double postback -->
      <button id="btnSearch" class="btn btn-success" runat="server" OnServerClick="btnSearch_Click" type="button">
         <span id="btnSearchIcon" class="glyphicon glyphicon-search"></span>&nbsp;Search
      </button>
   </ContentTemplate>
</ajax:UpdatePanel>