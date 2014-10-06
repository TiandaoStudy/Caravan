<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportList.ascx.cs" Inherits="FLEX.Web.UserControls.ExportList" %>

<asp:Panel ID="pnlExport" CssClass="btn-group dropup" runat="server">
   <button class="btn btn-primary btn-sm"  data-toggle="dropdown" data-hover="dropdown" data-delay="300" id="btnExports" data-close-others="false">
      <b class="caret"></b>&nbsp;Export 
   </button>
   
   <ul class="dropdown-menu text-left" role="menu">
      <li>
         <asp:LinkButton ID="lnkbExcel" runat="server" UseSubmitBehavior="false" OnClick="ExportList_OnClickExcel" OnClientClick=" common.disableButtonsBeforePostBack = false; return true; "><span class="glyphicon glyphicon-list-alt"></span>&nbsp;Excel</asp:LinkButton>        
      </li>
      <li>
         <asp:LinkButton ID="lnkbCSV" UseSubmitBehavior="false" runat="server" OnClick="ExportList_OnClickCSV" OnClientClick=" common.disableButtonsBeforePostBack = false; return true; "><span class="glyphicon glyphicon-file"></span>&nbsp;CSV</asp:LinkButton>                    
      </li>
      <li>
         <asp:LinkButton ID="lnkbPDF" UseSubmitBehavior="false" runat="server" OnClick="ExportList_OnClickPdf" OnClientClick=" common.disableButtonsBeforePostBack = false; return true; "><span class="icomoon icon-file-pdf"></span>&nbsp;PDF</asp:LinkButton>                      
      </li>
   </ul>
</asp:Panel>