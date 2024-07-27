using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 工作的性质
    /// </summary>
    public enum WorkKinds
    {
        /// <summary>
        /// 全职
        /// </summary>
        [EnumItemDescription("zh-CN", "全职")]
        AllTimeJob = 1,

        /// <summary>
        /// 兼职
        /// </summary>
        [EnumItemDescription("zh-CN", "兼职")]
        PartTimeJob = 2,

        [EnumItemDescription("en-AU", "Casual")]
        Casual = 30,

        [EnumItemDescription("en-AU", "Self-Employed")]
        SelfEmployed = 40,

        [EnumItemDescription("en-AU", "Other")]
        Other = 100,
    }
}
