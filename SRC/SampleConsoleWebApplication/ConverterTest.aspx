<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConverterTest.aspx.cs" Inherits="WebApplicationConsole.ConverterTest" %>

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
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="DataTableConvertToList" />
        
    </div>
    </form>
</body>
</html>
