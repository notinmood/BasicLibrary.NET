using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.Attributes;
using HiLand.Utility.Data;
using HiLand.Utility.Reflection;
using WebApplicationConsole.ClassesForTest;

namespace WebApplicationConsole
{
    public partial class ReflectHelperTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            s.Name = "学生";
            s.Number = "098977";
            s.Age = 30;

            GraduateStudent gsFrom = new GraduateStudent();
            gsFrom.Name = "研究生";
            gsFrom.Age = 20;
            gsFrom.IsMarried = true;

            Worker worker = new Worker
            {
                Name = "Hello",
                FactoryName = "World"
            };

            SomethingElse somethingElse = new SomethingElse()
            {
                Name = "beijing"
            };

            GraduateStudent gsTo = new GraduateStudent();

            ReflectHelper.CopyMemberValue<Worker, GraduateStudent>(worker, gsTo, true);
            //ReflectHelper.CopyMemberValue<SomethingElse, GraduateStudent>(somethingElse, gsTo, false);
            //ReflectHelper.CopyMemberValue<Student, GraduateStudent>(s,gsTo,true);
            //ReflectHelper.CopyMemberValue<GraduateStudent, GraduateStudent>(gsFrom, gsTo, true);
            //ReflectHelper.CopyMemberValue<GraduateStudent, GraduateStudent>(gsFrom, gsTo, false);

            int i = 9;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            s.Name = "学生";
            s.Number = "098977";
            s.Age = 30;

            GraduateStudent gs = Converter.InheritedEntityConvert<Student, GraduateStudent>(s);

            int i = 9;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TestEnum te = TestEnum.Item1;

            EnumItemIsDisplayInListAttribute att = ReflectHelper.GetAttribute<EnumItemIsDisplayInListAttribute>(te.GetType().GetField("Item3"));
            if (att != null)
            {
                this.Button3.Text = att.IsDisplayInList.ToString();
            }
        }

        private enum TestEnum
        {
            [EnumItemIsDisplayInListAttribute(true)]
            Item1,
            [EnumItemIsDisplayInListAttribute(false)]
            Item2,
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Student s1 = new Student();
            s1.Name = "学生1";
            s1.Number = "098977";
            s1.Age = 30;

            Student s2 = new Student();
            s2.Name = "学生2";
            s2.Number = "0989775";
            s2.Age = 31;

            List<string> resultData;
            this.Button4.Text= ReflectHelper.Compare(s1, s2, out resultData).ToString();
            this.Literal1.Text = string.Empty;
            foreach (string item in resultData)
            {
                this.Literal1.Text += item+"; ";
            }
        }
    }
}