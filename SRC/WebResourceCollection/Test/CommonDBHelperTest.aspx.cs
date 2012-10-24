using System;
using System.Data;
using System.Data.SqlClient;
using HiLand.Utility.DataBase;

namespace WebResourceCollection.Test
{
    public partial class CommonDBHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string connectionString = SqlHelper.Instance.ConnectionString;

        protected void Button3_Click(object sender, EventArgs e)
        {
            string commandText = "SELECT [LoanGuid]  FROM [GeneralLoanBasic] WHERE  [LoanTermCount] = @LoanTermCount";
            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@LoanTermCount", 3) };
            this.Button3.Text= SqlHelper.Instance.ExecuteScalar(connectionString,CommandType.Text,commandText,sqlParas).ToString();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string commandText = @"Update [GeneralLoanBasic] Set   
					[LoanPurpose] = @LoanPurpose,
					[CheckUserID] = @CheckUserID
					Where [LoanGuid] = @LoanGuid";

            SqlParameter[] sqlParas = new SqlParameter[]
            {
                new SqlParameter("@LoanPurpose","ssssssssss"),
                new SqlParameter("@CheckUserID","6246bcd0-96ef-463e-95ae-acbdf72a3557"),
                new SqlParameter("@LoanGuid","6246bcd0-96ef-463e-95ae-acbdf72a3556")
            };

           this.Button4.Text= SqlHelper.Instance.ExecuteNonQuery(connectionString,CommandType.Text,commandText,sqlParas).ToString();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void Button8_Click(object sender, EventArgs e)
        {

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            string commandText = "SELECT * FROM [GeneralLoanBasic] WHERE  [LoanTermCount] = @LoanTermCount";
            SqlParameter[] sqlParas = new SqlParameter[] { new SqlParameter("@LoanTermCount", 3) };
            using (SqlDataReader reader = SqlHelper.Instance.ExecuteReader(connectionString, CommandType.Text, commandText, sqlParas))
            {
                int rowCount = 0;
                while (reader.Read())
                {
                    rowCount++;
                }
                this.Button7.Text = rowCount.ToString();
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {

        }
    }
}