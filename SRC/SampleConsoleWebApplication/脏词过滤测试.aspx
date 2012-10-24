<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="脏词过滤测试.aspx.cs" Inherits="WebApplicationConsole.脏词过滤测试" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="存在测试" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="替换测试" />
&nbsp;<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="文档中所有脏词" />
    
    </div>
    </form>
</body>
</html>
