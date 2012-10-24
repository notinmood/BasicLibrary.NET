using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Algorithm.DirtyWordsFilter;

namespace WebApplicationConsole
{
    public partial class 脏词过滤测试 : System.Web.UI.Page
    {
        string targetString = string.Format(@"钓鱼岛是中国的，日本滚出去");
        HashFilter hash = new HashFilter();

        private void LoadDirthWords()
        {
            using (StreamReader sw = new StreamReader(File.OpenRead(Server.MapPath("~/脏词过滤测试.txt"))))
            {
                string key = sw.ReadLine();
                while (key != null)
                {
                    if (key != string.Empty)
                    {
                        hash.AddKey(key);
                    }
                    key = sw.ReadLine();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadDirthWords();
            this.Label1.Text = hash.HasDirtyWord(targetString).ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            LoadDirthWords();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string result = hash.Replace(targetString);
            sw.Stop();


            this.Label1.Text = string.Format("共消耗{0}毫秒，结果为{1}", sw.ElapsedMilliseconds, result);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LoadDirthWords();
            List<string> result = hash.FindAll(targetString);
            this.Label1.Text = result.Aggregate((a, b) => a + "," + b);
        }
    }
}