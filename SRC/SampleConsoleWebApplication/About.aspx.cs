using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;
using System.Reflection;

namespace WebApplicationConsole
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                assembly= Assembly.Load("HiLand.Utility");
                this.litVersion.Text = AssemblyHelper.GetCompiledTime(assembly).ToString();
            }
        }
    }
}
