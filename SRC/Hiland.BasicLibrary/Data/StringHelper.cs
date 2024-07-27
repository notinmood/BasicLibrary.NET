using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Hiland.BasicLibrary.Data
{
    /// <summary>
    /// 字符串操作辅助类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 空字符串
        /// </summary>
        /// <remarks>
        /// 此处之所以声明为常量，是因为其可以在方法参数的缺省值中使用
        /// 如果是在一般情况下可以直接使用String.Empty
        /// </remarks>
        public const string Empty = "";

        #region 字符拆分与截取
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="data"></param>
        /// <param name="remainCharCount"></param>
        /// <returns></returns>
        public static string SubString(string data, int remainCharCount)
        {
            return SubString(data, remainCharCount, "...");
        }

        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="data"></param>
        /// <param name="remainCharCount"></param>
        /// <param name="postFixAdded">在截取后的字符串后加入后缀</param>
        /// <returns></returns>
        public static string SubString(string data, int remainCharCount, string postFixAdded)
        {
            string result = string.Empty;
            if (data.Length > remainCharCount)
            {
                result = data.Substring(0, remainCharCount);
                if (string.IsNullOrEmpty(postFixAdded) == false)
                {
                    result += postFixAdded;
                }
            }
            else
            {
                result = data;
            }

            return result;
        }

        /// <summary>
        /// 给字符串添加固定数目的空格字符作为前缀
        /// </summary>
        /// <param name="data"></param>
        /// <param name="prefixCharCount"></param>
        /// <returns></returns>
        public static string PrefixString(string data, int prefixCharCount)
        {
            return PrefixString(data, prefixCharCount, " ");
        }

        /// <summary>
        /// 给字符串添加固定数目的指定字符串作为前缀
        /// </summary>
        /// <param name="data"></param>
        /// <param name="prefixCharCount"></param>
        /// <param name="prefexer"></param>
        /// <returns></returns>
        public static string PrefixString(string data, int prefixCharCount, string prefexer)
        {
            string result = string.Empty;
            if (prefixCharCount >= 0)
            {
                for (int i = 0; i < prefixCharCount; i++)
                {
                    result += prefexer;
                }
            }

            return result + data;
        }

        /// <summary>
        /// 把字符串的第一个字符变为大写
        /// </summary>
        /// <param name="data">要转换的字符串</param>
        /// <returns></returns>
        public static string FirstCharToUpper(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return data;
            }

            if (data.Length > 1)
            {
                return data.Substring(0, 1).ToUpper() + data.Substring(1);
            }
            else
            {
                return data.ToUpper();
            }
        }

        /// <summary>
        /// 将某字符串按(类似如下格式 A||B||C)照某分隔符进行切分成字符数组
        /// </summary>
        /// <param name="data">原始字符串</param>
        /// <param name="seperators">分隔符数组</param>
        /// <returns></returns>
        public static string[] SplitToArray(string data, params string[] seperators)
        {
            if (seperators == null || seperators.Length == 0)
            {
                seperators = new string[] { "," };
            }
            return data.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 将某字符串(类似如下格式 key1:value1||key2:value2)按照分隔符进行切分成字典
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyValueSeperator">key与value之间的分隔符</param>
        /// <param name="itemSeperators">多个项之间的分割符</param>
        /// <returns></returns>
        public static Dictionary<string, string> SplitToDictionary(string data, string keyValueSeperator, params string[] itemSeperators)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] items = SplitToArray(data,itemSeperators);
            foreach (string item in items)
            {
                string key = GetBeforeSeperatorString(item,keyValueSeperator);
                string value = GetAfterSeperatorString(item,keyValueSeperator);
                result.Add(key,value);
            }

            return result;
        }

        /// <summary>
        /// 获取某字符串内的占位子字符串集合
        /// </summary>
        /// <param name="data"></param>
        /// <param name="placeHolderPrefixer"></param>
        /// <param name="placeHolderPostfixer"></param>
        /// <returns></returns>
        /// <remarks>
        /// 待处理的字符串类似如下格式 {AccidentEnterprise}+{AccidentPerson}，其中大括号是占位符的开始结束前后缀，
        /// AccidentEnterprise和AccidentPerson是占位子字符串
        /// </remarks>
        public static List<string> GetPlaceHolderList(string data, string placeHolderPrefixer,string placeHolderPostfixer)
        {
            List<string> result = new List<string>();
            int prefixerIndex = data.IndexOf(placeHolderPrefixer);
            while (prefixerIndex>=0)
            {
                int contentIndex= prefixerIndex+ placeHolderPrefixer.Length;
                int postfixerIndex = data.IndexOf(placeHolderPostfixer);
                if (postfixerIndex >= 0)
                {
                    string item = data.Substring(contentIndex, postfixerIndex - contentIndex);
                    if (string.IsNullOrEmpty(item) == false)
                    {
                        result.Add(item);
                    }

                    data = data.Substring(postfixerIndex + placeHolderPostfixer.Length);
                    prefixerIndex = data.IndexOf(placeHolderPrefixer);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取字符串分隔符前面的内容
        /// </summary>
        /// <param name="data"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string GetBeforeSeperatorString(string data, string seperator)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(data) == false)
            {
                int seperatorPosition = data.IndexOf(seperator);
                if (seperatorPosition >= 0)
                {
                    result = data.Substring(0, seperatorPosition);
                }
                else
                {
                    result = data;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取字符串分隔符后面的内容
        /// </summary>
        /// <param name="data"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string GetAfterSeperatorString(string data, string seperator)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(data) == false)
            {
                int seperatorPosition = data.IndexOf(seperator);
                int startPosition = seperatorPosition + seperator.Length;
                if (seperatorPosition >= 0 && startPosition <= data.Length)
                {
                    result = data.Substring(startPosition, data.Length - startPosition);
                }
            }

            return result;
        }
        #endregion

        #region 字符串的格式判断
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="data">参数名称</param>
        /// <returns></returns>
        public static bool IsIP(string data)
        {
            return Regex.IsMatch(data, RegexHelper.IPFormat);
        }

        /// <summary>
        /// 判断一个字符串是否为Email格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsEmail(string data)
        {
            return Regex.IsMatch(data, RegexHelper.EmailFormat);
        }

        /// <summary>
        /// 判断一个字符串是否是数字
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsInt(string data)
        {
            int outValue;
            if (int.TryParse(data, out outValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断一个字符串是否是时间
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <remarks>
        ///  TODO:这个还需要加入时间格式（区域）判断
        /// </remarks>
        public static bool IsDateTime(string data)
        {
            DateTime outValue;
            if (DateTime.TryParse(data, out outValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断一个字符串是否是decimal类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(string data)
        {
            decimal outValue;
            if (decimal.TryParse(data, out outValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 字符串其他各种操作
        /// <summary>
        /// 获取重复n次后的字符串
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="repeateCount"></param>
        /// <returns></returns>
        public static string Repeate(string originalValue, int repeateCount)
        {
            string result = string.Empty;

            if (repeateCount < 0)
            {
                repeateCount = 0;
            }

            for (int i = 0; i < repeateCount; i++)
            {
                result += originalValue;
            }

            return result;
        }
        #endregion

        #region 字符串与字节数组的转换
        /// <summary>
        /// 获取外部字节数组的方法
        /// </summary>
        /// <param name="data">待转换字符串</param>
        /// <returns></returns>
        public static byte[] GetByteArray(string data)
        {
            return GetByteArray(data, Encoding.UTF8 );
        }

        /// <summary>
        /// 获取外部字节数组的方法
        /// </summary>
        /// <param name="data">待转换字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static byte[] GetByteArray(string data, Encoding encoding)
        {
            byte[] result = encoding.GetBytes(data);
            return result;
        }
        #endregion
    }
}
