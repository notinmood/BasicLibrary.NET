using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using HiLand.Utility.Data;

namespace HiLand.Utility.Web
{
    /// <summary>
    /// 对Url的参数进行拼接
    /// </summary>
    public class UrlInfo
    {
        private string baseUrl = string.Empty;
        private Dictionary<string,string> urlParamDic= new Dictionary<string,string>();

        /// <summary>
        /// 是否仅用`&`进行连接
        /// </summary>
        private bool isOnlyUseAndConcat = false;

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="inputUrl">传入的地址</param>
        public UrlInfo(string inputUrl)
        {
            NameValueCollection tempNVC = new NameValueCollection();
            RequestHelper.ParseUrl(inputUrl, out baseUrl, out tempNVC);
            if (tempNVC != null)
            {
                foreach (string key in tempNVC.Keys)
                {
                    urlParamDic[key] = tempNVC[key];
                }
            }
        }

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="isOnlyUseAndConcat">是否仅用"&"进行连接</param>
        public UrlInfo(bool isOnlyUseAndConcat)
        {
            this.isOnlyUseAndConcat = isOnlyUseAndConcat;
        }

        /// <summary>
        /// 参数拼接
        /// </summary>
        /// <param name="key">参数key</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public UrlInfo Concat(string key, string value)
        {
            urlParamDic[key] = value;
            return this;
        }

        /// <summary>
        /// 获取拼接后的地址
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// 获取拼接后的地址
        /// </summary>
        /// <param name="isAppendRandom"></param>
        /// <returns></returns>
        public string ToString(bool isAppendRandom)
        {
            string result = baseUrl;

            if (isAppendRandom == true)
            {
                this.Concat("r", RandomHelper.GetRandomString(Enums.CharCategories.NumberAndChar, 6));
            }

            foreach (string key in urlParamDic.Keys)
            {
                if (result.IndexOf("?") > -1 || isOnlyUseAndConcat == true)
                {
                    result += string.Format("&{0}={1}", key, Uri.EscapeDataString(urlParamDic[key]));
                }
                else
                {
                    result += string.Format("?{0}={1}", key, Uri.EscapeDataString(urlParamDic[key]));
                }
            }

            return result;
        }

        /// <summary>
        /// 静态实例方法
        /// </summary>
        /// <param name="basicUrl">url基地址</param>
        /// <returns></returns>
        public static UrlInfo New(string basicUrl)
        {
            return new UrlInfo(basicUrl);
        }

        /// <summary>
        /// 静态实例方法
        /// </summary>
        /// <param name="isOnlyUseAndConcat">是否仅用"&"进行连接</param>
        /// <returns></returns>
        public static UrlInfo New(bool isOnlyUseAndConcat)
        {
            return new UrlInfo(isOnlyUseAndConcat);
        }
    }
}
