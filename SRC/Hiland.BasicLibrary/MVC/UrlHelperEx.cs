//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;
//using System.Web;

//namespace HiLand.Utility4.MVC
//{
//    /// <summary>
//    /// UrlHelper扩展类
//    /// </summary>
//    public static class UrlHelperEx
//    {
//        /// <summary>
//        /// 在页面内生成JavaScript引用
//        /// </summary>
//        /// <param name="urlHelper"></param>
//        /// <param name="scriptVirtualPath"></param>
//        /// <returns></returns>
//        public static MvcHtmlString JavaScript(this UrlHelper urlHelper, string scriptVirtualPath)
//        {
//            var builder = new TagBuilder("script");
//            builder.Attributes["src"] = urlHelper.Content(scriptVirtualPath);
//            builder.Attributes["type"] = "text/javascript";

//            string resultString = builder.ToString();
//            return MvcHtmlString.Create(resultString);
//        }

//        /// <summary>
//        /// 在页面内生成CSS引用
//        /// </summary>
//        /// <param name="urlHelper"></param>
//        /// <param name="cssVirtualPath"></param>
//        /// <returns></returns>
//        public static MvcHtmlString CSS(this UrlHelper urlHelper, string cssVirtualPath)
//        {
//            var builder = new TagBuilder("link");
//            builder.Attributes["href"] = urlHelper.Content(cssVirtualPath);
//            builder.Attributes["type"] = "text/css";
//            builder.Attributes["rel"] = "stylesheet";

//            string resultString = builder.ToString();
//            return MvcHtmlString.Create(resultString);
//        }

//        /// <summary>
//        /// 从当前请求信息中构建的Url帮助器
//        /// </summary>
//        public static UrlHelper UrlHelper
//        {
//            get
//            {
//                return new UrlHelper(HttpContext.Current.Request.RequestContext);
//            }
//        }
//    }
//}
