using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.IO;
using System.Text;

namespace WebApplicationConsole
{
    public partial class FileTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileFullName = @"C:\Windows\zh-CN\bootfix.bin";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("文件名称为{0}<br/>", FileHelper.GetFileShortName(fileFullName));
            sb.AppendFormat("文件主名称为{0}<br/>", FileHelper.GetFileMainName(fileFullName));
            sb.AppendFormat("文件扩展名称为{0}<br/>", FileHelper.GetFileExtensionName(fileFullName));
            sb.AppendFormat("文件按年月分目录的名称为{0}<br/>", FileHelper.GenerateYearMonthSeperatedFileFullName(@"C:\Windows\zh-CN","", DatePathFormaters.Y_M_D));
            this.Response.Write(sb.ToString());
        }
    }
}