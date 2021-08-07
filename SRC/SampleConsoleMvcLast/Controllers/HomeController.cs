using HiLand.General.BLL;
using HiLand.General.Entity;
using HiLand.Utility.Data;
using HiLand.Utility.Setting;
using HiLand.Utility.Web;
using HiLand.Utility4.MVC.Engine;
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

        public ActionResult bar()
        {
            var connectionString = Config.GetConnectionString();
            this.ViewData["displayData"] = connectionString;

            return View();
        }

        public ActionResult About()
        {
            //MyCookie cookie = MyCookie.Load<MyCookie>();

            //var where = string.Format("loanid= 16132");
            //var list = LoanBasicBLL.Instance.GetList(where);

            //var entity= LoanBasicBLL.Instance.Get("9947DF4B-A1D1-4DA1-9DA8-CC9F8959ABC1");
            //entity.LoanPurpose = "my home!";
            //entity.LoanAmount = 3999;
            //var isOk= LoanBasicBLL.Instance.Update(entity);

            //LoanBasicEntity entity = new LoanBasicEntity();
            //entity.LoanGuid = GuidHelper.NewGuid();
            //entity.LoanDate = DateTime.Now;
            //entity.LoanInterest = 0.05M;
            //entity.LoanOwnerKey = "ssss";
            //entity.LoanOwnerType = HiLand.General.Enums.LoanOwnerTypes.Person;
            //entity.LoanPurpose = "okkk!";
            //var isOk=  LoanBasicBLL.Instance.Create(entity);

            //JsonHelper.

            var ss= ServerHelper.IsRunning("http://www.veryhuo.com");
            var pp = ServerHelper.IsRunning("http://www.baidu.com"); 

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