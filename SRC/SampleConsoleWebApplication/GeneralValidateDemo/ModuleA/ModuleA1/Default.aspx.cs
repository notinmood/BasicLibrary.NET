using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using HiLand.Utility.Setting.SectionHandler;

namespace WebApplicationConsole.GenericValidateDemo.ModuleA.ModuleA1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           GeneralValidateConfig config= ConfigurationManager.GetSection("permissionValidate/generalValidate") as GeneralValidateConfig;
           //config = config;
        }
    }
}