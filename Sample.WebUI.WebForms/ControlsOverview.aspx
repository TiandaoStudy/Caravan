<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlsOverview.aspx.cs" Inherits="FLEX.Sample.WebUI.ControlsOverview" MasterPageFile="~/FLEX/MasterPages/CustomView.Master" %>
<%@ MasterType VirtualPath="~/FLEX/MasterPages/CustomView.Master"%>
<%@ Register TagPrefix="flex" TagName="AutoSuggest" Src="~/FLEX/UserControls/Ajax/AutoSuggest.ascx" %>
<%@ Register TagPrefix="flex" TagName="CollapsibleCheckBoxList" Src="~/FLEX/UserControls/Ajax/CollapsibleCheckBoxList.ascx" %>
<%@ Register TagPrefix="flex" TagName="DatePicker" Src="~/FLEX/UserControls/Ajax/DatePicker.ascx" %>
<%@ Register TagPrefix="flex" TagName="ImageButton" Src="~/FLEX/UserControls/Ajax/ImageButton.ascx" %>
<%@ Register TagPrefix="flex" TagName="NumericSpinner" Src="~/FLEX/UserControls/Ajax/NumericSpinner.ascx" %>
<%@ Register TagPrefix="flex" TagName="OnOffSwitch" Src="~/FLEX/UserControls/Ajax/OnOffSwitch.ascx" %>
<%@ Register TagPrefix="flex" TagName="FileUpload" Src="~/FLEX/UserControls/FileUpload.ascx" %>
<%@ Register TagPrefix="flex" TagName="MultiSelect" Src="~/FLEX/UserControls/Ajax/MultiSelect.ascx" %>

<asp:Content ID="aspHeadContent" ContentPlaceHolderID="headContent" runat="server">
   <title>Controls Overview</title>
</asp:Content>

<asp:Content ID="aspUpperContent" ContentPlaceHolderID="upperContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspCustomContent" ContentPlaceHolderID="customContent" runat="server">
       
   <flex:OnOffSwitch runat="server" ID="flexEnabledSwitch" Switched="True" OnValueSelected="flexEnabledSwitch_OnValueSelected" DoPostBack="True" />
   
   <flex:AutoSuggest runat="server" ID="flexEmplByName" XmlLookup="Employees" LookupBy="Emp_Name" DoPostBack="False" MinLengthForHint="2" PlaceHolder="Full name" />
   <flex:DatePicker runat="server" ID="flexYearPicker" StartView="Year" MinView="Years" />
   <flex:NumericSpinner runat="server" ID="flexNumSpinner" MinValue="0" MaxValue="100" DecimalCount="0" Step="1" />        
   <flex:CollapsibleCheckBoxList runat="server" ID="flexABC" MaxVisibleItemCount="2" />
   <flex:ImageButton runat="server" ID="btnError" ButtonClass="btn btn-warning" ButtonText="Error"  OnClick="OnClick_Error" />
   
   <flex:MultiSelect runat="server" ID="flexMultipleSelect"></flex:MultiSelect>
   
   <flex:NumericSpinner runat="server" ID="flexIgnoredSpinner" />
   <flex:FileUpload runat="server" ID="flexFileUpload" Title="Browse" />
   
</asp:Content>

<asp:Content ID="aspLeftButtonsContent" ContentPlaceHolderID="leftButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspCenterButtonsContent" ContentPlaceHolderID="centerButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspRightButtonsContent" ContentPlaceHolderID="rightButtonsContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspLowerContent" ContentPlaceHolderID="lowerContent" runat="server">
   
</asp:Content>

<asp:Content ID="aspHiddenContent" ContentPlaceHolderID="hiddenContent" runat="server">
   
</asp:Content>