using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hiland.BasicLibrary.Data;

namespace Hiland.BasicLibrary4.Data
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringEx
    {
        /// <summary>
        /// 对使用占位符的字符串进行格式化
        /// </summary>
        /// <param name="stringWithFormating"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string stringWithFormating, params object[] args)
        {
            return string.Format(stringWithFormating, args);
        }

        /// <summary>
        /// 对SQL语句进行反注入处理
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringBuilder SQLAppend(this StringBuilder sb, string value)
        {
            string objString = SQLInjectionHelper.GetSafeSqlBeforeSave(value);
            return sb.Append(objString);
        }

        /// <summary>
        /// 对SQL语句进行反注入处理(带占位符格式化功能)
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StringBuilder SQLAppendFormat(this StringBuilder sb, string format, params string[] args)
        {
            List<string> argList = new List<string>();
            foreach (string obj in args)
            {
                string objString = SQLInjectionHelper.GetSafeSqlBeforeSave(obj);
                argList.Add(objString);
            }

            return sb.AppendFormat(format, argList.ToArray());
        }
    }
}
