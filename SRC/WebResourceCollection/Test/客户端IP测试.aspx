<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="客户端IP测试.aspx.cs" Inherits="WebResourceCollection.Test.客户端IP测试" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        本机ip地址
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <br />
        是否为本机服务器 <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
        <br />
        <br />
        是否为本地服务器 <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
    </div>
<table style="width: 100%;">
        <tr>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>

    </form>
    
</body>
</html>
