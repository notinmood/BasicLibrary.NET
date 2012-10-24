using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Web;

namespace WebApplicationConsole
{
    public partial class UrlInfoTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UrlInfo urlInfo = UrlInfo.New("www.sina.com.cn?e=ui");
            urlInfo.Concat("q","wwww");
            urlInfo.Concat("e","sssss");
            urlInfo.Concat("q","青岛 的");
            urlInfo.Concat("g", "sssss");
            this.Button1.Text = urlInfo.ToString();
        }
    }
}