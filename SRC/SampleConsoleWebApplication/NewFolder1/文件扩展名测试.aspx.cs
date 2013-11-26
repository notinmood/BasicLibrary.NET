using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.IO;

namespace WebApplicationConsole.NewFolder1
{
    public partial class 文件扩展名测试 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Stream stream = this.FileUpload1.FileContent;
            string newFileName = Server.MapPath("~/NewFolder1/" + this.FileUpload1.FileName);
            this.FileUpload1.SaveAs(newFileName);
            System.IO.FileStream fs = new System.IO.FileStream(newFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            this.Button1.Text = FileHelper.GetRealFormat(fs).ToString();
        }
    }
}