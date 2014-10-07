<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupExitButton.ascx.cs" Inherits="FLEX.Web.UserControls.PopupExitButton" %>

<a id="btnExit" class="btn btn-primary" onclick="window.setTimeout(closeWindow, 0);" href="#">
   <span class="glyphicon glyphicon-remove"></span>&nbsp;<asp:Label runat="server" ID="lblText" Text="Exit" />
</a>