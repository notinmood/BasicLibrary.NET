using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Cache;

namespace WebApplicationConsole
{
    public partial class CacheUsing : System.Web.UI.Page
    {
        private static string cachekeyInt = "cachekeyInt";
        private static string cachekeyList = "cachekeyList";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           int i= CacheHelper.Access<int>(cachekeyInt,200,Foo);
        }

        private int Foo()
        {
            return 5;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<string> list = CacheHelper.Access(cachekeyList,200,Fooo);
        }

        private List<string> Fooo()
        {
            List<string> list = new List<string>();
            list.Add("qingdao");
            return list;
        }
    }
}