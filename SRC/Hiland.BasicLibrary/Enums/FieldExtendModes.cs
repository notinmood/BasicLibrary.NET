using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 扩展信息的存储方式
    /// </summary>
    public enum FieldExtendModes
    {
        /// <summary>
        /// 不是一个扩展字段
        /// </summary>
        None,
        /// <summary>
        /// 使用实体对应的本表的扩展字段存储（所有的扩展信息都记录在同一个扩展字段内，无法SQL过滤检索）
        /// </summary>
        SelfTable,
        /// <summary>
        /// 使用独立的表存储扩展信息（每个扩展信息在独立表中均为一条独立记录，可以SQL过滤检索）
        /// </summary>
        IsolateTable,
    }
}
