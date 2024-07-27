//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;

//namespace Hiland.BasicLibrary4.MVC
//{
//    //TODO:能够记录当前那个Controller和Action被执行

//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
//    public class LogMessageAttribute : FilterAttribute,IActionFilter, IResultFilter
//    {
//        //public event Doo BeforeAction;
//        //public Doo ff
//        //{
//        //    get { return BeforeAction; }
//        //    set { BeforeAction = value; }
//        //}
//        private Action<string, string> action;

//        //public LogMessageAttribute()
//        //{
           
//        //}

//        public LogMessageAttribute(Action<string, string> action)
//        {
//            this.action = action;
//        }

//        public void OnResultExecuted(ResultExecutedContext filterContext)
//        {
//            //throw new NotImplementedException();
//        }

//        public void OnResultExecuting(ResultExecutingContext filterContext)
//        {
//            //throw new NotImplementedException();
//        }

//        public void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            //sfilterContext.
//            //throw new NotImplementedException();
//        }

//        public void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            if (this.action != null)
//            {
//                action("a","b");
//            }
            
//            //if (BeforeAction != null)
//            //{
//            //    BeforeAction(this,EventArgs.Empty);
//            //}
//        }
//    }

//    public delegate void Doo(object sender, EventArgs e);
//}
