using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 数据权限的类型
    /// </summary>
    public enum PermissionDataTypes
    {
        /// <summary>
        /// 无数据权限
        /// </summary>
        [EnumItemDescription("zh-CN", "无数据权限")]
        None = 0,

        /// <summary>
        /// 自己的数据
        /// </summary>
        [EnumItemDescription("zh-CN", "自我数据")]
        Self = 10,

        /// <summary>
        /// 部门数据（包括子部门）
        /// </summary>
        [EnumItemDescription("zh-CN", "部门数据")]
        DepatmentWithSub = 20,

        /// <summary>
        /// 部门数据（不包括子部门）
        /// </summary>
        [EnumItemDescription("zh-CN", "部门数据（不包括子部门）")]
        DepartmentWithoutSub = 21,

        /// <summary>
        /// 所有数据
        /// </summary>
        [EnumItemDescription("zh-CN", "所有数据")]
        All = 100,
    }
}
