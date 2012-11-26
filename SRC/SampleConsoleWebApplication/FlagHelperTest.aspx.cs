using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebApplicationConsole
{
    public partial class FlagHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //this.Button1.Text = FlagHelper.AddFlag(5,2).ToString();
            this.Button1.Text = FlagHelper.AddFlag(7, 2).ToString();
        }
    }
}