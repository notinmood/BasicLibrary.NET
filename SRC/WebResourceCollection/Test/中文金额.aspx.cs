using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Finance;

namespace WebResourceCollection.Test
{
    public partial class 中文金额 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = RMB.GetChineseDisplayValue(100001.07M);
            this.Button1.Text = s;
        }

    }
}