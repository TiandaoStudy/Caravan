<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="Finsa.Caravan.ReportingService.ReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rvweb" %>
<%@ OutputCache Duration="120000" Location="ServerAndClient" VaryByParam="*" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title><%: Page.Title %></title>

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <!-- Styles -->
    <link href="~/Content/normalize.css" rel="stylesheet" type="text/css" media="screen" />
</head>

<body>
    <form runat="server">
        <asp:ScriptManager runat="server" />
        <rvweb:ReportViewer ID="theReport" runat="server" Width="100%" Height="1000px" />
    </form>
</body>

</html>