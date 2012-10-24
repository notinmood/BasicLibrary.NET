<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FunctionDemo.aspx.cs" Inherits="WebResourceCollection.Test.FunctionDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="验证自定义DAL" />
    
        <asp:Button ID="Button2" runat="server" Text="验证扩展IDAL" 
            onclick="Button2_Click" />
    
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Create" />
        <asp:Button ID="Button4" runat="server" Text="Updata" onclick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Delete" onclick="Button5_Click" />
        <asp:Button ID="Button6" runat="server" Text="Get" onclick="Button6_Click" />
        <asp:Button ID="Button8" runat="server" Text="GetCount" 
            onclick="Button8_Click" />
        <asp:Button ID="Button7" runat="server" Text="GetList" 
            onclick="Button7_Click" />
        <asp:Button ID="Button9" runat="server" Text="GetPagedCollection" 
            onclick="Button9_Click" />
    
    </div>
    </form>
</body>
</html>
