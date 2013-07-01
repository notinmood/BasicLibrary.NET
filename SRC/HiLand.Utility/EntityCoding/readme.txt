本方法来源于http://www.cnblogs.com/happyframework/archive/2013/05/12/3073688.html
以下为使用方法（其对外暴露的为CodeRuleInterpreter）：

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HiLand.Utility.EntityCoding
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var employeeCode = CodeRuleInterpreter
//                .Interpret("前缀_<日期:yyyy|MM_dd-HHmmss>_<属性:NamePinYin>")
//                .Generate(new Employee { NamePinYin = "DUANGW" });

//            Console.WriteLine(employeeCode);
//            Console.ReadLine();
//        }
//    }

//    class Employee
//    {
//        public string NamePinYin { get; set; }
//        public string EmployeeCode { get; set; }
//    }
//}



