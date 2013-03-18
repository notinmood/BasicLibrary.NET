using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DigitHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToSafeString(decimal data)
        {
            return ToSafeString(data, "0.00", null);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <param name="replaceZeroWithString">是否将0变成空字符串显示,空字符串表示输入出空信息，null表示不改变而输出原值</param>
        /// <returns></returns>
        public static string ToSafeString(decimal data, string format, string replaceZeroWithString)
        {
            string result = string.Empty;
            if (replaceZeroWithString!=null && data == 0)
            {
                result = replaceZeroWithString;
            }
            else
            {
                if (string.IsNullOrEmpty(format))
                {
                    result = data.ToString();
                }
                else
                {
                    result = data.ToString(format);
                }
            }

            return result;
        }

    }
}
