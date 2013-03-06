<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnumHelperTest.aspx.cs" Inherits="WebApplicationConsole.EnumHelperTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="通过友好名称获取枚举项" />
    
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="通过前缀获取枚举项列表" />
    
    </div>
    </form>
</body>
</html>
