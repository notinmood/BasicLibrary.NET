using System;
using System.Collections.Generic;
using System.Text;
using HiLand.Utility.Enums.OP;

namespace HiLand.Utility.Enums
{
    /// <summary>
    /// 用户登录的状态
    /// </summary>
    public enum LoginStatuses
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        [EnumItemDescription("zh-CN", "登录成功")]
        Successful,

        /// <summary>
        /// 登录失败(用户账号(用户名或者EMail)不存在)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(用户账号(用户名或者EMail)不存在)")]
        FailureNoAccount,

        /// <summary>
        /// 登录失败(用户名不存在)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(用户名不存在)")]
        FailureNoName,

        /// <summary>
        /// 登录失败(EMail不存在)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(EMail不存在)")]
        FailureNoEMail,

        /// <summary>
        /// 登录失败(IDCard不存在)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(IDCard不存在)")]
        FailureNoIDCard,

        /// <summary>
        /// 登录失败(输入的账号(用户名或者EMail)跟口令不匹配)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(输入的账号(用户名、EMail或者IDCard)跟口令不匹配)")]
        FailureNotMatchPassword,

        /// <summary>
        /// 登录失败(用户被禁用等原因不允许登录)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(用户被禁用等原因不允许登录)")]
        FailureUserDenied,

        /// <summary>
        /// 登录失败(用户尚未激活)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(用户尚未激活)")]
        FailureUnactive,

        /// <summary>
        /// 登录失败(未知原因)
        /// </summary>
        [EnumItemDescription("zh-CN", "登录失败(未知原因)")]
        FailureUnknowReason,
    }
}
