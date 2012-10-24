using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Framework.BusinessCore.Enum
{
    /// <summary>
    /// 部门类型
    /// </summary>
    public enum DepartmentTypes
    {
        /// <summary>
        /// 一般部门
        /// </summary>
        [EnumItemDescription("zh-CN", "一般部门")]
        CommonDepartment=1,
        
        /// <summary>
        /// 外部部门
        /// </summary>
        [EnumItemDescription("zh-CN", "外部部门")]
        OuterDepartment=2,
    }
}
