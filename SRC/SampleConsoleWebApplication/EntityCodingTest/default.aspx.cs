using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiLand.Utility.EntityCoding;

namespace WebApplicationConsole.EntityCodingTest
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var employeeCode = CodeRuleInterpreter
                .Interpret("前缀_<日期:yyyy|MM_dd-HHmmss>_<属性:NamePinYin>")
                .Generate(new Employee { NamePinYin = "DUANGW" });

            this.Button1.Text = employeeCode;
        }

        class Employee
        {
            public string NamePinYin { get; set; }
            public string EmployeeCode { get; set; }
        }
    }
}