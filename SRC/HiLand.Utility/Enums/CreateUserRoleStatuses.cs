using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 创建用户角色时在状态
    /// </summary>
    public enum CreateUserRoleStatuses
    {
        /// <summary>
        /// 成功
        /// </summary>
        [EnumItemDescription("zh-CN", "是")]
        Successful,

        /// <summary>
        /// 名称重复失败
        /// </summary>
        [EnumItemDescription("zh-CN", "名称重复失败")]
        FailureDuplicateName,

        /// <summary>
        /// Email重复失败
        /// </summary>
        [EnumItemDescription("zh-CN", "Email重复失败")]
        FailureDuplicateEMail,

        /// <summary>
        /// 身份证重复失败
        /// </summary>
        [EnumItemDescription("zh-CN", "身份证重复失败")]
        FailureDuplicateIDCard,

        /// <summary>
        /// 其他未知原因失败
        /// </summary>
        [EnumItemDescription("zh-CN", "其他未知原因失败")]
        FailureUnknowReason,
    }
}
 