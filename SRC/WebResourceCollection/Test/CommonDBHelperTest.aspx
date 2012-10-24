<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonDBHelperTest.aspx.cs" Inherits="WebResourceCollection.Test.CommonDBHelperTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="ExecuteScarlar" />
        <asp:Button ID="Button4" runat="server" Text="ExecuteNonQuery" 
            onclick="Button4_Click" />
        <asp:Button ID="Button7" runat="server" Text="ExecuteReader" 
            onclick="Button7_Click" />

    </div>
    </form>
</body>
</html>
