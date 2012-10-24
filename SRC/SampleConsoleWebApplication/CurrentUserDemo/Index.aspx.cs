using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Utility.Enums;
using HiLand.Framework.BusinessCore;

namespace WebApplicationConsole.CurrentUserDemo
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            LoginStatuses loginStatus = LoginStatuses.Successful;
            BusinessUserBLL.Login("admin", "123", out loginStatus);
            this.Button2.Text = loginStatus.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BusinessUser currentUser = BusinessUserBLL.CurrentUser;
            this.Button1.Text = currentUser.UserName;
        }
    }
}