using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using HiLand.Utility.DataBase;
using System.Data.SQLite;
using System.Data;
using HiLand.Utility.Data;
using System.Diagnostics;

namespace WebApplicationConsole
{
    public partial class SQLiteTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private static string fileFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sqlite3.test.db");
        private static string connectionString = string.Format("Data Source={0};", fileFullName);

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileFullName) == false)
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(fileFullName);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = conn.CreateCommand())
                {
                    conn.Open();

                    command.CommandText = @"CREATE TABLE [MyUsers2] (
                      [UserID] INT, 
                      [UserName] NVARCHAR(255));";
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string sqlClause = string.Format("INSERT INTO [MyUsers] VALUES('{0}','{1}')", "{" + GuidHelper.NewGuid() + "}", "QingdaoCity");
            SQLiteHelper.Instance.ExecuteNonQuery(connectionString, CommandType.Text, sqlClause);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Guid temp = Guid.NewGuid();
            Guid tt = GuidHelper.NewGuid();
            this.Button4.Text = temp.ToString();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            string sqlClause = string.Format("SELECT COUNT(*) FROM [MyUsers]");
            this.Button5.Text = SQLiteHelper.Instance.ExecuteScalar(connectionString, CommandType.Text, sqlClause).ToString();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            string sqlClause = string.Format("SELECT * FROM [MyUsers]");
            int count = 0;
            using (IDataReader reader = SQLiteHelper.Instance.ExecuteReader(connectionString, CommandType.Text, sqlClause))
            {
                while (reader.Read())
                {
                    count++;
                }

                this.Button6.Text = count.ToString();
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteTransaction tran = conn.BeginTransaction())
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < 10000; i++)
                    {
                        string sqlClause = string.Format("INSERT INTO [MyUsers] VALUES('{0}','{1}')", "{" + GuidHelper.NewGuid() + "}", GuidHelper.NewGuidString());
                        SQLiteHelper.Instance.ExecuteNonQuery(tran, CommandType.Text, sqlClause);
                    }
                    tran.Commit();
                    sw.Stop();
                    this.Button7.Text = string.Format("消耗的时间为{0}s",sw.Elapsed.TotalSeconds);
                }
            }
        }
    }
}