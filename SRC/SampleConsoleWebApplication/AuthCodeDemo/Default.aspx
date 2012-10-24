<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplicationConsole.AuthCodeDemo.Default" %>

<%@ Register Assembly="HiLand.Utility" Namespace="HiLand.Utility.UI" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        验证码：<cc1:AuthCode ID="AuthCode1" CharCount="5" runat="server" />
        <br />
        请输入：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="验证" onclick="Button1_Click" />

    </div>
    </form>
</body>
</html>
