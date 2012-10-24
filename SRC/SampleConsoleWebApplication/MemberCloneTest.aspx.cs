using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebApplicationConsole
{
    public partial class MemberCloneTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AA<BB> aa = new AA<BB>();
            aa.MyProperty = 2;
            aa.MyProperty2 = "sss";

            BB bb = null;

            ////1.
            bb= aa.Clone();

            //2.
            //bb= Converter.ChangeType<BB>(aa);

            this.Button1.Text = bb.MyProperty2;
        }
    }

    public class AA<TT> where TT:AA<TT>
    {
        public int MyProperty { get; set; }
        public string MyProperty2 { get; set; }

        public TT Clone()
        {
            return this.MemberwiseClone() as TT;
        }
    }

    public class BB : AA<BB>
    {
        public int MyProperty3 { get; set; }
    }
}