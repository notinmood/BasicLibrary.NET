using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Framework.BusinessCore;

namespace WebApplicationConsole
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            //AppDomain.CurrentDomain.AppendPrivatePath("E:\\MyWorkSpace\\myProject\\HiLand\\SRC\\WebApplicationConsole\\plugins");
            AppDomainSetup setup = new AppDomainSetup();
            setup.PrivateBinPath = @"E:\MyWorkSpace\myProject\HiLand\SRC\WebApplicationConsole\plugins";
            ApplicationService.LoadPlugins();
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            Dictionary<Guid, IApplication> dic = ApplicationService.Plugins;
            this.TextBox1.Text = string.Empty;

            foreach (KeyValuePair<Guid, IApplication> kvp in dic)
            {
                this.TextBox1.Text += kvp.Value.ApplicationName + "\r\n";
            }
        }
    }
}
