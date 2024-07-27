using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sexes
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumItemDescription("zh-CN", "未设置")]
        UnSet=0,
        
        /// <summary>
        /// 男
        /// </summary>
        [EnumItemDescription("zh-CN", "男")]
        Male=1,
       
        /// <summary>
        /// 女
        /// </summary>
        [EnumItemDescription("zh-CN", "女")]
        Female=2,
    }
}
