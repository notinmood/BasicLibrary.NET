<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LanguageResourceTest.aspx.cs" Inherits="WebApplicationConsole.LanguageResourceTest" %>

<%@ Register Assembly="HiLand.Utility" Namespace="HiLand.Utility.UI" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:DDLCompressTypes ID="DDLCompressTypes1" runat="server">
        </cc1:DDLCompressTypes>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
