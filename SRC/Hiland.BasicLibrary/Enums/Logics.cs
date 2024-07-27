using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 逻辑状态
    /// </summary>
    public enum Logics
    {
        /// <summary>
        /// 无效（否）
        /// </summary>
        [EnumItemDescription("zh-CN", "否")]
        [EnumItemDescription("Effect", "无效")]
        [EnumItemDescription("Direction", "Minus")]
        [EnumItemDescription("Attitude", "Negative")] 
        False=0,
        
        /// <summary>
        /// 有效（是）
        /// </summary>
        [EnumItemDescription("zh-CN", "是")]
        [EnumItemDescription("Effect", "有效")]
        [EnumItemDescription("Direction", "Plus")]
        [EnumItemDescription("Attitude", "Positive")] 
        True=1,
    }
}