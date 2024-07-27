using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 通用设置级别
    /// </summary>
    public enum CommonLevels
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [EnumItemDescription("zh-CN", "未设置")]
        [EnumItemDescription("star", "未设置")]
        NonSet = 0,

        /// <summary>
        /// 级别一
        /// </summary>
        [EnumItemDescription("zh-CN", "级别一")]
        [EnumItemDescription("star", "☆")]
        Level1 = 1,

        /// <summary>
        /// 级别二
        /// </summary>
        [EnumItemDescription("zh-CN", "级别二")]
        [EnumItemDescription("star", "☆☆")]
        Level2 = 2,

        /// <summary>
        /// 级别三
        /// </summary>
        [EnumItemDescription("zh-CN", "级别三")]
        [EnumItemDescription("star", "☆☆☆")]
        Level3 = 3,

        /// <summary>
        /// 级别四
        /// </summary>
        [EnumItemDescription("zh-CN", "级别四")]
        [EnumItemDescription("star", "☆☆☆☆")]
        Level4 = 4,

        /// <summary>
        /// 级别五
        /// </summary>
        [EnumItemDescription("zh-CN", "级别五")]
        [EnumItemDescription("star", "☆☆☆☆☆")]
        Level5 = 5,
    }
}
