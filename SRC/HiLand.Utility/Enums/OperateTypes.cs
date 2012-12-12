using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 操作动作的类别
    /// </summary>
    public enum OperateTypes
    {
        /// <summary>
        /// 创建
        /// </summary>
        [EnumItemDescription("zh-CN", "创建")]
        Create = 1,

        /// <summary>
        /// 更新
        /// </summary>
        [EnumItemDescription("zh-CN", "更新")]
        Update = 2,


        /// <summary>
        /// 获取
        /// </summary>
        [EnumItemDescription("zh-CN", "获取")]
        Get = 3,

        /// <summary>
        /// 删除
        /// </summary>
        [EnumItemDescription("zh-CN", "删除")]
        Delete = 10,
    }
}
