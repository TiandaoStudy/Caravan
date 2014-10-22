<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecGroupList.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecGroupList" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="FLEX.WebForms" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>

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
         <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
               <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Edit group information">
                  <span class="glyphicon glyphicon-pencil"></span>
               </asp:LinkButton>
               <asp:LinkButton runat="server" ID="btnDelete" ToolTip="Delete this group">
                  <span class="glyphicon glyphicon-trash"></span>
               </asp:LinkButton>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
         <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
         <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
         <asp:TemplateField HeaderText="Admin" SortExpression="IsAdmin">
            <ItemTemplate>
               <asp:CheckBox ID="chkAdmin" runat="server" Enabled="False" Checked='<%# Eval("IsAdmin") %>' />
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
   </flex:DataGrid>
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
   <flex:ImageButton runat="server" ID="btnInsert" ButtonClass="btn btn-primary" ButtonText="Insert" IconClass="glyphicon glyphicon-plus" OnClick="btnInsert_Click" />
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>