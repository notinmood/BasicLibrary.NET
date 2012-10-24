using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HiLand.Utility.Data;
using HiLand.Utility.Entity;

namespace WebResourceCollection.Test.jquery.cascadingDropDown测试
{
    /// <summary>
    /// Summary description for ContentHandler
    /// </summary>
    public class ContentHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Params["level1"] != null)
            {
                CascadingCollection coll = new CascadingCollection();
                coll.AddItem(context.Request.Params["level1"]+"Item11", "item11");
                coll.AddItem(context.Request.Params["level1"]+"Item12", "item12");
                coll.AddItem(context.Request.Params["level1"]+"Item13", "item13");

                string result = coll.GetJSON();
                context.Response.ContentType = "text/plain";
                context.Response.Write(result);
            }

            if (context.Request.Params["level2"] != null)
            {
                CascadingCollection coll = new CascadingCollection();
                coll.AddItem(context.Request.Params["level2"] + "Item11", "item11");
                coll.AddItem(context.Request.Params["level2"] + "Item12", "item12");
                coll.AddItem(context.Request.Params["level2"] + "Item13", "item13");

                string result = coll.GetJSON();
                context.Response.ContentType = "text/plain";
                context.Response.Write(result);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}