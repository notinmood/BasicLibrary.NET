using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebApplicationConsole
{
    public partial class 集合操作测试 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> list= new List<string>();
            list.Add("qingdao");
            list.Add("beijing");
            list.Add("shanghai");
            //this.Button1.Text= CollectionHelper.Concat(",", "[", "]", list);
            this.Button1.Text = CollectionHelper.Concat<string>(",", list);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<string> list1 = new List<string>();
            list1.Add("qingdao");
            list1.Add("beijing");
            list1.Add("shanghai");

            List<string> list2 = new List<string>();
            list2.Add("qingdao1");
            list2.Add("beijing1");
            list2.Add("shanghai1");

            this.Button2.Text = CollectionHelper.IsExistAtLeastOneElement(list1,list2).ToString();
        }
    }
}