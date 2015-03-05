<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecSessionList.aspx.cs" Inherits="Finsa.Caravan.WebForms.Pages.SecSessionList" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Import Namespace="FLEX.Web.MasterPages" %>
<%@ Register TagPrefix="crvn" Namespace="FLEX.WebForms.UserControls" Assembly="Finsa.Caravan.WebForms" %>
<%@ Register TagPrefix="crvn" TagName="HiddenTrigger" Src="~/FLEX/UserControls/Ajax/HiddenTrigger.ascx" %>
<%@ Register TagPrefix="crvn" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="crvn" TagName="AutoSuggest" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>
<%@ Register TagPrefix="crvn" TagName="CollapsibleCheckBoxList" Src="~/FLEX/UserControls/Ajax/CollapsibleCheckBoxList.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Connected Users</title>
   
   <script type="text/javascript">
       function users_filter() {
           return "CUSR_ACTIVE = 1";
       }
   </script>
</asp:Content>

<asp:Content ID="aspSearchContent" ContentPlaceHolderID="searchContent" runat="server">
     <crvn:AutoSuggest ID="crvnUsersLkp" XmlLookup="Users" LookupBy="Curs_Name_LastName" MenuWidth="250" MenuMaxHeight="300" DoPostBack="true" runat="server" QueryFilter="users_filter()" PlaceHolder="Name LastName"/>
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspGridContent" ContentPlaceHolderID="gridContent" runat="server">
   <crvn:DataGrid runat="server" ID="fdtgConnectedUsers" DefaultSortExpression="Login" DefaultSortDirection="Ascending" PageSize="12" 
                  OnDataSourceUpdating="fdtgConnectedUsers_DataSourceUpdating" OnRowDataBound="fdtgConnectedUsers_OnRowDataBound">
      <Columns>
         <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
         <asp:BoundField DataField="UserHostName" HeaderText="UserHostName" SortExpression="UserHostName" />
         <asp:BoundField DataField="UserHostAddress" HeaderText="UserHostAddress" SortExpression="UserHostAddress" />
         <asp:BoundField DataField="LastVisit" HeaderText="LastVisit" SortExpression="LastVisit" />
      </Columns>
   </crvn:DataGrid>
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server"> 
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
</asp:Content>