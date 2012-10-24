using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebApplicationConsole
{
    public partial class 字符串解析测试 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string target = "{StandardSalaryUrban}*({MedicalEnterprise}+{MedicalPerson}+{AccidentEnterprise}+{AccidentPerson})";
            List<string> placeHolders= StringHelper.GetPlaceHolderList(target,"{","}");
            this.Button1.Text = placeHolders.Count.ToString();
        }
    }
}