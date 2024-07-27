using System;
using System.Collections.Generic;
using System.Text;
using Hiland.BasicLibrary.Enums.OP;

namespace Hiland.BasicLibrary.Enums
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserTypes
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        [EnumItemDescription("zh-CN", "终端用户")]
        CommonUser = 1,

        /// <summary>
        /// 企业用户
        /// </summary>
        [EnumItemDescription("zh-CN", "企业用户")]
        EnterpriseUser = 2,
        
        /// <summary>
        /// 代理商用户
        /// </summary>
        [EnumItemDescription("zh-CN", "代理商用户")]
        Broker = 4,
        
        /// <summary>
        /// 一般管理员
        /// </summary>
        [EnumItemDescription("zh-CN", "数据管理员")]
        Manager = 8,
        
        /// <summary>
        /// 超级用户（缺省情况下拥有最高权限）
        /// </summary>
        [EnumItemDescription("zh-CN", "系统管理员")]
        SuperAdmin = 64,
    }
}
