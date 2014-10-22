<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecGroupList.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecGroupList" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Import Namespace="Finsa.Caravan.Extensions" %>
<%@ Import Namespace="FLEX.Web.MasterPages" %>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="FLEX.WebForms" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Group Management</title>
   
   <script type="text/javascript">
      function editGroup(groupName) {
         openModal({
            url: '<%= Head.FlexPath %>/Pages/SecGroupDetails.aspx?mode=edit&groupName=' + groupName + randomQueryTag(),
            width: "400px",
            height: "480px",
            draggable: true,
            closeCallback: editGroupCallBack,
            title: "Group Details"
         });
         return false;
      }

      function editGroupCallBack() {
         try {
            if (typeof (top.returnValue) != "undefined" && top.returnValue != null && top.returnValue.Return) {
               
            }
         } catch (ex) {
            alert('editGroupCallBack: ' + ex.message);
         }
         return false;
      }
   </script>
</asp:Content>

<asp:Content ID="aspSearchContent" ContentPlaceHolderID="searchContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspGridContent" ContentPlaceHolderID="gridContent" runat="server">
   <flex:DataGrid runat="server" ID="fdtgGroups" DefaultSortExpression="Name" DefaultSortDirection="Ascending" 
                  OnDataSourceUpdating="fdtgGroups_DataSourceUpdating" OnRowDataBound="fdtgGroups_OnRowDataBound">
      <Columns>
         <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
               <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Edit group information" CommandName="editGroup">
                  <span class="glyphicon glyphicon-pencil padded-icon"></span>
               </asp:LinkButton>
               <asp:LinkButton runat="server" ID="btnDelete" ToolTip="Delete this group" CommandName="deleteGroup">
                  <span class="glyphicon glyphicon-trash padded-icon"></span>
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
   <flex:ImageButton runat="server" ID="btnInsert" ButtonClass="btn btn-success" ButtonText="Insert" IconClass="glyphicon glyphicon-plus" OnClick="btnInsert_Click" />
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>