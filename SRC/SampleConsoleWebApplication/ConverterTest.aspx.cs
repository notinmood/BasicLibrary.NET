using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Data;

namespace WebApplicationConsole
{
    public partial class ConverterTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IList<UserInfo> users = Converter.ToList<UserInfo>(MockData());

            foreach (var user in users)
            {
                this.Literal1.Text += string.Format("{0} : {1} - {2};", user.UserID, user.UserName, user.Salary);
            }
        }


        /// <summary>
        /// 用户信息实体
        /// </summary>
        public class UserInfo
        {
            /// <summary>
            /// 用户编号
            /// </summary>
            public int UserID { get; set; }

            /// <summary>
            /// 用户姓名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 用户薪资
            /// </summary>
            public decimal Salary { get; set; }
        }

        /// <summary>
        /// 模拟数据
        /// </summary>
        /// <returns></returns>
        private static DataTable MockData()
        {
            DataTable dt = new DataTable("UserInfo");

            // 添加数据列
            DataColumn dc = null;

            dc = dt.Columns.Add("UserID", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;    //自动增加
            dc.AutoIncrementSeed = 1;   //起始为1
            dc.AutoIncrementStep = 1;   //步长为1
            dc.AllowDBNull = false;

            dc = dt.Columns.Add("UserName", Type.GetType("System.String"));
            dc = dt.Columns.Add("Salary", Type.GetType("System.Decimal"));

            #region 添加数据行

            // 方式1
            //DataRow dr = null;

            //dr = dt.NewRow();
            //dr["UserName"] = "洪自军";
            //dr["Salary"] = 123.45;
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["UserName"] = "武建昌";
            //dr["Salary"] = 987.65;
            //dt.Rows.Add(dr);

            // 方式2
            dt.Rows.Add(new object[] { null, "張洋", 123.45 });
            dt.Rows.Add(new object[] { null, "張兄家", 987.65 });
            dt.Rows.Add(new object[] { null, "王生杰", 111.11 });
            dt.Rows.Add(new object[] { null, "呉QQ", 888.88 });
            dt.Rows.Add(new object[] { null, "劉瑞", 222.22 });

            #endregion

            return dt;
        }
    }
}