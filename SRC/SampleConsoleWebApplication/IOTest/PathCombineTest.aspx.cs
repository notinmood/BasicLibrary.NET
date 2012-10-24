using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.IO;

namespace WebApplicationConsole.IOTest
{
    public partial class PathCombineTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Button1.Text= PathHelper.CombineForNative("c:","/sss/","\\www","sss.wsx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Button2.Text = PathHelper.CombineForVirtual("/sss/", "\\www", "sss.wsx");
        }
    }
}