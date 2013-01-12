using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Logging;

namespace WebResourceCollection.Test
{
    public partial class FileLogerTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FileLogger loger = new FileLogger();
            loger.Log("vbbvbs");
        }
    }
}