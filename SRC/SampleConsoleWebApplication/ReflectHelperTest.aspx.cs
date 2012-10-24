using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

            Worker worker = new Worker { 
                Name="Hello",
                FactoryName="World"
            };

            SomethingElse somethingElse = new SomethingElse() { 
                Name="beijing"
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
    }
}