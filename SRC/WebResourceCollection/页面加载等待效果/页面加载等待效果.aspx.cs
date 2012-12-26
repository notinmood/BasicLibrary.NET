using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebResourceCollection
{
    public partial class 页面加载等待效果 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnOk.Attributes.Add("onclick",
                "javascript:document.getElementById('runing').style.visibility='visible';window.setInterval('tick()',1000);");
        }

        override protected void OnPreRenderComplete(EventArgs e)
        {
            runing.Style.Add("visibility", "hidden");
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string s = "";
            for (int n = 0; n < 50000; n++)
            {
                s += "a";
            }
        }
    }
}