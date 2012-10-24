using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace HiLand.Framework.PluginsWebSample.InformationFoo
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.litDisplay.Text = string.Format("Hello Mr. Xie, now time is {0}", DateTime.Now.ToString());
        }
    }
}