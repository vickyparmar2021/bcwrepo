<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteAddress.aspx.cs" Inherits="DeleteAddress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblErrrorMessage" runat="server" Text=""></asp:Label><br />
            <br />
            <asp:TextBox ID="txtAddressId" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
