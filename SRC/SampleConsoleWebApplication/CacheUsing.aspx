

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheUsing.aspx.cs" Inherits="WebApplicationConsole.CacheUsing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:button ID="Button1" runat="server" text="简单类型缓存" 
            onclick="Button1_Click" />
    
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="复杂类型缓存" />
    
    </div>
    </form>
</body>
</html>
