using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;
using HiLand.Utility.Data;
using HiLand.Utility.Enums;
using HiLand.Utility.IO;

namespace HiLand.Utility.Web
{
    public class RequestHelper
    {
        /// <summary>
        /// 当前的请求信息
        /// </summary>
        public static HttpRequest CurrentRequest
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        /// <summary>
        /// 获取当前请求的完整url
        /// </summary>
        public static string CurrentFullUrl
        {
            get
            {
                return CurrentRequest.RawUrl;
            }
        }

        /// <summary>
        /// 添加或者修改请求信息中的查询字符串的值
        /// </summary>
        /// <param name="request"></param>
        /// <param name="queryStringkey">要修改的查询查询字符串的键</param>
        /// <param name="queryStringValue">要修改的查询查询字符串的值(如果其为string.Emtpy或者null,那么表示从查询字符串中移除这个选项)</param>
        /// <returns></returns>
        public static string AddOrModifyQueryString(HttpRequest request, string queryStringkey, string queryStringValue)
        {
            if (string.IsNullOrEmpty(queryStringkey))
            {
                return request.Url.OriginalString;
            }

            string originalUrlWithoutQuery = string.Empty;
            string queryInfo = request.Url.Query;
            if (string.IsNullOrEmpty(queryInfo) == true)
            {
                originalUrlWithoutQuery = request.Url.OriginalString;
            }
            else
            {
                originalUrlWithoutQuery = request.Url.OriginalString.Substring(0, request.Url.OriginalString.IndexOf("?"));
            }

            string result = string.Empty;
            NameValueCollection nvc = request.QueryString;
            if (nvc == null)
            {
                result = string.Format("{0}?{1}={2}", originalUrlWithoutQuery, queryStringkey, queryStringValue);
            }
            else
            {
                Dictionary<string, string> queryStringDic = new Dictionary<string, string>();
                for (int i = 0; i < nvc.Count; i++)
                {
                    if (queryStringDic.ContainsKey(nvc.AllKeys[i]) == false)
                    {
                        queryStringDic.Add(nvc.AllKeys[i], nvc[i]);
                    }
                }

                if (string.IsNullOrEmpty(queryStringValue))
                {
                    if (queryStringDic.ContainsKey(queryStringkey))
                    {
                        queryStringDic.Remove(queryStringkey);
                    }
                }
                else
                {
                    if (queryStringDic.ContainsKey(queryStringkey))
                    {
                        queryStringDic[queryStringkey] = queryStringValue;
                    }
                    else
                    {
                        queryStringDic.Add(queryStringkey, queryStringValue);
                    }
                }

                string queryStrings = string.Empty;
                foreach (KeyValuePair<string, string> kvp in queryStringDic)
                {
                    queryStrings += string.Format("&{0}={1}", kvp.Key, kvp.Value);
                }

                if (queryStrings.StartsWith("&"))
                {
                    queryStrings = "?" + queryStrings.Substring(1);
                }

                result = originalUrlWithoutQuery + queryStrings;
            }

            return WebHelper.Server.UrlPathEncode(result);
        }

        /// <summary>
        /// 获取当前请求的不带请求头(例如"http://"等)的原始地址
        /// </summary>
        /// <returns></returns>
        public static string GetOriginalUrlWithoutSchemeHeader()
        {
            string originalUrl = CurrentRequest.Url.OriginalString;
            return GetOriginalUrlWithoutSchemeHeader(originalUrl);
        }

        /// <summary>
        /// 获取不带请求头(例如"http://"等)的原始地址
        /// </summary>
        /// <param name="originalUrl">完整的请求地址（即HttpContext.Current.Request.Url.OriginalString）</param>
        /// <returns></returns>
        public static string GetOriginalUrlWithoutSchemeHeader(string originalUrl)
        {
            string result = originalUrl;
            int headerSeperatorPos = originalUrl.IndexOf("://");
            if (headerSeperatorPos >= 0)
            {
                result = originalUrl.Substring(headerSeperatorPos + 3);
            }

            return result;
        }

        //TODO:xieran20121026 考虑对返回值加入HttpUtility.UrlDecode();去除编码
        /// <summary>
        /// 获取请求信息的参数值
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns></returns>
        /// <remarks>
        /// 其会从多种集合里面获取传递的参数信息：From，Cookie，Session，QueryString，ServerVariables等
        /// </remarks>
        public static string GetValue(string paramName)
        {
            string paramValue = CurrentRequest.Params[paramName];
            if (paramValue == null)
            {
                paramValue = CurrentRequest.Params["amp;" + paramName];
            }

            if (paramValue == null)
            {
                paramValue = string.Empty;
            }

            return paramValue;
        }

        /// <summary>
        /// 获取请求信息的参数值
        /// </summary>
        /// <typeparam name="T">参数的类型</typeparam>
        /// <param name="paramName">参数名称</param>
        /// <returns></returns>
        /// <remarks>
        /// 其会从多种集合里面获取传递的参数信息：From，Cookie，Session，QueryString，ServerVariables等
        /// </remarks>
        public static T GetValue<T>(string paramName)
        {
            string result = GetValue(paramName);
            if (string.IsNullOrEmpty(result))
            {
                return default(T);
            }
            else
            {
                return Converter.ChangeType<T>(result);
            }
        }

        /// <summary>
        /// 获取请求信息的参数值
        /// </summary>
        /// <typeparam name="T">参数的类型</typeparam>
        /// <param name="paramName">参数名称</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>
        /// 
        /// </returns>
        /// <remarks>
        /// 其会从多种集合里面获取传递的参数信息：From，Cookie，Session，QueryString，ServerVariables等
        /// </remarks>
        public static T GetValue<T>(string paramName, T defaultValue)
        {
            string result = GetValue(paramName);
            if (string.IsNullOrEmpty(result))
            {
                return defaultValue;
            }
            else
            {
                return Converter.ChangeType<T>(result);
            }
        }

        /// <summary>
        /// 获取请求信息的参数值
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns></returns>
        /// <remarks>
        /// 其会从指定的数据源集合里面获取传递的参数信息，数据源包括：From，Cookie，Session，QueryString，ServerVariables等
        /// </remarks>
        public static string GetValue(PassingParamValueSourceTypes sourceType, string paramName, string defaultValue)
        {
            string paramValue = GetValue(sourceType, paramName);

            if (paramValue == null)
            {
                paramValue = GetValue(sourceType, "amp;" + paramName);
            }

            if (paramValue == null)
            {
                paramValue = defaultValue;
            }

            return paramValue;
        }

        /// <summary>
        /// 获取请求信息的参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceType"></param>
        /// <param name="paramName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <remarks>
        /// 其会从指定的数据源集合里面获取传递的参数信息，数据源包括：From，Cookie，Session，QueryString，ServerVariables等
        /// </remarks>
        public static T GetValue<T>(PassingParamValueSourceTypes sourceType, string paramName, T defaultValue)
        {
            string result = GetValue(sourceType, paramName, string.Empty);
            if (string.IsNullOrEmpty(result))
            {
                return defaultValue;
            }
            else
            {
                return Converter.ChangeType<T>(result);
            }
        }

        /// <summary>
        /// 获取请求信息的参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceType"></param>
        /// <param name="paramName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <remarks>
        /// 其会从指定的数据源集合里面获取传递的参数信息，数据源包括：From，Cookie，Session，QueryString，ServerVariables等
        /// </remarks>
        public static T GetValue<T>(PassingParamValueSourceTypes sourceType, string paramName)
        {
            return GetValue<T>(sourceType, paramName, default(T));
        }

        //TODO:xieran20121015 其他各种数据传递方式需要实现
        private static string GetValue(PassingParamValueSourceTypes sourceType, string paramName)
        {
            string paramValue = string.Empty;

            switch (sourceType)
            {
                case PassingParamValueSourceTypes.Form:
                    paramValue = CurrentRequest.Form[paramName];
                    break;
                case PassingParamValueSourceTypes.QueryString:
                    paramValue = CurrentRequest.QueryString[paramName];
                    break;
                case PassingParamValueSourceTypes.Cookie:
                    HttpCookie temp = CurrentRequest.Cookies[paramName];
                    if (temp != null)
                    {
                        paramValue = temp.Value;
                    }
                    break;

                case PassingParamValueSourceTypes.ServerVariables:
                    paramName = CurrentRequest.ServerVariables[paramName];
                    break;
                //case PassingParamValueSourceTypes.Session:
                //    paramName = CurrentRequest.get[paramName];
                //    break;
                //case PassingParamValueSourceTypes.Application:
                //    paramName = CurrentRequest.ServerVariables[paramName];
                //    break;   
                //case PassingParamValueSourceTypes.ViewState:
                //    paramName = CurrentRequest.QueryString[paramName];
                //    break;
                //case PassingParamValueSourceTypes.Database:
                //    paramName = CurrentRequest.QueryString[paramName];
                //    break;
                //case PassingParamValueSourceTypes.Other:
                //    break;
                default:
                    paramValue = null;
                    break;
            }
            return paramValue;
        }

        /// <summary>
        /// 判断当前是否为Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPostRequest
        {
            get { return CurrentRequest.HttpMethod.ToLower().Equals("post"); }
        }

        /// <summary>
        /// 判断当前是否为Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGetRequest
        {
            get { return CurrentRequest.HttpMethod.ToLower().Equals("get"); }
        }

        /// <summary>
        /// 获取当前应用程序的跟路径
        /// </summary>
        public static string ApplicationRootPath
        {
            get
            {
                return CurrentRequest.ApplicationPath;
            }
        }

        /// <summary>
        /// 解析页面能够使用的url（相当于Page.ResolveUrl）
        /// </summary>
        /// <param name="originalUrl"></param>
        /// <returns></returns>
        public static string ResolveUrl(string originalUrl)
        {
            if (originalUrl == null)
            {
                return string.Empty;
            }

            // *** Absolute path - just return
            if (originalUrl.Contains("://") == true)
            {
                return originalUrl;
            }

            // *** Fix up image path for ~ root app dir directory
            if (originalUrl.StartsWith("~"))
            {
                string newUrl = "";

                if (HttpContext.Current != null)
                {
                    newUrl = PathHelper.CombineForVirtual(ApplicationRootPath, originalUrl.Substring(1)); //HttpContext.Current.Request.ApplicationPath + originalUrl.Substring(1).Replace("//", "/");
                }
                else
                {
                    // *** Not context: assume current directory is the base directory
                    throw new ArgumentException("Invalid URL: Relative URL not allowed.");
                }

                // *** Just to be sure fix up any double slashes
                return newUrl;
            }

            return originalUrl;
        }

        /// <summary>
        /// 分析 url 字符串中的参数信息
        /// </summary>
        /// <param name="url">输入的 URL</param>
        /// <param name="baseUrl">输出 URL 的基础部分</param>
        /// <param name="nvc">输出分析后得到的 (参数名,参数值) 的集合</param>
        public static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            nvc = new NameValueCollection();
            baseUrl = "";

            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            int questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }

            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
            {
                return;
            }

            string ps = url.Substring(questionMarkIndex + 1);

            // 开始分析参数对    
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }
    }
}
