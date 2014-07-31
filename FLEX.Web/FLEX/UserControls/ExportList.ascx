<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportList.ascx.cs" Inherits="FLEX.Web.UserControls.ExportList" %>


<style>
</style>

<asp:Panel ID="pnlExport" CssClass="btn-group" runat="server">
     <button type="button" class="btn btn-primary  btn-sm"  data-toggle="dropdown" id="btnExports" runat="server">Export</button>
     <button type="button" class="btn btn-primary dropdown-toggle btn-sm" data-toggle="dropdown" id="btnCaret" runat="server" style="margin-left: 0"><span class="caret"></span></button>
     <div class="dropdown-menu text-left" runat="server" id="div_listExport">
         <asp:LinkButton ID="lnkbExcel" runat="server" UseSubmitBehavior="false" OnClick="ExportList_OnClickExcel" OnClientClick="common.disableButtonsBeforePostBack = false; return true;"><span class="glyphicon glyphicon-list-alt"></span>&nbsp;Excel</asp:LinkButton>
         <br/>
         <asp:LinkButton ID="lnkbPDF" UseSubmitBehavior="false" runat="server" OnClick="ExportList_OnClickPdf" OnClientClick="common.disableButtonsBeforePostBack = false; return true;"><span class="icomoon icon-file-pdf"></span>&nbsp;Pdf</asp:LinkButton>
         <br/> 
         <asp:LinkButton ID="lnkbCSV" UseSubmitBehavior="false" runat="server" OnClick="ExportList_OnClickCSV" OnClientClick="common.disableButtonsBeforePostBack = false; return true;"><span class="icomoon icon-file-pdf"></span>&nbsp;CSV</asp:LinkButton>
      </div>
</asp:Panel>