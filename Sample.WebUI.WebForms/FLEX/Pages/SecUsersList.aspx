<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecUsersList.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecUsersList" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Import Namespace="FLEX.Web.MasterPages" %>
<%@ Register TagPrefix="crvn" Namespace="FLEX.WebForms.UserControls" Assembly="Finsa.Caravan.WebForms" %>
<%@ Register TagPrefix="crvn" TagName="HiddenTrigger" Src="~/FLEX/UserControls/Ajax/HiddenTrigger.ascx" %>
<%@ Register TagPrefix="crvn" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="crvn" TagName="AutoSuggest" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>
<%@ Register TagPrefix="crvn" TagName="CollapsibleCheckBoxList" Src="~/FLEX/UserControls/Ajax/CollapsibleCheckBoxList.ascx" %>
<%@ Register TagPrefix="crvn" TagName="ExportList" Src="~/FLEX/UserControls/ExportList.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>User Management</title>
   
   <script type="text/javascript">
 <%--     function insertGroup() {
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
      }--%>

      function editUser(login) {
         openModal({
            url: '<%= Head.FlexPath %>/Pages/SecUsersDetails.aspx?mode=edit&login=' + login + randomQueryTag(),
            width: "900px",
            height: "480px",
            draggable: true,
            closeCallback: editUserCallback,
            title: "Edit user details"
         });
         return false;
      }

      function editUserCallback() {
         try {
            if (typeof (top.returnValue) != "undefined" && top.returnValue != null && top.returnValue.Return) {
               triggerAsyncPostBack("<%= hiddenRefresh.Trigger.ClientID %>");
            }
         } catch (ex) {
            alert('editUserCallBack: ' + ex.message);
         }
         return false;
      }

      function deleteUser(login) {
         $("#<%= loginToBeDeleted.ClientID %>").val(login);
          bootbox.confirm("Do you really want to delete that user?", deleteUserCallback);
         return false;
      }

      function deleteUserCallback(result) {
         try {
            if (result) {
               triggerAsyncPostBack("<%= hiddenDelete.Trigger.ClientID %>");
            }
         } catch (ex) {
            alert('deleteUserCallback: ' + ex.message);
         }
      }

       function users_filter() {
           var chk = $("#<%=crvnActive.VisibleCheckBoxList.ClientID%> input");
           var active = chk[0].checked;
           var inactive = chk[1].checked;
           if ((active && inactive) || (!active && !inactive)) {
               return "1=1";
           }
           var op = active ? "=" : "<>";
           return "CUSR_ACTIVE " + op + " 1";
       }
   </script>
</asp:Content>

<asp:Content ID="aspSearchContent" ContentPlaceHolderID="searchContent" runat="server">
     <crvn:AutoSuggest ID="crvnUsersLkp" XmlLookup="Users" LookupBy="Curs_Name_LastName" MenuWidth="250" MenuMaxHeight="300" DoPostBack="true" runat="server"  QueryFilter="users_filter()" PlaceHolder="Name LastName"/>
     <crvn:CollapsibleCheckBoxList ID="crvnActive" runat="server" MaxVisibleItemCount="2" DoPostBack="true" />
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspGridContent" ContentPlaceHolderID="gridContent" runat="server">
   <crvn:DataGrid runat="server" ID="fdtgUsers" DefaultSortExpression="FirstName" DefaultSortDirection="Ascending" PageSize="12" 
                  OnDataSourceUpdating="fdtgUsers_DataSourceUpdating" OnRowDataBound="fdtgUsers_OnRowDataBound">
      <Columns>
         <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
               <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Edit users information" CommandName="editUser" CssClass="padded-icon">
                  <span class="glyphicon glyphicon-pencil"></span>
               </asp:LinkButton>
               <asp:LinkButton runat="server" ID="btnDelete" ToolTip="Delete this user" CommandName="deleteUser" CssClass="padded-icon">
                  <span class="glyphicon glyphicon-trash"></span>
               </asp:LinkButton>
            </ItemTemplate>
         </asp:TemplateField>
         <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
         <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
         <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
         <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
         <asp:TemplateField HeaderText="Active" SortExpression="Active">
            <ItemTemplate>
               <asp:CheckBox ID="chkActive" runat="server"  Enabled="False" Checked='<%# Eval("Active").Equals(1) %>' />
            </ItemTemplate>
         </asp:TemplateField>

         <asp:TemplateField visible="false">
            <ItemTemplate>
               <asp:Label ID="lbActive" runat="server" Text=  '<%# Eval("Active").Equals(1)? "Yes":"No" %>'   />
            </ItemTemplate>
         </asp:TemplateField>
        
      </Columns>
   </crvn:DataGrid>
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
  <%-- <crvn:ImageButton runat="server" ID="btnInsert" ButtonClass="btn btn-success" ButtonText="Insert" IconClass="glyphicon glyphicon-plus" OnClientClick="return insertGroup();" />--%>
   <crvn:ExportList runat="server" ID="crvnExportList" OnDataSourceNeeded="crvnExportList_DataSourceNeeded" ReportName="UserManagement"></crvn:ExportList> 
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   <crvn:HiddenTrigger runat="server" ID="hiddenRefresh" OnTriggered="hiddenRefresh_OnTriggered" />
   <crvn:HiddenTrigger runat="server" ID="hiddenDelete" OnTriggered="hiddenDelete_OnTriggered" />
   <asp:HiddenField runat="server" ID="loginToBeDeleted" />
</asp:Content>