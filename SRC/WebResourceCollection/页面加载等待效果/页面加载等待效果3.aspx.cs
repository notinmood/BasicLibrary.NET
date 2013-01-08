using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebResourceCollection
{
    public partial class 页面加载等待效果3 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Attributes.Add("onsubmit", "$('body').mask('数据计算中，请等待...');");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int i = 0;

            Thread.Sleep(10000);
            int j = i;
        }
    }
}