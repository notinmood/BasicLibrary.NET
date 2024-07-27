//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Web;

//namespace Hiland.BasicLibrary.Web
//{
//    /// <summary>
//    /// 响应对象帮助器
//    /// </summary>
//    public static class ResponseHelper
//    {
//        /// <summary>
//        /// 当前的响应信息
//        /// </summary>
//        public static HttpResponse CurrentResponse
//        {
//            get 
//            {
//                return  HttpContext.Current.Response;
//            }
//        }

//        /// <summary>
//        /// 禁用客户端缓存
//        /// </summary>
//        public static void SetNoCache()
//        {
//            SetNoCache(CurrentResponse);
//        }

//        /// <summary>
//        /// 禁用客户端缓存
//        /// </summary>
//        public static void SetNoCache(HttpResponse response)
//        {
//            response.Buffer = true;
//            response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
//            response.Expires = 0;
//            response.CacheControl = "no-cache";
//            response.AddHeader("Cache-Control", "no-cache");
//            response.AddHeader("Pragma", "no-cache");
            
//            response.Cache.SetNoStore();
//            response.Cache.SetExpires(DateTime.MinValue);
//            response.Cache.SetCacheability(HttpCacheability.NoCache);
//            response.Cache.SetValidUntilExpires(false);
//        }

//        /// <summary>
//        /// 跳转页面，同时将跳转前的页面作为返回参数进行传递
//        /// </summary>
//        /// <param name="targetUrl">跳转的目标页面</param>
//        public static void RedirectPage(string targetUrl)
//        {
//            string currentVirtualPath = HttpContext.Current.Request.Url.OriginalString.ToLower();
//            currentVirtualPath = HttpContext.Current.Server.UrlEncode(currentVirtualPath);
//            if (string.IsNullOrEmpty(currentVirtualPath) == false)
//            {
//                targetUrl += "?returnurl=" + currentVirtualPath;
//            }
//            CurrentResponse.Redirect(targetUrl);
//        }
//    }
//}
