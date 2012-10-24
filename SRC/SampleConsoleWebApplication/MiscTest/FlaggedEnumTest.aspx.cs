using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;
using HiLand.Utility4.Data;

namespace WebApplicationConsole.MiscTest
{
    public partial class FlaggedEnumTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            UserTypes userType = UserTypes.Manager | UserTypes.SuperAdmin;
            //this.Button1.Text = EnumHelper.ContainsFlag<UserTypes>(userType,UserTypes.Manager).ToString();
            //this.Button1.Text= EnumHelper.ContainsFlag<UserTypes>(userType,UserTypes.Manager).ToString();
            this.Button1.Text = userType.ContainsFlag(UserTypes.Manager).ToString();
            //this.Button1.Text = userType.ContainsFlag(UserTypes.CommonUser).ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            UserTypes userType = UserTypes.Manager;
            userType = userType.AddFlag(UserTypes.SuperAdmin);
            this.Button2.Text = userType.ContainsFlag(UserTypes.SuperAdmin).ToString();
            //this.Button2.Text = userType.ContainsFlag(UserTypes.CommonUser).ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            UserTypes userType = UserTypes.Manager | UserTypes.SuperAdmin;
            userType = userType.RemoveFlag(UserTypes.SuperAdmin);
            bool isContains = userType.ContainsFlag(UserTypes.SuperAdmin);
            this.Button3.Text = isContains.ToString();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            this.Button4.Text = 12.ContainsFlag(4).ToString();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            this.Button5.Text = 4.AddFlag(8).ToString();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            this.Button6.Text = 12.RemoveFlag(4).ToString();
        }
    }
}