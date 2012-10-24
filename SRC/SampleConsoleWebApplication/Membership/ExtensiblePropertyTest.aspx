<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExtensiblePropertyTest.aspx.cs" Inherits="WebApplicationConsole.Membership.ExtensiblePropertyTest" %>

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
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text=" 测试银行（通用功能）" />
    
        <asp:Button ID="Button2" runat="server" 
            Text=" 测试人员（核心功能）" onclick="Button2_Click" />
    
    </div>
    </form>
</body>
</html>
