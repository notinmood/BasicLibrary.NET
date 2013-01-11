using System;
using System.Collections.Generic;
using System.Text;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 权限验证的状态
    /// </summary>
    public enum PermissionValidateStatuses
    {
        /// <summary>
        /// 验证成功
        /// </summary>
        Successful,

        /// <summary>
        /// 验证失败(未登录)
        /// </summary>
        FailureUnLogin,

        /// <summary>
        /// 验证失败(权限不足)
        /// </summary>
        FailureNoPermission,
    }
}
