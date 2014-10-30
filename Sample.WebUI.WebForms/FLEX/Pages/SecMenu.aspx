<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecMenu.aspx.cs" Inherits="FLEX.Web.Pages.SecMenu" MasterPageFile="~/FLEX/MasterPages/VerticalSearch.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/VerticalSearch.Master"%>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>
<%@ Register TagPrefix="flex" TagName="MultiSelect" Src="~/FLEX/UserControls/Ajax/MultiSelect.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>My Vertical Search</title>
</asp:Content>

<asp:Content ID="aspSearchContent" ContentPlaceHolderID="searchContent" runat="server">
     <ajax:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>     
               <asp:TreeView ID="TreeView1" runat="server" onselectednodechanged="TreeView1_SelectedNodeChanged" OnTreeNodeExpanded="TreeView1_TreeNodeExpanded" OnTreeNodeCollapsed="TreeView1_TreeNodeCollapsed">
                </asp:TreeView>
          </ContentTemplate>
      </ajax:UpdatePanel>
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspGridContent" ContentPlaceHolderID="gridContent" runat="server">
       <div class="panel-body">
               <ul id="cv-tabs" class="nav nav-tabs nav-justified" role="tablist" style="margin-bottom:20px">
                  <li id="li_0" class="text-center active"  runat="server" ><a href="#Users" data-toggle="tab" runat="server" id="aUsers">Users&nbsp;<span class="glyphicon glyphicon-user"></span></a></li>
                  <li id="li_1" class="text-center"  runat="server" ><a href="#Groups" data-toggle="tab" runat="server" id="aGroups">Groups&nbsp;<span class="icomoon icon-users"></span></a></li>
               </ul>
    
              <div class="tab-content">
               <div  class="tab-pane active" id="Users">
                  <asp:PlaceHolder ID="phUsers" runat="server">
                     <ajax:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                              <flex:MultiSelect runat="server" ID="flexMultipleSelectUsers"></flex:MultiSelect>
                        </ContentTemplate>
                     </ajax:UpdatePanel>
                  </asp:PlaceHolder>
               </div>
               <div class="tab-pane" id="Groups">
                  <asp:PlaceHolder ID="phGroups" runat="server">
                     <ajax:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                             <flex:MultiSelect runat="server" ID="flexMultiSelectGroups"></flex:MultiSelect>
                        </ContentTemplate>
                     </ajax:UpdatePanel>
                  </asp:PlaceHolder>
               </div>
              </div> 
       </div>  
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>