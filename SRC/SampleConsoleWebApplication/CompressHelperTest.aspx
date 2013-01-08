<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompressHelperTest.aspx.cs" Inherits="WebApplicationConsole.CompressHelperTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="压缩" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="解压" />
    
    </div>
    </form>
</body>
</html>
