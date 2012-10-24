using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Resources;

namespace WebApplicationConsole
{
    public partial class LanguageResourceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                string path = ResourcesManager.GetResourceWebUrl("img_allow.gif");
                this.Literal1.Text = path;

                Dictionary<string, string> DIC = ResourcesManager.Resource;
                string tt = ResourcesManager.GetValue("userPasswordTest");
            }
        }
    }
}