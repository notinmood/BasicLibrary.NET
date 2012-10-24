using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 数据比较的模式
    /// </summary>
    public enum CompareModes
    {
        /// <summary>
        /// 相等
        /// </summary>
        [EnumItemDescription("zh-CN", "等于")]
        [EnumItemDescription("stand-SQL", "=")]
        Equal,

        /// <summary>
        /// 不相等
        /// </summary>
        [EnumItemDescription("zh-CN", "不等于")]
        [EnumItemDescription("stand-SQL", "!=")]
        NotEqual,

        /// <summary>
        /// 少于
        /// </summary>
         [EnumItemDescription("zh-CN", "小于")]
         [EnumItemDescription("stand-SQL", "<")]
        LessThan,

        /// <summary>
        /// 大于等于
        /// </summary>
         [EnumItemDescription("zh-CN", "不小于")]
         [EnumItemDescription("stand-SQL", ">=")]
        NotLessThan,

        /// <summary>
        /// 大于
        /// </summary>
         [EnumItemDescription("zh-CN", "大于")]
         [EnumItemDescription("stand-SQL", ">")]
        MoreThan,

        /// <summary>
        /// 少于等于
        /// </summary>
         [EnumItemDescription("zh-CN", "不大于")]
         [EnumItemDescription("stand-SQL", "<=")]
        NotMoreThan,

        /// <summary>
        /// 相似
        /// </summary>
        [EnumItemDescription("zh-CN", "包含")]
        [EnumItemDescription("stand-SQL", "like")]
        Like,

        /// <summary>
        /// 左侧相似
        /// </summary>
        [EnumItemDescription("zh-CN", "开始于")]
        [EnumItemDescription("stand-SQL", "likeleft")]
        LikeLeft,


        /// <summary>
        /// 右侧相似
        /// </summary>
        [EnumItemDescription("zh-CN", "结束于")]
        [EnumItemDescription("stand-SQL", "likeright")]
        LikeRight,
    }
}
