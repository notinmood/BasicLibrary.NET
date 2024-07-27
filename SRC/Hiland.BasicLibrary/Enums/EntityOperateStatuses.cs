﻿using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 实体操作的状态
    /// </summary>
    public enum EntityOperateStatuses
    {
        /// <summary>
        /// 成功
        /// </summary>
        [EnumItemDescription("zh-CN", "成功")]
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
