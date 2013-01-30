using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums;
using HiLand.Utility.Setting;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 日期操作辅助类
    /// </summary>
    public static class DateTimeHelper
    {
        #region SQLServer数据库中安全的最大最小日期
        /// <summary>
        /// 最小日期（在Sql类型的数据中最小的日期）
        /// </summary>
        public static DateTime Min
        {
            get
            {
                return new DateTime(1753, 1, 1);
            }
        }

        /// <summary>
        /// 最大日期（在Sql类型的数据中最大的日期）
        /// </summary>
        public static DateTime Max
        {
            get
            {
                return new DateTime(9999, 12, 31);
            }
        }
        #endregion

        #region 获取某段时间的第一天/最后一天
        /// <summary>
        /// 取某月的第一天
        /// </summary>
        /// <param name="value">传入时间</param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfMonth(DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        /// <summary>
        /// 取某月的最后一天
        /// </summary>
        /// <param name="value">传入时间</param>
        /// <returns></returns>
        public static DateTime GetLastDateOfMonth(DateTime value)
        {
            return GetFirstDateOfMonth(value).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 取某季度的第一天
        /// </summary>
        /// <param name="value">传入时间</param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfQuarter(DateTime value)
        {
            int m = 0;
            switch (value.Month)
            {
                case 1:
                case 2:
                case 3:
                    m = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    m = 4; 
                    break;
                case 7:
                case 8:
                case 9:
                    m = 7; 
                    break;
                case 10:
                case 11:
                case 12:
                    m = 10;
                    break;
            }

           return new DateTime(value.Year, m, 1);
        }

         /// <summary>
        /// 取某季度的最后一天
        /// </summary>
        /// <param name="value">传入时间</param>
        /// <returns></returns>
        public static DateTime GetLastDateOfQuarter(DateTime value)
        {
            return GetFirstDateOfQuarter(value).AddMonths(3).AddDays(-1);
        }

        /// <summary>
        /// 取某年的第一天
        /// </summary>
        /// <param name="value">传入时间</param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfYear(DateTime value)
        {
            return new DateTime(value.Year, 1, 1);
        }

        /// <summary>
        /// 取某年的最后一天
        /// </summary>
        /// <param name="value">传入时间</param>
        /// <returns></returns>
        public static DateTime GetLastDateOfYear(DateTime value)
        {
            return GetLastDateOfYear(value).AddYears(1).AddDays(-1);
        }
        #endregion

        #region 字符串和日期之间的转换
        /// <summary>
        /// 将日期字符串解析成具体的日期
        /// </summary>
        /// <param name="dateString">日期字符串</param>
        /// <param name="dateFormat">日期格式</param>
        /// <param name="dateSeperator">日期各个部分之间的分割符号</param>
        /// <returns>返回解析后的日期，如果解析失败则返回系统的最小日期</returns>
        /// <remarks>
        /// 对于通常日期的格式解析请使用系统自身的 DateTime.TryParser("",IFormatProvider)。
        /// 在某些无法使用以上方法的时候才考虑使用本方法。
        /// </remarks>
        public static DateTime Parse(string dateString, DateFormats dateFormat, params string[] dateSeperator)
        {
            return Parse(dateString, dateFormat, DateTimeHelper.Min, dateSeperator);
        }
        
        /// <summary>
        /// 将日期字符串解析成具体的日期
        /// </summary>
        /// <param name="dateString">日期字符串</param>
        /// <param name="dateFormat">日期格式</param>
        /// <param name="dateSeperator">日期各个部分之间的分割符号</param>
        /// <param name="defaultValue">解析失败时返回的默认日期</param>
        /// <returns>返回解析后的日期，如果解析失败则返回设定的默认日期defaultValue</returns>
        /// <remarks>
        /// 对于通常日期的格式解析请使用系统自身的 DateTime.TryParser("",IFormatProvider)。
        /// 在某些无法使用以上方法的时候才考虑使用本方法。
        /// </remarks>
        public static DateTime Parse(string dateString, DateFormats dateFormat,DateTime defaultValue, params string[] dateSeperator)
        {
            DateTime resultDate = defaultValue;
            if (string.IsNullOrEmpty(dateString) == false)
            {
                if (dateSeperator == null || dateSeperator.Length == 0)
                {
                    dateSeperator = new string[] { "/", "-", "_", "\\" };
                }

                try
                {
                    string[] dateStrings = dateString.Split(dateSeperator, StringSplitOptions.RemoveEmptyEntries);
                    if (dateStrings.Length == 3)
                    {
                        int resultYear = 0;
                        int resultMonth = 0;
                        int resultDay = 0;

                        switch (dateFormat)
                        {
                            case DateFormats.MDY:
                                resultYear = Convert.ToInt32(dateStrings[2]);
                                resultMonth = Convert.ToInt32(dateStrings[0]);
                                resultDay = Convert.ToInt32(dateStrings[1]);
                                break;
                            case DateFormats.DMY:
                                resultYear = Convert.ToInt32(dateStrings[2]);
                                resultMonth = Convert.ToInt32(dateStrings[1]);
                                resultDay = Convert.ToInt32(dateStrings[0]);
                                break;
                            case DateFormats.YMD:
                            default:
                                resultYear = Convert.ToInt32(dateStrings[0]);
                                resultMonth = Convert.ToInt32(dateStrings[1]);
                                resultDay = Convert.ToInt32(dateStrings[2]);
                                break;
                        }

                        resultDate = new DateTime(resultYear, resultMonth, resultDay);
                    }
                }
                catch 
                {
                    resultDate = defaultValue;
                }
            }

            return resultDate;
        }

        /// <summary>
        /// 将具体的日期转换成目标格式的日期字符串
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="dateFormat"></param>
        /// <param name="dateSeperator">日期各个部分之间的分割符号</param>
        /// <returns></returns>
        public static string UnParse(DateTime dateValue, DateFormats dateFormat, string dateSeperator)
        {
            string resultValue = string.Empty;
            int resultYear = dateValue.Year;
            int resultMonth = dateValue.Month;
            int resultDay = dateValue.Day;

            switch (dateFormat)
            {
                case DateFormats.MDY:
                    resultValue = string.Format("{1}{0}{2}{0}{3}", dateSeperator, resultMonth, resultDay, resultYear);
                    break;
                case DateFormats.DMY:
                    resultValue = string.Format("{1}{0}{2}{0}{3}", dateSeperator, resultDay, resultMonth, resultYear);
                    break;
                case DateFormats.YMD:
                default:
                    resultValue = string.Format("{1}{0}{2}{0}{3}", dateSeperator, resultYear, resultMonth, resultDay);
                    break;
            }

            return resultValue;
        }
        #endregion

        #region 计算获取托管方与系统运营当地时间的时差
        private static int timeZoneDiff = 25;
        /// <summary>
        /// 本地时间跟服务器的时差
        /// </summary>
        public static int TimeZoneDiff
        {
            get
            {
                if (timeZoneDiff == 25)
                {
                    timeZoneDiff = Config.GetAppSetting<int>("TimeZoneDiff",0);
                }

                return timeZoneDiff;
            }
        }

        /// <summary>
        /// 主运营地区的当地时间
        /// </summary>
        public static DateTime RunningLocalNow
        {
            get
            {
                return DateTime.Now.AddHours(TimeZoneDiff);
            }
        }

        /// <summary>
        /// 主运营地区的当地日期
        /// </summary>
        public static DateTime RunningLocalToday
        {
            get
            {
                return RunningLocalNow.Date;
            }
        }
        #endregion

        #region 格式化
        /// <summary>
        /// 获取安全的字符串显示
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToSaftShortDateString(DateTime data)
        {
            string result = string.Empty;
            if (data != Min)
            {
                result = data.ToShortDateString();
            }
            return result;
        }

        /// <summary>
        /// 获取安全的字符串显示
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToSaftString(DateTime data)
        {
            return ToSaftString(data, "yyyy/MM/dd");
        }

        /// <summary>
        /// 获取安全的字符串显示
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToSaftString(DateTime data, string format)
        {
            string result = string.Empty;
            if (data != Min)
            {
                result = data.ToString(format);
            }
            return result;
        }
        #endregion
    }
}
