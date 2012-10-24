using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Algorithm;

namespace WebApplicationConsole
{
    public partial class 字符串编辑距离测试 : System.Web.UI.Page
    {
        string sNew = @"GAMBOL";
        string sOld = @"GU BO fy";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Levenshtein l = new Levenshtein();
            this.Button1.Text = Levenshtein.EditDistance(sNew, sOld).ToString();
        }
    }
}