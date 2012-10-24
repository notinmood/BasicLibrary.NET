<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlaggedEnumTest.aspx.cs" Inherits="WebApplicationConsole.MiscTest.FlaggedEnumTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <fieldset>
        <legend>具有Flag功能的枚举判断</legend>
        <asp:Button ID="Button1" runat="server" Text="判断是否包含" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="添加Flag项到集合" />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="从集合中Flag枚举项" />
    </fieldset>

        <fieldset>
        <legend>具有Flag功能的int判断</legend>
        <asp:Button ID="Button4" runat="server" Text="判断是否包含" onclick="Button4_Click"  />
        <asp:Button ID="Button5" runat="server"  
            Text="添加Flag项到集合" onclick="Button5_Click" />
        <asp:Button ID="Button6" runat="server" 
            Text="从集合中移除Flag项" onclick="Button6_Click" />
    </fieldset>
    </div>
    </form>
</body>
</html>
