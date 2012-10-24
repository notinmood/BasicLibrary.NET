using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 数据常用状态
    /// </summary>
    /// <remarks>
    /// 除了正常和非正常还可能有第三种状态(只是目前未用的)(这是跟枚举Logics的区别)
    /// </remarks>
    public enum CommonStatuses
    {
        /// <summary>
        /// 非正常状态
        /// </summary>
        [EnumItemDescription("zh-CN", "已禁用")]
        UnNormal = 0,
        
        /// <summary>
        /// 正常状态
        /// </summary>
        [EnumItemDescription("zh-CN", "正常")]
        Normal = 1,
    }
}
