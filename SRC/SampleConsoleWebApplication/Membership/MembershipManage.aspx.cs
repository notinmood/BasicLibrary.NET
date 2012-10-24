using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;
using HiLand.Utility.Enums;

namespace WebApplicationConsole.Membership
{
    public partial class MembershipManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BusinessUser user = new BusinessUser();
            user.AreaCode = "qingdao";
            user.UserName = "MyName";
            user.UserNameCN = "UserNameCN";
            user.UserEmail = Guid.NewGuid().ToString();
            CreateUserRoleStatuses createUserRoleStatuses;
            BusinessUser returnUser= BusinessUserBLL.CreateUser(user, out createUserRoleStatuses);

            this.Literal1.Text = string.Format("创建用户的成败的状态为{0}",createUserRoleStatuses.ToString());
            if (createUserRoleStatuses == CreateUserRoleStatuses.Successful)
            {
                this.Literal1.Text+= "新生产用户的GUID为："+ returnUser.UserGuid;
            }
        }
    }
}