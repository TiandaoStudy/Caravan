<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecGroupList.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecGroupList" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Import Namespace="FLEX.Web.MasterPages" %>
<%@ Register TagPrefix="crvn" Namespace="FLEX.WebForms.UserControls" Assembly="FLEX.WebForms" %>
<%@ Register TagPrefix="crvn" TagName="HiddenTrigger" Src="~/FLEX/UserControls/Ajax/HiddenTrigger.ascx" %>
<%@ Register TagPrefix="crvn" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="crvn" TagName="AutoSuggest" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Group Management</title>
   
   <script type="text/javascript">
      function insertGroup() {
         openModal({
            url: '<%= Head.FlexPath %>/Pages/SecGroupDetails.aspx?mode=new' + randomQueryTag(),
            width: "900px",
            height: "480px",
            draggable: true,
            closeCallback: insertGroupCallback,
            title: "Insert new group"
         });
         return false;
      }

      function insertGroupCallback() {
         try {
            if (typeof (top.returnValue) != "undefined" && top.returnValue != null && top.returnValue.Return) {
               triggerAsyncPostBack("<%= hiddenRefresh.Trigger.ClientID %>");
            }
         } catch (ex) {
            alert('insertGroupCallback: ' + ex.message);
         }
         return false;
      }

      function editGroup(groupName) {
         openModal({
            url: '<%= Head.FlexPath %>/Pages/SecGroupDetails.aspx?mode=edit&groupName=' + groupName + randomQueryTag(),
            width: "900px",
            height: "480px",
            draggable: true,
            closeCallback: editGroupCallback,
            title: "Edit group details"
         });
         return false;
      }

      function editGroupCallback() {
         try {
            if (typeof (top.returnValue) != "undefined" && top.returnValue != null && top.returnValue.Return) {
               triggerAsyncPostBack("<%= hiddenRefresh.Trigger.ClientID %>");
            }
         } catch (ex) {
            alert('editGroupCallBack: ' + ex.message);
         }
         return false;
      }

      function deleteGroup(groupName) {
         $("#<%= groupNameToBeDeleted.ClientID %>").val(groupName);
         bootbox.confirm("Do you really want to delete that group?", deleteGroupCallback);
         return false;
      }

      function deleteGroupCallback(result) {
         try {
            if (result) {
               triggerAsyncPostBack("<%= hiddenDelete.Trigger.ClientID %>");
            }
         } catch (ex) {
            alert('deleteGroupCallback: ' + ex.message);
         }
      }
   </script>
</asp:Content>

<asp:Content ID="aspSearchContent" ContentPlaceHolderID="searchContent" runat="server">
     <crvn:AutoSuggest ID="crvnGroupsLkp" XmlLookup="Groups" LookupBy="CGRP_NAME" MenuWidth="250" MenuMaxHeight="300" DoPostBack="true" runat="server" />
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspGridContent" ContentPlaceHolderID="gridContent" runat="server">
   <crvn:DataGrid runat="server" ID="fdtgGroups" DefaultSortExpression="Name" DefaultSortDirection="Ascending" PageSize="12" 
                  OnDataSourceUpdating="fdtgGroups_DataSourceUpdating" OnRowDataBound="fdtgGroups_OnRowDataBound">
      <Columns>
         <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
               <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Edit group information" CommandName="editGroup" CssClass="padded-icon">
                  <span class="glyphicon glyphicon-pencil"></span>
               </asp:LinkButton>
               <asp:LinkButton runat="server" ID="btnDelete" ToolTip="Delete this group" CommandName="deleteGroup" CssClass="padded-icon">
                  <span class="glyphicon glyphicon-trash"></span>
               </asp:LinkButton>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
         <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
         <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
         <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
         <asp:TemplateField HeaderText="Admin" SortExpression="IsAdmin">
            <ItemTemplate>
               <asp:CheckBox ID="chkAdmin" runat="server" Enabled="False" Checked='<%# Eval("IsAdmin").Equals(1) %>' />
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
   </crvn:DataGrid>
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
   <crvn:ImageButton runat="server" ID="btnInsert" ButtonClass="btn btn-success" ButtonText="Insert" IconClass="glyphicon glyphicon-plus" OnClientClick="return insertGroup();" />
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   <crvn:HiddenTrigger runat="server" ID="hiddenRefresh" OnTriggered="hiddenRefresh_OnTriggered" />

   <crvn:HiddenTrigger runat="server" ID="hiddenDelete" OnTriggered="hiddenDelete_OnTriggered" />
   <asp:HiddenField runat="server" ID="groupNameToBeDeleted" />
</asp:Content>