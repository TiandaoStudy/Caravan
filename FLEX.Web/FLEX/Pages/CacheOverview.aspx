<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheOverview.aspx.cs" Inherits="FLEX.Web.Pages.CacheOverview" MasterPageFile="~/FLEX/MasterPages/DataView.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/DataView.Master"%>
<%@ Register TagPrefix="flex" Namespace="FLEX.Web.WebControls" Assembly="FLEX.Web" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>

<asp:Content runat="server" ID="aspHeadContent" ContentPlaceHolderID="headContent">
   <title>Cache Overview</title>
</asp:Content>

<asp:Content runat="server" ID="aspGridContent" ContentPlaceHolderID="gridContent">
   <flex:DataGrid runat="server" ID="fdtgCache" DefaultSortExpression="KEY" DefaultSortDirection="Ascending" OnDataSourceUpdating="fdtgCache_DataSourceUpdating">
      <Columns>
         <asp:BoundField DataField="KEY" HeaderText="Key" SortExpression="KEY" Visible="true">
            <HeaderStyle Width="70%" Wrap="false"></HeaderStyle>
            <ItemStyle Width="70%" Wrap="false"></ItemStyle>
         </asp:BoundField>
         <asp:BoundField DataField="VALUE" HeaderText="Value" SortExpression="VALUE" Visible="true">
            <HeaderStyle Width="30%" Wrap="false"></HeaderStyle>
            <ItemStyle Width="30%" Wrap="false"></ItemStyle>
         </asp:BoundField>
      </Columns>
   </flex:DataGrid>
</asp:Content>

<asp:Content runat="server" ID="aspButtonsContent" ContentPlaceHolderID="buttonsContent">
   <flex:ImageButton runat="server" ID="btnRefresh" ButtonClass="btn btn-primary btn-sm" ButtonText="Refresh" IconClass="glyphicon glyphicon-refresh" />
   <flex:ImageButton runat="server" ID="btnClear" ButtonClass="btn btn-warning btn-sm" ButtonText="Clear" IconClass="glyphicon glyphicon-trash" />
</asp:Content>