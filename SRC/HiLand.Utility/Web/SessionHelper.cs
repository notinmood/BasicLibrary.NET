using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HiLand.Utility.Data;

namespace HiLand.Utility.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// 获取指定Session值
        /// </summary>
        /// <param name="sessionName">Session的名称</param>
        /// <returns></returns>
        public static T GetSession<T>(string sessionName)
        {
            T sessionValue = Converter.ChangeType(HttpContext.Current.Session[sessionName],default(T));
            return sessionValue;
        }

        /// <summary>
        /// 获取指定Session值
        /// </summary>
        /// <param name="sessionName">Session的名称</param>
        /// <returns></returns>
        public static string GetSession(string sessionName)
        {
            return GetSession<string>(sessionName);
        }

        /// <summary>
        /// 给指定的名称设置Session的值
        /// </summary>
        /// <param name="sessionName"></param>
        /// <param name="sessionValue"></param>
        public static void SetSession<T>(string sessionName,T sessionValue)
        {
            HttpContext.Current.Session[sessionName] = sessionValue;
        }

        /// <summary>
        /// 获取当前请求用户的SessionID
        /// </summary>
        /// <returns></returns>
        public static string GetSessionID()
        {
            //为了防止session内没有任何信息，其每次请求都重新生成SessionID,此处明确给session添加一条信息。
            //具体请查看 http://weibo.com/1087372422/y1E6Akdtz
            SetSession("qingdao.hiland.key2000", "qingdao.hiland.value");
            return HttpContext.Current.Session.SessionID;
        }
    }
}
