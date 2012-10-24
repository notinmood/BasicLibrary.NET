using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;

namespace WebApplicationConsole
{
    public partial class EnumHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Sexes sex= EnumHelper.GetItem<Sexes>("男");
            int i = 9;
        }
    }
}