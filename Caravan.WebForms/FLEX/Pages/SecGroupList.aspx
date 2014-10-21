<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecGroupList.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecGroupList" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="FLEX.WebForms" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Group Management</title>
</asp:Content>

<asp:Content ID="aspSearchContent" ContentPlaceHolderID="searchContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspGridContent" ContentPlaceHolderID="gridContent" runat="server">
   <flex:DataGrid runat="server" ID="fdtgGroups" DefaultSortExpression="Key" DefaultSortDirection="Ascending" OnDataSourceUpdating="fdtgGroups_DataSourceUpdating">
      <Columns>
         <asp:BoundField DataField="Partition" HeaderText="Partition" SortExpression="Partition" Visible="true" />
         <asp:BoundField DataField="Key" HeaderText="Key" SortExpression="Key" Visible="true" />
         <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" Visible="true" />
         <asp:BoundField DataField="UtcCreation" HeaderText="Created On" SortExpression="UtcCreation" Visible="true" />
         <asp:BoundField DataField="UtcExpiry" HeaderText="Expires On" SortExpression="UtcExpiry" Visible="true" />
         <asp:BoundField DataField="Interval" HeaderText="Refresh Interval" SortExpression="Interval" Visible="true" />
      </Columns>
   </flex:DataGrid>
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>