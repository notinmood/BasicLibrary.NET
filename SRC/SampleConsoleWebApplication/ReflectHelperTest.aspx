<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReflectHelperTest.aspx.cs" Inherits="WebApplicationConsole.ReflectHelperTest" %>

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
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="相同属性赋值测试" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="继承对象复制测试" />
    
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="获取Attribute测试" />
    
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="两实体属性比较" />
    
    </div>
    </form>
</body>
</html>
