using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 日期格式
    /// </summary>
    public enum DateFormats
    {
        /// <summary>
        /// 年月日格式（中国的等亚洲国家多用此格式）
        /// </summary>
        YMD,
        /// <summary>
        /// 月日年格式（欧美多用此格式）
        /// </summary>
        MDY,
        /// <summary>
        /// 日月年格式（澳洲多用此格式）
        /// </summary>
        DMY,
    }
}
