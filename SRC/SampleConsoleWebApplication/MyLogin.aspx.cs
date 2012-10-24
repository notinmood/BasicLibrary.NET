using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Web;
using HiLand.Framework.Permission;

namespace WebApplicationConsole.Account
{
    public partial class MyLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> roles = new List<string>();
            roles.Add("admin");
            PermissionValidation.WriteCookieAndRedirect("xieran", roles);
        }
    }
}