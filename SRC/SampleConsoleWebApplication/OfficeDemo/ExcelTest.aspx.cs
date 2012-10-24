using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using HiLand.Utility.IO;
using HiLand.Utility.Office;
using HiLand.Utility.Web;

namespace WebApplicationConsole.OfficeDemo
{
    public partial class ExcelTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                string connString = "server=.;database=TXTQD;uid=sa;pwd=123456;";
                string commString = "SELECT *  FROM [CoreUser]";

                SqlDataAdapter ada = new SqlDataAdapter(commString,connString);
                DataSet ds = new DataSet();
                ada.Fill(ds,"myTable");

                Stream excleStream= ExcelHelper.WriteExcel(ds.Tables[0],false);
                DownloadHelper.Down(excleStream,"xls","myExcel.xls");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/OfficeDemo/myExcel.xls");
            using (Stream stream = FileHelper.GetStreamFromFile(filePath))
            {
                using (DataTable dt = ExcelHelper.ReadExcel(stream, 0, 0))
                {
                    this.GridView1.DataSource = dt;
                    this.GridView1.DataMember = dt.TableName;
                    this.GridView1.DataBind();
                }
            }
        }
    }
}