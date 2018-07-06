using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Setting;

namespace WebResourceCollection.Test
{
    public partial class Config测试 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Literal1.Text = Config.GetAppSetting<string>("keyString");
            this.Literal1.Text += Config.GetAppSetting<bool>("keyBool").ToString();
            this.Literal1.Text += Config.GetAppSetting<int>("keyInt").ToString();
            this.Literal1.Text += Config.GetAppSetting<float>("keyFloat").ToString();
        }
    }
}