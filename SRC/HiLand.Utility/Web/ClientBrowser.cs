using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HiLand.Utility.Data;

namespace HiLand.Utility.Web
{
    /// <summary>
    /// 客户端浏览器
    /// </summary>
    public class ClientBrowser
    {
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetClientIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || StringHelper.IsIP(result)==false)
            {
                return "0.0.0.0";
            }
            return result;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowser()
        {
            string[] browserNameArray = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string currentBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < browserNameArray.Length; i++)
            {
                if (currentBrowser.IndexOf(browserNameArray[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
