using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Security;

namespace WebApplicationConsole
{
    public partial class EncryptServiceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.TextBox1.Text=  EncryptService.ToBase64(this.TextBox1.Text);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.TextBox1.Text = EncryptService.FromBase64(this.TextBox1.Text);
        }
    }
}