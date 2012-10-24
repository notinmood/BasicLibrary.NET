using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;
using HiLand.Utility.Web;

namespace WebApplicationConsole
{
    public partial class RequestHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string temp= HttpContext.Current.Request.Url.LocalPath;
            string ssss = temp;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string afterUrl = RequestHelper.AddOrModifyQueryString(this.Request,"city","qingdao");
            string foo = afterUrl;
        }
    }
}