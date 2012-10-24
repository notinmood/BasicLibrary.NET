

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelTest.aspx.cs" Inherits="WebApplicationConsole.OfficeDemo.ExcelTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:button ID="Button1" runat="server" text="导出Excel" onclick="Button1_Click" />
<asp:button ID="Button2" runat="server" text="导入Excel" onclick="Button2_Click" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
