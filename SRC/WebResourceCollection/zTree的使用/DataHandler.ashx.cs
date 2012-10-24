using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebResourceCollection.zTree的使用
{
    /// <summary>
    /// Summary description for DataHandler
    /// </summary>
    public class DataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string pId = "0";
            string pName = "";
            string pLevel = "";
            string pCheck = "";

            if (context.Request["id"] != null)
            {
                pId = context.Request["id"];
            }

            if (context.Request["lv"] != null)
            {
                pLevel = context.Request["lv"];
            }

            if (context.Request["n"] != null)
            {
                pName = context.Request["n"];
            }

            if (context.Request["chk"] != null)
            {
                pCheck = context.Request["chk"];
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

            for (int i = 1; i < 5; i++)
            {
                string nId = pId + "." + i;
                string nName = pName + ".n." + i;
                bool isParent = false;
                if (Convert.ToInt32(pLevel) < 2 && i % 2 != 0)
                {
                    isParent = true;
                }

                string temp = "";
                temp = string.Format("id:'{0}',name:'{1}',isParent:'{2}'", nId, nName, isParent);
                temp = "{" + temp + "}";

                if (i < 4)
                {
                    result += temp + ",";
                }
                else
                {
                    result += temp;
                }
            }

            result = string.Format("[{0}]",result);

            context.Response.Write(result);
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