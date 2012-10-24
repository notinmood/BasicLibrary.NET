<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebResourceCollection.URLEncode._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    http://www.cnblogs.com/artwl/archive/2012/03/07/2382848.html
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function encodeUri_onclick() {
            var url = "http://www.cnblogs.com/a file with spaces.html?p=3&m=中国";
            //alert(encodeURI(url));
            console.log(encodeURI(url));
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="Button1" type="button" value="encodeUri" onclick="return encodeUri_onclick()" /><input id="Button2" type="button"
            value="button" />
    </div>
    </form>
</body>
</html>
