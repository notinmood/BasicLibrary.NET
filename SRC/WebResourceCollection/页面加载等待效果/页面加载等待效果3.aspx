<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="页面加载等待效果3.aspx.cs" Inherits="WebResourceCollection.页面加载等待效果3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="mask.css" rel="stylesheet" />
    <script src="../Scripts/jQuery/jquery-1.4.4.min.js"></script>
    <script src="mask.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="submit" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").unmask();
            //ShowDiv();
        });
    </script>
</body>
</html>
