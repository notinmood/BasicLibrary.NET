using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationConsole.AuthCodeDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.AuthCode1.GetAuthCodeValue() == this.TextBox1.Text)
            {
                this.Button1.Text = "ok";
            }
            else
            {
                this.Button1.Text = "error";
            }
        }
    }
}