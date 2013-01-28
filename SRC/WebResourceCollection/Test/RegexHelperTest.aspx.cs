using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebResourceCollection.Test
{
    public partial class RegexHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string idCardValue = this.TextBox1.Text;
            if (Regex.IsMatch(idCardValue, RegexHelper.IDCardFormat, RegexOptions.IgnoreCase))
            {
                this.Literal1.Text = "身份证格式OK";
            }
            else
            {
                this.Literal1.Text = "身份证格式有问题";
            }
        }
    }
}