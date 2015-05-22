<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheOverview.aspx.cs" Inherits="FLEX.WebForms.Pages.CacheOverview" MasterPageFile="~/FLEX/MasterPages/DataView.Master" %>

<%@ MasterType VirtualPath="~/FLEX/MasterPages/DataView.Master" %>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="Finsa.Caravan.WebForms" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>

<asp:Content runat="server" ID="aspHeadContent" ContentPlaceHolderID="headContent">
    <title>Cache Overview</title>
</asp:Content>

<asp:Content runat="server" ID="aspGridContent" ContentPlaceHolderID="gridContent">
    <flex:DataGrid runat="server" ID="fdtgCache" DefaultSortExpression="Key" DefaultSortDirection="Ascending" OnDataSourceUpdating="fdtgCache_DataSourceUpdating">
        <columns>
         <asp:BoundField DataField="Partition" HeaderText="Partition" SortExpression="Partition" Visible="true" />
         <asp:BoundField DataField="Key" HeaderText="Key" SortExpression="Key" Visible="true" />
         <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" Visible="true" />
         <asp:BoundField DataField="UtcCreation" HeaderText="Created On" SortExpression="UtcCreation" Visible="true" />
         <asp:BoundField DataField="UtcExpiry" HeaderText="Expires On" SortExpression="UtcExpiry" Visible="true" />
         <asp:BoundField DataField="Interval" HeaderText="Refresh Interval" SortExpression="Interval" Visible="true" />
      </columns>
    </flex:DataGrid>
</asp:Content>

<asp:Content runat="server" ID="aspButtonsContent" ContentPlaceHolderID="buttonsContent">
    <flex:ImageButton runat="server" ID="btnRefresh" ButtonClass="btn btn-primary" ButtonText="Refresh" IconClass="glyphicon glyphicon-refresh" OnClick="btnRefresh_Click" />
    <flex:ImageButton runat="server" ID="btnClear" ButtonClass="btn btn-warning" ButtonText="Clear" IconClass="glyphicon glyphicon-trash" OnClick="btnClear_Click" />
</asp:Content>