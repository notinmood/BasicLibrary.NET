using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebResourceCollection.Misc
{
    public partial class 服务端持续输出信息到客户端 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //由于asp.net持续输出到客户端
            //注意：(IE内核浏览器)下需要字符达到256个字符以上(可以提前输出样式,控制后面信息样式)
            //才能想客户端即时发送新的信息
            StringBuilder sbResponse = new StringBuilder();
            sbResponse.Append("<style type=\"text/css\">span{color:Red;}</style>");
            while (sbResponse.Length < 257)
            {
                sbResponse.Append(" ");
            }
            Response.Write(sbResponse.ToString());
            Response.Flush();
            int j = 0;
            while (true)
            {
                j++;
                Response.Write("<span>" + j + "</span>\t");
                Response.Flush();
                //1秒输送一次(模拟复杂计算耗时)
                Thread.Sleep(1000);
            }
        }
    }
}