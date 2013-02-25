<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptServiceTest.aspx.cs" Inherits="WebApplicationConsole.EncryptServiceTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Base64編碼" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Base64解碼" />
    
    </div>
    </form>
</body>
</html>
