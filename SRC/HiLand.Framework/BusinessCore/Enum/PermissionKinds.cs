using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Framework.BusinessCore.Enum
{
    /// <summary>
    /// 权限控制类型
    /// </summary>
    /// <remarks>
    /// 即是控制操作是否可以执行，还是控制数据是否可见
    /// </remarks>
    public enum PermissionKinds
    {
        /// <summary>
        /// 操作权限
        /// </summary>
        Operating=0,

        /// <summary>
        /// 数据权限
        /// </summary>
        Data=1,
    }
}
