<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicReportViewer.aspx.cs" Inherits="FLEX.WebForms.Pages.DynamicReportViewer" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="FLEX.WebForms" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Dynamic Report Viewer</title>
</asp:Content>

<asp:Content ID="aspSearchContent" ContentPlaceHolderID="searchContent" runat="server">
   <asp:Repeater ID="repSearchCriteria" runat="server" OnItemDataBound="repSearchCriteria_OnItemDataBound">
      <ItemTemplate>
         <div class="vertical-search-label">
            <asp:Label ID="lblSearchCriterium" runat="server"></asp:Label>
         </div>
         <div class="vertical-search-control">
            <asp:PlaceHolder ID="plhSearchCriterium" runat="server"></asp:PlaceHolder>
         </div>
      </ItemTemplate>
   </asp:Repeater>
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspGridContent" ContentPlaceHolderID="gridContent" runat="server">
   <flex:DataGrid runat="server" ID="fdtgReport" OnDataSourceUpdating="fdtgReport_OnDataSourceUpdating"></flex:DataGrid>
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>