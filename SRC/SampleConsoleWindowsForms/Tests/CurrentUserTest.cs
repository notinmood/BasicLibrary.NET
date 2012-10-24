using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HiLand.Framework.BusinessCore;
using HiLand.Framework.BusinessCore.BLL;

namespace HiLand.Framework.WindowsFormsConsole.Tests
{
    public partial class CurrentUserTest : Form
    {
        public CurrentUserTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BusinessUser user = new BusinessUser();
            user.UserName = "qindgao";
            BusinessUserBLL.CurrentUser = user;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BusinessUser user = BusinessUserBLL.CurrentUser;
            this.button2.Text = user.UserName;
        }
    }
}
