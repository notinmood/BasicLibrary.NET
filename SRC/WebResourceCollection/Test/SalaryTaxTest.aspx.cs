using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Finance;

namespace WebResourceCollection.Test
{
    public partial class SalaryTaxTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Button1.Text= SalaryTaxHelper.GetSalaryTax(Convert.ToDecimal( this.TextBox1.Text)).ToString();
        }
    }
}