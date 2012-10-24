using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// Guid操作辅助类
    /// </summary>
    public static class GuidHelper
    {
        private static DateTime orginalDate = new DateTime(2000, 1, 1);
        
        /// <summary>
        /// 空Guid
        /// </summary>
        public static Guid Empty
        {
            get { return Guid.Empty; }
        }

        /// <summary>
        /// 空Guid字符串
        /// </summary>
        /// <remarks>
        /// 此处之所以声明为常量，是因为其可以在方法参数的缺省值中使用
        /// </remarks>
        public const string EmptyString = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// 系统保留Guid字符串（使用场景：比如系统发送的自动消息，发送人就可以指定为此值）
        /// </summary>
        public static string SystemKeyString = "99999999-9999-9999-9999-999999999999";

        /// <summary>
        /// 系统保留Guid（使用场景：比如系统发送的自动消息，发送人就可以指定为此值）
        /// </summary>
        public static Guid SystemKey = new Guid(SystemKeyString);
        
        /// <summary>
        /// 生成按时间排序后的Guid
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            TimeSpan timeSpan = DateTime.Now - orginalDate;
            double totalMilliseconds = timeSpan.TotalMilliseconds;
            string totalMillisecondsHex = Convert.ToString((long)totalMilliseconds,16);
            string guidStartString = totalMillisecondsHex.PadLeft(11,'0');
            string guidString = (guidStartString + Guid.NewGuid().ToString().Replace("-",string.Empty)).Substring(0,32);
            return new Guid(guidString);
        }

        /// <summary>
        /// 生成按时间排序后的Guid字符串
        /// </summary>
        /// <returns></returns>
        public static string NewGuidString()
        {
            return NewGuid().ToString();
        }

        /// <summary>
        /// 将字符串转换成Guid
        /// </summary>
        /// <param name="data">待转换的字符串</param>
        /// <returns></returns>
        public static Guid TryConvert(string data)
        {
            return TryConvert(data,Guid.Empty);
        }

        /// <summary>
        /// 将字符串转换成Guid
        /// </summary>
        /// <param name="value">待转换的字符串</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns></returns>
        public static Guid TryConvert(string data, Guid defaultValue)
        {
            Guid result = Guid.Empty;
            try
            {
                result = new Guid(data);
            }
            catch
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// 判断字符串是否为空的guid内容，或者为无效的guid值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsInvalidOrEmpty(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return true;
            }

            if (data == EmptyString)
            {
                return true;
            }

            if (TryConvert(data) == Guid.Empty)
            {
                return true;
            }

            return false;
        }
    }
}
