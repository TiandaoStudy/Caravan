<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Popup.aspx.cs" Inherits="FLEX.Sample.WebUI.Popup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.11.0.min.js"></script>
    <script type="text/javascript" src="Scripts/popupHandler.js"></script>
    <script type="text/javascript">
        function popupSubmit() {
            var name = $("#TextBoxName").val();
            var surname = $("#TextBoxSurname").val();

            alert("Nome: " + name + "\nCognome: " + surname + "\nSumbit!");

            //var retVal = new Object;
            //retVal.name = name;
            //retVal.surname = surname;
            //top.returnValue = retVal;

            sessionStorage.name = name;
            sessionStorage.surname = surname;

            closeWindow();
        }

        function popupCancel() {
            closeWindow();
        }

        $(document).ready(function () {
            $("#TextBoxName").val(sessionStorage.name);
            $("#TextBoxSurname").val(sessionStorage.surname);
        });
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div>
        <asp:Label ID="LabelName" Text="Nome:" runat="server"></asp:Label>&nbsp;<br />
        <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox><br />
        <asp:Label ID="LabelSurname" Text="Cognome:" runat="server"></asp:Label>&nbsp;<br />
        <asp:TextBox ID="TextBoxSurname" runat="server"></asp:TextBox><br />
        <asp:Button ID="SubmitButton" Text="Submit" OnClick="SubmitBtn_Click" OnClientClick="popupSubmit();" runat="server" />
        <asp:Button ID="CancelButton" Text="Cancel" OnClientClick="popupCancel();" runat="server" />
    </div>
    </form>
</body>
</html>
