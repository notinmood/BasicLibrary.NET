using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 查询条件项之间的关系
    /// </summary>
    public enum ConditionItemRelationships
    {
        /// <summary>
        /// 并且
        /// </summary>
        [EnumItemDescription("zh-CN", "并且")]
        AND = 1,

        /// <summary>
        /// 或者
        /// </summary>
        [EnumItemDescription("zh-CN", "或者")]
        OR = 2,
    }
}
