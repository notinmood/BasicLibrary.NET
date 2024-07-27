//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Web;

//namespace Hiland.BasicLibrary.Web
//{
//    /// <summary>
//    /// 客户端页面
//    /// </summary>
//    public static class ClientPage
//    {
//        /// <summary>
//        /// 清空客户端页面缓存
//        /// </summary>
//        public static void ClearClientPageCache()
//        {
//            HttpContext.Current.Response.Buffer = true;
//            HttpContext.Current.Response.Expires = 0;
//            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
//            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
//            HttpContext.Current.Response.AddHeader("cache-control", "private");
//            HttpContext.Current.Response.CacheControl = "no-cache";
//        }

//    }
//}
