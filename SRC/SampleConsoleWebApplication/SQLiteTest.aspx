<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SQLiteTest.aspx.cs" Inherits="WebApplicationConsole.SQLiteTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="Button2" runat="server" Text="创建库" onclick="Button2_Click" />
    <asp:Button ID="Button1" runat="server" Text="创建表" onclick="Button1_Click" />
    <asp:Button ID="Button3" runat="server" Text="插入数据测试NoQuery" 
            onclick="Button3_Click" />
    <asp:Button ID="Button5" runat="server" Text="测试Scalar" onclick="Button5_Click" />
    <asp:Button ID="Button6" runat="server" Text="测试Reader" onclick="Button6_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="Button4" runat="server" Text="GUID生成测试" onclick="Button4_Click" />
        <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
            Text="测试大数据性能" />
    </div>
    
    </form>
</body>
</html>
