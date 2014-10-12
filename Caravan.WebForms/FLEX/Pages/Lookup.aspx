<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lookup.aspx.cs" Inherits="FLEX.Web.Pages.Lookup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="LabelName" Text="Nome:" runat="server"></asp:Label>&nbsp;<br />
        <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox><br />
        <asp:Label ID="LabelSurname" Text="Cognome:" runat="server"></asp:Label>&nbsp;<br />
        <asp:TextBox ID="TextBoxSurname" runat="server"></asp:TextBox><br />
        <asp:Button ID="SubmitButton" Text="Submit" runat="server" />
    </div>
    </form>
</body>
</html>
