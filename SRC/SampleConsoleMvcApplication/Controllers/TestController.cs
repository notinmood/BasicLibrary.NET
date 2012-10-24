using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiLand.Utility.Data;
using HiLand.Utility.Entity;
using HiLand.Utility.Web;
using HiLand.Utility4.MVC;
using HiLand.Utility4.MVC.Controls;
using HiLand.Utility4.MVC.Data;

namespace SampleMvcApplication.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            string data = "name:xieran||age:32||id:3702021979****";
            Dictionary<string,string> result= StringHelper.SplitToDictionary(data,":","||");

            MVCHelper.SetCustomData("myData","sssssssssss");
            MVCHelper.SetCustomData("myInt", 6666);
            MVCHelper.SetCustomData<Type>("myType", typeof(int));

            this.PassParam<string>("myBag","sssssssssss");

            return View();
        }

        [HttpPost]
        public ActionResult Index(bool isOnlyPlaceHolder= true)
        {
            string queryCondtion = QueryControlHelper.GetQueryCondition("myQuery");


            this.ViewBag.queryCondtion = queryCondtion;
            return View();
        }

        public ActionResult PassValueTest()
        {
            this.PassParam<int>("myParam",6);
            return View(9);
        }

        public ActionResult AutoCompleteTest()
        {
            return View();
        }

        public ActionResult AutocompleteData()
        {
            string query = RequestHelper.GetValue("term");
            List<AutoCompleteEntity> itemList = new List<AutoCompleteEntity>();
            for (int i = 0; i < 3; i++)
            {
                AutoCompleteEntity item = new AutoCompleteEntity();
                item.details = "nothing"+ query+ i;
                item.key = "ID" + query + i;
                item.label = "lable"+ query+ i+"(qingdao)";
                item.value = query + i;

                itemList.Add(item);
            }

            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RouteValuesPass()
        {
            List<string> list = new List<string>();
            list.Add("ssss");
            list.Add("pppp");

            this.ViewBag.sss = list;
            this.TempData.Add("myKey",list);
            return RedirectToActionPermanent("RouteValueGet", list );
        }

        public ActionResult RouteValueGet(List<string> myParam)
        {
            //List<string> list = this.Request.RequestContext.RouteData.

            List<string> list= this.ViewBag.sss;
            object myKey = this.TempData["myKey"];
            int i = 9;
            return View();
        }
    }
}
