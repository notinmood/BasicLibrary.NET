using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 日志记录的级别
    /// </summary>
    public enum LogLevels
    {
        /// <summary>
        /// 紧急 - 系统无法使用
        /// </summary>
        [EnumItemDescription("zh-CN", "紧急-系统无法使用")]
        Emerg,

        /// <summary>
        /// 必须立即采取措施
        /// </summary>
        [EnumItemDescription("zh-CN", "必须立即采取措施")]
        Alert,

        /// <summary>
        /// 致命情况
        /// </summary>
        [EnumItemDescription("zh-CN", "致命情况")]
        Crit,

        /// <summary>
        /// 错误情况
        /// </summary>
        [EnumItemDescription("zh-CN", "错误情况")]
        Error,

        /// <summary>
        /// 警告情况
        /// </summary>
        [EnumItemDescription("zh-CN", "警告情况")]
        Warn,

        /// <summary>
        /// 一般重要情况
        /// </summary>
        [EnumItemDescription("zh-CN", "一般重要情况")]
        Notice,

        /// <summary>
        /// 普通信息
        /// </summary>
        [EnumItemDescription("zh-CN", "普通信息")]
        Info,

        /// <summary>
        /// 开发时调试信息
        /// </summary>
        [EnumItemDescription("zh-CN", "开发时调试信息")]
        Debug,
    }
}
