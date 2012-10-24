<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceUsing.aspx.cs"
    Inherits="WebApplicationConsole.ResourceUsing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%=HiLand.Utility.Resources.Javascript.Common%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script type="text/javascript">
            document.write(format("And the {0} want to know whose {1} you {2}", "papers", "shirt", "wear"));
        </script>
    </div>
    </form>
</body>
</html>
