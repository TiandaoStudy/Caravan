<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogSearch.aspx.cs" Inherits="FLEX.WebForms.Pages.LogSearch" MasterPageFile="~/FLEX/MasterPages/DataView.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/DataView.Master"%>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="Finsa.Caravan.WebForms" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="flex" TagName="LongTextContainer" Src="~/FLEX/UserControls/LongTextContainer.ascx" %>

<asp:Content runat="server" ID="aspHeadContent" ContentPlaceHolderID="headContent">
   <title>Logs</title>
</asp:Content>

<asp:Content runat="server" ID="aspGridContent" ContentPlaceHolderID="gridContent">
   <flex:DataGrid runat="server" ID="fdtgLogs" DefaultSortExpression="Date" DefaultSortDirection="Descending" OnDataSourceUpdating="fdtgLogs_DataSourceUpdating">
      <Columns>
         <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" Visible="true" />
         <asp:BoundField DataField="UserLogin" HeaderText="User Login" SortExpression="UserLogin" Visible="true" />
         <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" Visible="true" />
         <asp:BoundField DataField="CodeUnit" HeaderText="Package or Class" SortExpression="CodeUnit" Visible="true" />
         <asp:BoundField DataField="Function" HeaderText="Function" SortExpression="Function" Visible="true" />
         <asp:BoundField DataField="ShortMessage" HeaderText="Short Message" SortExpression="ShortMessage" Visible="true" />
         <asp:TemplateField HeaderText="Long Message" SortExpression="LongMessage" Visible="true">
            <ItemTemplate>
               <flex:LongTextContainer runat="server" MaxTextLength="50" ContainerTitle="Long Message" Text='<%# Eval("LongMessage") %>' ID="flexLongMsg" />
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
   </flex:DataGrid>
</asp:Content>

<asp:Content runat="server" ID="aspButtonsContent" ContentPlaceHolderID="buttonsContent">
   <flex:ImageButton runat="server" ID="btnRefresh" ButtonClass="btn btn-primary" ButtonText="Refresh" IconClass="glyphicon glyphicon-refresh" OnClick="btnRefresh_Click" />
</asp:Content>