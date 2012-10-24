using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiLand.Framework4.Permission.Attributes
{
    /// <summary>
    /// 验证模式类型
    /// </summary>
    public enum PermissionAuthorizeModes
    {
        /// <summary>
        /// 正常验证
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 登录即为通过验证
        /// </summary>
        LoginedAsPass = 2,

        /// <summary>
        /// 跳过验证
        /// </summary>
        None = 3,
    }
}
