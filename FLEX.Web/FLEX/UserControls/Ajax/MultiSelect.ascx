<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiSelect.ascx.cs" Inherits="FLEX.Web.UserControls.Ajax.MultiSelect" %>
<%@ Register TagPrefix="ajax" Namespace="System.Web.UI" Assembly="System.Web.Extensions" %>
<%@ Register TagPrefix="flex" Namespace="FLEX.WebForms.UserControls" Assembly="FLEX.WebForms" %>

<style type="text/css">
   .workingArea { height: 400px; }

   #buttons { margin-top: 150px; }

   #leftPanel { overflow-x: auto; }

   #rightPanel { overflow-x: auto; }
</style>

<div class="row">
   <!-- Start Left Panel -->
   <div id="leftPanel" class="col-xs-5-5">
      <ajax:UpdatePanel ID="leftUpdatePanel" runat="server">
         <ContentTemplate>
            <div class="panel panel-primary"> 
               <div class="panel-heading">
                  <div class="row">
                     <div class="col-xs-4 text-left">
                        <strong ID="leftPanelTitle" class="panel-title" runat="server"><asp:Label id="lbTitlePanelLeft" runat="server"></asp:label></strong>
                     </div>
                     <div class="col-xs-4 text-center">
                        <div class="input-group">
                           <asp:TextBox ID="txtApplyLeft" runat="server"  ForeColor="Black" CssClass="form-control" />
                           <span class="input-group-btn">
                              <asp:LinkButton runat="server" ID="btnApplyLeft" CssClass="btn btn-default btn-xs" ToolTip="Apply" Text="✓" OnClick="btnApplyLeft_Click"/>
                              <asp:LinkButton runat="server" ID="lkbtnClearLeft" CssClass="btn btn-default btn-xs" ToolTip="Clear" OnClick="btnClearLeft_Click"><span class="glyphicon glyphicon-trash padded-icon"></span></asp:LinkButton>
                           </span>
                        </div>
                     </div>
                     <div class="col-xs-4 text-right">
                        <span class="badge"><asp:Label id="lbCountDataLeft" runat="server"></asp:label> rows</span>
                     </div>
                  </div>
               </div>
               <div class="workingArea">
                  <!-- Custom content, like a grid view -->
                  <flex:DataGrid ID="fdtgLeft" runat="server" OnDataSourceUpdating="fdtgLeft_DataSourceUpdating" OnRowDataBound="fdtgLeft_RowDataBound" OnRowCommand="fdtgLeft_RowCommand" AutoGenerateColumns="true" AllowPaging="False">
                     <Columns>
                        <asp:TemplateField HeaderText="">
                           <ItemTemplate>
                              <asp:LinkButton Text="Move Right" ToolTip="Move Right" ID="lkbMoveRight" runat="server" CommandName="moveright"  CommandArgument='<%# Container.DataItemIndex %>'><span class="glyphicon glyphicon-chevron-right"></span></asp:LinkButton>
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                           <ItemTemplate>
                              <div>
                                 <asp:CheckBox ID="chkSelectLeft" runat="server" Enabled="true" Checked="false"></asp:CheckBox>
                              </div>
                           </ItemTemplate>
                        </asp:TemplateField>
                     </Columns>
                  </flex:DataGrid>
               </div>
            </div>
         </ContentTemplate>
      </ajax:UpdatePanel>
   </div>
   <!-- End Left Panel -->

   <!-- Start Vertical Buttons -->
   <ajax:UpdatePanel runat="server">
      <contenttemplate>
         <div id="buttons" class="col-xs-1 btn-group-vertical">
            <asp:LinkButton ID="btnMoveRight" runat="server" CssClass="btn btn-success" OnClick="btnMoveRight_Click" ToolTip="Move Elements Right">
               <span class="glyphicon glyphicon-chevron-right"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="btnMoveAllRight" runat="server" CssClass="btn btn-success" OnClick="btnMoveAllRight_Click" ToolTip="Move All Elements Right">
               <span class="glyphicon glyphicon-forward"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="btnMoveAllLeft" runat="server" CssClass="btn btn-danger" OnClick="btnMoveAllLeft_Click" ToolTip="Move All Elements Left">
               <span class="glyphicon glyphicon-backward"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="btnMoveLeft" runat="server" CssClass="btn btn-danger" OnClick="btnMoveLeft_Click" ToolTip="Move Elements Left">
               <span class="glyphicon glyphicon-chevron-left"></span>
            </asp:LinkButton>
         </div>
      </contenttemplate>
   </ajax:UpdatePanel>
   <!-- End Vertical Buttons -->

   <!-- Start Right Panel -->
   <div id="rightPanel" class="col-xs-5-5">
      <ajax:UpdatePanel ID="rightUpdatePanel" runat="server">
         <ContentTemplate>
            <div class="panel panel-primary">
               <div class="panel-heading">
                  <div class="row">
                     <div class="col-xs-4 text-left">
                        <strong ID="rightPanelTitle" class="panel-title" runat="server"><asp:Label id="lbTitlePanelRight" runat="server"></asp:label></strong>
                     </div>
                     <div class="col-xs-4 text-center">
                        <div class="input-group">
                           <asp:TextBox ID="txtApplyRight" runat="server" ForeColor="Black" CssClass="form-control" />
                           <span class="input-group-btn">
                              <asp:LinkButton runat="server" ID="btnApplyRight" CssClass="btn btn-default btn-xs" ToolTip="Apply" Text="✓" OnClick="btnApplyRight_Click"/>
                              <asp:LinkButton runat="server" ID="lkbtnClearRight" CssClass="btn btn-default btn-xs" ToolTip="Clear" OnClick="btnClearRight_Click"><span class="glyphicon glyphicon-trash padded-icon"></span></asp:LinkButton>
                           </span>
                        </div>
                     </div>
                     <div class="col-xs-4 text-right">
                        <span class="badge"><asp:Label id="lbCountDataRight" runat="server"></asp:label> rows</span>
                     </div>
                  </div>
               </div>
               <div class="workingArea">
                  <!-- Custom content, like a grid view -->
                  <flex:DataGrid ID="fdtgRight" runat="server" OnDataSourceUpdating="fdtgRight_DataSourceUpdating"  OnRowDataBound="fdtgRight_RowDataBound"  OnRowCommand="fdtgRight_RowCommand" AutoGenerateColumns="true" AllowPaging="False">
                     <Columns>
                        <asp:TemplateField HeaderText="">
                           <ItemTemplate>
                              <asp:LinkButton Text="Move Left" ToolTip="Move Left" ID="lkbMoveLeft" runat="server" CommandName="moveleft" CommandArgument='<%# Container.DataItemIndex %>'><span class="glyphicon glyphicon-chevron-left"></span></asp:LinkButton>
                           </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                           <HeaderStyle Width="50px" Wrap="True"></HeaderStyle>
                           <ItemStyle Width="50px" Wrap="True"></ItemStyle>
                           <ItemTemplate>
                              <div>
                                 <asp:CheckBox ID="chkSelectRight" runat="server" Enabled="true" Checked="false"></asp:CheckBox>
                              </div>
                           </ItemTemplate>
                        </asp:TemplateField>
                     </Columns>
                  </flex:DataGrid>
               </div>
            </div>
         </ContentTemplate>
      </ajax:UpdatePanel>
   </div>
   <!-- End Right Panel -->
</div>