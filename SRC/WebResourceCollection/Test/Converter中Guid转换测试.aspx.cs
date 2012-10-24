using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebResourceCollection.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string valueString = "0fac1828-5859-465d-b373-75d2e9a7a17c";
            Guid result = Converter.ChangeType<Guid>(valueString);
            this.Button1.Text = result.ToString();
        }
    }
}