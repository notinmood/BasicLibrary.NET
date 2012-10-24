<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetNodeValueTest.aspx.cs" Inherits="WebApplicationConsole.XMLHelperDemo.GetNodeValueTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <asp:Button ID="Button1" runat="server" Text="获取节点值" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="获取属性值" onclick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="传递空XPath获取节点值" />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="传递空XPath获取属性值" />
    </div>
    </form>
</body>
</html>
