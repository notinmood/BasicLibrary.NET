using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Framework.BusinessCore.Enum
{
    /// <summary>
    /// 权限模式（允许权限还是拒绝权限）
    /// </summary>
    public enum PermissionModes
    {
        /// <summary>
        /// 允许权限
        /// </summary>
        [EnumItemDescription("zh-CN", "允许权限")]
        Allow=1,
        /// <summary>
        /// 拒绝权限
        /// </summary>
        [EnumItemDescription("zh-CN", "拒绝权限")]
        Deny=2,
    }
}
