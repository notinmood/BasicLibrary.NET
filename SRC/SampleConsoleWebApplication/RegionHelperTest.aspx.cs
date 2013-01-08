using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebApplicationConsole
{
    public partial class RegionHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RegionData<DateTime> regionSource = new RegionData<DateTime>(new DateTime(2012,12,10),new DateTime(2012,12,20));

            RegionData<DateTime> regionTarget1 = new RegionData<DateTime>(new  DateTime(2012,12,15),new DateTime(2012,12,18));
            this.Literal1.Text = string.Format("时间15-18号，跟目标区间10-20号，有交集：{0}<br/>", RegionHelper.HasOverlap(regionSource, regionTarget1));

            RegionData<DateTime> regionTarget2 = new RegionData<DateTime>(new DateTime(2012, 12, 15), new DateTime(2012, 12, 22));
            this.Literal1.Text += string.Format("时间15-22号，跟目标区间10-20号，有交集：{0}<br/>", RegionHelper.HasOverlap(regionSource, regionTarget2));

            RegionData<DateTime> regionTarget3 = new RegionData<DateTime>(new DateTime(2012, 12, 9), new DateTime(2012, 12, 18));
            this.Literal1.Text += string.Format("时间9-18号，跟目标区间10-22号，有交集：{0}<br/>", RegionHelper.HasOverlap(regionSource, regionTarget3));

            RegionData<DateTime> regionTarget4 = new RegionData<DateTime>(new DateTime(2012, 12, 9), new DateTime(2012, 12, 22));
            this.Literal1.Text += string.Format("时间9-22号，跟目标区间10-22号，有交集：{0}<br/>", RegionHelper.HasOverlap(regionSource, regionTarget4));

            RegionData<DateTime> regionTarget5 = new RegionData<DateTime>(new DateTime(2012, 12, 8), new DateTime(2012, 12, 9));
            this.Literal1.Text += string.Format("时间8-9号，跟目标区间10-22号，有交集：{0}<br/>", RegionHelper.HasOverlap(regionSource, regionTarget5));
        }
    }
}