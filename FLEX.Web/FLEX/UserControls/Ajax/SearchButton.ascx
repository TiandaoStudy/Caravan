﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchButton.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.SearchButton" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>

<ajax:UpdatePanel ID="updPanel" runat="server">
   <ContentTemplate>      
      <button id="btnSearch" class="btn btn-success btn-sm" runat="server" OnServerClick="btnSearch_Click">
         <span id="btnSearchIcon" class="glyphicon glyphicon-search"></span>&nbsp;Search
      </button>
   </ContentTemplate>
</ajax:UpdatePanel>