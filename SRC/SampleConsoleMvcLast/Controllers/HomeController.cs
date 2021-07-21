using HiLand.General.BLL;
using HiLand.General.Entity;
using SampleConsoleMvcLast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleConsoleMvcLast.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult bar() {
            return View();
        }

        public ActionResult About()
        {
            //MyCookie cookie = MyCookie.Load<MyCookie>();
            var where = string.Format("loanid= 16132");
            var list = LoanBasicBLL.Instance.GetList(where);
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}