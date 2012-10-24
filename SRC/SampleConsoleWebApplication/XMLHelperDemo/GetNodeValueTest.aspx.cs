using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;
using System.Xml;

namespace WebApplicationConsole.XMLHelperDemo
{
    public partial class GetNodeValueTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XMLHelperDemo/XMLFileDemo.xml"));
            this.Literal1.Text = XmlHelper.GetNodeValue(doc, "root/branch2/subBranch");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XMLHelperDemo/XMLFileDemo.xml"));
            this.Literal1.Text = XmlHelper.GetNodeValue(doc, "root/branch2/subBranch", "name");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XMLHelperDemo/XMLFileDemo.xml"));
            XmlNode node = doc.SelectSingleNode("root/branch2/subBranch");
            this.Literal1.Text = XmlHelper.GetNodeValue(node);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XMLHelperDemo/XMLFileDemo.xml"));
            XmlNode node = doc.SelectSingleNode("root/branch2/subBranch");
            this.Literal1.Text = XmlHelper.GetNodeValue(node, "","name");
        }
    }
}