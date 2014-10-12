<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="FLEX.Web.Pages.ReportViewer" MasterPageFile="~/FLEX/MasterPages/Popup.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/Popup.Master"%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="flex" TagName="PopupExitButton" Src="~/FLEX/UserControls/PopupExitButton.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Report Viewer</title>
   <style>
      #mainContainer {
         padding-left: 8px;
         padding-right: 8px;
         padding-top: 0;
      }
   </style>
</asp:Content>

<asp:Content ID="aspMainContent" ContentPlaceHolderID="mainContent" runat="server">
   <!-- Width should be one pixel less than the one specified in Scripts/FLEX.Head.Coffee -> openReportViewer(...) -->
   <rsweb:ReportViewer ID="myReportViewer" runat="server" Width="999px" Height="500px"></rsweb:ReportViewer>
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="buttonsContent" runat="server">   
   <flex:PopupExitButton ID="btnExit" Text="Exit" runat="server" />
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>