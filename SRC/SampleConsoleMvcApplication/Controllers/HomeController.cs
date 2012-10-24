using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiLand.Utility4.MVC;

namespace SampleMvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }


        //[LogMessage(Doo)]
        public ActionResult About()
        {
            return View();
        }

        private void Doo(string x, string y)
        { 
            //
        }

        public ActionResult TreeTest()
        {
            return View();
        }

        public ActionResult TreeData()
        {
            string pId = "0";
            string pName = "";
            string pLevel = "";
            string pCheck = "";

            if (HttpContext.Request["id"] != null)
            {
                pId = HttpContext.Request["id"];
            }

            if (HttpContext.Request["lv"] != null)
            {
                pLevel = HttpContext.Request["lv"];
            }

            if (HttpContext.Request["n"] != null)
            {
                pName = HttpContext.Request["n"];
            }

            if (HttpContext.Request["chk"] != null)
            {
                pCheck = HttpContext.Request["chk"];
            }


            if (pId == null || pId == "")
            {
                pId = "0";
            }

            if (pLevel == null || pLevel == "")
            {
                pLevel = "0";
            }

            if (pName == null)
            {
                pName = "";
            }
            else
            {
                pName = pName + ".";
            }

            string result = string.Empty;
            List<object> stringList = new List<object>();

            for (int i = 1; i < 5; i++)
            {
                string nId = pId + "." + i;
                string nName = pName + ".n." + i;
                bool isParent = false;
                if (Convert.ToInt32(pLevel) < 2 && i % 2 != 0)
                {
                    isParent = true;
                }

                var obj = new { id = nId, name = nName, isParent = isParent };

                stringList.Add(obj);
            }

            return Json(stringList,JsonRequestBehavior.AllowGet);
        }

        public ActionResult MVCControl()
        {
            return View();
        }
    }
}
