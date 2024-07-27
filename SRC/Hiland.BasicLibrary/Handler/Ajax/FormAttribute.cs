//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Hiland.BasicLibrary.Handler.Ajax
//{
//    public class FormAttribute:ParameterAttribute
//    {
//        public FormAttribute() { }

//        public FormAttribute(string name) : base(name, null) { }

//        public FormAttribute(string name, object defaultValue) : base(name, defaultValue) { }

//        public override object GetParameterValue(System.Web.HttpContext context)
//        {
//            return context.Request.Form[Name];
//        }
//    }
//}
