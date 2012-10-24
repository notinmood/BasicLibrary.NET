using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Web;

namespace HiLand.Utility.Web
{
    /// <summary>
    /// Cookies基类。将需要保存Cookies的数据类此类派生，可以将强类型对象在Cookies中的保存和读取。
    /// </summary>
    /// <remarks>
    /// 2009.8.6    ziqiu.zhang     created
    /// 保存数据的派生类中的属性，最好仅使用string，int类型；复杂类型不推荐使用（有可能会出现问题）
    /// </remarks>

    [System.Serializable]
    public class CookieInfo
    {

        #region ==================== Constructed Method ====================
        /// <summary>
        /// 构造函数
        /// </summary>
        public CookieInfo()
        {

        }
        #endregion


        #region ==================== Public Method ====================
        /// <summary>
        /// 得到当前Cookies的过期时间
        /// </summary>
        /// <returns>过期时间</returns>
        public DateTime GetExpiresTime()
        {
            string cookieName = GetType().ToString();
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                return HttpContext.Current.Request.Cookies[cookieName].Expires;
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// 保存Cookies，过期时间为浏览器关闭则失效。
        /// </summary>
        /// <param name="expiresTime">Cookies过期事件</param>
        /// <returns>是否保存成功</returns>
        public bool Save()
        {
            return this.Save(DateTime.MinValue);
        }

        /// <summary>
        /// 保存Cookies，需要指定过期时间。
        /// </summary>
        /// <param name="expiresTime">Cookies过期事件</param>
        /// <returns>是否保存成功</returns>
        public bool Save(DateTime expiresTime)
        {
            string CookieName = GetType().ToString();
            HttpCookie SessionCookie = null;

            //对 SessionId 进行备份.
            if (HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                string SesssionId = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value.ToString();
                SessionCookie = new HttpCookie("ASP.NET_SessionId");
                SessionCookie.Value = SesssionId;

            }
            //设定cookie 过期时间.
            DateTime dtExpiry = expiresTime;
            HttpContext.Current.Response.Cookies[CookieName].Expires = dtExpiry;

            //设定cookie 域名.
            string domain = string.Empty;
            if (HttpContext.Current.Request.Params["HTTP_HOST"] != null)
            {
                //domain = "www.elong.com";
                domain = HttpContext.Current.Request.Params["HTTP_HOST"].ToString();
            }


            //TODO:xieran 目前这个做法是不对的，例如对于 ***.com.cn,就会出现问题。需要重新考虑如何通过当前域名获取主域名的事情。
            ////如果是www.elong.com或多级域名,需要转化为elong.com
            //if (domain.IndexOf(".") > -1)
            //{
            //    string[] temp = domain.Split('.');

            //    if (temp.Length >= 3)
            //    {
            //        domain = temp[temp.Length - 2].Trim() + "." + temp[temp.Length - 1].Trim();
            //    }

            //    HttpContext.Current.Response.Cookies[CookieName].Domain = domain;
            //}


            //把类的属性, 写入Cookie.
            PropertyInfo[] Propertys = GetType().GetProperties();

            foreach (PropertyInfo pi in Propertys)
            {
                object oj = pi.GetValue(this, null);
                Type type = pi.PropertyType;
                string valueStr = string.Empty;

                if (oj != null && oj.ToString() != string.Empty)
                {
                    if (type == Type.GetType("System.DateTime"))
                    {
                        valueStr = ((DateTime)oj).ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    }
                    else
                    {
                        valueStr = oj.ToString();
                    }

                    HttpContext.Current.Response.Cookies[CookieName][pi.Name] = HttpUtility.UrlEncode(valueStr);
                }

            }

            //如果cookie总数超过20 个, 重写ASP.NET_SessionId, 以防Session 丢失.
            if (HttpContext.Current.Request.Cookies.Count > 20 && SessionCookie != null)
            {
                if (SessionCookie.Value != string.Empty)
                {
                    HttpContext.Current.Response.Cookies.Remove("ASP.NET_SessionId");
                    HttpContext.Current.Response.Cookies.Add(SessionCookie);
                }
            }

            return true;
        }

        /// <summary>
        /// 找回Cookie值
        /// </summary>
        public void Load()
        {
            string cookieValue = string.Empty;
            string CookieName = GetType().ToString();

            //通过遍历属性, 从cookie 中找回值, 回写到属性.
            PropertyInfo[] Propertys = GetType().GetProperties();

            foreach (PropertyInfo pi in Propertys)
            {
                try
                {
                    cookieValue = HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[CookieName][pi.Name].ToString());
                }
                catch
                {
                    cookieValue = string.Empty;
                }

                if (pi.CanWrite && cookieValue != null && cookieValue != string.Empty)
                {
                    try
                    {
                        object obb = cookieValue;
                        Type type = pi.PropertyType;

                        obb = Convert.ChangeType(obb, type);
                        pi.SetValue(this, obb, null);
                    }
                    catch { }
                }
            }

        }

        /// <summary>
        /// 构造Cookie对象并载入其值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Load<T>() where T : CookieInfo,new()
        {
            T t = new T();
            t.Load();
            return t;
        }
        #endregion

    }
}
