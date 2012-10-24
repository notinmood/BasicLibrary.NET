using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleMvcApplication.Areas.Manage.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Manage/Main/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bar()
        {
            return View();
        }

        public ActionResult MVCHelperTest()
        {
            return View();
        }

    }
}
