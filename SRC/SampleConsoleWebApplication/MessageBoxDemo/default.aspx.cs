using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.UI;

namespace WebApplicationConsole.MessageBoxDemo
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageBox.ShowConfirm(this.Button3,"ssssss");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Page,"ssssssssssssss");
            MessageBox.Show("sssssssssssss");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //MessageBox.ShowAndRedirect("ssssss", "test.aspx");
            MessageBox.ShowAndRedirect("ssssss", "~/default.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            this.Button3.Text = DateTime.Now.ToString();
        }
    }
}