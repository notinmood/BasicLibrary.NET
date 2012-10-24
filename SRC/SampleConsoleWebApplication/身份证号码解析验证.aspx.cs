using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Entity;

namespace WebApplicationConsole
{
    public partial class 身份证号码解析验证 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IDCard myIDCard=IDCard.Parse("370202197911105436");
            if (myIDCard.HasError)
            {
                this.Button1.Text = myIDCard.Error;
            }
            else
            {
                this.Button1.Text = myIDCard.GetAddress();
            }
        }
    }
}