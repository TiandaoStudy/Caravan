<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="FLEX.Web.Pages.ReportViewer" MasterPageFile="~/FLEX/MasterPages/Popup.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/Popup.Master"%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Report Viewer</title>
</asp:Content>

<asp:Content ID="aspMainContent" ContentPlaceHolderID="mainContent" runat="server">
   <rsweb:ReportViewer ID="myReportViewer" runat="server" Width="700px"></rsweb:ReportViewer>
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="buttonsContent" runat="server">
   <flex:ImageButton runat="server" ID="btnExit" DoPostBack="False" OnClientClick="closeWindow();" ButtonClass="btn btn-primary btn-sm" ButtonText="Exit" IconClass="glyphicon glyphicon-remove" />
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>