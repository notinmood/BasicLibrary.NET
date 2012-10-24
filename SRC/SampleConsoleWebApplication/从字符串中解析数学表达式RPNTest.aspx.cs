using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Mathes.StringParse;

namespace WebApplicationConsole
{
    public partial class RPNTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RPN rpn = new RPN();
            string inputData = this.TextBox1.Text;
            if (rpn.Parse(inputData) == true)
            {
                this.Button1.Text = rpn.Evaluate().ToString();
            }
        }
    }
}