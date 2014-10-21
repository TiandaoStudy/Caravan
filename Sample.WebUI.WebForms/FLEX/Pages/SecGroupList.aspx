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
   <flex:DataGrid runat="server" ID="fdtgGroups" DefaultSortExpression="Name" DefaultSortDirection="Ascending" OnDataSourceUpdating="fdtgGroups_DataSourceUpdating">
      <Columns>
         <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="true" />
         <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" Visible="true" />
         <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" Visible="true" />
         <asp:TemplateField HeaderText="Admin">
            <ItemTemplate>
               <asp:CheckBox ID="chkAdmin" runat="server" Enabled="False" Checked='<%# Eval("IsAdmin").Equals("true") %>' />
            </ItemTemplate>
         </asp:TemplateField>
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