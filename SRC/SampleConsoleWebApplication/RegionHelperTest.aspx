<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegionHelperTest.aspx.cs" Inherits="WebApplicationConsole.RegionHelperTest" %>

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
               <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="判断区间是否有重叠" />
    </div>
    </form>
</body>
</html>
