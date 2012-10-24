using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Web;

namespace WebResourceCollection.Test
{
    public partial class 客户端IP测试 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Button1.Text= ClientBrowser.GetClientIP();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Button2.Text = WebHelper.IsSelfServer.ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            this.Button3.Text = WebHelper.IsLocalServer.ToString();
        }
    }
}