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
    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookieName">cookie的名称</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = RequestHelper.CurrentRequest.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-1);
                RequestHelper.CurrentRequest.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookieName">cookie的名称</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            HttpCookie cookie = RequestHelper.CurrentRequest.Cookies[cookieName];
            string value = string.Empty;
            if (cookie != null)
            {
                value = cookie.Value;
            }
            return value;
        }

        /// <summary>
        /// 添加一个Cookie（临时cookie）
        /// </summary>
        /// <param name="cookieName">cookie的名称</param>
        /// <param name="cookieValue">cookie的值</param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            SetCookie(cookieName, cookieValue, DateTime.Now.AddDays(-1.0));
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookieName">cookie的名称</param>
        /// <param name="cookieValue">cookie的值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookieName, string cookieValue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = expires
            };
            ResponseHelper.CurrentResponse.Cookies.Add(cookie);
        }
        
        /// <summary>
        /// 获取为每个一个客户端分配的一个Guid
        /// </summary>
        /// <returns></returns>
        public static Guid ClientID()
        {
            string cookieName = "Hiland.ClientID.20120126";
            string clientID = GetCookie(cookieName);
            
            if (string.IsNullOrEmpty(clientID))
            {
                Guid cookieValue = GuidHelper.NewGuid();
                SetCookie(cookieName, cookieValue.ToString());
                return cookieValue;
            }
            else
            {
                return new Guid(clientID);
            }
        }
    }
}
