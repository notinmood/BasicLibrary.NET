using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.IO;

namespace WebApplicationConsole.IOTest
{
    public partial class EnsureDatePathTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath= IOHelper.EnsureDatePath(basePath,DatePathFormaters.YMD);
            this.Button1.Text = fullPath;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = FileHelper.GenerateYearMonthSeperatedFileFullName(basePath,"", DatePathFormaters.Y_M_D);
            this.Button2.Text = fullPath;
        }
    }
}