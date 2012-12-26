<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="页面加载等待效果.aspx.cs" Inherits="WebResourceCollection.页面加载等待效果" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>等待效果</title>
    <script language="javascript">
        var times = 0;
        function tick() //用于显示执行的时长
        {
            times++;
            var min = Math.floor(times / 60);
            var scend = times - min * 60;
            document.getElementById('Clocktimes').innerHTML = min + '分' + scend + ' 秒';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="runing" runat="server" style="z-index: 12000; left: 0px; width: 100%;
    cursor: wait;position: absolute; top: 0px; height: 100%">
    <table width="100%" height="100%">
        <tr align="center" valign="middle">
            <td>
                <table width="200" height="120" bgcolor="Gray"
                       style="filter: Alpha(Opacity=70); color:White">                  
                    <tr align="center" valign="middle" >
                        <td>     
         
                        <div id="Clocktimes"></div><br>             
                            正在提交<br>
                            请稍候....
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
    <div style="text-align:center">
    
        姓名：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br>
        单位：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br>
        地址：<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br>
        籍贯：<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br>       
        <asp:Button ID="btnOk" runat="server" Text=" 提交 " OnClick="btnOk_Click" />
    </div>
    </form>
</body>
</html>
